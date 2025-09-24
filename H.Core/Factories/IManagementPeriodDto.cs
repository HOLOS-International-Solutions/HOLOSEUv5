namespace H.Core.Factories;

public interface IManagementPeriodDto : IDto
{
    int NumberOfDays { get; set; }
    DateTime Start { get; set; }
    DateTime End { get; set; }
}