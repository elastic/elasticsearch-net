using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class NestedQueryJson
	{
		[Test]
		public void NestedQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff=>ff
					.Nested(n=>n
						.Path(f=>f.Followers[0])
						.Query(q=>q.Term(f=>f.Followers[0].FirstName,"elasticsearch.pm"))
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					nested: {
						query: {
							term: {
								""followers.firstName"": { value: ""elasticsearch.pm"" }
							}
						},
						path: ""followers""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
