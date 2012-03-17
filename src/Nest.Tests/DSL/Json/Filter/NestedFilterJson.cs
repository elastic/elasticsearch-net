using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class NestedFilterJson
	{
		[Test]
		public void NestedFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Cache(true)
					.Name("nesty")
					.Nested(n=>n
						.Path(f=>f.Followers[0])
						.Query(q=>q.Term(f=>f.Followers[0].FirstName,"elasticsearch.pm"))
					)
				);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					nested: {
						query: {
							term: {
								""followers.firstName"": { value: ""elasticsearch.pm"" }
							}
						},
						path: ""followers"",
						_cache: true,
						_name: ""nesty""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
