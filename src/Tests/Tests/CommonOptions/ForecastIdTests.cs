using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.CommonOptions
{
	public class ForecastIdTests
	{
		[U] public void Equal()
		{
			var forecastIds1 = new ForecastIds("1,2,3");
			var forecastIds2 = new ForecastIds(new [] { "3", "2", "1" });

			forecastIds1.Should().Be(forecastIds2);
			forecastIds1.GetHashCode().Should().Be(forecastIds2.GetHashCode());
		}

		[U] public void NotEqual()
		{
			var forecastIds1 = new ForecastIds("1,2,3,3");
			var forecastIds2 = new ForecastIds(new [] { "3", "2", "1" });

			forecastIds1.Should().NotBe(forecastIds2);
			forecastIds1.GetHashCode().Should().NotBe(forecastIds2.GetHashCode());
		}
	}
}
