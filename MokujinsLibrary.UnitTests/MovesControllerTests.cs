using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MokujinsLibrary.Controllers;
using MokujinsLibrary.Dtos;
using MokujinsLibrary.Entities;
using Moq;
using Xunit;
using MokujinsLibrary.Repositories;

namespace MokujinsLibrary.UnitTests {};

public class MovesControllerTests
{
    private readonly Mock<IMoveRepo> repoStub = new();
    private readonly Mock<ILogger<MovesController>> loggerStub = new();

    [Fact]
    public async Task GetMoveAsync_WithUnexistingMove_ReturnsNotFound()
    {
        //Arrange
        repoStub.Setup(repo => repo.GetMoveAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((Move)null);

        var controller = new MovesController(repoStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetMove("dragunov", "d+2");

        //Assert
        //Assert.IsType<NotFoundResult>(result.Result);
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetMoveAsync_WithExistingMove_ReturnsExpectedMove()
    {
        //Arrange
        var expectedMove = CreateTestMove();
        
        repoStub.Setup(repo => repo.GetMoveAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(expectedMove);
        var controller = new MovesController(repoStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetMove("", "");
        
        //Assert
        result.Value.Should().BeEquivalentTo(expectedMove, options => options.ComparingByMembers<Move>());
    }

    private Move CreateTestMove()
    {
        return new()
        {
            character = "TestChar",
            moveName = "TestMoveName",
            input = "TestInputString",
            damage = "TestDamageVal",
            hitLevel = "TestHitLevel",
            framesStartup = "TestFrames",
            framesOnBlock = "TestFrames",
            notes = " "
        };
    }
}