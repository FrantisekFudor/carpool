using System;
using Carpool.App.Extensions;
using Carpool.App.Messages;
using Carpool.App.Wrappers;
using Carpool.App.Services;
using Carpool.App.Commands;
using Carpool.BL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.BL.Facade;
using Carpool.App.ViewModels.Interfaces;
using System.Threading;
using System.Windows;

namespace Carpool.App.ViewModels
{
    public class ProfileViewModel : ViewModelBase, IProfileViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;

        public UserModel? User { get; set; }

        public ProfileViewModel(UserFacade userFacade, CarFacade carFacade, IMediator mediator)
        {
            _carFacade = carFacade;
            _userFacade = userFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            DeleteCarCommand = new AsyncRelayCommand(DeleteCarAsync);

            AddCarCommand = new RelayCommand(() => _mediator.Send(new ChangeMessage<IAddCarViewModel>()));

            _mediator.Register<SelectedMessage<UserWrapper>>(UserSelected);
            _mediator.Register<ReloadMessage<IProfileViewModel>>(OnReload);

            CarSelectedCommand = new RelayCommand<CarListModel>(CarSelected);
        }

        public static CarListModel? SelectedCar { get; set; }

        private void CarSelected(CarListModel? car) => CarToSelect(SelectedCar);

        private void CarToSelect(CarListModel? car)
        {
            if (car == null)
            {
                return;
            }
            _mediator.Send(new SelectedMessage<CarWrapper> { Id = car?.Id });
        }

        public UserWrapper? Model { get; set; }

        private bool CanSave()
        {
            if (User == null)
            {
                return false;
            }
            return true;
        }
        private async void UserSelected(SelectedMessage<UserWrapper> message)
        {
            if (message.Id == null)
            {
                return;
            }

            await LoadAsync((Guid)message.Id);
        }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand DeleteCarCommand { get; }

        public ICommand CarSelectedCommand { get; }

        public ICommand AddCarCommand { get; }

        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        private async void OnReload(object obj)
        {
            await LoadAsync(User.Id);
        }
        public async Task LoadAsync(Guid id)
        {
            User = await _userFacade.GetAsync(id) ?? UserModel.Empty;
            Cars.Clear();
            if (User != null)
            {
                ObservableCollectionExtensions.AddRange<CarListModel>(Cars, User.Cars);
                if (Cars.Count > 0)
                {
                    SelectedCar = Cars[0];
                }
                else
                {
                    SelectedCar = null;
                }
            }
            ChangePhotoUrl();
        }
        private void ChangePhotoUrl()
        {
            if (User != null)
                User.PhotoUrl = "https://www.clipartmax.com/png/full/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png";
        }

        public async Task DeleteAsync()
        {
            if (User == null)
            {
                return;
            }

            if (User.Id != Guid.Empty)
            {

                await _userFacade.DeleteAsync(User.Id);
            }

            MessageBox.Show("User deleted");
            _mediator.Send(new ChangeMessage<ILoginViewModel>());
        }

        public async Task DeleteCarAsync()
        {
            if (SelectedCar == null)
            {
                return;
            }

            if (SelectedCar.Id != Guid.Empty)
            {
                await _carFacade.DeleteAsync(SelectedCar.Id);
                MessageBox.Show("Car deleted");
            }


            if (User != null)
            {
                await LoadAsync(User.Id);
            }
            _mediator.Send(new ReloadMessage<IHomePageViewModel>());
        }

        public async Task SaveAsync()
        {
            if (User == null)
            {
                return;
            }

            var savedUser = await _userFacade.SaveAsync(User);
            User = savedUser;
            SaveMessages();
            MessageBox.Show("User saved!");
        }
        private void SaveMessages()
        {
            Thread.Sleep(100);
            _mediator.Send(new SelectedMessage<UserWrapper>
            {
                Id = User != null ? User.Id : throw new NullReferenceException()
            });
            _mediator.Send(new ReloadMessage<ILoginViewModel>());
        }

    }
}