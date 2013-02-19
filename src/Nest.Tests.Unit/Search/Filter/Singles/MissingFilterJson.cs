using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class MissingFilterJson
	{
		[Test]
		public void MissingFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(ff=>ff.Missing(f=>f.Name));
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						missing : { field : ""name"" }
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
