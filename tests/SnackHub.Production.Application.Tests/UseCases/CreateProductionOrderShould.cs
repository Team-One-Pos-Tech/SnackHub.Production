using FluentAssertions;
using Moq;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Production.Application.Models;
using SnackHub.Production.Application.UseCases;

namespace SnackHub.Application.Tests.UseCases;

internal class CreateProductionOrderShould
{

    [Test]
    public async Task Should_Create_Production_Order_Async()
    {
        #region Arrange
        var productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        var createProductionOrder = new CreateProductionOrder(productionOrderRepositoryMock.Object);

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
}
