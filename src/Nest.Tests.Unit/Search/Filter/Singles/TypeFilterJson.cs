using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class TypeFilterJson
	{
		[Test]
		public void TypeFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(f=>f.Type("my_type"));
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						type : { 
							value : ""my_type""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
