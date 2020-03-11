using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue4487
	{
		[U]
		public void CanSerializeDistanceFeatureQueryWithoutBoost()
		{
			var searchRequest = new SearchRequest
			{
				Query = new DistanceFeatureQuery
				{
					Field = "foo",
					Origin = new GeoCoordinate(90, 90),
					Pivot = Distance.Meters(100)
				}
			};

			var json = TestClient.DefaultInMemoryClient.RequestResponseSerializer.SerializeToString(searchRequest);
			json.Should().Be(@"{""query"":{""distance_feature"":{""field"":""foo"",""origin"":[90.0,90.0],""pivot"":""100m""}}}");
		}
	}
}
