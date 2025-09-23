using H.Core.Enumerations;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class DeerComponentViewModel : OtherAnimalsViewModelBase
    {
        #region Constructors

        public DeerComponentViewModel(ILogger logger, IStorageService storageService) : base(logger, storageService) 
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
