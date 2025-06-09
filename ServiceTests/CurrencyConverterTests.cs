using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Models;                  // for Price.ISOCurrency
using ServiceImplementations;        // for CurrencyConverter

namespace ServiceTests;

[TestClass]
public class CurrencyConverterTests
{
    private CurrencyConverter _converter;

    [TestInitialize]
    public void Setup()
    {
        _converter = new CurrencyConverter();
    }

    [TestMethod]
    public void Convert_USD_To_DKK_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(10m, Price.ISOCurrency.USD);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.DKK);

        // Assert
        Assert.AreEqual(65.74m, result.Amount); // 10 * 6.57385
        Assert.AreEqual(Price.ISOCurrency.DKK, result.Currency);
    }

    [TestMethod]
    public void Convert_DKK_To_USD_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(65.74m, Price.ISOCurrency.DKK);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.USD);

        // Assert
        Assert.AreEqual(10.00m, result.Amount); // 65.74 / 6.57385
        Assert.AreEqual(Price.ISOCurrency.USD, result.Currency);
    }

    [TestMethod]
    public void Convert_EUR_To_USD_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(5m, Price.ISOCurrency.EUR);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.USD);

        // Assert
        Assert.AreEqual(5.64m, result.Amount); // 5 * 1.127774
        Assert.AreEqual(Price.ISOCurrency.USD, result.Currency);
    }

    [TestMethod]
    public void Convert_USD_To_EUR_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(5m, Price.ISOCurrency.USD);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.EUR);

        // Assert
        Assert.AreEqual(4.43m, result.Amount); // 5 / 1.127774
        Assert.AreEqual(Price.ISOCurrency.EUR, result.Currency);
    }

    [TestMethod]
    public void Convert_GBP_To_EUR_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(10m, Price.ISOCurrency.GBP);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.EUR);

        // Assert
        Assert.AreEqual(11.85m, result.Amount); // 10 * 1.184998
        Assert.AreEqual(Price.ISOCurrency.EUR, result.Currency);
    }

    [TestMethod]
    public void Convert_JPY_To_EUR_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(1000m, Price.ISOCurrency.JPY);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.EUR);

        // Assert
        Assert.AreEqual(6.12m, result.Amount); // 1000 * 0.006118
        Assert.AreEqual(Price.ISOCurrency.EUR, result.Currency);
    }

    [TestMethod]
    public void Convert_SameCurrency_ShouldReturnSameAmount()
    {
        // Arrange
        Price price = new Price(123.45m, Price.ISOCurrency.EUR);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.EUR);

        // Assert
        Assert.AreEqual(123.45m, result.Amount);
        Assert.AreEqual(Price.ISOCurrency.EUR, result.Currency);
    }

    [TestMethod]
    public void Convert_GBP_To_JPY_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(10m, Price.ISOCurrency.GBP);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.JPY);

        // Assert
        Assert.AreEqual(1934.70m, result.Amount);
        // GBP -> EUR (1.184998), EUR -> JPY (1 / 0.006118) ? 163.44 
        // => 10 * 1.184998 * 163.44 = 1934.70
        Assert.AreEqual(Price.ISOCurrency.JPY, result.Currency);
    }

    [TestMethod]
    public void Convert_EUR_To_DKK_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(5m, Price.ISOCurrency.EUR);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.DKK);

        // Assert
        Assert.AreEqual(37.15m, result.Amount); // 5 * 7.430
        Assert.AreEqual(Price.ISOCurrency.DKK, result.Currency);
    }

    [TestMethod]
    public void Convert_GBP_To_DKK_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(10m, Price.ISOCurrency.GBP);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.DKK);

        // Assert
        Assert.AreEqual(88.12m, result.Amount); // 10 * 8.812
        Assert.AreEqual(Price.ISOCurrency.DKK, result.Currency);
    }

    [TestMethod]
    public void Convert_DKK_To_JPY_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(100m, Price.ISOCurrency.DKK);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.JPY);

        // Assert
        Assert.AreEqual(2197.80m, result.Amount);
        // DKK -> EUR: 100 / 7.43 = 13.46
        // EUR -> JPY: 13.46 / 0.006118 = 2197.80
        Assert.AreEqual(Price.ISOCurrency.JPY, result.Currency);
    }

    [TestMethod]
    public void Convert_USD_To_JPY_ShouldBeCorrect()
    {
        // Arrange
        Price price = new Price(10m, Price.ISOCurrency.USD);

        // Act
        var result = _converter.Convert(price, Price.ISOCurrency.JPY);

        // Assert
        Assert.AreEqual(1449.66m, result.Amount);
        // USD -> EUR: 10 / 1.127774 ? 8.87
        // EUR -> JPY: 8.87 / 0.006118 ? 1440.40
        Assert.AreEqual(Price.ISOCurrency.JPY, result.Currency);
    }
}
