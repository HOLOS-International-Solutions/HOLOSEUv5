using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Models.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;
using System;

namespace H.Avalonia.ViewModels.ComponentViews;

public class AnimalComponentViewModelDesign : AnimalComponentViewModelBase
{
    public AnimalComponentViewModelDesign()
    {
        base.SelectedAnimalComponentDto = new AnimalComponentDto();
        base.SelectedAnimalComponentDto.Name = "Bison #2";

        ViewName = "Bison";
    }

    protected AnimalComponentViewModelDesign(ILogger logger, IStorageService storageService) : base(logger, storageService)
    {
    }
}