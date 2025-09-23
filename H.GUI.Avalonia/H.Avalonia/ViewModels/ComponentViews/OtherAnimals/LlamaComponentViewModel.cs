using H.Core.Enumerations;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class LlamaComponentViewModel : OtherAnimalsViewModelBase
    {
        #region

        public LlamaComponentViewModel(ILogger logger, IStorageService storageService, IAnimalComponentService animalComponentService) : base(logger, animalComponentService, storageService) 
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
