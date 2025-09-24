using System.ComponentModel;

namespace H.Core.Factories
{
    public class ManagementPeriodDto : DtoBase, IManagementPeriodDto
    {
        #region Fields

        private DateTime _startDate;
        private DateTime _endDate;
        private int _numberOfDays;

        #endregion

        #region Constructors

        public ManagementPeriodDto()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        #endregion

        #region Properties

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public int NumberOfDays
        {
            get => _numberOfDays;
            set => SetProperty(ref _numberOfDays, value);
        }

        #endregion

        #region Private Methods

        private void ValidatePeriodName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddError(nameof(Name), H.Core.Properties.Resources.ErrorNameCannotBeEmpty);
            }
            else
            {
                RemoveError(nameof(Name));
            }
        }

        private void ValidateStartDate()
        {
            if ((StartDate >= EndDate && EndDate != default) || StartDate == default)
            {
                AddError(nameof(StartDate), H.Core.Properties.Resources.ErrorStartDate);
            }
            else
            {
                RemoveError(nameof(StartDate));
            }
        }

        private void ValidateEndDate()
        {
            if ((EndDate <= StartDate && StartDate != default) || EndDate == default)
            {
                AddError(nameof(EndDate), H.Core.Properties.Resources.ErrorEndDate);
            }
            else
            {
                RemoveError(nameof(EndDate));
            }
        }

        private void ValidateNumberOfDays()
        {
            if (NumberOfDays <= 0)
            {
                AddError(nameof(NumberOfDays), H.Core.Properties.Resources.ErrorMustBeGreaterThan0);
            }
            else
            {
                RemoveError(nameof(NumberOfDays));
            }
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != null)
            {
                if (e.PropertyName.Equals(nameof(Name)))
                {
                    this.ValidatePeriodName();
                }
                else if (e.PropertyName.Equals(nameof(NumberOfDays)))
                {
                    this.ValidateNumberOfDays();
                }
                else if (e.PropertyName.Equals(nameof(StartDate)))
                {
                    this.ValidateStartDate();
                }
                else if (e.PropertyName.Equals(nameof(EndDate)))
                {
                    this.ValidateEndDate();
                }
            }
        }

        #endregion
    }
}
