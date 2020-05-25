// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class ReverseNestedAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/reverse-nested-aggregation.asciidoc:19")]
		public void Line19()
		{
			// tag::817891bd13da04e5981a797247601145[]
			var response0 = new SearchResponse<object>();
			// end::817891bd13da04e5981a797247601145[]

			response0.MatchesExample(@"PUT /issues
			{
			    ""mappings"": {
			         ""properties"" : {
			             ""tags"" : { ""type"" : ""keyword"" },
			             ""comments"" : { \<1>
			                 ""type"" : ""nested"",
			                 ""properties"" : {
			                     ""username"" : { ""type"" : ""keyword"" },
			                     ""comment"" : { ""type"" : ""text"" }
			                 }
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/reverse-nested-aggregation.asciidoc:54")]
		public void Line54()
		{
			// tag::aee26dd62fbb6d614a0798f3344c0598[]
			var response0 = new SearchResponse<object>();
			// end::aee26dd62fbb6d614a0798f3344c0598[]

			response0.MatchesExample(@"GET /issues/_search
			{
			  ""query"": {
			    ""match_all"": {}
			  },
			  ""aggs"": {
			    ""comments"": {
			      ""nested"": {
			        ""path"": ""comments""
			      },
			      ""aggs"": {
			        ""top_usernames"": {
			          ""terms"": {
			            ""field"": ""comments.username""
			          },
			          ""aggs"": {
			            ""comment_to_issue"": {
			              ""reverse_nested"": {}, \<1>
			              ""aggs"": {
			                ""top_tags_per_comment"": {
			                  ""terms"": {
			                    ""field"": ""tags""
			                  }
			                }
			              }
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
