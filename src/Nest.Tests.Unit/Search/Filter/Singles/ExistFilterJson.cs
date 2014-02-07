using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class ExistFilterJson
	{
		[Test]
		public void ExistFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Filter(filter=>filter
					.Exists(f=>f.Name)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						exists : { field : ""name"" }
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
