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
			var s = new SearchDescriptor<ElasticsearchProject>()
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
			var s = new SearchDescriptor<ElasticsearchProject>()
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

		[Test]
		public void TermsFilterWithConditionlessQueryWithCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				
				.FacetTerm(ft => ft
					.FacetFilter(ff => ff.Query(fq => fq.Term("somefield", "")))
					.AllTerms()
					.OnField(p=>p.Name)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				facets: {
					name: {
					  terms: {
						field: ""name"",
						all_terms: true
					  }
					}
				  }
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
