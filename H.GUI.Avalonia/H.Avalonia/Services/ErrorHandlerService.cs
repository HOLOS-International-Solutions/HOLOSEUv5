using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using Avalonia.Controls.Notifications;
using H.Core.Events;
using H.Core.Models;

namespace H.Avalonia.Services
{
    public class ErrorHandlerService : IErrorHandlerService
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;
        private WindowNotificationManager _notificationManager;

        #endregion

        #region Properties

        public WindowNotificationManager NotificationManager
        {
            get => _notificationManager;
            set => _notificationManager = value;
        }

        #endregion

        #region Constructors

        public ErrorHandlerService()
        {

        }

        public ErrorHandlerService(ILogger logger, IEventAggregator eventAggregator)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        #endregion

        #region Public Methods

        public void HandleValidationError(string validationMessage)
        {
            _logger.LogWarning("Validation Error: {ValidationMessage}", validationMessage);

            _eventAggregator.GetEvent<ValidationErrorOccurredEvent>().Publish(new ErrorInformation(validationMessage));

            ShowToastMessage(validationMessage, NotificationType.Error);
        }

        public void HandleValidationWarning(string validationMessage)
        {
            _logger.LogWarning("Validation Error: {ValidationMessage}", validationMessage);

            _eventAggregator.GetEvent<ValidationErrorOccurredEvent>().Publish(new ErrorInformation(validationMessage));

            ShowToastMessage(validationMessage, NotificationType.Warning);
        }

        #endregion

        #region Private Methods

        private void ShowToastMessage(string toastMessage, NotificationType type)
        {
            NotificationManager?.Show(new Notification(
                title: type.ToString(),
                message: toastMessage,
                type: type,
                expiration: TimeSpan.FromSeconds(5)));
        }

        #endregion
    }
}
