using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.CrossClusterReplication.Follow.CreateFollowIndex
{
	public class CreateFollowIndexApiTests : ApiTestBase<XPackCluster, ICreateFollowIndexResponse, ICreateFollowIndexRequest, CreateFollowIndexDescriptor, CreateFollowIndexRequest>
	{
		public CreateFollowIndexApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			leader_index = "leader",
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
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/x/_ccr/follow";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CreateFollowIndex("x", f),
			(client, f) => client.CreateFollowIndexAsync("x", f),
			(client, r) => client.CreateFollowIndex(r),
			(client, r) => client.CreateFollowIndexAsync(r)
		);
	}
}
