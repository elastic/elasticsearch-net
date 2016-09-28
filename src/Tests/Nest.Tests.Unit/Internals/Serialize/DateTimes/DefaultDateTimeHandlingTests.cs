using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize.DateTimes
{
	/// <summary>
	/// Test for the default DateTime and DateTimeOffset serialization within NEST
	/// </summary>
	[TestFixture]
	public class DefaultDateTimeHandlingTests : DateTimeHandlingTestsBase
	{
		[Test]
		public void Default()
		{
			var jsonWithRoundtripTimeZone = this.SerializeUsing();
			var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
			   ""DepartureDateUtcWithTicks"": ""2013-01-21T00:00:00.3456789Z"",
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
			   ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
			jsonWithRoundtripTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithRoundtripTimeZone);

			var flight = this.DeserializeUsing(jsonWithRoundtripTimeZone);

			flight.Should().Be(Flight);
			flight.DepartureDate.Kind.Should().Be(Flight.DepartureDate.Kind);
			flight.DepartureDateLocal.Kind.Should().Be(Flight.DepartureDateLocal.Kind);
			flight.DepartureDateUtc.Kind.Should().Be(Flight.DepartureDateUtc.Kind);
			flight.DepartureDateUtcWithTicks.Kind.Should().Be(Flight.DepartureDateUtcWithTicks.Kind);
			flight.DepartureDateOffset.Offset.Should().Be(Flight.DepartureDateOffset.Offset);
			flight.DepartureDateOffsetZero.Offset.Should().Be(Flight.DepartureDateOffsetZero.Offset);
			flight.DepartureDateOffsetNonLocal.Offset.Should().Be(Flight.DepartureDateOffsetNonLocal.Offset);
		}
	}
}