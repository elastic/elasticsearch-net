using System;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;

namespace Tests.Reproduce
{
	public class GithubIssue3164
	{
		[U]
		public void SerializerRespectsDateTimeValuesFromElasticsearch()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			// simulate a request rather than using the source serializer directly, so that
			// the SourceConverter is invoked.
			var json = @"{
				""_index"" : ""dates"",
				""_type"" : ""_doc"",
				""_id"" : ""1"",
				""_version"" : 1,
				""found"": true,
				""_source"" : {
					""DateTimeLocal"": ""2018-02-01T15:00:00+10:00"",
					""DateTimeUnspecified"": ""2018-06-01T15:00:00"",
					""DateTimeUtc"": ""2018-04-01T00:00:00Z"",
					""DateTimeOffset"": ""2018-08-01T15:00:00+10:00"",
					""DateTimeOffsetUtc"": ""2018-10-01T00:00:00+00:00""
				}
			}";

			var connectionSettings = new ConnectionSettings(
				pool,
				new InMemoryConnection(Encoding.UTF8.GetBytes(json)),
				(builtin, values) => new CustomSerializer(builtin, values));

			var client = new ElasticClient(connectionSettings);

			var getResponse = client.Get<Dates>(1, g => g.Index("dates").Type("_doc"));
			var dates = getResponse.Source;

			dates.DateTimeLocal.Kind.Should().Be(DateTimeKind.Local);
			dates.DateTimeUnspecified.Kind.Should().Be(DateTimeKind.Unspecified);
			dates.DateTimeUtc.Kind.Should().Be(DateTimeKind.Utc);
			dates.DateTimeOffset.Offset.Should().Be(TimeSpan.FromHours(10));
			dates.DateTimeOffsetUtc.Offset.Should().Be(TimeSpan.Zero);
		}

		public sealed class CustomSerializer : ConnectionSettingsAwareSerializerBase
		{
			public CustomSerializer(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
				: base(builtinSerializer, connectionSettings)
			{
			}

			protected override JsonSerializerSettings CreateJsonSerializerSettings()
			{
				return new JsonSerializerSettings
				{
					DateFormatHandling = DateFormatHandling.IsoDateFormat,
					DateParseHandling = DateParseHandling.DateTimeOffset,
					DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
					Formatting = Formatting.Indented
				};
			}
		}

		private class Dates
		{
			public DateTime DateTimeLocal { get; set; }
			public DateTime DateTimeUnspecified { get; set; }
			public DateTime DateTimeUtc { get; set; }
			public DateTimeOffset DateTimeOffset { get; set; }
			public DateTimeOffset DateTimeOffsetUtc { get; set; }
		}
	}
}
