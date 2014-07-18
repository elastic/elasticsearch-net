using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Warmers
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class IndicesWarmersTests : IntegrationTests 
	{

		[Test]
		public void CreateIndexWithWarmer()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();

			var result = this.Client.CreateIndex(index, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddWarmer(wd => wd
					.Type<ElasticsearchProject>()
					.WarmerName("warmer_createindexwithwarmer")
					.Search<ElasticsearchProject>(s => s
						.Query(q => q
							.Term(p => p.Name, "strange-value")
						)
					)
				)
			);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.ConnectionStatus.Should().NotBeNull();

			var warmerResult = this.Client.GetWarmer("warmer_createindexwithwarmer", w => w
				.Name("warmer_createindexwithwarmer")
			);

			warmerResult.IsValid.Should().BeTrue();
			warmerResult.Indices.Should().NotBeNull();
			warmerResult.Indices.Should().ContainKey(index);
			warmerResult.Indices[index].Should().ContainKey("warmer_createindexwithwarmer");
			var warmerMapping = warmerResult.Indices[index]["warmer_createindexwithwarmer"];
			warmerMapping.Name.Should().Be("warmer_createindexwithwarmer");
			//warmerMapping.Source.Should().Contain("\"strange-value\"");

		}

	}
}
