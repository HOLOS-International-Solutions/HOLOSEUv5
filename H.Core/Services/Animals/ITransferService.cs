using H.Core.Factories;
using H.Infrastructure;

namespace H.Core.Services.Animals
{
    public interface ITransferService<TModelBase, TDto>
        where TModelBase : ModelBase
        where TDto : IDto, new()
    {
        TDto TransferToDto(TModelBase model);
    }
}