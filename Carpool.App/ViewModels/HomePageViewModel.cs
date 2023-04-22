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
    public class HomePageViewModel : ViewModelBase, IHomePageViewModel
    {
        public HomePageViewModel(UserFacade userFacade, RideFacade rideFacade, CarFacade carFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _rideFacade = rideFacade;
            _carFacade = carFacade;
            _mediator = mediator;
            
            CreateRideCommand = new AsyncRelayCommand(CreateRideAsync, CreateRideControl);  
            BrowseRideCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<IJoinRideViewModel>()));

            _mediator.Register<SelectedMessage<UserWrapper>>(UserSelected);
            _mediator.Register<ReloadMessage<IHomePageViewModel>>(OnReload);

            _createRideStart = "--Start--";
            _createRideDestination = "--Destination--";
            SelectedUserName = "Selected user";
        }

        private readonly UserFacade _userFacade;
        private readonly RideFacade _rideFacade;
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;

        public static CarListModel? SelectedCar { get; set; }

        private static UserModel? _user;
        public UserModel? User
        {
            get => _user;
        }
        private async void OnReload(object obj)
        {
            await LoadAsync(_user.Id);
        }
        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        public async Task LoadAsync(Guid id)
        {
            _user = await _userFacade.GetAsync(id) ?? UserModel.Empty;
            Cars.Clear();
            if (User != null)
            {
                ObservableCollectionExtensions.AddRange<CarListModel>(Cars, User.Cars);
            }
        }

        private async void UserSelected(SelectedMessage<UserWrapper> message)
        {
            if (message.Id == null)
            {
                return;
            }

            await LoadAsync((Guid)message.Id);
        }

        public async Task CreateRideAsync()
        {
            var userList = await _userFacade.GetAsyncList(_user.Id);
            var selectedCarList = await _carFacade.GetAsyncList(SelectedCar.Id);

            var ride = new RideModel(
                Start: _createRideStart, 
                Destination: _createRideDestination, 
                DepartureTime: _createRideDepartureTime, 
                Driver: userList,
                Car: selectedCarList)
            {
                Duration = _createRideDuration,
            };
            
            var rideSaved = await _rideFacade.SaveAsync(ride);
            
            MessageBox.Show("Ride Created!");
            _mediator.Send(new ReloadMessage<IMyRidesViewModel>());
            _mediator.Send(new ChangeMessage<IMyRidesViewModel>());
        }

        private bool CreateRideControl()
        {
            if (_createRideStart == "" || _createRideStart == "--Start--")
            {
                return false;
            }

            if (_createRideDestination == "" || _createRideDestination == "--Destination--")
            {
                return false;
            }

            if (_createRideDuration == TimeSpan.Zero)
            {
                return false;
            }

            return true;
        }

        public ICommand CreateRideCommand { get; set; }
        public ICommand BrowseRideCommand { get; set; }

        private string _createRideStart;
        public string CreateRideStart
        {
            get { return this._createRideStart; }
            set { 
                this._createRideStart = value;
            }
        }

        private string _createRideDestination;

        public string CreateRideDestination
        {
            get { return this._createRideDestination; }
            set { this._createRideDestination = value; }
        }

        private DateTime _createRideDepartureTime = new DateTime(2022,1,1,0,0,0);

        public DateTime CreateRideDepartureTime
        {
            get { return this._createRideDepartureTime; }
            set { this._createRideDepartureTime = value; }
        }

        public string SelectedUserName;

        private TimeSpan _createRideDuration = TimeSpan.Zero;
        public TimeSpan CreateRideDuration
        {
            get { return this._createRideDuration; }
            set { this._createRideDuration = value; }
        }
    }
}