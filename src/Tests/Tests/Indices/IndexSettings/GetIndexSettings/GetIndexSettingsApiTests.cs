using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.GetIndexSettings
{
	public class GetIndexSettingsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetIndexSettingsResponse, IGetIndexSettingsRequest, GetIndexSettingsDescriptor,
			GetIndexSettingsRequest>
	{
		public GetIndexSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> Fluent => d => d
			.Index<PercolatedQuery>()
			.Name("index.*")
			.Local();

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetIndexSettingsRequest Initializer => new GetIndexSettingsRequest(Infer.Index<PercolatedQuery>(), "index.*")
		{
			Local = true
		};

		protected override string UrlPath => $"/queries/_settings/index.%2A?local=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetIndexSettings(f),
			(client, f) => client.GetIndexSettingsAsync(f),
			(client, r) => client.GetIndexSettings(r),
			(client, r) => client.GetIndexSettingsAsync(r)
		);

		protected override void ExpectResponse(IGetIndexSettingsResponse response)
		{
			response.Indices.Should().NotBeEmpty();
			var index = response.Indices.First().Value;
			index.Should().NotBeNull();
			index.Settings.NumberOfShards.Should().HaveValue().And.BeGreaterThan(0);
			index.Settings.NumberOfReplicas.Should().HaveValue();
			index.Settings.AutoExpandReplicas.Should().NotBeNull();
			index.Settings.AutoExpandReplicas.MinReplicas.Should().Be(0);
			index.Settings.AutoExpandReplicas.MaxReplicas.Match(
				i => { Assert.True(false, "expecting a string"); },
				s => s.Should().Be("all"));
			index.Settings.AutoExpandReplicas.ToString().Should().Be("0-all");
		}
	}
}
