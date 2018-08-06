using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.AliasManagement.GetAliasesPointingToIndex
{
	public class GetAliasesPointingToIndexTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;
		private readonly IElasticClient _client;
		private static string Unique = RandomString();
		private static readonly string Index = "aliases-index-" + Unique;
		private static string Alias(int alias) => "aliases-index-" + Unique + "-alias-" + alias;

		public GetAliasesPointingToIndexTests(WritableCluster cluster) : base(cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client;

			if (_client.IndexExists(Index).Exists) return;
			lock(Unique)
			{
				if (_client.IndexExists(Index).Exists) return;
				var createResponse = _client.CreateIndex(Index, c => c
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

		private static void AssertGetAliasesPointingToIndexResponse(IReadOnlyDictionary<string, AliasDefinition> aliasesPointingToIndex)
		{
			aliasesPointingToIndex.Should().NotBeEmpty().And.HaveCount(3)
				.And.ContainKey(Alias(1))
				.And.ContainKey(Alias(2))
				.And.ContainKey(Alias(3));

			aliasesPointingToIndex[Alias(1)].Should().NotBeNull();
			aliasesPointingToIndex[Alias(2)].Should().NotBeNull();
			aliasesPointingToIndex[Alias(3)].Should().NotBeNull();

		}

	}
}
