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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using AutoMapper;
using Carpool.BL.Facade;
using Carpool.App.ViewModels.Interfaces;

namespace Carpool.App.ViewModels
{
    public class EditRideViewModel : ViewModelBase, IEditRideViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly PassengerFacade _passengerFacade;
        private readonly IMediator _mediator;

        private RideModel? _ride;

        public EditRideViewModel(RideFacade rideFacade, UserFacade userFacade, PassengerFacade passengerFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _passengerFacade = passengerFacade;
            _mediator = mediator;

            _mediator.Register<SelectedMessage<RideWrapper>>(RideSelected);

            RemovePassengerCommand = new AsyncRelayCommand(RemovePassengerAsync, CanRemovePassenger);
            BackCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<IMyRidesViewModel>()));
        }

        public RideWrapper? Model { get; set; }
        public RideModel? Ride
        {
            get => _ride;
        }
        public ObservableCollection<PassengerListModel> Passengers { get; set; } = new();

        private async void RideSelected(SelectedMessage<RideWrapper> message)
        {
            if (message.Id == null)
            {
                return;
            }

            await LoadAsync((Guid)message.Id);
        }

        public static PassengerListModel? SelectedPassenger { get; set; }

        public ICommand BackCommand { get; }
        public ICommand RemovePassengerCommand { get; }

        public bool CanRemovePassenger()
        {
            if (SelectedPassenger == null)
                return false;
            return true;
        }

        public async Task RemovePassengerAsync()
        {
            await _passengerFacade.DeleteAsync(SelectedPassenger.Id);

            MessageBox.Show("Passenger removed");

            if (_ride.Passengers.Count == 0)
            {
                _mediator.Send(new ChangeMessage<IMyRidesViewModel>());
            }
            else
            {
                await LoadAsync(_ride.Id);
            }
        }

        public async Task LoadAsync(Guid id)
        {
            _ride = await _rideFacade.GetAsync(id) ?? RideModel.Empty;

            Passengers.Clear();
            ObservableCollectionExtensions.AddRange<PassengerListModel>(Passengers, _ride.Passengers);

            _mediator.Send(new ChangeMessage<IEditRideViewModel>());
        }

        public async Task SaveAsync()
        {
            if (_ride == null)
            {
                return;
            }

            var savedRide = await _rideFacade.SaveAsync(_ride);

            Thread.Sleep(100);
            MessageBox.Show("Ride saved!");
            _mediator.Send(new ReloadMessage<IProfileViewModel>());
            _mediator.Send(new ChangeMessage<IProfileViewModel>());
        }
        public bool CanSaveCar()
        {
            return true;
        }

        public async Task DeleteAsync()
        {
            var rides = await _rideFacade.GetAsync();
            throw new NotImplementedException();
        }
    }
}