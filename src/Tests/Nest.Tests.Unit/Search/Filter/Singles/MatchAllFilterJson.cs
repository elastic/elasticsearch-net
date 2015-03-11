using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class MatchAllFilterJson
	{
		[Test]
		public void MatchAllFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(f=>f.MatchAll());
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						match_all : {}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
