using System;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.AliasManagement.GetIndicesPointingToAlias
{
	public class GetIndicesPointingToAliasTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;
		private readonly IElasticClient _client;
		private static string Unique = RandomString();

		private static readonly string Alias = "alias-" + Unique;

		private static readonly string[] Indices = {
			$"alias-index-{Unique}-1",
			$"alias-index-{Unique}-2",
			$"alias-index-{Unique}-3"
		};

		public GetIndicesPointingToAliasTests(WritableCluster cluster) : base(cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client;

			foreach (var index in Indices)
			{
				if (_client.IndexExists(index).Exists) continue;

				lock (Unique)
				{
					if (_client.IndexExists(index).Exists) continue;

					var createResponse = _client.CreateIndex(index, c => c
						.Settings(s => s
							.NumberOfShards(1)
							.NumberOfReplicas(0)
						)
						.Aliases(a => a
							.Alias(Alias)
						)
					);
					createResponse.ShouldBeValid();
				}
			}
		}

		[I]
		public void ShouldGetAliasesPointingToIndex()
		{
			var indicesPointingToAlias = _client.GetIndicesPointingToAlias(Alias);

			indicesPointingToAlias.Should().NotBeEmpty().And.HaveCount(3);
			indicesPointingToAlias.Should().Contain(Indices);
		}

		[I]
		public async Task ShouldGetAliasesPointingToIndexAsync()
		{
			var indicesPointingToAlias = await _client.GetIndicesPointingToAliasAsync(Alias);

			indicesPointingToAlias.Should().NotBeEmpty().And.HaveCount(3);
			indicesPointingToAlias.Should().Contain(Indices);
		}
	}
}
