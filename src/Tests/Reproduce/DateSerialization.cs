using System;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class DateSerialization
	{
		[U]
		public void ShouldRoundtripDateTimeAndDateTimeOffsetWithSameKindAndOffset()
		{
			var dates = new Dates
			{
				DateTimeUtcKind = new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc),
				DateTimeOffset = new DateTimeOffset(1999, 1, 1, 1, 1, 1, 1, TimeSpan.FromHours(5))
			};

			var client = new ElasticClient();
			var serializedDates = client.Serializer.SerializeToString(dates, SerializationFormatting.None);

			serializedDates.Should()
				.Be("{\"dateTimeUtcKind\":\"2016-01-01T01:01:01Z\",\"dateTimeOffset\":\"1999-01-01T01:01:01.001+05:00\"}");

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedDates)))
			{
				var deserializedDates = client.Serializer.Deserialize<Dates>(stream);

				deserializedDates.DateTimeUtcKind.Should().Be(dates.DateTimeUtcKind);
				deserializedDates.DateTimeUtcKind.Kind.Should().Be(dates.DateTimeUtcKind.Kind);

				deserializedDates.DateTimeOffset.Should().Be(dates.DateTimeOffset);
				deserializedDates.DateTimeOffset.Offset.Should().Be(dates.DateTimeOffset.Offset);
				deserializedDates.DateTimeOffset.Date.Kind.Should().Be(dates.DateTimeOffset.Date.Kind);
			}
		}

		private class Dates
		{
			public DateTime DateTimeUtcKind { get; set; }
			public DateTimeOffset DateTimeOffset { get; set; }
		}
	}
}
