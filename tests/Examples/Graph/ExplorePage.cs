// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Graph
{
	public class ExplorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("graph/explore.asciidoc:204")]
		public void Line204()
		{
			// tag::8bf5ac11eb42e652023a685af4a45ae2[]
			var response0 = new SearchResponse<object>();
			// end::8bf5ac11eb42e652023a685af4a45ae2[]

			response0.MatchesExample(@"POST clicklogs/_graph/explore
			{
			    ""query"": { \<1>
			        ""match"": {
			            ""query.raw"": ""midi""
			        }
			    },
			    ""vertices"": [ \<2>
			        {
			            ""field"": ""product""
			        }
			    ],
			    ""connections"": {  \<3>
			        ""vertices"": [
			            {
			                ""field"": ""query.raw""
			            }
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("graph/explore.asciidoc:309")]
		public void Line309()
		{
			// tag::6a1a238984d74771420d150dec47fd91[]
			var response0 = new SearchResponse<object>();
			// end::6a1a238984d74771420d150dec47fd91[]

			response0.MatchesExample(@"POST clicklogs/_graph/explore
			{
			    ""query"": {
			        ""match"": {
			            ""query.raw"": ""midi""
			        }
			    },
			   ""controls"": {
			      ""use_significance"": false,\<1>
			      ""sample_size"": 2000,\<2>
			      ""timeout"": 2000,\<3>
			      ""sample_diversity"": {\<4>
			         ""field"": ""category.raw"",
			         ""max_docs_per_value"": 500
			      }
			   },
			   ""vertices"": [
			      {
			         ""field"": ""product"",
			         ""size"": 5,\<5>
			         ""min_doc_count"": 10,\<6>
			         ""shard_min_doc_count"": 3\<7>
			      }
			   ],
			   ""connections"": {
			      ""query"": {\<8>
			         ""bool"": {
			            ""filter"": [
			               {
			                  ""range"": {
			                     ""query_time"": {
			                        ""gte"": ""2015-10-01 00:00:00""
			                     }
			                  }
			               }
			            ]
			         }
			      },
			      ""vertices"": [
			         {
			            ""field"": ""query.raw"",
			            ""size"": 5,
			            ""min_doc_count"": 10,
			            ""shard_min_doc_count"": 3
			         }
			      ]
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("graph/explore.asciidoc:396")]
		public void Line396()
		{
			// tag::fa82d86a046d67366cfe9ce65535e433[]
			var response0 = new SearchResponse<object>();
			// end::fa82d86a046d67366cfe9ce65535e433[]

			response0.MatchesExample(@"POST clicklogs/_graph/explore
			{
			   ""vertices"": [
			      {
			         ""field"": ""product"",
			         ""include"": [ ""1854873"" ] \<1>
			      }
			   ],
			   ""connections"": {
			      ""vertices"": [
			         {
			            ""field"": ""query.raw"",
			            ""exclude"": [ \<2>
			               ""midi keyboard"",
			               ""midi"",
			               ""synth""
			            ]
			         }
			      ]
			   }
			}");
		}
	}
}
