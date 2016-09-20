using System;
using FluentAssertions;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize.DateTimes
{
    /// <summary>
    /// Tests for Json.Net default DateTime zone serialization
    /// </summary>
    /// <remarks>
    /// These tests are based on the Json.NET unit tests for DateTimeZoneHandling
    /// http://www.newtonsoft.com/json/help/html/SerializeDateTimeZoneHandling.htm
    /// and
    /// https://github.com/JamesNK/Newtonsoft.Json/blob/bcd6982419c6165ed2c606eb9994c1aa6bce3735/Src/Newtonsoft.Json.Tests/Documentation/Samples/Serializer/SerializeDateTimeZoneHandling.cs
    /// </remarks>
    [TestFixture]
	public class JsonNetDefaultDateTimeHandlingTests : DateTimeHandlingTestsBase
	{
        [Test]
	    public void RoundTripKind()
	    {
	        var dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

	        var jsonWithRoundtripTimeZone = this.SerializeUsing(dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));
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

	        var flight = this.DeserializeUsing(jsonWithRoundtripTimeZone, dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));

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
            var depatureDateLocalString = departureDateLocalInUtc.ToString("yyyy-MM-ddTHH:mm:ssZ");

            var jsonWithUtcTimeZone = this.SerializeUsing(dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": """ + depatureDateLocalString + @""",
			   ""DepartureDateUtcWithTicks"": ""2013-01-21T00:00:00.3456789Z"",
               ""DepartureDateOffset"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithUtcTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUtcTimeZone);

            var flight = this.DeserializeUsing(jsonWithUtcTimeZone, dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));

            flight.DepartureDate.Should().Be(Flight.DepartureDate);	        
	        flight.DepartureDate.Kind.Should().Be(dateTimeKind);

            // The deserialized local will be the UTC DateTime + the local timezone offset,
            // AND with a DateTimeKind of UTC when deserialized.
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

            var jsonWithUnspecifiedTimeZone = this.SerializeUsing(dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));
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

            var flight = this.DeserializeUsing(jsonWithUnspecifiedTimeZone, dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));

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

	        var jsonWithLocalTimeZone = this.SerializeUsing(dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));
            var departureDateUtcInLocal = TimeZoneInfo.ConvertTimeFromUtc(Flight.DepartureDateUtc, TimeZoneInfo.Local);
            var depatureDateLocalString = departureDateUtcInLocal.ToString("yyyy-MM-ddTHH:mm:ss");

            var expected = @" {
			  ""DepartureDate"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
			  ""DepartureDateUtc"": """ + depatureDateLocalString + LocalOffsetString + @""",
			  ""DepartureDateLocal"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
			  ""DepartureDateUtcWithTicks"": """ + depatureDateLocalString + ".3456789" + LocalOffsetString + @""",
			  ""DepartureDateOffset"": ""2013-01-21T00:00:00" + LocalOffsetString + @""",
              ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
              ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithLocalTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithLocalTimeZone);

            var flight = DeserializeUsing(jsonWithLocalTimeZone, dateTimeZoneHandling, s => new DefaultDateTimeContractResolver(s));

            flight.DepartureDate.Should().Be(Flight.DepartureDate);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);

            // The deserialized local will be the local DateTime
            // and a DateTimeKind of Local when deserialized.
            flight.DepartureDateLocal.Should().Be(Flight.DepartureDateLocal);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            // The deserialized UTC will be the UTC DateTime + the local timezone offset
            // BUT with a DateTimeKind of LOCAL when deserialized.
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

		/// <summary>
		/// Contract resolver that removes the <see cref="IsoDateTimeConverter"/> for handling
		/// DateTime, falling back to Json.NET default DateTime serialization behaviour
		/// </summary>
        private class DefaultDateTimeContractResolver : ElasticContractResolver
        {
            public DefaultDateTimeContractResolver(IConnectionSettingsValues connectionSettings)
                : base(connectionSettings)
            {
            }

            protected override JsonContract CreateContract(Type objectType)
            {
                var contract = base.CreateContract(objectType);

                // remove the default IsoDateTimeConverter
                if (objectType == typeof(System.DateTime) || objectType == typeof(System.DateTime?))
                    contract.Converter = null;

                return contract;
            }
        }
    }
}
