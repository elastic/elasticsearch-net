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
	[Collection(IntegrationContext.OwnIndex)]
	public class GetAliasesPointingToIndexTests
	{
		private readonly OwnIndexCluster _cluster;
		private readonly IElasticClient _client;
		private static readonly string Index = "aliases-index";

		public GetAliasesPointingToIndexTests(OwnIndexCluster cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client();

			if (_client.IndexExists(Index).Exists)
				_client.DeleteIndex(Index);

			var createResponse = _client.CreateIndex(Index, c => c
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
				)
				.Aliases(a => a
					.Alias("alias-1")
					.Alias("alias-2")
					.Alias("alias-3")
				)
			);

			if (!createResponse.IsValid)
				throw new Exception("problem creating index for integration test");
		}

		[I]
		public void ShouldGetAliasesPointingToIndex()
		{
			var aliasesPointingToIndex = _client.GetAliasesPointingToIndex(Index);

			aliasesPointingToIndex.Should().NotBeEmpty().And.HaveCount(3);
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == "alias-1").Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == "alias-2").Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == "alias-3").Should().NotBeNull();
		}

		[I]
		public async Task ShouldGetAliasesPointingToIndexAsync()
		{
			var aliasesPointingToIndex = await _client.GetAliasesPointingToIndexAsync(Index);

			aliasesPointingToIndex.Should().NotBeEmpty().And.HaveCount(3);
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == "alias-1").Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == "alias-2").Should().NotBeNull();
			aliasesPointingToIndex.FirstOrDefault(a => a.Name == "alias-3").Should().NotBeNull();
		}
	}
}
