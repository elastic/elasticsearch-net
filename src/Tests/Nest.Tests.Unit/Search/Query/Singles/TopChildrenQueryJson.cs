using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class TopChildrenQueryJson
	{
		[Test]
		public void TopChildrenQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TopChildren<Person>(fz => fz
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
						.Scope("my_scope")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ top_children: { 
				type: ""person"",
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
		[Test]
		public void HasChildOverrideTypeQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TopChildren<Person>(fz => fz
						.Query(qq => qq.Term(f => f.FirstName, "john"))
						.Score(TopChildrenScore.Average)
						.Scope("my_scope")
						.Type("sillypeople")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ top_children: { 
				type: ""sillypeople"",
				_scope: ""my_scope"",
				score: ""avg"",
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
