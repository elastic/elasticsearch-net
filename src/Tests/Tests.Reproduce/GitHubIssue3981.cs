using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue3981
	{
		// This test always passes because the JsonPropertyAttribute on
		// the GeoLocation type are recognized by the JsonNetSerializer when used in tests, because the
		// IL rewriting to internalize Json.NET in the client happens *after* tests are run.
		[U]
		public void JsonNetSerializerSerializesGeoLocation()
		{
			var document = new Document
			{
				Location = new GeoLocation(45, 45)
			};

			var indexResponse = TestClient.InMemoryWithJsonNetSerializer.IndexDocument(document);
			Encoding.UTF8.GetString(indexResponse.ApiCall.RequestBodyInBytes).Should().Contain("\"lat\"").And.Contain("\"lon\"");
		}

		private class Document
		{
			public GeoLocation Location { get; set; }
		}
	}
}
