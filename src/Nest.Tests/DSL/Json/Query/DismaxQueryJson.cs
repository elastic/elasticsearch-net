using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.FilterTests
{
	[TestFixture]
	public class DismaxQueryJson
	{
		[Test]
		public void DismaxQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(qd=>qd
					.Dismax(d=>d
						.Boost(1.2)
						.TieBreaker(0.7)
						.Queries(
							q => q.MatchAll(),
							q => q.Term(f=>f.Name, "elasticsearch.pm")
						)
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						dis_max : { 
							tie_breaker: 0.7,
							boost: 1.2,
							queries :[
								{ match_all : {} },
								{ term : { name : { value : ""elasticsearch.pm"" } } }
							]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
