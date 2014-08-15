using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class ConditionlessQueryJson
	{
		[Test]
		public void FallbackTerm()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q=>q
					.Conditionless(qs=>qs
						.Query(qcq=>qcq.Term("this_term_is_conditionless", ""))
						.Fallback(qcf=>qcf.Term("name", "do_me_instead")
					)
				)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						term : { 
							name : { value : ""do_me_instead"" }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		public void FallbackMatch()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.Conditionless(qs => qs
						.Query(qcq => qcq
							.Match(m => m
								.OnField(p => p.Name)
								.Query("")
							)
						)
						.Fallback(qcf=>qcf
							.Match(m => m
								.OnField(p => p.Name)
								.Query("do_me_instead")
							)
						)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						match : { 
							name : {
								query: ""do_me_instead""
							}
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void UseQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.Conditionless(qs => qs
						.Query(qcq => qcq.Term("name", "NEST"))
						.Fallback(qcf => qcf.Term("name", "do_me_instead")
					)
				)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						term : { 
							name : { value : ""NEST"" }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void BothConditionless()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.Conditionless(qs => qs
						.Query(qcq => qcq.Term("name", ""))
						.Fallback(qcf => qcf.Term("name", "")
					)
				)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10 }";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
