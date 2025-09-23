using H.Core.Enumerations;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class LlamaComponentViewModel : OtherAnimalsViewModelBase
    {
        #region

        public LlamaComponentViewModel(ILogger logger, IStorageService storageService) : base(logger, storageService) 
        {
            ViewName = "Llamas";
            OtherAnimalType = AnimalType.Llamas;
        }

        public LlamaComponentViewModel()
        {

        }

        #endregion
    }
}
