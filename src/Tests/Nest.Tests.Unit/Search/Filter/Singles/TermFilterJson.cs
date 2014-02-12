using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class TermFilterJson
	{
		[Test]
		public void TermFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Term(f=>f.Name, "elasticsearch.pm")
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						term: {
							""name"": ""elasticsearch.pm""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TermFilterWithCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Cache(false)
					.Name("term_filter")
					.Term(f => f.Name, "elasticsearch.pm")
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						term: {
							""name"": ""elasticsearch.pm"",
							_cache:false,
							_name:""term_filter""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
