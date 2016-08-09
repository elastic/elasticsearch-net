using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasManagement.GetIndicesPointingToAlias
{
	[Collection(IntegrationContext.OwnIndex)]
	public class GetIndicesPointingToAliasTests
	{
		private readonly OwnIndexCluster _cluster;
		private readonly IElasticClient _client;
		private static readonly string Alias = "alias";

		private static readonly string[] Indices = { "alias-index-1", "alias-index-2", "alias-index-3" };

		public GetIndicesPointingToAliasTests(OwnIndexCluster cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client();

			foreach (var index in Indices)
			{
				if (_client.IndexExists(index).Exists)
					_client.DeleteIndex(index);

				var createResponse = _client.CreateIndex(index, c => c
					.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
					)
					.Aliases(a => a
						.Alias(Alias)
					)
				);

				if (!createResponse.IsValid)
					throw new Exception("problem creating index for integration test");
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
