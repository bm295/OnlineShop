namespace OnlineShop.Tests;

public class ModernizationTests
{
    [Fact]
    public void WeatherForecastModel_ComputesFahrenheit()
    {
        var forecast = new WebAppAngular.WeatherForecast { TemperatureC = 0 };

        Assert.Equal(32, forecast.TemperatureF);
    }
}
