using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Document.Multiple.Reindex;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexRethrottle
{
	[Collection(IntegrationContext.Reindex)]
	public class ReindexRethrottleApiTests
		: ApiIntegrationTestBase<IReindexRethrottleResponse, IReindexRethrottleRequest, ReindexRethrottleDescriptor, ReindexRethrottleRequest>
	{
		private readonly TaskId _taskId = "rhtoNesNR4aXVIY2bRR4GQ:13056";

		public ReindexRethrottleApiTests(ReindexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			base.OnBeforeCall(client);
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ReindexRethrottle(f),
			fluentAsync: (client, f) => client.ReindexRethrottleAsync(f),
			request: (client, r) => client.ReindexRethrottle(r),
			requestAsync: (client, r) => client.ReindexRethrottleAsync(r)
		);
		protected override void OnAfterCall(IElasticClient client) => client.Refresh(CallIsolatedValue);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;


		protected override string UrlPath => $"/_reindex/rhtoNesNR4aXVIY2bRR4GQ%3A13056/_rethrottle";

		protected override bool SupportsDeserialization => false;

		protected override Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> Fluent => d => d
			.TaskId(_taskId);

		protected override ReindexRethrottleRequest Initializer => new ReindexRethrottleRequest(_taskId);

		protected override void ExpectResponse(IReindexRethrottleResponse response)
		{
		}

		protected override object ExpectJson => null;
	}
}
