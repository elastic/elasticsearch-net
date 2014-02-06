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
			var result = this._client.Map<ElasticSearchProject>(m => m
				.Type("elasticsearchprojects2")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
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
			var result = this._client.Map<ElasticSearchProject>(m => m
				.Type("elasticsearchprojects_allow")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.allow)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this._client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_allow"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.allow);

			result = this._client.Map<ElasticSearchProject>(m => m
				.Type("elasticsearchprojects_allow2")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(true)
			);
			this.DefaultResponseAssertations(result);
			getResult = this._client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_allow"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.allow);

		}

		[Test]
		public void DynamicIgnoreSetAndGet()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.Type("elasticsearchprojects_ignore")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.ignore)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this._client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_ignore"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.ignore);

			result = this._client.Map<ElasticSearchProject>(m => m
				.Type("elasticsearchprojects_ignore2")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(false)
			);
			this.DefaultResponseAssertations(result);
			getResult = this._client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_ignore2"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.ignore);

		}
		[Test]
		public void DynamicStrictSetAndGet()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.Type("elasticsearchprojects_strict")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.strict)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this._client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_strict"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.strict);

		}
	}
}
