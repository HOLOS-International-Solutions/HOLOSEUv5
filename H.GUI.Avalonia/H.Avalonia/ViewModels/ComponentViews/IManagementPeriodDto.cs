using System;
using H.Core.Factories;

namespace H.Avalonia.ViewModels.ComponentViews;

public interface IManagementPeriodDto : IDto
{
    int NumberOfDays { get; set; }
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
}