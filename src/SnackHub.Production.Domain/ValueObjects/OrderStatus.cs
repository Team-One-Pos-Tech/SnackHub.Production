namespace SnackHub.Domain.ValueObjects;

public enum OrderStatus
{
    Pending = 0,    // Default status
    Cancelled = 1,  // Order was cancelled by the client
    Confirmed = 2,  // Order was confirmed by the client
    Accepted = 3,   // Order was accepted by the store (upon successful payment)
    Declined = 4,   // Order was declined by the store (upon failed payment)
}