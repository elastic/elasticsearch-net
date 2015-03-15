using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class QueryFilterJson
	{
		[Test]
		public void QueryFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(ff=>ff
					.Query(q=>q.Term(f=>f.Name,"elasticsearch.pm"))
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						fquery: {
							query:{
								term : {
									""name"": {
										value: ""elasticsearch.pm""
									}
								}
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void QueryFilterCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(ff => ff
					.Cache(true)
					.Name("query_filter")
					.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						fquery: {
							query:{
								term : {
									name: {
										value: ""elasticsearch.pm""
									}
								}
							},
							_cache:true, 
							_name: ""query_filter""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
