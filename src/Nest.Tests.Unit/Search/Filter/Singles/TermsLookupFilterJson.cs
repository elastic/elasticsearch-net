using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class TermsLookupFilterJson
	{
		[Test]
		public void TermsLookupFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.TermsLookup(f=>f.Name, t => t.Lookup<ElasticSearchProject>(p=>p.Name, "NEST"))
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					terms: {
						""name"": {
							id: ""NEST"",
							type: ""elasticsearchprojects"",
							index: ""mydefaultindex"",
							path: ""name""
						}	
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TermsLookupFilterWithCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Cache(true)
					.Name("terms_filter")
					.CacheKey("NEST_TERMS")
					.TermsLookup(f => f.Name, t => t
						.Lookup<ElasticSearchProject>(p => p.Name, "NEST")
						.Routing("dot_net_clients")
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					terms: {
						""name"": {
							id: ""NEST"",
							type: ""elasticsearchprojects"",
							index: ""mydefaultindex"",
							path: ""name"",
							routing: ""dot_net_clients""
						},
						_cache:true,
						_name: ""terms_filter"",
						_cache_key: ""NEST_TERMS""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
