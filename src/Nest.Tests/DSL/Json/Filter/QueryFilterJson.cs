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
	public class QueryFilterJson
	{
		[Test]
		public void QueryFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Query(q=>q.Term(f=>f.Name,"elasticsearch.pm"))
				);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						fquery: {
							query:{
								term : {
									""name"": {
										value: ""elasticsearch.pm""
									}
								}
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void QueryFilterCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Cache(true)
					.Name("query_filter")
					.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						fquery: {
							query:{
								term : {
									name: {
										value: ""elasticsearch.pm""
									}
								}
							},
							_cache:true, 
							_name: ""query_filter""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
