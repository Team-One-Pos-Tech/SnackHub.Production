using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.Contracts;

public interface IUpdateKitchenOrderStatusUseCase
{
    Task<UpdateKitchenOrderStatusResponse> Execute(UpdateKitchenOrderStatusRequest orderStatusRequest);
}