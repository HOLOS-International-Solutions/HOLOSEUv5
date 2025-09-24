using System;
using H.Core;
using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Models.Animals;
using H.Core.Services.StorageService;


namespace H.Avalonia.ViewModels.ComponentViews.OtherAnimals
{
    public class GoatsComponentViewModelDesign : GoatsComponentViewModel
    {
        public GoatsComponentViewModelDesign()
        {
            ViewName = "Goats";
            OtherAnimalType = AnimalType.Goats;
            Groups.Add(new AnimalGroup { GroupType = AnimalType.Goats });
            ManagementPeriodViewModels.Add(new ManagementPeriodDto { Name = "Test Group #1", Start = new DateTime(2000, 01, 01), End = new DateTime(2001, 01, 01), NumberOfDays = 364 });
        }
    }
}
