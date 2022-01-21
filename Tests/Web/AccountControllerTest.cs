using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Service.Enums;
using Service.Interfaces;
using Web.Controllers;
using Web.Inputs;
using Xunit;

namespace Tests.Web;

public class AccountControllerTest
{
    [Fact]
    public async Task Get_ShouldReturn_OkResult()
    {
        // Arrange
        IEnumerable<Account> expected = new List<Account>();
        
        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);
        
        mock.Setup(mock => mock.Account
            .AllAsync(null, null, It.IsAny<Func<IQueryable<Account>, IIncludableQueryable<Account, object>>?>(), 0, int.MaxValue, Track.NoTracking))
            .Returns(Task.FromResult(expected));

        // Act
        var response = await controller.Get();
        var actual = (response.Result as ObjectResult)?.Value;

        // Assert
        Assert.IsAssignableFrom<OkObjectResult>(response.Result);
        mock.Verify(mock => mock.Account.AllAsync(null, null, It.IsAny<Func<IQueryable<Account>, IIncludableQueryable<Account, object>>?>(), 0, int.MaxValue, Track.NoTracking), Times.Once);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetById_ShouldReturn_OkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expected = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");
        
        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.NoTracking)).Returns(Task.FromResult(expected)!);

        // Act
        var response = await controller.GetById(id);
        var actual = (response.Result as ObjectResult)?.Value;

        // Assert
        Assert.IsAssignableFrom<OkObjectResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.NoTracking), Times.Once);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetById_ShouldReturn_NotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        Account? expected = null;

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.NoTracking)).Returns(Task.FromResult(expected));        

        // Act
        var response = await controller.GetById(id);
        var actual = (response.Result as ObjectResult)?.Value;

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.NoTracking), Times.Once);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Add_ShouldReturn_CreatedResult()
    {
        // Arrange
        var input = new AccountInput();

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.Add(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Add(input);

        // Assert
        Assert.IsAssignableFrom<CreatedAtActionResult>(response.Result);
        mock.Verify(mock => mock.Account.Add(It.IsAny<Account>()), Times.Once);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Add_ShouldReturn_BadRequestResult()
    {
        // Arrange
        var input = new AccountInput();

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.Add(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(0));

        // Act
        var response = await controller.Add(input);

        // Assert
        Assert.IsAssignableFrom<BadRequestResult>(response.Result);
        mock.Verify(mock => mock.Account.Add(It.IsAny<Account>()), Times.Once);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldReturn_OkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var input = new AccountInput();
        var expected = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking)).Returns(Task.FromResult(expected)!);
        mock.Setup(mock => mock.Account.Update(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Update(id, input);
        var actual = (response.Result as ObjectResult)?.Value;

        // Assert
        Assert.IsAssignableFrom<OkObjectResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking), Times.Once);
        mock.Verify(mock => mock.Account.Update(It.IsAny<Account>()), Times.Once);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Update_ShouldReturn_NotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var input = new AccountInput();
        Account? account = null;

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking)).Returns(Task.FromResult(account));
        mock.Setup(mock => mock.Account.Update(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Update(id, input);

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking), Times.Once);
        mock.Verify(mock => mock.Account.Update(It.IsAny<Account>()), Times.Never);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Update_ShouldReturn_BadRequestResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var input = new AccountInput();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, default, default)).Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Update(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(0));

        // Act
        var response = await controller.Update(id, input);

        // Assert
        Assert.IsAssignableFrom<BadRequestResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking), Times.Once);
        mock.Verify(mock => mock.Account.Update(It.IsAny<Account>()), Times.Once);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Remove_ShouldReturn_OkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, default, default)).Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Remove(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Remove(id);

        // Assert
        Assert.IsAssignableFrom<NoContentResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking), Times.Once);
        mock.Verify(mock => mock.Account.Remove(It.IsAny<Account>()), Times.Once);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Remove_ShouldReturn_NotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        Account? account = null;

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, default, default)).Returns(Task.FromResult(account));
        mock.Setup(mock => mock.Account.Remove(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Remove(id);

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking), Times.Once);
        mock.Verify(mock => mock.Account.Remove(It.IsAny<Account>()), Times.Never);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Remove_ShouldReturn_BadRequestResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.SingleAsync(account => account.AccountId == id, default, default)).Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Remove(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(0));

        // Act
        var response = await controller.Remove(id);

        // Assert
        Assert.IsAssignableFrom<BadRequestResult>(response.Result);
        mock.Verify(mock => mock.Account.SingleAsync(account => account.AccountId == id, null, Track.Tracking), Times.Once);
        mock.Verify(mock => mock.Account.Remove(It.IsAny<Account>()), Times.Once);
        mock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
    }
}