using System;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;

namespace H.Avalonia.Services;
public interface IErrorHandlerService
{
    public WindowNotificationManager NotificationManager { get; set; }
    /// <summary>
    /// Handles a validation error, draws error (red) toast message to screen.
    /// </summary>
    /// <param name="validationMessage">The validation error message.</param>
    void HandleValidationError(string validationMessage);

    /// <summary>
    /// Handles a validation warning, draws warning (orange) toast message to screen.
    /// </summary>
    /// <param name="validationMessage">The validation warning message.</param>
    void HandleValidationWarning(string validationMessage);
}
