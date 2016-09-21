using System;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize.DateTimes
{
	public abstract class DateTimeHandlingTestsBase
	{
		protected Flight Flight;
		protected string LocalOffsetString;
		protected TimeSpan LocalOffset;

		[SetUp]
		public void SetUp()
		{
			var departureDateLocal = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Local);
			LocalOffset = TimeZoneInfo.Local.GetUtcOffset(departureDateLocal);

			var departureDateUtc = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Utc);

			Flight = new Flight
			{
				DepartureDate = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Unspecified),
				DepartureDateUtc = departureDateUtc,
				DepartureDateLocal = departureDateLocal,
				DepartureDateUtcWithTicks = departureDateUtc.AddTicks(3456789),
				DepartureDateOffset = new DateTimeOffset(2013, 1, 21, 0, 0, 0, LocalOffset),
				DepartureDateOffsetZero = new DateTimeOffset(2013, 1, 21, 0, 0, 0, TimeSpan.Zero),
				DepartureDateOffsetNonLocal = new DateTimeOffset(2013, 1, 21, 0, 0, 0, TimeSpan.FromHours(-6.25)),
			};

			LocalOffsetString = string.Format("{0}:{1}",
				LocalOffset.Hours.ToString("+00;-00;"),
				LocalOffset.Minutes.ToString("00"));
		}

		protected string SerializeUsing(
			DateTimeZoneHandling? handling = null,
			Func<IConnectionSettingsValues, IContractResolver> contractResolver = null)
		{
			var settings = new ConnectionSettings();

			settings
				.SetDefaultPropertyNameInferrer(p => p)
				.SetJsonSerializerSettingsModifier(s =>
				{
					s.Formatting = Formatting.Indented;

					if (handling.HasValue)
						s.DateTimeZoneHandling = handling.Value;
					
					if (contractResolver != null)
						s.ContractResolver = contractResolver(settings);
				});

			var client = new ElasticClient(settings);
			return client.Serializer.Serialize(Flight).Utf8String();
		}

		protected Flight DeserializeUsing(
			string json, 
			DateTimeZoneHandling? handling = null, 
			Func<IConnectionSettingsValues, IContractResolver> contractResolver = null)
		{
			var settings = new ConnectionSettings();

			settings
				.SetDefaultPropertyNameInferrer(p => p)
				.SetJsonSerializerSettingsModifier(s =>
				{
					if (handling.HasValue)
						s.DateTimeZoneHandling = handling.Value;

					if (contractResolver != null)
						s.ContractResolver = contractResolver(settings);
				});

			var client = new ElasticClient(settings);
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				return client.Serializer.Deserialize<Flight>(stream);
			}
		}
	}
}