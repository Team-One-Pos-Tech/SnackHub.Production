using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.Contracts;

public interface ICreateProductionOrder
{
    Task<CreateKitchenOrderResponse> Execute(CreateProductionOrderRequest request);
}