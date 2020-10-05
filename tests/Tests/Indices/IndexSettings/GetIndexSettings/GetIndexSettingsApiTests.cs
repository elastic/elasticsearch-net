// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.IndexSettings.GetIndexSettings
{
	public class GetIndexSettingsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetIndexSettingsResponse, IGetIndexSettingsRequest, GetIndexSettingsDescriptor,
			GetIndexSettingsRequest>
	{
		private static readonly IndexName PercolationIndex = Index<ProjectPercolation>();

		public GetIndexSettingsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> Fluent => d => d
			.Name("index.*")
			.Local();


		protected override GetIndexSettingsRequest Initializer => new GetIndexSettingsRequest(PercolationIndex, "index.*")
		{
			Local = true
		};

		protected override string UrlPath => $"/queries/_settings/index.%2A?local=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetSettings(Index<ProjectPercolation>(), f),
			(client, f) => client.Indices.GetSettingsAsync(Index<ProjectPercolation>(), f),
			(client, r) => client.Indices.GetSettings(r),
			(client, r) => client.Indices.GetSettingsAsync(r)
		);

		protected override void ExpectResponse(GetIndexSettingsResponse response)
		{
			response.Indices.Should().NotBeEmpty();
			var index = response.Indices[PercolationIndex];
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
