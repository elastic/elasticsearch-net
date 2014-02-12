using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class HasParentQueryJson
	{
		[Test]
		public void HasParentThisQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.HasParent<Person>(fz => fz
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
						.Scope("my_scope")
						.Score(ParentScoreType.score)
                   )
            );
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ has_parent: { 
				type: ""person"",
				_scope: ""my_scope"",
				score_type: ""score"",
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
