using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.UseCases;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;

namespace SnackHub.Application.Tests.UseCases;

internal class CreateProductionOrderShould
{
    private Mock<IProductionOrderRepository> productionOrderRepositoryMock;
    private Mock<ILogger<CreateProductionOrder>> loggerMock;
    private CreateProductionOrder createProductionOrder;

    [SetUp]
    public void Setup()
    {
        productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        loggerMock = new Mock<ILogger<CreateProductionOrder>>();

        createProductionOrder = new CreateProductionOrder(loggerMock.Object, productionOrderRepositoryMock.Object);
    }

    [Test]
    public async Task Should_Create_Production_Order_Async()
    {
        #region Arrange
        

        var orderId = Guid.NewGuid();

        #endregion

        #region Act
        var response = await createProductionOrder.Execute(new CreateProductionOrderRequest(orderId));

        #endregion

        #region Assert
        response.IsValid.Should().BeTrue();

        productionOrderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ProductionOrder>()), Times.Once);
        #endregion
    }

    [Test]
    public async Task Should_Create_Production_Order_With_Product_Items()
    {
        #region Arrange

        var orderId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var request = new CreateProductionOrderRequest(orderId)
        {
            Items = new List<ProductionItemRequest>
            {
                new() {
                    ProductId = productId,
                    Quantity = 3
                }
            }
        };

        #endregion

        #region Act

        var response = await createProductionOrder.Execute(request);

        #endregion

        #region Assert
        response.IsValid.Should().BeTrue();

        productionOrderRepositoryMock.Verify(x => x.AddAsync(It.Is<ProductionOrder>(
                request => request.Items.Any(x => x.ProductId == productId && x.Quantity == 3)
            )), Times.Once);
        #endregion
    }
}
