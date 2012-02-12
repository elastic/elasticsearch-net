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
	public class TermFilterJson
	{
		[Test]
		public void TermFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Term(f=>f.Name, "elasticsearch.pm")
				);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						term: {
							""name"": ""elasticsearch.pm""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TermFilterWithCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Term(f => f.Name, "elasticsearch.pm", Cache: false)
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						term: {
							""name"": ""elasticsearch.pm"",
							_cache:false
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
