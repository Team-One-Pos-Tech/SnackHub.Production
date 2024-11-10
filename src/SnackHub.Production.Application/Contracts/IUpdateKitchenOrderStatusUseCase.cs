using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.Contracts;

public interface IUpdateKitchenOrderStatusUseCase
{
    Task<UpdateKitchenOrderStatusResponse> Execute(UpdateProductionOrderStatus orderStatusRequest);
}