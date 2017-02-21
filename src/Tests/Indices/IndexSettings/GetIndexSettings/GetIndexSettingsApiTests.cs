using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Indices.IndexSettings.GetIndexSettings
{
	public class GetIndexSettingsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetIndexSettingsResponse, IGetIndexSettingsRequest, GetIndexSettingsDescriptor, GetIndexSettingsRequest>
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
		protected override string UrlPath => $"/project/_settings/index.%2A?local=true";

		protected override Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> Fluent => d => d
			.Index<Project>()
			.Name("index.*")
			.Local();

		protected override GetIndexSettingsRequest Initializer => new GetIndexSettingsRequest(Infer.Index<Project>(), "index.*")
		{
			Local = true
		};

		protected override void ExpectResponse(IGetIndexSettingsResponse response)
		{
			response.Indices.Should().NotBeEmpty();
			var index = response.Indices.First().Value;
			index.Should().NotBeNull();
			index.Settings.NumberOfShards.Should().HaveValue().And.BeGreaterThan(0);
			index.Settings.NumberOfReplicas.Should().HaveValue();
		}
	}
}
