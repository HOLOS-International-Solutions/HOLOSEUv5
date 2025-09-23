using H.Core.Enumerations;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class DeerComponentViewModel : OtherAnimalsViewModelBase
    {
        #region Constructors

        public DeerComponentViewModel(ILogger logger, IStorageService storageService, IAnimalComponentService animalComponentService) : base(logger, animalComponentService, storageService)
        {
            ViewName = "Deer";
            OtherAnimalType = AnimalType.Deer;
        }

        public DeerComponentViewModel()
        {

        }

        #endregion
    }
}
