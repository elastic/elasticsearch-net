using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class NestedFilterJson
	{
		[Test]
		public void NestedFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(ff=>ff
					.Cache(true)
					.Name("nesty")
					.Nested(n=>n
						.Path(f=>f.Followers[0])
						.Query(q=>q.Term(f=>f.Followers[0].FirstName,"elasticsearch.pm"))
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
					nested: {
						query: {
							term: {
								""followers.firstName"": { value: ""elasticsearch.pm"" }
							}
						},
						path: ""followers"",
						_cache: true,
						_name: ""nesty""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
