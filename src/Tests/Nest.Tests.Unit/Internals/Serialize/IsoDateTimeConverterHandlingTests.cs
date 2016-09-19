using System;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize
{
    /// <summary>
    /// Tests for default DateTime zone serialization within NEST
    /// </summary>
    [TestFixture]
    public class IsoDateTimeConverterHandlingTests
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
                DepartureDateOffsetNonLocal = new DateTimeOffset(2013, 1, 21, 0, 0, 0, TimeSpan.FromHours(-6.25)),
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
        /// Offset = Timezone offset specified 
        /// </remarks>
        [Test]
        public void RoundTripKind()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

            var jsonWithRoundtripTimeZone = this.SerializeUsing(dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00" + _offset + @""",
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
			   ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithRoundtripTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithRoundtripTimeZone);

            var flight = this.DeserializeUsing(jsonWithRoundtripTimeZone, dateTimeZoneHandling);

            flight.Should().Be(_flight);
            flight.DepartureDate.Kind.Should().Be(_flight.DepartureDate.Kind);
            flight.DepartureDateLocal.Kind.Should().Be(_flight.DepartureDateLocal.Kind);
            flight.DepartureDateUtc.Kind.Should().Be(_flight.DepartureDateUtc.Kind);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(_flight.DepartureDateOffsetNonLocal.Offset);
        }

        /// <remarks>
        /// Unspecified = Serialized as is with UTC offset
        /// UTC = Serialized as is with UTC Offset
        /// Local = Serialied as is with the local offset
        /// Offset = Serialized as is with specified offset
        /// </remarks>
        [Test]
        public void Utc()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.Utc;
            var dateTimeKind = DateTimeKind.Utc;
            var departureDateLocalInUtc = TimeZoneInfo.ConvertTimeToUtc(_flight.DepartureDateLocal, TimeZoneInfo.Local);

            var jsonWithUtcTimeZone = this.SerializeUsing(dateTimeZoneHandling);
            var expected = @" {
			   ""DepartureDate"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00" + _offset + @""",
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithUtcTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUtcTimeZone);

            var flight = this.DeserializeUsing(jsonWithUtcTimeZone, dateTimeZoneHandling);

            flight.DepartureDate.Should().Be(_flight.DepartureDate);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);

            // The deserialized local will be the UTC DateTime + the local timezone offset,
            // and with a DateTimeKind of UTC when deserialized.
            // 
            // Calling .ToLocalTime() will return DepartureDateLocal with correct
            // local datetime and DateTimeKind.Local
            flight.DepartureDateLocal.Should().Be(departureDateLocalInUtc);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateUtc.Should().Be(_flight.DepartureDateUtc);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateOffset.Should().Be(_flight.DepartureDateOffset);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);

            flight.DepartureDateOffsetZero.Should().Be(_flight.DepartureDateOffsetZero);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);

            flight.DepartureDateOffsetNonLocal.Should().Be(_flight.DepartureDateOffsetNonLocal);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(_flight.DepartureDateOffsetNonLocal.Offset);
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
			   ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
               ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
               ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			 }";
            jsonWithUnspecifiedTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUnspecifiedTimeZone);

            var flight = this.DeserializeUsing(jsonWithUnspecifiedTimeZone, dateTimeZoneHandling);

            flight.Should().Be(_flight);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(_flight.DepartureDateOffsetNonLocal.Offset);
        }

        [Test]
        public void Local()
        {
            var dateTimeZoneHandling = DateTimeZoneHandling.Local;
            var dateTimeKind = DateTimeKind.Local;

            var jsonWithLocalTimeZone = this.SerializeUsing(dateTimeZoneHandling);
            var departureDateUtcInLocal = TimeZoneInfo.ConvertTimeFromUtc(_flight.DepartureDateUtc, TimeZoneInfo.Local);

            var expected = @"
			{
			  ""DepartureDate"": ""2013-01-21T00:00:00"",
			  ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			  ""DepartureDateLocal"": ""2013-01-21T00:00:00" + _offset + @""",
			  ""DepartureDateOffset"": ""2013-01-21T00:00:00" + _offset + @""",
              ""DepartureDateOffsetZero"": ""2013-01-21T00:00:00+00:00"",
              ""DepartureDateOffsetNonLocal"": ""2013-01-21T00:00:00-06:15""
			}";
            jsonWithLocalTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithLocalTimeZone);

            var flight = this.DeserializeUsing(jsonWithLocalTimeZone, dateTimeZoneHandling);

            flight.DepartureDate.Should().Be(_flight.DepartureDate);
            flight.DepartureDate.Kind.Should().Be(dateTimeKind);


            flight.DepartureDateLocal.Should().Be(_flight.DepartureDateLocal);
            flight.DepartureDateLocal.Kind.Should().Be(dateTimeKind);

            // The deserialized UTC will be the UTC DateTime + the local timezone offset
            // and a DateTimeKind of LOCAL when deserialized.
            //
            // Calling .ToUniversalTime() will return DepartureDateUtc with correct
            // UTC datetime and DateTimeKind.Utc
            flight.DepartureDateUtc.Should().Be(departureDateUtcInLocal);
            flight.DepartureDateUtc.Kind.Should().Be(dateTimeKind);

            flight.DepartureDateOffset.Should().Be(_flight.DepartureDateOffset);
            flight.DepartureDateOffset.Offset.Should().Be(_flight.DepartureDateOffset.Offset);

            flight.DepartureDateOffsetZero.Should().Be(_flight.DepartureDateOffsetZero);
            flight.DepartureDateOffsetZero.Offset.Should().Be(_flight.DepartureDateOffsetZero.Offset);

            flight.DepartureDateOffsetNonLocal.Should().Be(_flight.DepartureDateOffsetNonLocal);
            flight.DepartureDateOffsetNonLocal.Offset.Should().Be(_flight.DepartureDateOffsetNonLocal.Offset);
        }

        private string SerializeUsing(DateTimeZoneHandling handling)
        {
            var settings = new ConnectionSettings()
                .SetDefaultPropertyNameInferrer(p => p)
                .SetJsonSerializerSettingsModifier(s =>
                {
                    s.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    s.DateTimeZoneHandling = handling;
                    s.Formatting = Formatting.Indented;
                });

            var client = new ElasticClient(settings);
            return client.Serializer.Serialize(_flight).Utf8String();
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
    }
}