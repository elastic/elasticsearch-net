// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.CrossClusterReplication.Follow.ResumeFollowIndex
{
	public class ResumeFollowIndexApiTests : ApiTestBase<XPackCluster, ResumeFollowIndexResponse, IResumeFollowIndexRequest, ResumeFollowIndexDescriptor, ResumeFollowIndexRequest>
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
			(client, f) => client.CrossClusterReplication.ResumeFollowIndex("x", f),
			(client, f) => client.CrossClusterReplication.ResumeFollowIndexAsync("x", f),
			(client, r) => client.CrossClusterReplication.ResumeFollowIndex(r),
			(client, r) => client.CrossClusterReplication.ResumeFollowIndexAsync(r)
		);
	}
}
