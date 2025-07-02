﻿using System.Collections.ObjectModel;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Models;
using H.Core.Services.LandManagement.Fields;
using H.Core.Services.StorageService;
using Prism.Events;
using Prism.Regions;

namespace H.Avalonia.ViewModels.ComponentViews.LandManagement.Field;

public class FieldComponentViewModelDesign : FieldComponentViewModel
{
    public FieldComponentViewModelDesign()
    {
        base.SelectedFieldSystemComponentDto = new FieldSystemComponentDto();
        base.SelectedFieldSystemComponentDto.Name = "A Field";

        base.SelectedFieldSystemComponentDto.CropDtos = new ObservableCollection<ICropDto>()
        {
            new CropDto() { Year = 2021, CropType = CropType.Wheat },
            new CropDto() { Year = 2022, CropType = CropType.Barley },
            new CropDto() { Year = 2023, CropType = CropType.Oats },
        };

        base.StorageService = new DefaultStorageService(new H.Core.Storage());
        base.StorageService.Storage = new H.Core.Storage();
        base.StorageService.Storage.ApplicationData = new ApplicationData
        {
            DisplayUnitStrings = new DisplayUnitStrings()
            {
                HectaresString = "(ha)",
            }
        };
    }

    public FieldComponentViewModelDesign(IRegionManager regionManager, IEventAggregator eventAggregator, IStorageService storageService, IFieldComponentDtoFactory fieldComponentDtoFactory, ICropFactory cropFactory, IFieldComponentService fieldComponentService, IUnitsOfMeasurementCalculator unitsOfMeasurementCalculator) : base(regionManager, eventAggregator, storageService, fieldComponentService, unitsOfMeasurementCalculator)
    {
    }
}