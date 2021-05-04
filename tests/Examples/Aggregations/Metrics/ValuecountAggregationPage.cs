// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class ValuecountAggregationPage : ExampleBase
	{
		[U]
		[Description("aggregations/metrics/valuecount-aggregation.asciidoc:13")]
		public void Line13()
		{
			// tag::5dd695679b5141d9142d3d30ba8d300a[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.ValueCount("types_count", vc => vc
						.Field("type")
					)
				)
			);
			// end::5dd695679b5141d9142d3d30ba8d300a[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""types_count"" : { ""value_count"" : { ""field"" : ""type"" } }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/metrics/valuecount-aggregation.asciidoc:46")]
		public void Line46()
		{
			// tag::3722cb3705b6bc7f486969deace3dd83[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.ValueCount("type_count", vc => vc
						.Script(sc => sc
							.Source("doc['type'].value")
						)
					)
				)
			);
			// end::3722cb3705b6bc7f486969deace3dd83[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""type_count"" : {
			            ""value_count"" : {
			                ""script"" : {
			                    ""source"" : ""doc['type'].value""
			                }
			            }
			        }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/metrics/valuecount-aggregation.asciidoc:65")]
		public void Line65()
		{
			// tag::213ab768f1b6a895e09403a0880e259a[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.ValueCount("types_count", vc => vc
						.Script(sc => sc
							.Id("my_script")
							.Params(p => p
								.Add("field", "type")
							)
						)
					)
				)
			);
			// end::213ab768f1b6a895e09403a0880e259a[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""types_count"" : {
			            ""value_count"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"" : {
			                        ""field"" : ""type""
			                    }
			                }
			            }
			        }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("aggregations/metrics/valuecount-aggregation.asciidoc:96")]
		public void Line96()
		{
			// tag::e9fe608f105d7e3268a15e409e2cb9ab[]
			var indexResponse1 = client.Index(new Dictionary<string, object>
			{
				["network.name"] = "net-1",
				["latency_histo"] = new
				{
					values = new [] {0.1, 0.2, 0.3, 0.4, 0.5},
					counts = new [] {3, 7, 23, 12, 6}
				}
			}, i => i.Index("metrics_index").Id(1));

			var indexResponse2 = client.Index(new Dictionary<string, object>
			{
				["network.name"] = "net-2",
				["latency_histo"] = new
				{
					values = new [] {0.1, 0.2, 0.3, 0.4, 0.5},
					counts = new [] {8, 17, 8, 7, 6}
				}
			}, i => i.Index("metrics_index").Id(2));

			var searchResponse = client.Search<object>(s => s
				.Index("metrics_index")
				.Size(0)
				.Aggregations(a => a
					.ValueCount("total_requests", vc => vc
						.Field("latency_histo")
					)
				)
			);
			// end::e9fe608f105d7e3268a15e409e2cb9ab[]

			indexResponse1.MatchesExample(@"PUT metrics_index/_doc/1
			{
			  ""network.name"" : ""net-1"",
			  ""latency_histo"" : {
			      ""values"" : [0.1, 0.2, 0.3, 0.4, 0.5],
			      ""counts"" : [3, 7, 23, 12, 6] <1>
			   }
			}");

			indexResponse2.MatchesExample(@"PUT metrics_index/_doc/2
			{
			  ""network.name"" : ""net-2"",
			  ""latency_histo"" : {
			      ""values"" :  [0.1, 0.2, 0.3, 0.4, 0.5],
			      ""counts"" : [8, 17, 8, 7, 6] <1>
			   }
			}");

			searchResponse.MatchesExample(@"POST /metrics_index/_search?size=0
			{
			    ""aggs"" : {
			        ""total_requests"" : {
			            ""value_count"" : { ""field"" : ""latency_histo"" }
			        }
			    }
			}", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}
	}
}
