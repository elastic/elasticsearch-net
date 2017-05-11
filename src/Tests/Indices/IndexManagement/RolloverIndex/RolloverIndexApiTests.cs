using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Indices.IndexManagement.RolloverIndex
{
	public class RolloverIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, IRolloverIndexResponse, IRolloverIndexRequest, RolloverIndexDescriptor, RolloverIndexRequest>
	{
		public RolloverIndexApiTests(WritableCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool SupportsDeserialization => false;

		protected override void OnBeforeCall(IElasticClient client)
		{
			var create = client.CreateIndex(CallIsolatedValue, c => c
				.Aliases(a => a
					.Alias(CallIsolatedValue + "-alias")
				)
			);
			create.ShouldBeValid();
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RolloverIndex(CallIsolatedValue + "-alias", f),
			fluentAsync: (client, f) => client.RolloverIndexAsync(CallIsolatedValue + "-alias", f),
			request: (client, r) => client.RolloverIndex(r),
			requestAsync: (client, r) => client.RolloverIndexAsync(r)
		);

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/{CallIsolatedValue}-alias/_rollover/{CallIsolatedValue}-new";

		protected override RolloverIndexDescriptor NewDescriptor() => new RolloverIndexDescriptor(CallIsolatedValue + "-alias");

		protected override object ExpectJson => new
		{
			conditions = new
			{
				max_age = "7d",
				max_docs = 1000
			}
		};

		protected override RolloverIndexRequest Initializer => new RolloverIndexRequest(CallIsolatedValue + "-alias", CallIsolatedValue + "-new")
		{
			Conditions = new RolloverConditions
			{
				MaxAge = "7d",
				MaxDocs = 1000
			}
		};

		protected override Func<RolloverIndexDescriptor, IRolloverIndexRequest> Fluent => f => f
			.NewIndex(CallIsolatedValue + "-new")
			.Conditions(c => c
				.MaxAge("7d")
				.MaxDocs(1000)
			);

		protected override void ExpectResponse(IRolloverIndexResponse response)
		{
			response.ShouldBeValid();
			response.OldIndex.Should().NotBeNullOrEmpty();
			response.NewIndex.Should().NotBeNullOrEmpty();
			response.RolledOver.Should().BeFalse();
			response.ShardsAcknowledged.Should().BeFalse();
			response.Conditions.Should().NotBeNull().And.HaveCount(2);
			response.Conditions["[max_age: 7d]"].Should().BeFalse();
			response.Conditions["[max_docs: 1000]"].Should().BeFalse();
		}
	}
}
