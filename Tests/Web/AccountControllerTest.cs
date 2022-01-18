using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;
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
        IEnumerable<Account> accounts = new List<Account>();

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .AllAsync(default, default, default, default, default, default))
            .Returns(Task.FromResult(accounts));

        // Act
        var response = await controller.Get();

        // Assert
        Assert.IsAssignableFrom<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task GetById_ShouldReturn_OkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(account => account.Id == id, default, Track.NoTracking))
            .Returns(Task.FromResult(account)!);

        // Act
        var response = await controller.GetById(id);
        
        // Assert
        Assert.IsAssignableFrom<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task GetById_ShouldReturn_NotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        Account? account = null;

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, Track.NoTracking))
            .Returns(Task.FromResult(account));        

        // Act
        var response = await controller.GetById(id);

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Add_ShouldReturn_CreatedResult()
    {
        // Arrange
        var input = new AddAccountInput();

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.Add(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Add(input);

        // Assert
        Assert.IsAssignableFrom<CreatedAtActionResult>(response.Result);
    }

    [Fact]
    public async Task Add_ShouldReturn_BadRequestResult()
    {
        // Arrange
        var input = new AddAccountInput();

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account.Add(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(0));

        // Act
        var response = await controller.Add(input);

        // Assert
        Assert.IsAssignableFrom<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Update_ShouldReturn_OkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var input = new AddAccountInput();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, default))
            .Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Update(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Update(id, input);

        // Assert
        Assert.IsAssignableFrom<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Update_ShouldReturn_NotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var input = new AddAccountInput();
        Account? account = null;

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, default))
            .Returns(Task.FromResult(account));
        mock.Setup(mock => mock.Account.Update(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));
        // mock.Verify(mock => mock.Account.SingleAsync(x => x.Id == id, default, default), Times.Once);
        // mock.Verify(mock => mock.Account.Update(It.IsAny<Account>()), Times.Never);
        // mock.Verify(mock => mock.SaveChangesAsync(), Times.Never);

        // Act
        var response = await controller.Update(id, input);

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Update_ShouldReturn_BadRequestResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var input = new AddAccountInput();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, default))
            .Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Update(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(0));

        // Act
        var response = await controller.Update(id, input);

        // Assert
        Assert.IsAssignableFrom<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Remove_ShouldReturn_OkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, default))
            .Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Remove(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Remove(id);

        // Assert
        Assert.IsAssignableFrom<NoContentResult>(response.Result);
    }

    [Fact]
    public async Task Remove_ShouldReturn_NotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        Account? account = null;

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, default))
            .Returns(Task.FromResult(account));
        mock.Setup(mock => mock.Account.Remove(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(1));

        // Act
        var response = await controller.Remove(id);

        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Remove_ShouldReturn_BadRequestResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var account = new Account("john doe", "john@doe.com", "123456789", "http://john.com", 21, true, RelationshipStatus.Other, "some note");

        var mock = new Mock<IUnitOfWork>();
        var controller = new AccountController(mock.Object);

        mock.Setup(mock => mock.Account
            .SingleAsync(x => x.Id == id, default, default))
            .Returns(Task.FromResult(account)!);
        mock.Setup(mock => mock.Account.Remove(It.IsAny<Account>()));
        mock.Setup(mock => mock.SaveChangesAsync()).Returns(Task.FromResult(0));

        // Act
        var response = await controller.Remove(id);

        // Assert
        Assert.IsAssignableFrom<BadRequestResult>(response.Result);
    }
}