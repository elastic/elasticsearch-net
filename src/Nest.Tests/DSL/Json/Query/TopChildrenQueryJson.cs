using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.FilterTests
{
	[TestFixture]
	public class TopChildrenQueryJson
	{
		[Test]
		public void TopChildrenQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TopChildren<Person>(fz => fz
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
						.Scope("my_scope")
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ top_children: { 
				type: ""people"",
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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TopChildren<Person>(fz => fz
						.Query(qq => qq.Term(f => f.FirstName, "john"))
						.Score(TopChildrenScore.avg)
						.Scope("my_scope")
						.Type("sillypeople")
					)
				);
			var json = ElasticClient.Serialize(s);
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
