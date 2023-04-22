using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Carpool.BL.Models;
using Carpool.BL.Facade;
using Carpool.App.Extensions;
using Carpool.App.Wrappers;
using Carpool.App.ViewModels.Interfaces;
using Carpool.App.Services;
using System.Windows.Input;
using Carpool.App.Messages;
using Carpool.App.Commands;
using System.Windows;

namespace Carpool.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Visibility _visibility;
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private Visibility _buttonsVisible;

        public Visibility ButtonsVisible
        {
            get => _buttonsVisible;
            set
            {
                _buttonsVisible = value;
                OnPropertyChanged("ButtonsVisibility");
            }
        }

        private UserModel? _user;
        private readonly UserFacade? _userFacade;

        public MainViewModel(
            UserFacade userFacade,
            ILoginViewModel loginViewModel,
            IProfileViewModel profileViewModel,
            IMyRidesViewModel myRidesViewModel,
            IHomePageViewModel homePageViewModel,
            IEditCarViewModel editCarViewModel,
            IEditRideViewModel editRideViewModel,
            IJoinRideViewModel joinRideViewModel,
            IAddCarViewModel addCarViewModel,
            IMediator mediator
            )
        {
            Visibility = Visibility.Collapsed;
            ButtonsVisible = Visibility.Hidden;
            LoginViewModel = loginViewModel;
            ProfileViewModel = profileViewModel;
            MyRidesViewModel = myRidesViewModel;
            HomePageViewModel = homePageViewModel;
            EditRideViewModel = editRideViewModel;
            JoinRideViewModel = joinRideViewModel;
            AddCarViewModel = addCarViewModel;
            CurrentViewModel = loginViewModel;
            EditCarViewModel = editCarViewModel;
            _userFacade = userFacade;

            mediator.Register<ChangeMessage<ILoginViewModel>>(ChangeToLoginViewModel);
            mediator.Register<ChangeMessage<IProfileViewModel>>(ChangeToProfileViewModel);
            mediator.Register<ChangeMessage<IMyRidesViewModel>>(ChangeToMyRidesViewModel);
            mediator.Register<ChangeMessage<IHomePageViewModel>>(ChangeToHomePageViewModel);
            mediator.Register<ChangeMessage<IEditCarViewModel>>(ChangeToEditCarViewModel);
            mediator.Register<ChangeMessage<IAddCarViewModel>>(ChangeToAddCarViewModel);
            mediator.Register<ChangeMessage<IJoinRideViewModel>>(ChangeToJoinRideViewModel);
            mediator.Register<ChangeMessage<IMyRidesViewModel>>(ChangeToMyRidesViewModel);
            mediator.Register<ChangeMessage<IEditRideViewModel>>(ChangeToEditRideViewModel);
            mediator.Register<SelectedMessage<UserWrapper>>(ChangeSelectedUserAsync);

            ChangeToLoginCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<ILoginViewModel>()));
            ChangeToProfileCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<IProfileViewModel>()));
            ChangeToMyRidesCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<IMyRidesViewModel>()));
            ChangeToHomePageCommand = new RelayCommand(() => mediator.Send(new ChangeMessage<IHomePageViewModel>()));
        }

        private IUserControlViewModel? _currentViewModel;

        public IUserControlViewModel? CurrentViewModel { 
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        private void ChangeViewModel(IUserControlViewModel? viewModel)
        {
            if(viewModel != null)
            {
                CurrentViewModel = viewModel;
            }
        }

        private async void ChangeSelectedUserAsync(SelectedMessage<UserWrapper> message)
        {
            if(message.Id == null)
            {
                return;
            }
            if (_userFacade != null)
            {
                SelectedUser = await _userFacade.GetAsync((Guid)message.Id) ?? UserModel.Empty;
            
                if(SelectedUser != UserModel.Empty)
                {
                    ButtonsVisible = Visibility.Visible;
                }
            }
        }

        public UserModel? SelectedUser
        {
            get => _user;
            set
            {
                _user = value;
            }
        }

        private void ChangeToLoginViewModel(object obj)
        {
            ChangeViewModel(LoginViewModel);
            SelectedUser = UserModel.Empty;
            Visibility = Visibility.Visible;
            ButtonsVisible = Visibility.Hidden;
        }

        private void ChangeToJoinRideViewModel(object obj)
        {
            ChangeViewModel(JoinRideViewModel);
            Visibility = Visibility.Visible;
        }

        private void ChangeToProfileViewModel(object obj)
        {
            ChangeViewModel(ProfileViewModel);
            Visibility = Visibility.Visible;
        }

        private void ChangeToAddCarViewModel(object obj)
        {
            ChangeViewModel(AddCarViewModel);
            Visibility = Visibility.Visible;
        }

        private void ChangeToEditCarViewModel(object obj)
        {
            ChangeViewModel(EditCarViewModel);
            Visibility = Visibility.Visible;
        }

        private void ChangeToMyRidesViewModel(object obj)
        {
            ChangeViewModel(MyRidesViewModel);
            Visibility = Visibility.Visible;
        }

        private void ChangeToHomePageViewModel(object obj)
        {
            ChangeViewModel(HomePageViewModel);
            Visibility = Visibility.Visible;
        }

        private void ChangeToEditRideViewModel(object obj)
        {
            ChangeViewModel(EditRideViewModel);
            Visibility = Visibility.Visible;
        }

        public ICommand ChangeToLoginCommand { get; }
        public ICommand ChangeToProfileCommand { get; }
        public ICommand ChangeToMyRidesCommand { get; }
        public ICommand ChangeToHomePageCommand { get; }

        
        public ILoginViewModel LoginViewModel { get; set; }
        public IProfileViewModel ProfileViewModel { get; set; }
        public IMyRidesViewModel MyRidesViewModel { get; set; }
        public IHomePageViewModel HomePageViewModel { get; set; }
        public IEditCarViewModel EditCarViewModel { get; set; }
        public IEditRideViewModel EditRideViewModel { get; set; }
        public IJoinRideViewModel JoinRideViewModel { get; set; }
        public IAddCarViewModel AddCarViewModel { get; set; }

        public ObservableCollection<CarListModel> Cars { get; } = new();

        public override void LoadInDesignMode()
        {
            Cars.Add(new CarListModel(
                Manufacturer: "Spaghetti",
                Type: "Man",
                Capacity: 5));
        }
    }
}
