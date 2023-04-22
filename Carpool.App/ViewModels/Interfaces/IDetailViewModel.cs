using System;
using System.Threading.Tasks;

namespace Carpool.App.ViewModels.Interfaces
{
    public interface IDetailViewModel<out TDetail> : IViewModel
    {
        TDetail? Model { get; }
        Task LoadAsync(Guid id);
        Task DeleteAsync();
        Task SaveAsync();
    }
}