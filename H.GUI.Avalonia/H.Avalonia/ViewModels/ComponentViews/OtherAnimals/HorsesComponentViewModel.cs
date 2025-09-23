using H.Core.Enumerations;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class HorsesComponentViewModel : OtherAnimalsViewModelBase
    {
        #region Constructors

        public HorsesComponentViewModel(ILogger logger, IStorageService storageService) : base(logger, storageService) 
        {
            ViewName = "Horses";
            OtherAnimalType = AnimalType.Horses;
        }

        public HorsesComponentViewModel() 
        { 
        
        }

        #endregion
    }
}