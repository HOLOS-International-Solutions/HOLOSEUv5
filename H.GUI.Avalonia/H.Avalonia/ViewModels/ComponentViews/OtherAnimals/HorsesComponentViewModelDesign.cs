using System;
using H.Core.Models.Animals;
using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Services.StorageService;

namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class HorsesComponentViewModelDesign : HorsesComponentViewModel
    {
        public HorsesComponentViewModelDesign() 
        {
            ViewName = "Horses";
            OtherAnimalType = AnimalType.Horses;
            Groups.Add(new AnimalGroup { GroupType = OtherAnimalType });
            ManagementPeriodDtos.Add(new ManagementPeriodDto { Name = "Test Group #1", Start = new DateTime(2000, 01, 01), End = new DateTime(2001, 01, 01), NumberOfDays = 364 });
        }
    }
}
