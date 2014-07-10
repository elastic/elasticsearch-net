using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Core.ClearScroll
{
	[TestFixture]
	public class ClearScrollTests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.0.2", "Scroll ids were not accepted in the request body until 1.0.3 (https://github.com/elasticsearch/elasticsearch/issues/5726)")]
		[SkipVersion("1.1.0", "Scroll ids in request body broken in 1.1.0 and fixed in 1.1.1")]
		public void ClearScroll()
		{
			var searchResults = this._client.Search<ElasticsearchProject>(s => s.Scroll("1m").SearchType(SearchType.Scan));
			var validScrollId = searchResults.ScrollId;
			validScrollId.Should().NotBeNullOrWhiteSpace();

			var clearResponse = this._client.ClearScroll(cs => cs.ScrollId(validScrollId));
			clearResponse.IsValid.Should().BeTrue();

			var bogusClearResponse = this._client.ClearScroll(cs => cs.ScrollId("asdasdadasdasd"));
			bogusClearResponse.IsValid.Should().BeFalse();
		}

		
	}
}
