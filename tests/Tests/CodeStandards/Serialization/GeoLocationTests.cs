using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.CodeStandards.Serialization
{
	public class GeoLocationTests
	{
		[U]
		public void CanDeserializeAndSerializeToWellKnownText()
		{
			var wkt = "{\"location\":\"POINT (-90 90)\"}";
			var client = TestClient.DisabledStreaming;

			Doc deserialized;
			using (var stream = ConnectionConfiguration.DefaultMemoryStreamFactory.Create(Encoding.UTF8.GetBytes(wkt)))
				deserialized = client.RequestResponseSerializer.Deserialize<Doc>(stream);

			deserialized.Location.Should().Be(new GeoLocation(90, -90));
			client.RequestResponseSerializer.SerializeToString(deserialized).Should().Be(wkt);
		}

		private class Doc
		{
			public GeoLocation Location { get; set; }
		}
	}
}
