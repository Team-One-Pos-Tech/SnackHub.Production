using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.Contracts;

public interface IListProductionOrders
{
    Task<IEnumerable<KitchenOrderResponse>> Get();
}