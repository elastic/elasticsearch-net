using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.RootProperties
{
	[TestFixture]
	public class MapRootObjectPropertiesTests : BaseMappingTests
	{
		[Test]
		[SkipVersion("1.2.0", "Fails on ES 1.2.0: https://github.com/elasticsearch/elasticsearch/pull/6353")]
		public void RootPropertiesShouldSerialize()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects2")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.IgnoreConflicts()
				.IndexAnalyzer("standard")
				.SearchAnalyzer("standard")
				.DynamicDateFormats(new[] { "dateOptionalTime", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" })
				.DateDetection(true)
				.NumericDetection(true)
				.SetParent<Person>() //makes no sense but i needed a type :)
				.AllField(a=>a.Enabled(false))
				.DisableIndexField(true)
				.DisableSizeField(true)
				.Dynamic()
				.Enabled()
				.IncludeInAll()
			);
			this.DefaultResponseAssertations(result);
		}

		[Test]
		public void DynamicAllowSetAndGet()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects_allow")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.Allow)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_allow"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.Allow);

			result = this.Client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects_allow2")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(true)
			);
			this.DefaultResponseAssertations(result);
			getResult = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_allow"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.Allow);

		}

		[Test]
		public void DynamicIgnoreSetAndGet()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects_ignore")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.Ignore)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_ignore"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.Ignore);

			result = this.Client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects_ignore2")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(false)
			);
			this.DefaultResponseAssertations(result);
			getResult = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_ignore2"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.Ignore);

		}
		[Test]
		public void DynamicStrictSetAndGet()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects_strict")
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex)
				.Dynamic(DynamicMappingOption.Strict)
			);
			this.DefaultResponseAssertations(result);
			var getResult = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects_strict"));
			Assert.AreEqual(getResult.Mapping.Dynamic, DynamicMappingOption.Strict);

		}
	}
}
