using H.Core.Enumerations;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class BisonComponentViewModel : OtherAnimalsViewModelBase
    {
        #region Constructors

        public BisonComponentViewModel()
        {
        }

        public BisonComponentViewModel(IAnimalComponentService componentService, IStorageService storageService) : base(componentService, storageService)
        {
            ViewName = "Bison";
            OtherAnimalType = AnimalType.Bison;
        }

        #endregion
    }
}
