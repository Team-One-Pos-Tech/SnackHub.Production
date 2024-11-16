using Microsoft.AspNetCore.Mvc;
using SnackHub.Production.Api.Extensions;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Api.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class ProductionOrderController(
    IListProductionOrders listProductionOrders,
    IUpdateProductionOrderStatus updateProductionOrderStatus,
    ICreateProductionOrder createProductionOrder
    ) : ControllerBase
{
    [HttpGet("GetAllProductionOrders")]
    [ProducesResponseType(typeof(IEnumerable<ProductionOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductionOrderResponse>>> GetAll()
    {
        var productionOrders = await listProductionOrders.Get();
        
        return Ok(productionOrders);
    }

    [HttpPut("CreateProductionOrder")]
    [ProducesResponseType(typeof(CreateProductionOrderResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductionOrderStatusResponse>>
        UpdateStatus([FromBody] CreateProductionOrderRequest request)
    {
        var response = await createProductionOrder.Execute(request);

        return response.IsValid
            ? Ok(response)
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    }

    [HttpPut("UpdateStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductionOrderStatusResponse>> 
        UpdateStatus([FromBody] UpdateStatusRequest updateStatusRequest)
    {
        var response = await updateProductionOrderStatus.Execute(updateStatusRequest);
        
        return response.IsValid 
            ? Ok(response) 
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    }
    
}