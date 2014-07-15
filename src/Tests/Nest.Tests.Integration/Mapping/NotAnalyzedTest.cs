using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class NotAnalyzedTests : IntegrationTests
	{
		[Test]
		public void NotAnalyzedReturnsOneItem()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this.Client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(m=>m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var typeMappingResponse = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(index).Type("elasticsearchprojects"));
			var typeMapping = typeMappingResponse.Mapping;
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			Assert.AreEqual(FieldIndexOption.NotAnalyzed, mapping.Index);
			
			var indexResult = this.Client.Index(new ElasticsearchProject
			{
				Country = "The Royal Kingdom Of The Netherlands"
			}, i=>i.Index(index).Refresh());
			Assert.IsTrue(indexResult.IsValid);

			var result = this.Client.Search<ElasticsearchProject>(s=>s
				.Index(index)
				.FacetTerm(ft=>ft.OnField(f=>f.Country))
				.MatchAll()
			);
			var facets = result.FacetItems<TermItem>(f=>f.Country);
			Assert.AreEqual(1, facets.Count());
			Assert.AreEqual("The Royal Kingdom Of The Netherlands", facets.FirstOrDefault().Term);
		}

		[Test]
		public void AnalyzedReturnsMoreItems()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this.Client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(m => m
					.MapFromAttributes()
					.Properties(pp=>pp
						.String(pps=>pps.Name(p=>p.Country).Index(FieldIndexOption.Analyzed))
					)
				)
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var indexResult = this.Client.Index(
				new ElasticsearchProject
				{
					Country = "The Royal Kingdom Of The Netherlands"
				}, i=> i.Index(index).Refresh()
				);
			Assert.IsTrue(indexResult.IsValid);

			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(index)
				.FacetTerm(ft => ft.OnField(f => f.Country))
				.MatchAll()
			);
			var facets = result.FacetItems<TermItem>(f => f.Country);
			Assert.AreEqual(5, facets.Count());
			facets.Select(f=>f.Term).Should().Contain("royal");
		}


	}
}
