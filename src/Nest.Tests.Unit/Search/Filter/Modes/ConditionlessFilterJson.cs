using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Filter.Modes
{
	[TestFixture]
	public class ConditionlessFilterJson
	{
		[Test]
		public void Fallback()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(q=>q
					.Conditionless(qs=>qs
						.Filter(qcq=>qcq.Term("this_term_is_conditionless", ""))
						.Fallback(qcf=>qcf.Term("name", "do_me_instead")
					)
				)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						term : { 
							name : ""do_me_instead""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		public void UseFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(q => q
					.Conditionless(qs => qs
						.Filter(qcq => qcq.Term("name", "NEST"))
						.Fallback(qcf => qcf.Term("name", "do_me_instead")
					)
				)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						term : { 
							name : ""NEST"" 
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void BothConditionless()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(q => q
					.Conditionless(qs => qs
						.Filter(qcq => qcq.Term("name", ""))
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
