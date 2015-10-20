using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Indices.IndexSettings.GetIndexSettings
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetIndexSettingsApiTests : ApiIntegrationTestBase<IGetIndexSettingsResponse, IGetIndexSettingsRequest, GetIndexSettingsDescriptor, GetIndexSettingsRequest>
	{
		public GetIndexSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetIndexSettings(f),
			fluentAsync: (client, f) => client.GetIndexSettingsAsync(f),
			request: (client, r) => client.GetIndexSettings(r),
			requestAsync: (client, r) => client.GetIndexSettingsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/_settings/index.*?local=true";

		protected override Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> Fluent => d => d
			.Index<Project>()
			.Name("index.*")
			.Local();

		protected override GetIndexSettingsRequest Initializer => new GetIndexSettingsRequest(Static.Index<Project>(), "index.*")
		{
			Local = true
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}