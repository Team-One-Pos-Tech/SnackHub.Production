using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.Contracts;

public interface IListProductionOrders
{
    Task<IEnumerable<KitchenOrderResponse>> Get();
}