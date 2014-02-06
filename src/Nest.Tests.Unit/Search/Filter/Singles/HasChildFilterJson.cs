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
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(ff=>ff
					.HasChild<Person>(d=>d
						.Scope("my_scope")
						.Query(q=>q.Term(p=>p.FirstName, "value"))
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					""has_child"": {
					  ""type"": ""person"",
					  ""_scope"": ""my_scope"",
					  ""query"": {
						""term"": {
						  ""firstName"": {
							""value"": ""value""
						  }
						}
					  }
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
