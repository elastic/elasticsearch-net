// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport.Extensions;
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
