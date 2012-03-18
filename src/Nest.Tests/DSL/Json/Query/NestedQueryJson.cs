using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class NestedQueryJson
	{
		[Test]
		public void NestedQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff=>ff
					.Nested(n=>n
						.Path(f=>f.Followers[0])
						.Query(q=>q.Term(f=>f.Followers[0].FirstName,"elasticsearch.pm"))
					)
				);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					nested: {
						query: {
							term: {
								""followers.firstName"": { value: ""elasticsearch.pm"" }
							}
						},
						path: ""followers""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
