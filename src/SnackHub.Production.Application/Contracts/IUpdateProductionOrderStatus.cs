using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.Contracts;

public interface IUpdateProductionOrderStatus
{
    Task<UpdateProductionOrderStatusResponse> Execute(UpdateStatusRequest orderStatusRequestRequest);
}