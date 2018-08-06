using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkInvalidVersionApiTests : ApiIntegrationTestBase<WritableCluster, IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		public BulkInvalidVersionApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Bulk(f),
			fluentAsync: (client, f) => client.BulkAsync(f),
			request: (client, r) => client.Bulk(r),
			requestAsync: (client, r) => client.BulkAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 400;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new object[]
		{
			new Dictionary<string, object>{ { "index", new { _type="doc", _id = Project.Instance.Name, routing = Project.Instance.Name } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>{ { "index", new { _type="doc", _id = Project.Instance.Name, routing = Project.Instance.Name, version = 0 } } },
			Project.InstanceAnonymous,
		};

		protected override void ExpectResponse(IBulkResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Type.Should().Be("action_request_validation_exception");
			response.ServerError.Error.Reason.Should().EndWith("illegal version value [0] for version type [INTERNAL];");
		}

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Index<Project>(i => i.Document(Project.Instance))
			.Index<Project>(i => i.Version(0).Document(Project.Instance));


		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = new List<IBulkOperation>
			{
				new BulkIndexOperation<Project>(Project.Instance),
				new BulkIndexOperation<Project>(Project.Instance) { Version = 0 }
			}
		};
	}
}
