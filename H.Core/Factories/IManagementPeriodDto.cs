namespace H.Core.Factories;

public interface IManagementPeriodDto : IDto
{
    int NumberOfDays { get; set; }
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
}