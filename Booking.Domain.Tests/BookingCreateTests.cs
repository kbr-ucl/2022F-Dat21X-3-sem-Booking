using System;
using System.Collections.Generic;
using Booking.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Booking.Domain.Tests;

public class BookingCreateTests
{
    private readonly IServiceProvider _serviceProvider;

    public BookingCreateTests()
    {
        var exsistingBookings = new List<Entities.Booking>(new[]
        {
            new Entities.Booking(Guid.NewGuid(), new DateTime(2022, 1, 1, 10, 0, 0), new DateTime(2022, 1, 1, 11, 0, 0)),
            new Entities.Booking(Guid.NewGuid(), new DateTime(2022, 1, 1, 14, 0, 0), new DateTime(2022, 1, 1, 15, 0, 0))
        });
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(foo => foo.GetExsistingBookings()).Returns(exsistingBookings);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped(_ => bookingDomainServiceMock.Object);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public void Given_Start_Is_Default_Value__Then_ArgumentOutOfRangeException_Is_Thrown()
    {
        // Arrange
        var slut = DateTime.Parse("2022-1-1 09:00");
        var expected = "Start dato skal være udfyldt (Parameter 'start')";

        // Act
        Action action = () => new Entities.Booking(_serviceProvider, default, slut);

        //Assert
        var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);
        Assert.Equal(expected, caughtException.Message);
    }

    [Fact]
    public void Given_Slut_Is_Default_Value__Then_ArgumentOutOfRangeException_Is_Thrown()
    {
        // Arrange
        var start = DateTime.Parse("2022-1-1 08:00");
        var expected = "Slut dato skal være udfyldt (Parameter 'slut')";

        // Act
        Action action = () => new Entities.Booking(_serviceProvider, start, default);

        //Assert
        var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);
        Assert.Equal(expected, caughtException.Message);
    }

    [Theory]
    [InlineData("2022-1-1 08:00", "2022-1-1 07:59")]
    [InlineData("2022-1-1 08:00", "2022-1-1 08:00")]
    public void Given_Slut_Is_Not_After_Start__Then_Exception_Is_Thrown(string startStr, string slutStr)
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var slut = DateTime.Parse(slutStr);
        var expected = $"Slut dato/tid skal være senere end start (start, slut): {start}, {slut}";

        // Act
        Action action = () => new Entities.Booking(_serviceProvider, start, slut);

        //Assert
        var caughtException = Assert.Throws<Exception>(action);
        Assert.Equal(expected, caughtException.Message);
    }
}