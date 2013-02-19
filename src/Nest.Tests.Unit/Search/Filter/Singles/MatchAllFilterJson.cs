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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(f=>f.MatchAll());
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						match_all : {}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
