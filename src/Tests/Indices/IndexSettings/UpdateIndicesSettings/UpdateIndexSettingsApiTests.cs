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
using static Nest.Static;

namespace Tests.Indices.IndexSettings.UpdateIndicesSettings
{
	[Collection(IntegrationContext.Indexing)]
	public class UpdateIndexSettingsApiTests : ApiIntegrationTestBase<IAcknowledgedResponse, IUpdateIndexSettingsRequest, UpdateIndexSettingsDescriptor, UpdateIndexSettingsRequest>
	{
		public UpdateIndexSettingsApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateIndexSettings(AllIndices, f),
			fluentAsync: (client, f) => client.UpdateIndexSettingsAsync(AllIndices, f),
			request: (client, r) => client.UpdateIndexSettings(r),
			requestAsync: (client, r) => client.UpdateIndexSettingsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_settings";

		protected override object ExpectJson { get; } = new Dictionary<string, object>
		{
			{ "index.blocks.write", false }
		};

		protected override Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> Fluent => d => d
			.BlocksWrite(false);

		protected override UpdateIndexSettingsRequest Initializer => new UpdateIndexSettingsRequest
		{
			BlocksWrite = false
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}