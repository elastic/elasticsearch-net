using System;
using Moq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Facets.ResponseDeserialisation
{
	[TestFixture]
	public class when_date_histogram_query_has_no_value_field
	{
		[Test]
		public void should_de_serialise_date_entry_histogram()
		{
			var widget1Histogram = new[]
			{
				new DateEntry
				{
					Count = 5181,
					Time = new DateTime(2012, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc)
				},
				new DateEntry
				{
					Count = 5509,
					Time = new DateTime(2012, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				}
			};

			var widget2Histogram = new[]
			{
				new DateEntry
				{
					Count = 173,
					Time = new DateTime(2012, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				},
				new DateEntry
				{
					Count = 162,
					Time = new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				}
			};

			const string mockJsonResponse =
				@"{""took"":378,""timed_out"":false,""_shards"":{""total"":4,""successful"":4,""failed"":0},""hits"":{""total"":3700979,""max_score"":1,""hits"":[]},""facets"":{""widget_1:histogram"":{""_type"":""date_histogram"",""entries"":[{""time"":1351728000000,""count"":5181},{""time"":1354320000000,""count"":5509}]},""widget_2:histogram"":{""_type"":""date_histogram"",""entries"":[{""time"":1330560000000,""count"":173},{""time"":1333238400000,""count"":162}]},""widget_1:terms"":{""_type"":""terms"",""missing"":0,""total"":14797,""other"":0,""terms"":[{""term"":""widget 1"",""count"":14797}]},""widget_2:terms"":{""_type"":""terms"",""missing"":0,""total"":2002,""other"":0,""terms"":[{""term"":""widget 2"",""count"":2002}]}}}";

			var connectionSettings = new ConnectionSettings(Test.Default.Uri, "index");
			var connectionMockery = new Mock<IConnection>();

			connectionMockery
				.Setup(status => status.PostSync("index/_search", "{}"))
				.Returns(new ConnectionStatus(connectionSettings, mockJsonResponse));

			var client = new ElasticClient(connectionSettings, connectionMockery.Object);

			var response = client.Search<dynamic>(descriptor => descriptor.Index("index").AllTypes());

			Assert.That(response.FacetItems<DateEntry>("widget_1:histogram"), DateEntriesConstraint.Sequence(widget1Histogram));
			Assert.That(response.FacetItems<DateEntry>("widget_2:histogram"), DateEntriesConstraint.Sequence(widget2Histogram));
		}
	}
}