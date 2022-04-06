using System;
using System.Collections.Generic;
using Booking.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Booking.Domain.Tests;

public class BookingEditTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly List<Entities.Booking> _exsistingBookings;

    public BookingEditTests()
    {
        _exsistingBookings = new List<Entities.Booking>(new[]
        {
            new Entities.Booking(Guid.NewGuid(), new DateTime(2022, 1, 1, 10, 0, 0), new DateTime(2022, 1, 1, 11, 0, 0)),
            new Entities.Booking(Guid.NewGuid(), new DateTime(2022, 1, 1, 14, 0, 0), new DateTime(2022, 1, 1, 15, 0, 0))
        });
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(foo => foo.GetOtherBookings(It.IsAny<Entities.Booking>())).Returns(_exsistingBookings);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped(_ => bookingDomainServiceMock.Object);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Theory]
    [InlineData("2022-1-1 08:00", "2022-1-1 09:00")]
    public void Given_No_Overlap_And_Edit__Then_Booking_Is_Created(string startStr, string slutStr)
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var slut = DateTime.Parse(slutStr);
        var sut = _exsistingBookings[0];
        sut.ServiceProvider = _serviceProvider;

        // Act
        sut.Update(start, slut, new byte[1]);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public void Given_Start_Is_Default_Value__Then_ArgumentOutOfRangeException_Is_Thrown()
    {
        // Arrange
        var expected = "Start dato skal være udfyldt (Parameter 'start')";
        var sut = _exsistingBookings[0];
        sut.ServiceProvider = _serviceProvider;

        // Act
        Action action = () => sut.Update(default, sut.Slut, new byte[1]);

        //Assert
        var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);
        Assert.Equal(expected, caughtException.Message);
    }

    [Fact]
    public void Given_Slut_Is_Default_Value__Then_ArgumentOutOfRangeException_Is_Thrown()
    {
        // Arrange
        var expected = "Slut dato skal være udfyldt (Parameter 'slut')";
        var sut = _exsistingBookings[0];
        sut.ServiceProvider = _serviceProvider;

        // Act
        Action action = () => sut.Update(sut.Start, default, new byte[1]);

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
        var sut = _exsistingBookings[0];
        sut.ServiceProvider = _serviceProvider;

        // Act
        Action action = () => sut.Update(start, slut, new byte[1]);

        //Assert
        var caughtException = Assert.Throws<Exception>(action);
        Assert.Equal(expected, caughtException.Message);
    }
}