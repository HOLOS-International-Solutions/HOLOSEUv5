using H.Core.Models;
using H.Core.Models.Animals;
using H.Core.Models.LandManagement.Fields;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using Prism.Regions;

namespace H.Avalonia.ViewModels.ComponentViews;

public abstract class AnimalViewModelBase : ViewModelBase
{
    protected IAnimalComponentService AnimalComponentService;

    protected AnimalViewModelBase()
    {
    }

    protected AnimalViewModelBase(IStorageService storageService) : base(storageService)
    {
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);

        if (navigationContext.Parameters.ContainsKey(GuiConstants.ComponentKey))
        {
            var parameter = navigationContext.Parameters[GuiConstants.ComponentKey];
            if (parameter is AnimalComponentBase animalComponent)
            {
                this.InitializeViewModel(animalComponent);
            }
        }
    }

    public override void InitializeViewModel(ComponentBase component)
    {
        base.InitializeViewModel(component);

        if (component is AnimalComponentBase animalComponentBase)
        {
            this.AnimalComponentService.InitializeAnimalComponent(base.StorageService.GetActiveFarm(), animalComponentBase);
        }
    }
}