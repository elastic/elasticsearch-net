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
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
						.Scope("my_scope")
						.Score(ChildScoreType.avg)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ has_child: { 
				type: ""person"",
				_scope: ""my_scope"",
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
						.Scope("my_scope")
						.Type("sillypeople")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ has_child: { 
				type: ""sillypeople"",
				_scope: ""my_scope"",
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
