using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize
{

	public class TestDate
	{
		public DateTime DateTime { get; set; }

	}

	[TestFixture]
	public class ConnectionSettingsTests
	{
		public class Flight
		{
			public string Destination { get; set; }
			public DateTime DepartureDate { get; set; }
			public DateTime DepartureDateUtc { get; set; }
			public DateTime DepartureDateLocal { get; set; }
			public TimeSpan Duration { get; set; }
		}

		private string SerializeUsing(Flight flight, DateTimeZoneHandling handling)
		{
			var settings = new ConnectionSettings()
				.SetDefaultPropertyNameInferrer(p=>p)
				.SetJsonSerializerSettingsModifier(s =>
				{
					s.DateFormatHandling = DateFormatHandling.IsoDateFormat;
					s.DateTimeZoneHandling = handling;
				});
			var client = new ElasticClient(settings);
			return client.Serializer.Serialize(flight).Utf8String();
		}

		[Test]
		public void Example()
		{
			var flight = new Flight
			{
				Destination = "Dubai",
				DepartureDate = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Unspecified),
				DepartureDateUtc = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Utc),
				DepartureDateLocal = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Local),
				Duration = TimeSpan.FromHours(5.5)
			};

			//These tests are based on the Json.NET unit tests for DateTimeZoneHandling
			//https://github.com/JamesNK/Newtonsoft.Json/blob/bcd6982419c6165ed2c606eb9994c1aa6bce3735/Src/Newtonsoft.Json.Tests/Documentation/Samples/Serializer/SerializeDateTimeZoneHandling.cs
		
			var jsonWithRoundtripTimeZone = this.SerializeUsing(flight, DateTimeZoneHandling.RoundtripKind);
			var expected = @" {
			   ""Destination"": ""Dubai"",
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00+01:00"",
			   ""Duration"": ""05:30:00""
			}";
			jsonWithRoundtripTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithRoundtripTimeZone);
			
			var jsonWithUtcTimeZone = this.SerializeUsing(flight, DateTimeZoneHandling.Utc);
			expected = @" {
			   ""Destination"": ""Dubai"",
			   ""DepartureDate"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00Z"",
			   ""DepartureDateLocal"": ""2013-01-20T23:00:00Z"",
			   ""Duration"": ""05:30:00""
			}";
			jsonWithUtcTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUtcTimeZone);
			
			var jsonWithUnspecifiedTimeZone = this.SerializeUsing(flight, DateTimeZoneHandling.Unspecified);
			expected = @" {
			   ""Destination"": ""Dubai"",
			   ""DepartureDate"": ""2013-01-21T00:00:00"",
			   ""DepartureDateUtc"": ""2013-01-21T00:00:00"",
			   ""DepartureDateLocal"": ""2013-01-21T00:00:00"",
			   ""Duration"": ""05:30:00""
			 }";
			jsonWithUnspecifiedTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithUnspecifiedTimeZone);
			
			var jsonWithLocalTimeZone = this.SerializeUsing(flight, DateTimeZoneHandling.Local);
			expected = @"
			{
			  ""Destination"": ""Dubai"",
			  ""DepartureDate"": ""2013-01-21T00:00:00+01:00"",
			  ""DepartureDateUtc"": ""2013-01-21T01:00:00+01:00"",
			  ""DepartureDateLocal"": ""2013-01-21T00:00:00+01:00"",
			  ""Duration"": ""05:30:00""
			}";
			jsonWithLocalTimeZone.JsonEquals(expected).Should().BeTrue("{0}", jsonWithLocalTimeZone);

			

			
		}



	}
}
