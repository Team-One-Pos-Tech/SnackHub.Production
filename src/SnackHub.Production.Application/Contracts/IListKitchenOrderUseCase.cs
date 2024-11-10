using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.Contracts;

public interface IListKitchenOrderUseCase
{
    Task<IEnumerable<KitchenOrderResponse>> Execute();
}