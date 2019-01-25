using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.CrossClusterReplication.Follow.ResumeFollowIndex
{
	public class ResumeFollowIndexApiTests : ApiTestBase<XPackCluster, IResumeFollowIndexResponse, IResumeFollowIndexRequest, ResumeFollowIndexDescriptor, ResumeFollowIndexRequest>
	{
		public ResumeFollowIndexApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
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
		};

		protected override ResumeFollowIndexDescriptor NewDescriptor() => new ResumeFollowIndexDescriptor("x");

		protected override Func<ResumeFollowIndexDescriptor, IResumeFollowIndexRequest> Fluent => d => d
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
		;

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ResumeFollowIndexRequest Initializer => new ResumeFollowIndexRequest("x")
		{
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
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/x/_ccr/resume_follow";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ResumeFollowIndex("x", f),
			(client, f) => client.ResumeFollowIndexAsync("x", f),
			(client, r) => client.ResumeFollowIndex(r),
			(client, r) => client.ResumeFollowIndexAsync(r)
		);
	}
}
