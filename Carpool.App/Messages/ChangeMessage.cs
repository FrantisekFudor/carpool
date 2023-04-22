using Carpool.BL.Models;
using Carpool.App.ViewModels.Interfaces;

namespace Carpool.App.Messages
{
    public record ChangeMessage<T> : IMessage
        where T : IUserControlViewModel
    {
    }
}