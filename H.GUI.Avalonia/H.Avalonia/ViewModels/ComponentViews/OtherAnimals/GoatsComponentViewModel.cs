using H.Core.Enumerations;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class GoatsComponentViewModel : OtherAnimalsViewModelBase
    {
        #region Constructors

        public GoatsComponentViewModel(ILogger logger, IStorageService storageService) : base(logger, storageService) 
        {
            ViewName = "Goats";
            OtherAnimalType = AnimalType.Goats;
        }

        public GoatsComponentViewModel() 
        { 

        }

        #endregion
    }
}
