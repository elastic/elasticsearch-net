using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize
{
    /// <summary>
    /// Tests for Json.Net default DateTime zone serialization
    /// </summary>
    /// <remarks>
    /// These tests are based on the Json.NET unit tests for DateTimeZoneHandling
    /// http://www.newtonsoft.com/json/help/html/SerializeDateTimeZoneHandling.htm
    /// 
    /// and
    /// 
    /// https://github.com/JamesNK/Newtonsoft.Json/blob/bcd6982419c6165ed2c606eb9994c1aa6bce3735/Src/Newtonsoft.Json.Tests/Documentation/Samples/Serializer/SerializeDateTimeZoneHandling.cs
    /// </remarks>
    [TestFixture]
	public class DateTimeZoneHandlingTests
	{
        private Flight _flight;
	    private string _offset;
	    private TimeSpan _timeSpanOffset;

	    [SetUp]
	    public void SetUp()
	    {
            var departureDateLocal = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Local);
            _timeSpanOffset = TimeZoneInfo.Local.GetUtcOffset(departureDateLocal);

            _flight = new Flight
            {
                DepartureDate = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Unspecified),
                DepartureDateUtc = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Utc),
                DepartureDateLocal = departureDateLocal,
                DepartureDateOffset = new DateTimeOffset(2013, 1, 21, 0, 0, 0, _timeSpanOffset),
                DepartureDateOffsetZero = new DateTimeOffset(2013, 1, 21, 0, 0, 0, TimeSpan.Zero),
            };

            _offset = string.Format("{0}:{1}",
                _timeSpanOffset.Hours.ToString("+00;-00;"),
                _timeSpanOffset.Minutes.ToString("00"));
        }

        /// <remarks>
        /// Timezone offset serialized is based on DateTimeKind
        /// Unspecified = None
        /// Utc = UTC Timezone identifier
        /// Local = Local Timezone offset
        /// </remarks>
        [Test]
	    public void RoundTripKind()
	    {
	        var dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

	        var jsonWithRoundtripTimeZone = this.SerializeUsing(_flight, dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00" + _offset + @""",
               ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
			}";
            jsonWithRoundtripTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithRoundtripTimeZone);

	        var flight = this.DeserializeUsing(jsonWithRoundtripTimeZone, dateTimeZoneHandling);

	        flight.Should().Be(_flight);
	        flight.DepartureDate.Kind.Should().Be(_flight.DepartureDate.Kind);
	        flight.DepartureDateLocal.Kind.Should().Be(_flight.DepartureDateLocal.Kind);
	        flight.DepartureDateUtc.Kind.Should().Be(_flight.DepartureDateUtc.Kind);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);
        }

        /// <remarks>
        /// Serialization
        /// -------------
        /// Unspecified = Serialized as is with UTC offset
        /// UTC = Serialized as is with UTC Offset
        /// Local = Converted to UniversalTime and serialized
        /// </remarks>
        [Test]
	    public void Utc()
	    {
	        var dateTimeZoneHandling = DateTimeZoneHandling.Utc;
	        var dateTimeKind = DateTimeKind.Utc;

	        var departureDateLocal = _flight.DepartureDateUtc.Subtract(_timeSpanOffset);
	        var depatureDateLocalString = departureDateLocal.ToString("yyyy-MM-ddTHH:mm:ssZ");

            var jsonWithUtcTimeZone = this.SerializeUsing(_flight, dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": """ + depatureDateLocalString + @""",
               ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
			}";
            jsonWithUtcTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUtcTimeZone);

            var flight = this.DeserializeUsing(jsonWithUtcTimeZone, dateTimeZoneHandling);

            flight.DepartureDate.Should().Be(_flight.DepartureDate);	        
	        flight.DepartureDate.Kind.Should().Be(dateTimeKind);

            // The deserialized local will be the UTC DateTime + the local timezone offset,
            // AND with a DateTimeKind of UTC when deserialized.
            flight.DepartureDateLocal.Should().Be(departureDateLocal);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateUtc.Should().Be(_flight.DepartureDateUtc);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateOffset.Should().Be(_flight.DepartureDateOffset);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);

            flight.DepartureDateOffsetZero.Should().Be(_flight.DepartureDateOffsetZero);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);
        }
    
        /// <remarks>
        /// No Timezone offset is serialized
        /// </remarks>
        [Test]
	    public void Unspecified()
	    {
	        var dateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
            var dateTimeKind = DateTimeKind.Unspecified;

            var jsonWithUnspecifiedTimeZone = this.SerializeUsing(_flight, dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00"",
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
			 }";
            jsonWithUnspecifiedTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUnspecifiedTimeZone);

            var flight = this.DeserializeUsing(jsonWithUnspecifiedTimeZone, dateTimeZoneHandling);

            flight.Should().Be(_flight);	        
	        flight.DepartureDate.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);
        }

        /// <remarks>
        /// Utc DateTime is converted to Local time
        /// Unspecified DateTime is assumed to be local and saved as-is with local offset
        /// Local DateTime is saved as is with local offset
        /// </remarks>
        [Test]
	    public void Local()
	    {
	        var dateTimeZoneHandling = DateTimeZoneHandling.Local;
	        var dateTimeKind = DateTimeKind.Local;

	        var jsonWithLocalTimeZone = this.SerializeUsing(_flight, dateTimeZoneHandling);
	        var departureDateLocal = _flight.DepartureDateUtc.Add(_timeSpanOffset);
	        var depatureDateLocalString = departureDateLocal.ToString("yyyy-MM-ddTHH:mm:ss");

            var expected = @"
			{
			  ""DepartureDate"": ""2013-01-21T00:00:00" + _offset + @""",
			  ""DepartureDateUtc"": """ + depatureDateLocalString + _offset + @""",
			  ""DepartureDateLocal"": ""2013-01-21T00:00:00" + _offset + @""",
			  ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
              ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
			}";
            jsonWithLocalTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithLocalTimeZone);

            var flight = this.DeserializeUsing(jsonWithLocalTimeZone, dateTimeZoneHandling);

            flight.DepartureDate.Should().Be(_flight.DepartureDate);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);

            // The deserialized local will be the local DateTime
            // and a DateTimeKind of Local when deserialized.
            flight.DepartureDateLocal.Should().Be(_flight.DepartureDateLocal);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            // The deserialized UTC will be the UTC DateTime + the local timezone offset
            // BUT with a DateTimeKind of LOCAL when deserialized.
            //
            // Calling .ToUniversalTime() will return DepartureDateUtc with correct
            // UTC datetime and DateTimeKind.Utc
            flight.DepartureDateUtc.Should().Be(departureDateLocal);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateOffset.Should().Be(_flight.DepartureDateOffset);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);

            flight.DepartureDateOffsetZero.Should().Be(_flight.DepartureDateOffsetZero);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);
        }

        private string SerializeUsing(Flight flight, DateTimeZoneHandling handling)
        {
            var settings = new ConnectionSettings();

            settings
                .SetDefaultPropertyNameInferrer(p => p)
                .SetJsonSerializerSettingsModifier(s =>
                {
                    s.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    s.DateTimeZoneHandling = handling;
                    s.Formatting = Formatting.Indented;
                    s.ContractResolver = new DefaultDateTimeContractResolver(settings);
                });
            var client = new ElasticClient(settings);
            return client.Serializer.Serialize(flight).Utf8String();
        }

        private Flight DeserializeUsing(string json, DateTimeZoneHandling handling)
        {
            var settings = new ConnectionSettings()
                .SetDefaultPropertyNameInferrer(p => p)
                .SetJsonSerializerSettingsModifier(s =>
                {
                    s.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    s.DateTimeZoneHandling = handling;
                });
            var client = new ElasticClient(settings);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return client.Serializer.Deserialize<Flight>(stream);
            }
        }

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
                if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
                    contract.Converter = null;

                return contract;
            }
        }
    }
}
