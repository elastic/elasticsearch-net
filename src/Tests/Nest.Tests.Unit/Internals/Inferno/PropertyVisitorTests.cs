using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class PropertyVisitorTests
	{
		[Test]
		public void SuffixMakesItIntoPropertyName()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Size(10)
			  .Query(q => q.Term(f => f.Name.Suffix("sort"), "value"));
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10,
			query: {
		  term: {
			""name.sort"": {
			  value: ""value""
			}
		  }
		}
	  }";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
