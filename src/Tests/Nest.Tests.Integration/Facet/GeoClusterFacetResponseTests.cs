using System.Linq;
using FluentAssertions;
using Nest.Domain.Facets;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Facet
{
	[TestFixture]
	public class GeoClusterFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleGeoClusterFacet()
		{
			var queryResults = this.SearchRaw<ElasticsearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""cluster"" :  {
							""geo_cluster"" : {
							""field"" : ""origin"",
							""factor"": 0.3
							} 
						}
					}
				}"
			);

			var facets = queryResults.FacetItems<GeoClusterItem>("cluster");
			this.TestDefaultAssertions(queryResults);
			var facet = facets.Last();
			facets.Count().Should().BeGreaterThan(0);
			Assert.Greater(facet.Count, 0.0);
			Assert.NotNull(facet.BottomRight);
			Assert.NotNull(facet.TopLeft);
			Assert.NotNull(facet.Center);
		}

	}
}
