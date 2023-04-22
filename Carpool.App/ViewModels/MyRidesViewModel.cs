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
using System.Windows;
using AutoMapper;
using Carpool.BL.Facade;
using Carpool.App.ViewModels.Interfaces;

namespace Carpool.App.ViewModels
{
    public class MyRidesViewModel : ViewModelBase, IMyRidesViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;
        private static UserModel? _user;

        public MyRidesViewModel(UserFacade userFacade, IMediator mediator, RideFacade rideFacade)
        {
            _userFacade = userFacade;
            _mediator = mediator;
            _rideFacade = rideFacade;

            _mediator.Register<SelectedMessage<UserWrapper>>(UserSelected);
            _mediator.Register<ReloadMessage<IMyRidesViewModel>>(OnReload);

            DeleteRideCommand = new AsyncRelayCommand(DeleteRideD);
            LeaveRideCommand = new AsyncRelayCommand(LeaveRide);
            EditRideCommand = new RelayCommand<RideListModel>(EditRide);
        }

        public ICommand DeleteRideCommand { get; set; }
        public ICommand LeaveRideCommand { get; set; }
        public ICommand EditRideCommand { get; set; }

        public static RideListModel? SelectedRideD { get; set; }
        public static RideListModel? SelectedRideP { get; set; }

        private void EditRide(RideListModel? ride)
        {
            if (SelectedRideD == null)
                return;
            _mediator.Send(new SelectedMessage<RideWrapper> { Id = SelectedRideD?.Id });
        }

        private async Task DeleteRideD()
        {
            if (SelectedRideD == null)
            {
                return;
            }

            if (SelectedRideD.Id != Guid.Empty)
            {

                await _rideFacade.DeleteAsync(SelectedRideD.Id);
                await LoadAsync(_user.Id);
                MessageBox.Show("Ride deleted");
            }

            
        }

        private async Task LeaveRide()
        {
            if (SelectedRideP == null)
            {
                return;
            }

            if (SelectedRideP.Id != Guid.Empty)
            {

                await _rideFacade.DeleteAsync(SelectedRideP.Id);
                await LoadAsync(_user.Id);
                MessageBox.Show("Ride left");
            }

        }


        public UserModel? User
        {
            get => _user;
        }
        private async void OnReload(object obj)
        {
            await LoadAsync(_user.Id);
        }
        private async void UserSelected(SelectedMessage<UserWrapper> message)
        {
            if (message.Id == null)
            {
                return;
            }

            await LoadAsync((Guid)message.Id);
        }

        public async Task LoadAsync(Guid id)
        {
            _user = await _userFacade.GetAsync(id) ?? UserModel.Empty;
            RidesAsDriver.Clear();
            RidesAsPassenger.Clear();
            if (_user != null)
            {
                ObservableCollectionExtensions.AddRange<RideListModel>(RidesAsDriver, _user.RidesAsDriver);

                foreach (var passenger in _user.RidesAsPassenger)
                {
                    RidesAsPassenger.Add(passenger.Ride);
                }

                if (RidesAsPassenger.Count > 0)
                {
                    SelectedRideP = RidesAsPassenger[0];
                }

                if (RidesAsDriver.Count > 0)
                {
                    SelectedRideD = RidesAsDriver[0];
                }
            }
        }

        Task IDetailViewModel<RideWrapper>.LoadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IDetailViewModel<RideWrapper>.DeleteAsync()
        {
            throw new NotImplementedException();
        }

        Task IDetailViewModel<RideWrapper>.SaveAsync()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<RideListModel> RidesAsDriver { get; set; } = new();
        public ObservableCollection<RideListModel> RidesAsPassenger { get; set; } = new();

        public ICommand? UserSelectedCommand { get; }
        public ICommand? UserNewCommand { get; }
        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        RideWrapper? IDetailViewModel<RideWrapper>.Model => throw new NotImplementedException();
    }
}