using System.Linq;
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
			var x = this._client.CreateIndex(index, s => s
				.AddMapping<ElasticSearchProject>(m=>m.MapFromAttributes())
			);
			Assert.IsTrue(x.OK, x.ConnectionStatus.ToString());

			var typeMapping = this._client.GetMapping(index, "elasticsearchprojects");
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			Assert.AreEqual(FieldIndexOption.not_analyzed, mapping.Index);
			
			var indexResult = this._client.Index(new ElasticSearchProject
			{
				Country = "The Royal Kingdom Of The Netherlands"
			}, indexParameters: new IndexParameters { Refresh = true }, index: index);
			Assert.IsTrue(indexResult.IsValid);

			var result = this._client.Search<ElasticSearchProject>(s=>s
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
			var x = this._client.CreateIndex(index, s => s
				.AddMapping<ElasticSearchProject>(m => m
					.MapFromAttributes()
					.Properties(pp=>pp
						.String(pps=>pps.Name(p=>p.Country).Index(FieldIndexOption.analyzed))
					)
				)
			);
			Assert.IsTrue(x.OK, x.ConnectionStatus.ToString());

			var indexResult = this._client.Index(
				new ElasticSearchProject
				{
					Country = "The Royal Kingdom Of The Netherlands"
				},
				indexParameters: new IndexParameters { Refresh = true }
				, index: index);
			Assert.IsTrue(indexResult.IsValid);

			var result = this._client.Search<ElasticSearchProject>(s => s
				.Index(index)
				.FacetTerm(ft => ft.OnField(f => f.Country))
				.MatchAll()
			);
			var facets = result.FacetItems<TermItem>(f => f.Country);
			Assert.AreEqual(3, facets.Count());
			Assert.AreEqual("royal", facets.FirstOrDefault().Term);
		}


	}
}
