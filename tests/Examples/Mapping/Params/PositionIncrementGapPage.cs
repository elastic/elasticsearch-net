// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class PositionIncrementGapPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/position-increment-gap.asciidoc:15")]
		public void Line15()
		{
			// tag::5e17abef396d757d65edf81dff5701b6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5e17abef396d757d65edf81dff5701b6[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			    ""names"": [ ""John Abraham"", ""Lincoln Smith""]
			}");

			response1.MatchesExample(@"GET my_index/_search
			{
			    ""query"": {
			        ""match_phrase"": {
			            ""names"": {
			                ""query"": ""Abraham Lincoln"" \<1>
			            }
			        }
			    }
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			    ""query"": {
			        ""match_phrase"": {
			            ""names"": {
			                ""query"": ""Abraham Lincoln"",
			                ""slop"": 101 \<2>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/position-increment-gap.asciidoc:53")]
		public void Line53()
		{
			// tag::a37ed1648f68b69e2ea467b38ce21ffc[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::a37ed1648f68b69e2ea467b38ce21ffc[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""names"": {
			        ""type"": ""text"",
			        ""position_increment_gap"": 0 \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			    ""names"": [ ""John Abraham"", ""Lincoln Smith""]
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			    ""query"": {
			        ""match_phrase"": {
			            ""names"": ""Abraham Lincoln"" \<2>
			        }
			    }
			}");
		}
	}
}
