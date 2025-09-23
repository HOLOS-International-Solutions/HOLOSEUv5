using H.Core.Enumerations;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class MulesComponentViewModel : OtherAnimalsViewModelBase
    {
        #region Constructors

        public MulesComponentViewModel(ILogger logger, IStorageService storageService, IAnimalComponentService animalComponentService) : base(logger, animalComponentService, storageService)
        {
            ViewName = "Mules";
            OtherAnimalType = AnimalType.Mules;
        }

        public MulesComponentViewModel() 
        { 
        
        }

        #endregion
    }
}