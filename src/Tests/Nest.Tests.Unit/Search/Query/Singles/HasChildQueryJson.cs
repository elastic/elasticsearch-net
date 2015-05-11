using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class HasChildQueryJson
	{
		[Test]
		public void HasChildThisQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.HasChild<Person>(fz => fz
						.Name("named_query")
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
						.Score(ChildScoreType.Average)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ has_child: { 
				_name: ""named_query"",
				type: ""person"",
				score_type: ""avg"",
				query: {
					term: {
						firstName: {
							value: ""john""
						}
					}
				}

			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void HasChildOverrideTypeQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.HasChild<Person>(fz => fz
						.Query(qq => qq.Term(f => f.FirstName, "john"))
						.Type("sillypeople")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ has_child: { 
				type: ""sillypeople"",
				query: {
					term: {
						firstName: {
							value: ""john""
						}
					}
				}

			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
