using System;
using System.ComponentModel;
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

    protected AnimalComponentViewModelBase(
        IAnimalComponentService animalComponentService, ILogger logger,
        IStorageService storageService, 
        IManagementPeriodService managementPeriodService) : base(storageService, logger)
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
        if (component is AnimalComponentBase animalComponentBase)
        {
            base.InitializeViewModel(component);

            this.PropertyChanged += OnPropertyChanged;

            _selectedAnimalComponent = animalComponentBase;

            this.AnimalComponentService.InitializeComponent(base.StorageService.GetActiveFarm(), animalComponentBase);

            // Build a DTO to represent the model/domain object
            var dto = this.AnimalComponentService.TransferToAnimalComponentDto(animalComponentBase);

            this.SelectedAnimalComponentDto = dto;

            dto.PropertyChanged += OnAnimalComponentDtoPropertyChanged;
        }
    }

    private void OnAnimalComponentDtoPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is IAnimalComponentDto dto)
        {
            // A property on the DTO has been changed by the user, assign the new value to the system object after any unit conversion (if necessary)
            this.AnimalComponentService.TransferAnimalComponentDtoToSystem(dto, _selectedAnimalComponent);
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        
    }

    #endregion
}