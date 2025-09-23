using System;
using H.Core.Factories;
using H.Core.Models;
using H.Core.Models.Animals;
using H.Core.Models.LandManagement.Fields;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;
using Prism.Regions;

namespace H.Avalonia.ViewModels.ComponentViews;

public abstract class AnimalComponentViewModelBase : ViewModelBase
{
    #region Fields

    private AnimalComponentBase _selectedAnimalComponent;

    protected IAnimalComponentDto _selectedAnimalComponentDto;

    protected IAnimalComponentService AnimalComponentService;

    #endregion

    #region Constructors

    protected AnimalComponentViewModelBase()
    {
    }

    protected AnimalComponentViewModelBase(ILogger logger, IStorageService storageService) : base(storageService, logger)
    {
    }

    protected AnimalComponentViewModelBase(IAnimalComponentService animalComponentService, ILogger logger, IStorageService storageService) : base(storageService, logger)
    {
        if (animalComponentService != null)
        {
            AnimalComponentService = animalComponentService; 
        }
        else
        {
            throw new ArgumentNullException(nameof(animalComponentService));
        }
    }

    #endregion

    #region Properties

    protected IAnimalComponentDto SelectedAnimalComponentDto
    {
        get => _selectedAnimalComponentDto;
        set => SetProperty(ref _selectedAnimalComponentDto, value);
    }

    #endregion

    #region Public Methods

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
            _selectedAnimalComponent = animalComponentBase;

            this.AnimalComponentService.InitializeAnimalComponent(base.StorageService.GetActiveFarm(), animalComponentBase);

            // Build a DTO to represent the model/domain object
            var dto = this.AnimalComponentService.TransferToAnimalComponentDto(animalComponentBase);
        }
    } 

    #endregion
}