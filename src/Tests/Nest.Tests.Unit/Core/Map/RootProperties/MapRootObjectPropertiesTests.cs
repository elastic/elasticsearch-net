using System;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.RootProperties
{
	[TestFixture]
	public class MapRootObjectPropertiesTests : BaseJsonTests
	{
		[Test]
		public void RootPropertiesShouldSerialize()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Type("elasticsearchprojects2")
				.Indices("nest_test_data", "nest_test_data_clone")
				.IgnoreConflicts()
				.IndexAnalyzer("standard1")
				.SearchAnalyzer("standard2")
				.DynamicDateFormats(new[] { "dateOptionalTime2", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" })
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
