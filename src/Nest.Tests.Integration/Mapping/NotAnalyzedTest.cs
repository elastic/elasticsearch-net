using System.Linq;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class NotAnalyzedTests : BaseElasticSearchTests
	{
		[Test]
		public void NotAnalyzedReturnsOneItem()
		{
			this._client.DeleteMapping<ElasticSearchProject>();
			this._client.DeleteMapping<ElasticSearchProject>(Test.Default.DefaultIndex + "_clone");
			var x = this._client.MapFromAttributes<ElasticSearchProject>();
			Assert.IsTrue(x.OK);

			var typeMapping = this._client.GetMapping(Test.Default.DefaultIndex, "elasticsearchprojects");
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			Assert.AreEqual("not_analyzed", mapping.Index);
			
			var indexResult = this._client.Index(new ElasticSearchProject
			{
				Country = "The Royal Kingdom Of The Netherlands"
			}, new IndexParameters { Refresh = true });
			Assert.IsTrue(indexResult.IsValid);

			var result = this._client.Search<ElasticSearchProject>(s=>s
				.FacetTerm(ft=>ft.OnField(f=>f.Country))
				.MatchAll()
			);
			var facets = result.FacetItems<TermItem>(f=>f.Country);
			Assert.AreEqual(1, facets.Count());
			Assert.AreEqual("The Royal Kingdom Of The Netherlands", facets.FirstOrDefault().Term);
		}

		[Test]
		public void AnalyzedReturnsTwoItems()
		{
			this._client.DeleteMapping<ElasticSearchProject>();
			var x = this._client.MapFromAttributes<ElasticSearchProject>();
			Assert.IsTrue(x.OK);

			var typeMapping = this._client.GetMapping(Test.Default.DefaultIndex, "elasticsearchprojects");
			this._client.DeleteMapping<ElasticSearchProject>();
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			mapping.Index = FieldIndexOption.analyzed;
			var updateMapResult = this._client.Map(typeMapping);
			Assert.True(updateMapResult.IsValid);

			var indexResult = this._client.Index(new ElasticSearchProject
			{
				Country = "The Royal Kingdom Of The Netherlands"
			}, new IndexParameters { Refresh = true });
			Assert.IsTrue(indexResult.IsValid);

			var result = this._client.Search<ElasticSearchProject>(s => s
				.FacetTerm(ft => ft.OnField(f => f.Country))
				.MatchAll()
			);
			var facets = result.FacetItems<TermItem>(f => f.Country);
			Assert.AreEqual(3, facets.Count());
			Assert.AreEqual("royal", facets.FirstOrDefault().Term);
		}


	}
}
