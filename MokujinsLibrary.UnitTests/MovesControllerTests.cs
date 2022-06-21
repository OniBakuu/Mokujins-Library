using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MokujinsLibrary.Controllers;
using MokujinsLibrary.Entities;
using Moq;
using Xunit;
using MokujinsLibrary.Repositories;

namespace MokujinsLibrary.UnitTests {};

public class MovesControllerTests
{
    [Fact]
    public async Task GetMoveAsync_WithUnexistingMove_ReturnsNotFound()
    {
        //Arrange
        var repoStub = new Mock<IMoveRepo>();
        repoStub.Setup(repo => repo.GetMoveAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((Move)null);
        var loggerStub = new Mock<ILogger<MovesController>>();

        var controller = new MovesController(repoStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetMove("dragunov", "d+2");

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}