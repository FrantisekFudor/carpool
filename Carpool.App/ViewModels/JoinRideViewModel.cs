using System;
using Carpool.App.Extensions;
using Carpool.App.Messages;
using Carpool.App.Wrappers;
using Carpool.App.Services;
using Carpool.App.Commands;
using Carpool.BL.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using AutoMapper;
using Carpool.BL.Facade;
using Carpool.App.ViewModels.Interfaces;
using System.Windows;

namespace Carpool.App.ViewModels
{
    public class JoinRideViewModel : ViewModelBase, IJoinRideViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly PassengerFacade _passengerFacade;
        private readonly IMediator _mediator;
        public string SearchRideStart { get; set; }
        public string SearchRideDestination { get; set; }
        public DateTime SearchRideDepartureTime { get; set; }

        public bool StartEnabled { get; set; }
        public bool DestinationEnabled { get; set; }
        public bool DateEnabled { get; set; }

        public static UserListModel? SelectedUser { get; set; }
        public static KeyValuePair<RideListModel, int>? SelectedRideKVP { get; set; }
        public static RideListModel? SelectedRide { get; set; }
        
        public JoinRideViewModel(RideFacade rideFacade, UserFacade userFacade, IMediator mediator, PassengerFacade passengerFacade)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _mediator = mediator;
            _passengerFacade = passengerFacade;
            SearchRideDepartureTime = DateTime.Now;
            StartEnabled = false;
            DestinationEnabled = false;
            DateEnabled = false;


            JoinRideCommand = new AsyncRelayCommand<RideListModel>(RideSelected);
            FilterCommand = new AsyncRelayCommand(() => Filter());
            ClearFilterCommand = new AsyncRelayCommand(() => ClearFilter());
            BackCommand = new RelayCommand(() => _mediator.Send(new ChangeMessage<IHomePageViewModel>()));

            StartEnabledCommand = new RelayCommand<bool>(EnableStart);
            DestinationEnabledCommand = new RelayCommand<bool>(EnableDestination);
            DateEnabledCommand = new RelayCommand<bool>(EnableDate);

            _mediator.Register<ReloadMessage<IJoinRideViewModel>>(OnReload);
            _mediator.Register<SelectedMessage<UserWrapper>>(UserSelected);
        }

        private async void OnReload(object obj)
        {
            await ClearFilter();
            await LoadAsync();
        }

        private async void UserSelected(SelectedMessage<UserWrapper> user)
        {
            if (user.Id == null)
            {
                return;
            }

            SelectedUser = await _userFacade.GetAsyncList((Guid)user.Id);

        }

        private async Task RideSelected(RideListModel? ride) => await RideToSelect(SelectedRideKVP);

        private async Task RideToSelect(KeyValuePair<RideListModel, int>? ride)
        {
            if (ride.Value.Key == null)
            {
                return;
            }
            SelectedRide = ride.Value.Key;
            try
            {
                await _passengerFacade.SaveAsync(new PassengerModel(SelectedUser, SelectedRide));
                MessageBox.Show("Successfully joined ride!");
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _mediator.Send(new ReloadMessage<IJoinRideViewModel>());
        }


        private async Task ClearFilter()
        {
            StartEnabled = false;
            DestinationEnabled = false;
            DateEnabled = false;
            await LoadAsync();
        }
        private void EnableStart(bool enabled)
        {
            StartEnabled = enabled;
        }

        private void EnableDestination(bool enabled)
        {
            DestinationEnabled = enabled;
        }

        private void EnableDate(bool enabled)
        {
            DateEnabled = enabled;
        }
        public ObservableCollection<KeyValuePair<RideListModel, int>> Rides { get; set; } = new();

        public ICommand JoinRideCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand StartEnabledCommand { get; }
        public ICommand DestinationEnabledCommand { get; }
        public ICommand DateEnabledCommand { get; }

        private async Task Filter()
        {
            var rides = await _rideFacade.GetAsync();
            if (StartEnabled)
            {
                rides = await _rideFacade.SearchRides(SearchRideStart);
            }
            if (DestinationEnabled)
            {
                rides = await _rideFacade.SearchRides("", SearchRideDestination);
            }
            if (DateEnabled)
            {
                rides = await _rideFacade.SearchRides("", "", SearchRideDepartureTime);
            }
            if (StartEnabled && DestinationEnabled)
            {
                rides = await _rideFacade.SearchRides(SearchRideStart, SearchRideDestination);
            }
            if (StartEnabled && DateEnabled)
            {
                rides = await _rideFacade.SearchRides(SearchRideStart, "", SearchRideDepartureTime);
            }
            if (DestinationEnabled && DateEnabled)
            {
                rides = await _rideFacade.SearchRides("", SearchRideDestination, SearchRideDepartureTime);
            }
            if (StartEnabled && DestinationEnabled && DateEnabled)
            {
                rides = await _rideFacade.SearchRides(SearchRideStart, SearchRideDestination, SearchRideDepartureTime);
            }

            Rides.Clear();

            Dictionary<RideListModel, int> tmp = new Dictionary<RideListModel, int>();
            foreach (var ride in rides)
            {
                tmp.Add(ride, await _rideFacade.GetRestCapacityAsync(ride));
            }

            foreach (var item in tmp)
            {
                Rides.Add(item);
            }
            if (Rides.Count > 0)
            {
                SelectedRideKVP = Rides[0];
            }
            else
            {
                SelectedRideKVP = null;
            }
            

        }

        public async Task LoadAsync()
        {
            SearchRideStart = String.Empty;
            SearchRideDestination = String.Empty;
            SearchRideDepartureTime = DateTime.Now;
            var rides = await _rideFacade.GetAsync();
            Rides.Clear();

            Dictionary<RideListModel, int> tmp = new Dictionary<RideListModel, int>();
            foreach (var ride in rides)
            {
                tmp.Add(ride, await _rideFacade.GetRestCapacityAsync(ride));
            }

            foreach(var item in tmp)
            {
                Rides.Add(item);
            }

            if (Rides.Count > 0)
            {
                SelectedRideKVP = Rides[0];
            }
            else
            {
                SelectedRideKVP = null;
            }

        }


    }
}