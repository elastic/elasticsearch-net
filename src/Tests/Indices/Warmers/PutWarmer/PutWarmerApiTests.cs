using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Indices.Warmers.PutWarmer
{
	[Collection(IntegrationContext.ReadOnly)]
	public class PutWarmerApiTests : ApiTestBase<IIndicesOperationResponse, IPutWarmerRequest, PutWarmerDescriptor, PutWarmerRequest>
	{
		public PutWarmerApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutWarmer(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutWarmerAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutWarmer(r),
			requestAsync: (client, r) => client.PutWarmerAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_warmer/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			query = new  { match_all = new { } }
		};

		protected override PutWarmerDescriptor NewDescriptor() => new PutWarmerDescriptor(CallIsolatedValue);

		protected override Func<PutWarmerDescriptor, IPutWarmerRequest> Fluent => d => d
			.Search<Project>(s=>s
				.Query(q=>q
					.MatchAll()
				)
			);

		protected override PutWarmerRequest Initializer => new PutWarmerRequest(CallIsolatedValue)
		{
			Search = new SearchRequest<Project>
			{
				Query = new MatchAllQuery()
			}
		};
	}
}
