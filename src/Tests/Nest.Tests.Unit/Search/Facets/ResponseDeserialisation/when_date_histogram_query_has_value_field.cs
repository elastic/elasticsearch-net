using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Moq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Facets.ResponseDeserialisation
{
	[TestFixture]
	public class when_date_histogram_query_has_value_field
	{
		[Test]
		public void should_de_serialise_date_entry_histogram()
		{
			var widget1Histogram = new[]
			{
				new DateEntry
				{
					Count = 5181,
					Max = 7.9899997711181641,
					Mean = 7.9899997711181641,
					Min = 7.9899997711181641,
					Time = new DateTime(2012, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc),
					Total = 41396.18881416321,
					TotalCount = 5181
				},
				new DateEntry
				{
					Count = 5509,
					Max = 7.9899997711181641,
					Mean = 7.9899997711181641,
					Min = 7.9899997711181641,
					Time = new DateTime(2012, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc),
					Total = 44016.908739089966,
					TotalCount = 5509
				}
			};

			var widget2Histogram = new[]
			{
				new DateEntry
				{
					Count = 173,
					Max = 7.989999771118164,
					Mean = 7.9899997711181641,
					Min = 7.9899997711181641,
					Time = new DateTime(2012, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc),
					Total = 1382.2699604034424,
					TotalCount = 173
				},
				new DateEntry
				{
					Count = 162,
					Max = 7.989999771118164,
					Mean = 7.989999771118164,
					Min = 7.989999771118164,
					Time = new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc),
					Total = 1294.3799629211426,
					TotalCount = 162
				}
			};

			const string mockJsonResponse =
				@"{""took"":378,""timed_out"":false,""_shards"":{""total"":4,""successful"":4,""failed"":0},""hits"":{""total"":3700979,""max_score"":1,""hits"":[]},""facets"":{""widget_1:histogram"":{""_type"":""date_histogram"",""entries"":[{""time"":1351728000000,""count"":5181,""min"":7.989999771118164,""max"":7.989999771118164,""total"":41396.18881416321,""total_count"":5181,""mean"":7.989999771118164},{""time"":1354320000000,""count"":5509,""min"":7.989999771118164,""max"":7.989999771118164,""total"":44016.908739089966,""total_count"":5509,""mean"":7.989999771118164}]},""widget_2:histogram"":{""_type"":""date_histogram"",""entries"":[{""time"":1330560000000,""count"":173,""min"":7.989999771118164,""max"":7.989999771118164,""total"":1382.2699604034424,""total_count"":173,""mean"":7.989999771118164},{""time"":1333238400000,""count"":162,""min"":7.989999771118164,""max"":7.989999771118164,""total"":1294.3799629211426,""total_count"":162,""mean"":7.989999771118164}]},""widget_1:terms"":{""_type"":""terms"",""missing"":0,""total"":14797,""other"":0,""terms"":[{""term"":""widget 1"",""count"":14797}]},""widget_2:terms"":{""_type"":""terms"",""missing"":0,""total"":2002,""other"":0,""terms"":[{""term"":""widget 2"",""count"":2002}]}}}";

			var connectionMockery = new Mock<IConnection>();
			var connectionSettings = new ConnectionSettings(Test.Default.Uri, "index");

			connectionMockery
				.Setup(status => status.PostSync("index/_search", It.IsAny<byte[]>()))
				.Returns(new ElasticsearchResponse(connectionSettings, mockJsonResponse));

			var client = new ElasticClient(connectionSettings, connectionMockery.Object);

			var response = client.Search<dynamic>(descriptor => descriptor.Index("index").AllTypes());

			Assert.That(response.FacetItems<DateEntry>("widget_1:histogram"), DateEntriesConstraint.Sequence(widget1Histogram));
			Assert.That(response.FacetItems<DateEntry>("widget_2:histogram"), DateEntriesConstraint.Sequence(widget2Histogram));
		}
	}
}