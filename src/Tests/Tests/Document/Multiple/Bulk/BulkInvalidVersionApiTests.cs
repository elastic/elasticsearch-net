using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkInvalidVersionApiTests : ApiIntegrationTestBase<WritableCluster, IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		public BulkInvalidVersionApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson { get; } = new object[]
		{
			new Dictionary<string, object> { { "index", new { _id = Project.Instance.Name, routing = Project.Instance.Name } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>
				{ { "index", new { _id = Project.Instance.Name, routing = Project.Instance.Name, version = 0 } } },
			Project.InstanceAnonymous,
		};

		protected override int ExpectStatusCode => 400;

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Index<Project>(i => i.Document(Project.Instance))
			.Index<Project>(i => i.Version(0).Document(Project.Instance));

		protected override HttpMethod HttpMethod => HttpMethod.POST;


		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = new List<IBulkOperation>
			{
				new BulkIndexOperation<Project>(Project.Instance),
				new BulkIndexOperation<Project>(Project.Instance) { Version = 0 }
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Bulk(f),
			(client, f) => client.BulkAsync(f),
			(client, r) => client.Bulk(r),
			(client, r) => client.BulkAsync(r)
		);

		protected override void ExpectResponse(IBulkResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Type.Should().Be("action_request_validation_exception");
			response.ServerError.Error.Reason.Should().EndWith("illegal version value [0] for version type [INTERNAL];");
		}
	}
}
