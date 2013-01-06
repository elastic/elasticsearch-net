using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FilteredQueryJson
	{
		[Test]
		public void FilteredQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(qd=>qd
					.Filtered(cs=>cs
						.Query(q=>q.MatchAll())
						.Filter(f => f.MatchAll())
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						filtered : { 
							query : { match_all : {} },
							filter : { match_all : {} }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
