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
				.IndexNames("nest_test_data", "nest_test_data_clone")
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
	}
}
