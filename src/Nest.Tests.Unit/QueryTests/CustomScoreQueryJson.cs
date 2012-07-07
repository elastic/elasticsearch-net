using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryTests
{
	[TestFixture]
	public class CustomScoreQueryJson
	{
		[Test]
		public void CustomScoreQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(q=>q
					.CustomScore(cs=>cs
						.Script("doc['num1'].value > 1")
						.Query(qq=>qq.MatchAll())
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						custom_score : { 
							script : ""doc['num1'].value > 1"",
							query : { match_all : {} }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void CustomScoreQueryParams()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(q => q
					.CustomScore(cs => cs
						.Script("doc['num1'].value > myvar")
						.Params(p=>p.Add("myvar", 1.0))
						.Query(qq => qq.MatchAll())
					)
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						custom_score : { 
							script : ""doc['num1'].value > myvar"",
							params : { myvar : 1.0 },
							query : { match_all : {} }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
