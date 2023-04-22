using Carpool.App.ViewModels.Interfaces;
namespace Carpool.App.Messages
{
    public record ReloadMessage<T> : IMessage
        where T : IUserControlViewModel
    {

    }
}
