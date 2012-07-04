using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.FilterTests
{
	[TestFixture]
	public class ScriptFilterJson
	{
		[Test]
		public void ScriptFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(filter=>filter
					.Script(sc=>sc
						.Script("doc['num1'].value > 1")
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						script : { 
							script : ""doc['num1'].value > 1""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void ScriptFilterParamsAndCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(filter => filter.Cache(true)
					.Script(sc => sc
						.Script("doc['num1'].value > param1")
						.Params(p=>p.Add("param1", 1))
					)
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						script : { 
							script : ""doc['num1'].value > param1"",
							params: { param1 : 1 },
							_cache: true
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
