using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class TermsFilterJson
	{
		[Test]
		public void TermsFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Terms(f=>f.Name, new [] {"elasticsearch.pm"})
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						terms: {
							""name"": [""elasticsearch.pm""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TermsFilterWithCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Cache(false).Name("terms_filter")
					.Terms(f => f.Name, new [] {"elasticsearch.pm"}, Execution:TermsExecution.@bool)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						terms: {
							""name"": [""elasticsearch.pm""],
							_cache:false,
							_name: ""terms_filter"",
							execution: ""bool""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
