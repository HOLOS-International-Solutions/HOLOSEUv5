using H.Core.Enumerations;
using H.Core.Models.Animals;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using System;
using H.Core.Factories;
using Microsoft.Extensions.Logging;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class BisonComponentViewModelDesign : BisonComponentViewModel
    {
        public BisonComponentViewModelDesign()
        {
            base.SelectedAnimalComponentDto = new AnimalComponentDto();
            base.SelectedAnimalComponentDto.Name = "Bison";

            ViewName = "Bison";
            OtherAnimalType = AnimalType.Bison;
            Groups.Add(new AnimalGroup { GroupType = OtherAnimalType });
            ManagementPeriodViewModels.Add(new ManagementPeriodDto { Name = "Test Group #1", Start = new DateTime(2000, 01, 01), End = new DateTime(2001, 01, 01), NumberOfDays = 364 });
        }

        public BisonComponentViewModelDesign(ILogger logger, IAnimalComponentService componentService, IStorageService storageService) : base(logger, componentService, storageService)
        {

        }
    }
}