using Carpool.App.Extensions;
using Carpool.App.Messages;
using Carpool.App.Wrappers;
using Carpool.App.Services;
using Carpool.App.Commands;
using Carpool.BL.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Carpool.BL.Facade;
using Carpool.App.ViewModels.Interfaces;

namespace Carpool.App.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        public LoginViewModel(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            _mediator.Register<ReloadMessage<ILoginViewModel>>(OnReload);

            UserSelectedCommand = new RelayCommand<UserListModel>(UserSelected);
        }

        public ObservableCollection<UserListModel> Users { get; set; } = new();

        public static UserListModel? SelectedUser { get; set; }
        public ICommand UserSelectedCommand { get; }


        private void UserSelected(UserListModel? user) => UserToSelect(SelectedUser);

        private void UserToSelect(UserListModel? user)
        {
            if(user == null)
            {
                return;
            }
            _mediator.Send(new SelectedMessage<UserWrapper> { Id = user?.Id });
            _mediator.Send(new ChangeMessage<IHomePageViewModel>());
        }

        private async void OnReload(object obj)
        {
            await LoadAsync();
        }


        public async Task LoadAsync()
        {
            Users.Clear();
            var users = await _userFacade.GetAsync();
            ObservableCollectionExtensions.AddRange<UserListModel>(Users, users);
        }


    }
}