using Carpool.App.Wrappers;

namespace Carpool.App.ViewModels.Interfaces
{
    public interface IMyRidesViewModel : IUserControlViewModel, IDetailViewModel<RideWrapper>
    {
    }
}