using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Integration.Core.Map.RootProperties
{
	[TestFixture]
	public class MapRootObjectPropertiesTests : BaseMappingTests
	{
		[Test]
		public void RootPropertiesShouldSerialize()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeName("elasticsearchprojects2")
				.IndexNames(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.IgnoreConflicts()
				.IndexAnalyzer("standard")
				.SearchAnalyzer("standard")
				.DynamicDateFormats(new[] { "dateOptionalTime", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" })
				.DateDetection(true)
				.NumericDetection(true)
				.SetParent<Person>() //makes no sense but i needed a type :)
				.DisableAllField(true)
				.DisableIndexField(true)
				.DisableSizeField(true)
				.Dynamic()
				.Enabled()
				.IncludeInAll()
				.Path("full")
			);
			this.DefaultResponseAssertations(result);
		}

		[Test]
		public void DynamicAllowSetAndGet()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeName("elasticsearchprojects_allow")
				.IndexNames(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.allow)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects_allow");
			Assert.AreEqual(getResult.Dynamic, DynamicMappingOption.allow);

			result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeName("elasticsearchprojects_allow2")
				.IndexNames(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(true)
			);
			this.DefaultResponseAssertations(result);
			getResult = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects_allow2");
			Assert.AreEqual(getResult.Dynamic, DynamicMappingOption.allow);

		}

		[Test]
		public void DynamicIgnoreSetAndGet()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeName("elasticsearchprojects_ignore")
				.IndexNames(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.ignore)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects_ignore");
			Assert.AreEqual(getResult.Dynamic, DynamicMappingOption.ignore);

			result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeName("elasticsearchprojects_ignore2")
				.IndexNames(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(false)
			);
			this.DefaultResponseAssertations(result);
			getResult = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects_ignore2");
			Assert.AreEqual(getResult.Dynamic, DynamicMappingOption.ignore);

		}
		[Test]
		public void DynamicStrictSetAndGet()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
				.TypeName("elasticsearchprojects_strict")
				.IndexNames(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.strict)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects_strict");
			Assert.AreEqual(getResult.Dynamic, DynamicMappingOption.strict);

		}
	}
}
