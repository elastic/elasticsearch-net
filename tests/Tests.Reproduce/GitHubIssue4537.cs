// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue4537
	{
		[U]
		public void CanDeserializeSnapshotShardFailure()
		{
			var json = @"{
						  ""snapshots"": [
						    {
						      ""snapshot"": ""snapshot_2020-03-31t00:02:18z"",
						      ""uuid"": ""P9oZzuEfS8qbT-FVZLFSgw"",
						      ""version_id"": 7040299,
						      ""version"": ""7.4.2"",
						      ""indices"": [ ""someIndices"" ],
						      ""include_global_state"": true,
						      ""state"": ""FAILED"",
						      ""reason"": ""Indices don't have primary shards [someIndex]"",
						      ""start_time"": ""2020-03-31T00:02:21.478Z"",
						      ""start_time_in_millis"": 1585612941478,
						      ""end_time"": ""2020-03-31T00:02:25.353Z"",
						      ""end_time_in_millis"": 1585612945353,
						      ""duration_in_millis"": 3875,
						      ""failures"": [
						        {
						          ""index"": ""someIndex"",
						          ""index_uuid"": ""someIndex"",
						          ""shard_id"": 1,
						          ""reason"": ""primary shard is not allocated"",
						          ""status"": ""INTERNAL_SERVER_ERROR""
						        },
						        {
						          ""index"": ""someIndex"",
						          ""index_uuid"": ""someIndex"",
						          ""shard_id"": 0,
						          ""reason"": ""primary shard is not allocated"",
						          ""status"": ""INTERNAL_SERVER_ERROR""
						        },
						        {
						          ""index"": ""someIndex"",
						          ""index_uuid"": ""someIndex"",
						          ""shard_id"": 2,
						          ""reason"": ""primary shard is not allocated"",
						          ""status"": ""INTERNAL_SERVER_ERROR""
						        }
						      ],
						      ""shards"": {
						        ""total"": 78,
						        ""failed"": 3,
						        ""successful"": 75
						      }
						    }
						  ]
						}";

			var client = TestClient.FixedInMemoryClient(Encoding.UTF8.GetBytes(json));

			Func<GetSnapshotResponse> action = () => client.Snapshot.Get("repo", "snapshot_2020-03-31t00:02:18z");

			action.Should().NotThrow();

			var response = action();

			var failures = response.Snapshots.First().Failures;
			failures.Should().HaveCount(3);
			failures.First().ShardId.Should().Be("1");
		}
	}
}
