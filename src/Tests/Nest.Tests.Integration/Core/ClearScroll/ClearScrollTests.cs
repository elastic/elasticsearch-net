using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Core.ClearScroll
{
	[TestFixture]
	public class ClearScrollTests : IntegrationTests
	{
		[Test]
		public void ClearScroll()
		{
			var searchResults = this.Client.Search<ElasticsearchProject>(s => s.Scroll("1m").SearchType(SearchTypeOptions.Scan));
			var validScrollId = searchResults.ScrollId;
			validScrollId.Should().NotBeNullOrWhiteSpace();

			var clearResponse = this.Client.ClearScroll(cs => cs.ScrollId(validScrollId));
			clearResponse.IsValid.Should().BeTrue();

			var bogusClearResponse = this.Client.ClearScroll(cs => cs.ScrollId("asdasdadasdasd"));
			bogusClearResponse.IsValid.Should().BeFalse();
		}

		
	}
}
