// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.DocumentationTests;

namespace Tests.Indices.AliasManagement.GetAliasesPointingToIndex
{
	public class GetAliasesPointingToIndexTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		private static readonly string Unique = RandomString();
		private static readonly string Index = "aliases-index-" + Unique;

		private readonly IElasticClient _client;

		public GetAliasesPointingToIndexTests(WritableCluster cluster) : base(cluster)
		{
			_client = cluster.Client;

			if (_client.Indices.Exists(Index).Exists) return;

			lock (Unique)
			{
				if (_client.Indices.Exists(Index).Exists) return;

				var createResponse = _client.Indices.Create(Index, c => c
					.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
					)
					.Aliases(a => a
						.Alias(Alias(1))
						.Alias(Alias(2))
						.Alias(Alias(3))
					)
				);

				createResponse.ShouldBeValid();
			}
		}

		private static string Alias(int alias) => "aliases-index-" + Unique + "-alias-" + alias;

		[I] public void ShouldGetAliasesPointingToIndex()
		{
			var aliasesPointingToIndex = _client.GetAliasesPointingToIndex(Index);
			AssertGetAliasesPointingToIndexResponse(aliasesPointingToIndex);
		}

		[I] public void ShouldGetIndicesPointingToAlias()
		{
			var indices = _client.GetIndicesPointingToAlias(Alias(3));
			indices.Should().NotBeEmpty().And.Contain(Index);
		}

		[I] public async Task ShouldGetAliasesPointingToIndexAsync()
		{
			var aliasesPointingToIndex = await _client.GetAliasesPointingToIndexAsync(Index);
			AssertGetAliasesPointingToIndexResponse(aliasesPointingToIndex);
		}

		[I] public async Task ShouldGetIndicesPointingToAliasAsync()
		{
			var indices = await _client.GetIndicesPointingToAliasAsync(Alias(3));
			indices.Should().NotBeEmpty().And.Contain(Index);
		}
		[I] public async Task NotFoundAliasReturnEmpty()
		{
			var indices = await _client.GetIndicesPointingToAliasAsync(Alias(4));
			indices.Should().BeEmpty();
		}

		private static void AssertGetAliasesPointingToIndexResponse(IReadOnlyDictionary<string, AliasDefinition> aliasesPointingToIndex)
		{
			aliasesPointingToIndex.Should()
				.NotBeEmpty()
				.And.HaveCount(3)
				.And.ContainKey(Alias(1))
				.And.ContainKey(Alias(2))
				.And.ContainKey(Alias(3));

			aliasesPointingToIndex[Alias(1)].Should().NotBeNull();
			aliasesPointingToIndex[Alias(2)].Should().NotBeNull();
			aliasesPointingToIndex[Alias(3)].Should().NotBeNull();
		}
	}
}
