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
					.Terms(f => f.Name, new [] {"elasticsearch.pm"}, Execution:TermsExecution.Bool)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						terms: {
							""name"": [""elasticsearch.pm""],
							execution: ""bool"",
							_cache:false,
							_name: ""terms_filter""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

        [Test]
        public void TermsFilter_EnumerableOfStringOverLoad_WithNonDefaultExecutionSpecified_AppliesExecution()
        {
            var s = new SearchDescriptor<ElasticsearchProject>()
                .From(0)
                .Size(10)
                .Filter(ff => ff
                    .Terms(f => f.MyStringArrayField, new[] { "elasticsearch.pm" }, Execution: TermsExecution.Bool)
                );

            var json = TestElasticClient.Serialize(s);
            var expected = @"{ from: 0, size: 10, 
				filter : {
						terms: {
							""myStringArrayField"": [""elasticsearch.pm""],
							execution: ""bool""
						}

					}
			}";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TermsFilter_NonLambdaField_WithNonDefaultExecutionSpecified_AppliesExecution()
        {
            var s = new SearchDescriptor<ElasticsearchProject>()
                .From(0)
                .Size(10)
                .Filter(ff => ff
                    .Terms("myStringArrayField", new[] { "elasticsearch.pm" }, Execution: TermsExecution.Bool)
                );

            var json = TestElasticClient.Serialize(s);
            var expected = @"{ from: 0, size: 10, 
				filter : {
						terms: {
							""myStringArrayField"": [""elasticsearch.pm""],
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

		[Test]
		public void TermsFilterWithNumericTerms()
		{
			var query = Query<ElasticsearchProject>.Filtered(filtered => filtered
				.Filter(f =>
					f.Terms(t => t.LongValue, new long[] { 1, 2, 3 })
				)
				.Query(q => q.MatchAll())
			);

			var json = TestElasticClient.Serialize(query);
			var expected = @"{ 
							  filtered: {
							  query: {
							    match_all: {}
							  },
							  filter: {
								terms: {
								  longValue: [1, 2, 3 ]
								}
							  }
							}
						  }";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TermsFilterWithNullTerms()
		{
			var query = Query<ElasticsearchProject>.Filtered(filtered => filtered
				.Filter(f =>
					f.Terms(t => t.Name, null)
				)
				.Query(q => q.MatchAll())
			);
			var json = TestElasticClient.Serialize(query);
			var expected = @"{ 
							  filtered: {
							  query: {
							    match_all: {}
							  },
							  filter: {}
							}
						  }";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
