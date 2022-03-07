using System;
using System.Collections.Generic;
using Booking.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Booking.Domain.Tests;

public class BookingTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly List<Entities.Booking> _exsistingBookings;

    public BookingTests()
    {
        _exsistingBookings = new List<Entities.Booking>(new[]
        {
            new Entities.Booking(Guid.NewGuid(), new DateTime(2022, 1, 1, 10, 0, 0), new DateTime(2022, 1, 1, 11, 0, 0)),
            new Entities.Booking(Guid.NewGuid(), new DateTime(2022, 1, 1, 14, 0, 0), new DateTime(2022, 1, 1, 15, 0, 0))
        });
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(foo => foo.GetExsistingBookings()).Returns(_exsistingBookings);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped(sp => bookingDomainServiceMock.Object);
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

    [Theory]
    [InlineData("2022-1-1 08:00", "2022-1-1 09:00")]
    public void Given_No_Overlap__Then_Booking_Is_Created(string startStr, string slutStr)
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var slut = DateTime.Parse(slutStr);

        // Act
        var sut = new Entities.Booking(_serviceProvider, start, slut);

        //Assert
        Assert.NotNull(sut);
    }


    // Eksisterende booking: "2022-1-1 10:00" - "2022-1-1 11:00"
    [Theory]
    [InlineData("2022-1-1 08:00", "2022-1-1 10:30", "Slut rækker ind i eksisterende booking")]
    [InlineData("2022-1-1 08:00", "2022-1-1 10:00", "Slut berører eksisterende booking")]
    [InlineData("2022-1-1 10:30", "2022-1-1 12:00", "Start rækker ind i eksisterende booking")]
    [InlineData("2022-1-1 11:00", "2022-1-1 12:00", "Start berører eksisterende booking")]
    [InlineData("2022-1-1 10:30", "2022-1-1 10:45", "Ny boking ligger inde i eksisterende booking")]
    [InlineData("2022-1-1 09:00", "2022-1-1 12:00", "Eksisterende booking ligger inde i ny booking")]
    public void Given_Overlap__Then_Exception_Is_Thrown(string startStr, string slutStr, string userMessage)
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var slut = DateTime.Parse(slutStr);
        var expected = "Booking overlapper med eksisterende booking";

        // Act
        Action action = () => new Entities.Booking(_serviceProvider, start, slut);

        //Assert
        var caughtException = Assert.Throws<Exception>(action);
        Assert.True(expected.Equals(caughtException.Message), userMessage);
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
        sut.Update(start, slut);

        //Assert
        Assert.NotNull(sut);
    }

    [Theory]
    [InlineData("2022-1-1 13:39", "2022-1-1 14:30", "Slut rækker ind i eksisterende booking")]
    public void Given_Overlap_And_Edit__Then_Booking_Then_Exception_Is_Thrown(string startStr, string slutStr, string userMessage)
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var slut = DateTime.Parse(slutStr);
        var sut = _exsistingBookings[0];
        sut.ServiceProvider = _serviceProvider;
        var expected = "Booking overlapper med eksisterende booking";

        // Act
        Action action = () => sut.Update(start, slut);

        //Assert
        var caughtException = Assert.Throws<Exception>(action);
        Assert.True(expected.Equals(caughtException.Message), userMessage);
    }
}