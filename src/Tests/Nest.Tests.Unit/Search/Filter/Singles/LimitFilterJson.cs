using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class LimitFilterJson
	{
		[Test]
		public void LimitFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Filter(filter=>filter
					.Limit(100)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						limit : { 
							value : 100
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
