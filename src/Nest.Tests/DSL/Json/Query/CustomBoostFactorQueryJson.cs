using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class CustomBoostFactorQueryJson
	{
		[Test]
		public void CustomBoostFactorQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(q=>q
						.CustomBoostFactor(cs=>cs
							.BoostFactor(5.2)
							.Query(qq=>qq.MatchAll())
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						custom_boost_factor : { 
							query : { match_all : {} },
							boost_factor : 5.2
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
