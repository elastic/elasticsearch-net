// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Reproduce
{
	public class GithubIssue2309
	{
		[U]
		public void FailedReIndexResponseMarkedAsInvalidAndContainFailures()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var json = @"{
				""took"": 4,
				""timed_out"": false,
				""total"": 1,
				""updated"": 0,
				""created"": 0,
				""deleted"": 0,
				""batches"": 1,
				""version_conflicts"": 0,
				""noops"": 0,
				""retries"": {
					""bulk"": 0,
					""search"": 0
				},
				""throttled_millis"": 0,
				""requests_per_second"": -1.0,
				""throttled_until_millis"": 0,
				""failures"": [{
					""index"": ""employees-v2"",
					""id"": ""57f7ce8df8a10336a0cf935b"",
					""cause"": {
						""type"": ""mapper_parsing_exception"",
						""reason"": ""failed to parse [id]"",
						""caused_by"": {
							""type"": ""number_format_exception"",
							""reason"": ""For input string: \""57f7ce8df8a10336a0cf935b\""""
						}
					},
					""status"": 400
				}]
			}";

			var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(json), 400);
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);

			var reindexResponse = client.ReindexOnServer(r => r
				.Source(s => s
					.Index("employees-v1")
				)
				.Destination(d => d
					.Index("employees-v2")
				)
				.Conflicts(Conflicts.Proceed)
			);

			reindexResponse.ShouldNotBeValid();
			reindexResponse.Failures.Should().NotBeNull().And.HaveCount(1);
		}
	}
}
