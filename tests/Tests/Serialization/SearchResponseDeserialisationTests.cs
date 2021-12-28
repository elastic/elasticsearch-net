using System.Collections.Generic;
using Tests.Core.Extensions;

namespace Tests.Serialization;

public class SearchResponseDeserialisationTests : SerializerTestBase
{
	[U]
	public void Should_HandleTsdbSortRoundTrip()
	{
		// Sorts against a time series required changes to the sort field in the response to support `Map` as an acceptable instance.
		// This test validates that we can roundtrip such values using the current model and serialiser.
		// See https://github.com/elastic/elasticsearch/pull/81583

		var json = @"{""_index"":""test"",""_id"":""9fLsn30BuA_D-fdpROsd"",""_score"":null,""_source"":{""@timestamp"":
""2021-04-28T18:51:03.142Z"",""k8s"":{""pod"":{""uid"":""df3145b3-0563-4d3b-a0f7-897eb2876ea9"",""ip"":""10.10.55.3"",
""name"":""dog"",""network"":{""tx"":1434595272,""rx"":530605511}}},""metricset"":""pod""},""sort"":[{""k8s.pod.uid"":
""df3145b3-0563-4d3b-a0f7-897eb2876ea9"",""metricset"":""pod""},1619635863142]}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<Hit<Metric>>(stream);

		search.Sort.Count.Should().Be(2);

		var roundTripExample = new SearchExample { Sort = search.Sort };

		var serialisedJson = SerializeAndGetJsonString(roundTripExample);
		serialisedJson.Should().Be(@"{""sort"":[{""k8s.pod.uid"":""df3145b3-0563-4d3b-a0f7-897eb2876ea9"",""metricset"":""pod""},1619635863142]}");

		var objectExample = new SearchExample
		{
			Sort = new object[]
			{
				new Dictionary<string, string>
				{
					{ "k8s.pod.uid", "df3145b3-0563-4d3b-a0f7-897eb2876ea9" },
					{ "metricset", "pod" }
				},
				1619635863142
			}
		};

		serialisedJson = SerializeAndGetJsonString(objectExample);
		serialisedJson.Should().Be(@"{""sort"":[{""k8s.pod.uid"":""df3145b3-0563-4d3b-a0f7-897eb2876ea9"",""metricset"":""pod""},1619635863142]}");
	}

	private class Metric
	{
		public string MetricSet { get; set; }
	}

	private class SearchExample
	{
		public IReadOnlyCollection<object> Sort { get; init; }
	}
}
