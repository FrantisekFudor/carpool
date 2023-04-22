using System;
using Carpool.App.Services;
using Carpool.App.Commands;
using Carpool.BL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.BL.Facade;
using Carpool.App.ViewModels.Interfaces;
using Carpool.App.Messages;
using Carpool.App.Wrappers;
using System.Windows;
using System.Threading;

namespace Carpool.App.ViewModels
{
    public class EditCarViewModel : ViewModelBase, IEditCarViewModel
    {
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private CarModel? _car;

        public CarWrapper? Model { get; set; }
        public EditCarViewModel(CarFacade carFacade, IMediator mediator)
        {
            _carFacade = carFacade;
            _mediator = mediator;

            _mediator.Register<SelectedMessage<CarWrapper>>(CarSelected);
            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSaveCar);
            BackCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<IProfileViewModel>()));
        }

        public CarModel? Car
        {
            get => _car;
        }

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }

        private async void CarSelected(SelectedMessage<CarWrapper> message)
        {
            if (message.Id == null)
            {
                return;
            }

            await LoadAsync((Guid)message.Id);
        }

        public async Task LoadAsync(Guid id)
        {
            _car = await _carFacade.GetAsync(id) ?? CarModel.Empty;
            
            if (_car != null)
                _car.PhotoUrl = "https://cdn-icons-png.flaticon.com/512/741/741407.png";

            _mediator.Send(new ChangeMessage<IEditCarViewModel>());
        }

        public async Task SaveAsync()
        {
            if (_car == null)
            {
                return;
            }

            var savedUser = await _carFacade.SaveAsync(_car);

            Thread.Sleep(100);
            MessageBox.Show("Car saved!");
            _mediator.Send(new ReloadMessage<IProfileViewModel>());
            _mediator.Send(new ChangeMessage<IProfileViewModel>());

            
        }
        public bool CanSaveCar()
        {
            return true;
        }

        public async Task DeleteAsync()
        {
            var users = await _carFacade.GetAsync();
            throw new NotImplementedException();
        }
    }
}