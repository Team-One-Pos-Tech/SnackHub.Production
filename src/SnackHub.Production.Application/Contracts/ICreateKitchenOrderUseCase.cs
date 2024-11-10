using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.Contracts;

public interface ICreateKitchenOrderUseCase
{
    Task<CreateKitchenOrderResponse> Execute(CreateKitchenOrderRequest request);
}