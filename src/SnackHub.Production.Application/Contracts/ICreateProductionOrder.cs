using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.Contracts;

public interface ICreateProductionOrder
{
    Task<CreateProductionOrderResponse> Execute(CreateProductionOrderRequest request);
}