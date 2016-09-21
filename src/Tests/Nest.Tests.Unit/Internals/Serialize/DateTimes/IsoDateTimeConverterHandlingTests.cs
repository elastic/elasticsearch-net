using System;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize.DateTimes
{
	/// <summary>
    /// Tests for DateTime zone serialization within NEST 
    /// with different <see cref="DateTimeZoneHandling"/> values
    /// </summary>
    [TestFixture]
    public class IsoDateTimeConverterHandlingTests : DateTimeHandlingTestsBase
	{
        [Test]
        public void RoundTripKind()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

            var jsonWithRoundtripTimeZone = this.SerializeUsing(dateTimeZoneHandling);
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

            var flight = this.DeserializeUsing(jsonWithRoundtripTimeZone, dateTimeZoneHandling);

            flight.Should().Be(Flight);
            flight.DepartureDate.Kind.Should().Be(Flight.DepartureDate.Kind);
            flight.DepartureDateLocal.Kind.Should().Be(Flight.DepartureDateLocal.Kind);
            flight.DepartureDateUtc.Kind.Should().Be(Flight.DepartureDateUtc.Kind);
            flight.DepartureDateOffset.Offset.Should().Be(Flight.DepartureDateOffset.Offset);
            flight.DepartureDateOffsetZero.Offset.Should().Be(Flight.DepartureDateOffsetZero.Offset);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(Flight.DepartureDateOffsetNonLocal.Offset);
        }

        [Test]
        public void Utc()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.Utc;
            var dateTimeKind = DateTimeKind.Utc;
            var departureDateLocalInUtc = TimeZoneInfo.ConvertTimeToUtc(Flight.DepartureDateLocal, TimeZoneInfo.Local);

            var jsonWithUtcTimeZone = this.SerializeUsing(dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
			   ""DepartureDateUtcWithTicks"": ""2013-01-21T00:00:00.3456789Z"",
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithUtcTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUtcTimeZone);

            var flight = this.DeserializeUsing(jsonWithUtcTimeZone, dateTimeZoneHandling);

            flight.DepartureDate.Should().Be(Flight.DepartureDate);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);

            // The deserialized local will be the UTC DateTime + the local timezone offset,
            // and with a DateTimeKind of UTC when deserialized.
            // 
            // Calling .ToLocalTime() will return DepartureDateLocal with correct
            // local datetime and DateTimeKind.Local
            flight.DepartureDateLocal.Should().Be(departureDateLocalInUtc);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateUtc.Should().Be(Flight.DepartureDateUtc);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateOffset.Should().Be(Flight.DepartureDateOffset);
            flight.DepartureDateOffset.Offset.Should().Be(Flight.DepartureDateOffset.Offset);

            flight.DepartureDateOffsetZero.Should().Be(Flight.DepartureDateOffsetZero);
            flight.DepartureDateOffsetZero.Offset.Should().Be(Flight.DepartureDateOffsetZero.Offset);

            flight.DepartureDateOffsetNonLocal.Should().Be(Flight.DepartureDateOffsetNonLocal);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(Flight.DepartureDateOffsetNonLocal.Offset);
        }

        [Test]
        public void Unspecified()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
            var dateTimeKind = DateTimeKind.Unspecified;

            var jsonWithUnspecifiedTimeZone = this.SerializeUsing(dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtcWithTicks"": ""2013-01-21T00:00:00.3456789"",
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			 }";
            jsonWithUnspecifiedTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUnspecifiedTimeZone);

            var flight = this.DeserializeUsing(jsonWithUnspecifiedTimeZone, dateTimeZoneHandling);

            flight.Should().Be(Flight);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateOffset.Offset.Should().Be(Flight.DepartureDateOffset.Offset);
            flight.DepartureDateOffsetZero.Offset.Should().Be(Flight.DepartureDateOffsetZero.Offset);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(Flight.DepartureDateOffsetNonLocal.Offset);
        }

        [Test]
        public void Local()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.Local;
            var dateTimeKind = DateTimeKind.Local;

            var jsonWithLocalTimeZone = this.SerializeUsing(dateTimeZoneHandling);
            var departureDateUtcInLocal = TimeZoneInfo.ConvertTimeFromUtc(Flight.DepartureDateUtc, TimeZoneInfo.Local);

            var expected = @"
			{
			  ""DepartureDate"": ""2013-01-21T00:00:00"",
			  ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			  ""DepartureDateLocal"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
			  ""DepartureDateUtcWithTicks"": ""2013-01-21T00:00:00.3456789Z"",
			  ""DepartureDateOffset"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
              ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
              ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithLocalTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithLocalTimeZone);

            var flight = this.DeserializeUsing(jsonWithLocalTimeZone, dateTimeZoneHandling);

            flight.DepartureDate.Should().Be(Flight.DepartureDate);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);


            flight.DepartureDateLocal.Should().Be(Flight.DepartureDateLocal);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            // The deserialized UTC will be the UTC DateTime + the local timezone offset
            // and a DateTimeKind of LOCAL when deserialized.
            //
            // Calling .ToUniversalTime() will return DepartureDateUtc with correct
            // UTC datetime and DateTimeKind.Utc
            flight.DepartureDateUtc.Should().Be(departureDateUtcInLocal);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateOffset.Should().Be(Flight.DepartureDateOffset);
            flight.DepartureDateOffset.Offset.Should().Be(Flight.DepartureDateOffset.Offset);

            flight.DepartureDateOffsetZero.Should().Be(Flight.DepartureDateOffsetZero);
            flight.DepartureDateOffsetZero.Offset.Should().Be(Flight.DepartureDateOffsetZero.Offset);

            flight.DepartureDateOffsetNonLocal.Should().Be(Flight.DepartureDateOffsetNonLocal);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(Flight.DepartureDateOffsetNonLocal.Offset);
        }
    }
}