using Flunt.Notifications;

namespace SnackHub.Production.Application.Models.Responses;

public class CreateProductionOrderResponse : Notifiable<Notification>
{
    public Guid Id { get; set; }

    public CreateProductionOrderResponse(Guid id)
    {
        Id = id;
    }
}
