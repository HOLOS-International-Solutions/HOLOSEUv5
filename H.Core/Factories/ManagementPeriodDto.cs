using System.ComponentModel;

namespace H.Core.Factories
{
    public class ManagementPeriodDto : DtoBase, IManagementPeriodDto
    {
        #region Fields

        private DateTime _start;
        private DateTime _end;
        private int _numberOfDays;

        #endregion

        #region Constructors

        public ManagementPeriodDto()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        #endregion

        #region Properties

        public DateTime Start
        {
            get => _start;
            set => SetProperty(ref _start, value);
        }

        public DateTime End
        {
            get => _end;
            set => SetProperty(ref _end, value);
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

        private void ValidateStart()
        {
            if ((Start >= End && End != default) || Start == default)
            {
                AddError(nameof(Start), H.Core.Properties.Resources.ErrorStartDate);
            }
            else
            {
                RemoveError(nameof(Start));
            }
        }

        private void ValidateEnd()
        {
            if ((End <= Start && Start != default) || End == default)
            {
                AddError(nameof(End), H.Core.Properties.Resources.ErrorEndDate);
            }
            else
            {
                RemoveError(nameof(End));
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
                else if (e.PropertyName.Equals(nameof(Start)))
                {
                    this.ValidateStart();
                }
                else if (e.PropertyName.Equals(nameof(End)))
                {
                    this.ValidateEnd();
                }
            }
        }

        #endregion
    }
}
