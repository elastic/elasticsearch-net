using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class HasChildFilterJson
	{
		[Test]
		public void HasChildFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Filter(ff=>ff
					.HasChild<Person>(d=>d
						.Query(q => q.Term(p => p.FirstName, "value"))
						.Filter(q => q.Term(p => p.Age, 42))
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					""has_child"": {
					  ""type"": ""person"",
					  ""query"": {
						""term"": {
						  ""firstName"": {
							""value"": ""value""
						  }
						}
					  },
					  ""filter"": {
						""term"": {
						  ""age"": 42
						}
					  }
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
