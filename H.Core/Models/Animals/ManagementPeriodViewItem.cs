using System;
using System.ComponentModel;
using H.Infrastructure;

namespace H.Core.Models.Animals
{
    /// <summary>
    /// A view item class for ManagementPeriod objects used in UI binding scenarios.
    /// Provides property validation and change notification for management period data.
    /// </summary>
    public class ManagementPeriodViewItem : ModelBase
    {
        #region Fields

        private DateTime _startDate;
        private DateTime _endDate;
        private int _numberOfDays;
        private string _name;

        #endregion

        #region Constructors

        public ManagementPeriodViewItem()
        {
            this.PropertyChanged += OnPropertyChanged;
            this.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.EndDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.Name = "New Management Period";
            CalculateNumberOfDays();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The start date of the management period
        /// </summary>
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                // Don't allow start date to be after end date
                if (value > EndDate && EndDate != default(DateTime))
                {
                    return;
                }
                
                SetProperty(ref _startDate, value);
                CalculateNumberOfDays();
            }
        }

        /// <summary>
        /// The end date of the management period
        /// </summary>
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                // Don't allow end date to be before start date
                if (value < StartDate && StartDate != default(DateTime))
                {
                    return;
                }
                
                SetProperty(ref _endDate, value);
                CalculateNumberOfDays();
            }
        }

        /// <summary>
        /// The number of days in the management period
        /// </summary>
        public int NumberOfDays
        {
            get => _numberOfDays;
            set => SetProperty(ref _numberOfDays, value);
        }

        /// <summary>
        /// The name of the management period
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        #endregion

        #region Private Methods

        private void CalculateNumberOfDays()
        {
            if (StartDate != default(DateTime) && EndDate != default(DateTime) && EndDate >= StartDate)
            {
                NumberOfDays = (EndDate - StartDate).Days + 1;
            }
            else
            {
                NumberOfDays = 0;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Recalculate days when dates change
            if (e.PropertyName == nameof(StartDate) || e.PropertyName == nameof(EndDate))
            {
                CalculateNumberOfDays();
            }
        }

        #endregion
    }
}