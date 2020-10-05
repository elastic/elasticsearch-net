// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce {
	public class GitHubIssue4285
	{
		[U]
		public void CanReadAggBucketWithLongKey()
		{
			var json = @"{
				""took"" : 2612,
				""timed_out"" : false,
				""_shards"" : {
					""total"" : 3,
					""successful"" : 3,
					""skipped"" : 0,
					""failed"" : 0
				},
				""hits"" : {
					""total"" : {
						""value"" : 10000,
						""relation"" : ""gte""
					},
					""max_score"" : null,
					""hits"" : [ ]
				},
				""aggregations"" : {
					""terms#top_tags"" : {
						""buckets"" : [
						{
							""key"" : 3515753798950990007,
							""doc_count"" : 3
						}
						]
					}
				}
			}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes)).DefaultIndex("default_index");
			var client = new ElasticClient(connectionSettings);

			var response = client.Search<object>(s => s);

			var longTerms = response.Aggregations.Terms<long>("top_tags");
			longTerms.Buckets.Should().HaveCount(1);
			longTerms.Buckets.First().Key.Should().Be(3515753798950990007);

			var stringTerms = response.Aggregations.Terms("top_tags");
			stringTerms.Buckets.Should().HaveCount(1);
			stringTerms.Buckets.First().Key.Should().Be("3515753798950990007");
		}
	}
}
