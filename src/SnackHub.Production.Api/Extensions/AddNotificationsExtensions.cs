using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SnackHub.Production.Api.Extensions;

public static class AddNotificationsExtensions
{
    public static ModelStateDictionary AddNotifications(this ModelStateDictionary modelState, IEnumerable<Notification> notifications)
    {
        foreach (var item in notifications)
        {
            modelState.AddModelError(item.Key, item.Message);
        }

        return modelState;
    }
}