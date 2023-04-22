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
using System.Threading;
using System.Windows;

namespace Carpool.App.ViewModels
{
    public class AddCarViewModel : ViewModelBase, IAddCarViewModel
    {
        private readonly CarFacade _carFacade;
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private static UserListModel? _user;
        private static CarModel? _car;
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }

        public CarWrapper? Model
        {
            get;
            set;
        }

        public AddCarViewModel(CarFacade carFacade, IMediator mediator, UserFacade userFacade)
        {
            _carFacade = carFacade;
            _mediator = mediator;
            _userFacade = userFacade;
            Capacity = 5;
            RegistrationDate = DateTime.Now;
            SaveCarCommand = new AsyncRelayCommand(SaveAsync, CanSaveCar);
            BackCommand = new RelayCommand(() => _mediator.Send(new ChangeMessage<IProfileViewModel>()));

            _mediator.Register<SelectedMessage<UserWrapper>>(UserSelected);
        }


        public async Task LoadAsync(Guid id)
        {
            var users = await _carFacade.GetAsync();
            throw new NotImplementedException();
        }

        public ICommand SaveCarCommand { get; }

        public ICommand BackCommand { get; }

        public bool CanSaveCar()
        {
            return true;
        }

        private async void UserSelected(SelectedMessage<UserWrapper> message)
        {
            if (message.Id == null)
            {
                return;
            }
            _user = await _userFacade.GetAsyncList((Guid)message.Id);
        }
        public async Task SaveAsync()
        {

            if(_user != null)
            {
                _car = new CarModel(Manufacturer, Type, RegistrationDate, Capacity, _user);
                
                if(_car != null)
                {
                    _car = await _carFacade.SaveAsync(_car);

                    Thread.Sleep(100);
                    MessageBox.Show("Car succesfully added!");
                    _mediator.Send(new ReloadMessage<IProfileViewModel>());
                    _mediator.Send(new ReloadMessage<IHomePageViewModel>());
                    _mediator.Send(new ChangeMessage<IProfileViewModel>());
                }
            }

        }

        public async Task DeleteAsync()
        {
            var users = await _carFacade.GetAsync();
            throw new NotImplementedException();
        }


    }
}