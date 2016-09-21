using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

using Elasticsearch.Net;
using Nest;
using FluentAssertions;

namespace Tests.Reproduce
{
	public class AggsDeserialization : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public AggsDeserialization(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
		}

		[I]
		public void EmptyBucketsThrowsNullReferenceException()
		{
			var client = _cluster.Client;

			var response = client.Search<Project>(s => s
				.SearchType(SearchType.Count)
				.Aggregations(a => a
					.Terms("names", ts => ts
						.Field(p => p.Name)
						.Size(0)
						.Order(TermsOrder.TermAscending)
						.Aggregations(aa => aa
							.Filters("filters", fs => fs
								.NamedFilters(nfs => nfs
									.Filter("foo", f => f
										.Term(t => t
											.Field(p => p.State)
											.Value(StateOfBeing.Stable)
										)
									)
								)
								.Aggregations(aaa => aaa
									.ValueCount("counts", vc => vc
										.Field(p => p.NumberOfCommits)
									)
									.BucketSelector("counts_bucket_filter", bs => bs
										.BucketsPath(bp => bp
											.Add("totalCounts", "counts")
										)
										.Script("totalCounts < 0") // intentionally filter all documents to cause empty buckets object
									)
								)
							)
						)
					)
				)
			);

			response.IsValid.Should().BeTrue();
		}
	}
}
