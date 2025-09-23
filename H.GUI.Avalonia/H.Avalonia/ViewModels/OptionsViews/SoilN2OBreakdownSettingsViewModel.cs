using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using Avalonia.Controls.Notifications;
using H.Core.Events;
using H.Avalonia.ViewModels.OptionsViews.DataTransferObjects;
using H.Core.Enumerations;
using H.Core.Models;
using H.Core.Providers.Animals;
using H.Avalonia.Services;
using H.Core.Services.StorageService;
using Prism.Events;
using Prism.Regions;

namespace H.Avalonia.ViewModels.OptionsViews
{
    public class SoilN2OBreakdownSettingsViewModel : ViewModelBase, IConfirmNavigationRequest
    {
        #region Fields

        private SoilN2OBreakdownSettingsDTO _data;
        private readonly IErrorHandlerService _errorHandlerService;
        private double _total;
        private bool _entriesAreValid;

        #endregion

        #region Constructors

        public SoilN2OBreakdownSettingsViewModel(IStorageService storageService, IEventAggregator eventAggregator, IErrorHandlerService errorHandlerService) : base(storageService)
        {
            EventAggregator = eventAggregator;
            _errorHandlerService = errorHandlerService as ErrorHandlerService;
        }
        #endregion

        #region Properties
        public SoilN2OBreakdownSettingsDTO Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        // Sets the instance of notification manager in the ErrorHandlerService
        public new WindowNotificationManager NotificationManager
        {
            get => base.NotificationManager;
            set
            {
                if (base.NotificationManager == value) return;
                base.NotificationManager = value;
                _errorHandlerService.NotificationManager = value;
            }
        }
        #endregion

        #region Public Methods

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!IsInitialized)
            {
                Data = new SoilN2OBreakdownSettingsDTO(StorageService);
                IsInitialized = true;
            }
            CalculateTotal();
            Data.PropertyChanged += ValidateTotalEquals100;
            EventAggregator.GetEvent<ValidationErrorOccurredEvent>().Subscribe(LockOnInvalidEntries);
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (_total == 100.0)
            {
                Data.PropertyChanged -= ValidateTotalEquals100;
                EventAggregator.GetEvent<ValidationErrorOccurredEvent>().Unsubscribe(LockOnInvalidEntries);
                continuationCallback(_entriesAreValid);
                return;
            }
            _errorHandlerService.HandleValidationError(string.Format(H.Core.Properties.Resources.SumOfMonthlyN2OInputsPercent, _total));
            continuationCallback(_entriesAreValid);
        }

        #endregion

        #region Private Methods

        private void CalculateTotal()
        {
            _total = 0;
            foreach (Months month in Enum.GetValues(typeof(Months)))
            {
                _total += this.Data.MonthlyValues.GetValueByMonth(month);
            }
        }

        #endregion

        #region Event Handlers

        private void ValidateTotalEquals100(object sender, PropertyChangedEventArgs e)
        {
            CalculateTotal();
            if (_total == 100.00)
            {
                _entriesAreValid = true;
            }
        }

        private void LockOnInvalidEntries(ErrorInformation errorInformation)
        {
            _entriesAreValid = false;
        }

        #endregion
    }
}
