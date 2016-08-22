using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasManagement.GetAliasesPointingToIndex
{
	public class GetAliasesPointingToIndexTests : SimpleIntegration, IClusterFixture<WritableCluster>
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

		[I]
		public void ShouldGetAliasesPointingToIndex()
		{
			var aliasesPointingToIndex = _client.GetAliasesPointingToIndex(Index);

			aliasesPointingToIndex.Should().NotBeEmpty().And.HaveCount(3);
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == Alias(1)).Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == Alias(2)).Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == Alias(3)).Should().NotBeNull();
		}

		[I]
		public async Task ShouldGetAliasesPointingToIndexAsync()
		{
			var aliasesPointingToIndex = await _client.GetAliasesPointingToIndexAsync(Index);

			aliasesPointingToIndex.Should().NotBeEmpty().And.HaveCount(3);
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == Alias(1)).Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == Alias(2)).Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == Alias(3)).Should().NotBeNull();
		}
	}
}
