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

namespace Tests.XPack.CrossClusterReplication.Follow.CreateFollowIndex
{
	public class CreateFollowIndexApiTests : ApiTestBase<XPackCluster, CreateFollowIndexResponse, ICreateFollowIndexRequest, CreateFollowIndexDescriptor, CreateFollowIndexRequest>
	{
		public CreateFollowIndexApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			leader_index = "leader",
			settings = new Dictionary<string, object>
			{
				["index.queries.cache.enabled"] = false
			},
			max_outstanding_read_requests = 100,
			max_outstanding_write_requests = 101,
			max_read_request_operation_count = 102,
			max_read_request_size = "4mb",
			max_retry_delay = "1m",
			max_write_buffer_count = 104,
			max_write_buffer_size = "1mb",
			max_write_request_operation_count = 103,
			max_write_request_size = "3mb",
			read_poll_timeout = "2m",
			remote_cluster = "x"
		};

		protected override CreateFollowIndexDescriptor NewDescriptor() => new CreateFollowIndexDescriptor("x");

		protected override Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> Fluent => d => d
			.RemoteCluster("x")
			.MaxWriteBufferSize("1mb")
			.MaxOutstandingReadRequests(100)
			.MaxOutstandingWriteRequests( 101)
			.MaxReadRequestOperationCount(102)
			.MaxWriteRequestOperationCount(103)
			.MaxRetryDelay("1m")
			.MaxWriteBufferCount(104)
			.MaxWriteRequestSize("3mb")
			.MaxRequestSize("4mb")
			.ReadPollTimeout("2m")
			.LeaderIndex("leader")
			.Settings(s => s
				.Queries(q => q
					.Cache(qc => qc
						.Enabled(false)
					)
				)
			)
		;

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateFollowIndexRequest Initializer => new CreateFollowIndexRequest("x")
		{
			RemoteCluster = "x",
			MaxWriteBufferSize = "1mb",
			MaxOutstandingReadRequests = 100,
			MaxOutstandingWriteRequests = 101,
			MaxReadRequestOperationCount = 102,
			MaxWriteRequestOperationCount = 103,
			MaxRetryDelay = "1m",
			MaxWriteBufferCount = 104,
			MaxWriteRequestSize = "3mb",
			MaxRequestSize = "4mb",
			ReadPollTimeout = "2m",
			LeaderIndex = "leader",
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
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/x/_ccr/follow";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CrossClusterReplication.CreateFollowIndex("x", f),
			(client, f) => client.CrossClusterReplication.CreateFollowIndexAsync("x", f),
			(client, r) => client.CrossClusterReplication.CreateFollowIndex(r),
			(client, r) => client.CrossClusterReplication.CreateFollowIndexAsync(r)
		);
	}
}
