using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Models.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;
using System;
using H.Core.Services.Animals;

namespace H.Avalonia.ViewModels.ComponentViews;

public class AnimalComponentViewModelDesign : AnimalComponentViewModelBase
{
    public AnimalComponentViewModelDesign()
    {
        base.SelectedAnimalComponentDto = new AnimalComponentDto();
        base.SelectedAnimalComponentDto.Name = "Bison #2";

        ViewName = "Bison";
    }

    protected AnimalComponentViewModelDesign(IAnimalComponentService animalComponentService, ILogger logger, IStorageService storageService) : base(animalComponentService, logger, storageService)
    {
    }
}