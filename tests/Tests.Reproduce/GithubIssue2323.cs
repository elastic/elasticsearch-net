// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GithubIssue2323 : ClusterTestClassBase<ReadOnlyCluster>
	{
		public GithubIssue2323(ReadOnlyCluster cluster) : base(cluster) { }

		[I]
		public void NestedInnerHitsShouldIncludedNestedProperty()
		{
			var client = Client;
			var response = client.Search<Project>(s => s
				.Query(q => q
					.Nested(n => n
						.Path(p => p.Tags)
						.Query(nq => nq
							.MatchAll()
						)
						.InnerHits(i => i
							.Source(false)
						)
					)
				)
			);

			response.ShouldBeValid();

			var innerHits = response.Hits.Select(h => h.InnerHits).ToList();

			innerHits.Should().NotBeNullOrEmpty();

			var innerHit = innerHits.First();
			innerHit.Should().ContainKey("tags");
			var hitMetadata = innerHit["tags"].Hits.Hits.First();

			hitMetadata.Nested.Should().NotBeNull();
			hitMetadata.Nested.Field.Should().Be(new Field("tags"));
			hitMetadata.Nested.Offset.Should().BeGreaterOrEqualTo(0);
		}
	}
}
