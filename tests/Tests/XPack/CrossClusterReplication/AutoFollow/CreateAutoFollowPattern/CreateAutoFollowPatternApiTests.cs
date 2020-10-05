// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
 using System.Collections.Generic;
 using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.CreateAutoFollowPattern
{
	public class CreateAutoFollowPatternApiTests : ApiTestBase<XPackCluster, CreateAutoFollowPatternResponse, ICreateAutoFollowPatternRequest, CreateAutoFollowPatternDescriptor, CreateAutoFollowPatternRequest>
	{
		public CreateAutoFollowPatternApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			follow_index_pattern = "y",
			leader_index_patterns = new [] { "z" },
			settings = new Dictionary<string, object>
			{
				["index.queries.cache.enabled"] = false
			},
			max_outstanding_read_requests = 100,
			max_outstanding_write_requests = 101,
			max_read_request_operation_count = 102,
			max_read_request_size = "2mb",
			max_retry_delay = "1m",
			max_write_buffer_count = 104,
			max_write_buffer_size = "1mb",
			max_write_request_operation_count = 103,
			max_write_request_size = "3mb",
			read_poll_timeout = "2m",
			remote_cluster = "x"
		};

		protected override CreateAutoFollowPatternDescriptor NewDescriptor() => new CreateAutoFollowPatternDescriptor("x");

		protected override Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> Fluent => d => d
			.RemoteCluster("x")
			.FollowIndexPattern("y")
			.LeaderIndexPatterns("z")
			.Settings(s => s
				.Queries(q => q
					.Cache(qc => qc
						.Enabled(false)
					)
				)
			)
			.MaxWriteBufferSize("1mb")
			.MaxOutstandingReadRequests(100)
			.MaxOutstandingWriteRequests( 101)
			.MaxReadRequestOperationCount(102)
			.MaxWriteRequestOperationCount(103)
			.MaxRetryDelay("1m")
			.MaxPollTimeout("2m")
			.MaxReadRequestSize("2mb")
			.MaxWriteBufferCount(104)
			.MaxWriteRequestSize("3mb")
		;

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateAutoFollowPatternRequest Initializer => new CreateAutoFollowPatternRequest("x")
		{
			RemoteCluster = "x",
			FollowIndexPattern = "y",
			LeaderIndexPatterns = new []{"z"},
			Settings = new IndexSettings
			{
				Queries = new QueriesSettings
				{
					Cache = new QueriesCacheSettings
					{
						Enabled = false
					}
				}
			},
			MaxWriteBufferSize = "1mb",
			MaxOutstandingReadRequests = 100,
			MaxOutstandingWriteRequests = 101,
			MaxReadRequestOperationCount = 102,
			MaxWriteRequestOperationCount = 103,
			MaxRetryDelay = "1m",
			MaxPollTimeout = "2m",
			MaxReadRequestSize = "2mb",
			MaxWriteBufferCount = 104,
			MaxWriteRequestSize = "3mb"

		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ccr/auto_follow/x";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CrossClusterReplication.CreateAutoFollowPattern("x", f),
			(client, f) => client.CrossClusterReplication.CreateAutoFollowPatternAsync("x", f),
			(client, r) => client.CrossClusterReplication.CreateAutoFollowPattern(r),
			(client, r) => client.CrossClusterReplication.CreateAutoFollowPatternAsync(r)
		);
	}
}
