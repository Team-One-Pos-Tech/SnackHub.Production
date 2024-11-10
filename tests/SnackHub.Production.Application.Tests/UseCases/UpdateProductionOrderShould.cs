using FluentAssertions;
using Moq;
using SnackHub.Domain.Contracts;
using UpdateProductionOrderStatus = SnackHub.Production.Application.Models.Requests.UpdateProductionOrderStatus;

namespace SnackHub.Application.Tests.UseCases;

internal class UpdateProductionOrdersShould
{
    private Mock<IProductionOrderRepository> productionOrderRepositoryMock;
    private Production.Application.UseCases.UpdateProductionOrderStatus updateProductionOrders;

    [SetUp]
    public void Setup()
    {
        productionOrderRepositoryMock = new Mock<IProductionOrderRepository>();

        updateProductionOrders = new Production.Application.UseCases.UpdateProductionOrderStatus(productionOrderRepositoryMock.Object);
    }

    [Test]
    public async Task List_Production_Orders()
    {
        #region Arrange

        var request = new UpdateProductionOrderStatus
        {
            OrderId = Guid.NewGuid()
        };

        #endregion

        #region Act

        var response = await updateProductionOrders.Execute(request);

        #endregion

        #region Assert

        response.IsValid.Should().BeTrue();

        productionOrderRepositoryMock.Verify(x => x.GetByOderIdAsync(request.OrderId), Times.Once);

        #endregion
    }
}
