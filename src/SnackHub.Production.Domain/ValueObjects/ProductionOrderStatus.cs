namespace SnackHub.Production.Domain.ValueObjects;

public enum ProductionOrderStatus
{
    Received = 0,    // Default status
    Preparing = 1,  // Being prepared by the Kitchen Staff
    Done = 2,       // Request had completed the request successfully
    Finished = 3,   // Request was complete and delivered
}