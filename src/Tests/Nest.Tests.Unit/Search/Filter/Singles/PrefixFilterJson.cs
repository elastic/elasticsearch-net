using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class PrefixFilterJson
	{
		[Test]
		public void PrefixFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Prefix(f=>f.Name, "elast")
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						prefix: {
							""name"": ""elast""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void PrefixFilterWithCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Cache(false)
					.Name("prefix_filter")
					.Prefix(f => f.Name, "elast")
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						prefix: {
							""name"": ""elast"",
							_cache:false,
							_name : ""prefix_filter""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
