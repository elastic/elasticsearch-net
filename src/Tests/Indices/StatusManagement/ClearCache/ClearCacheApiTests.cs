using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Nest.Static;

namespace Tests.Indices.StatusManagement.ClearCache
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClearCacheApiTests : ApiIntegrationTestBase<IShardsOperationResponse, IClearCacheRequest, ClearCacheDescriptor, ClearCacheRequest>
	{
		public ClearCacheApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClearCache(AllIndices, f),
			fluentAsync: (client, f) => client.ClearCacheAsync(AllIndices, f),
			request: (client, r) => client.ClearCache(r),
			requestAsync: (client, r) => client.ClearCacheAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_cache/clear?recycler=true";

		protected override Func<ClearCacheDescriptor, IClearCacheRequest> Fluent => d => d.Recycler();

		protected override ClearCacheRequest Initializer => new ClearCacheRequest(AllIndices) { Recycler = true };

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
