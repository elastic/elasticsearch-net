// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class SpanMultiTermQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-multi-term-query.asciidoc:12")]
		public void Line12()
		{
			// tag::a22f79d01a4a625840072024feb60b46[]
			var response0 = new SearchResponse<object>();
			// end::a22f79d01a4a625840072024feb60b46[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_multi"":{
			            ""match"":{
			                ""prefix"" : { ""user"" :  { ""value"" : ""ki"" } }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-multi-term-query.asciidoc:28")]
		public void Line28()
		{
			// tag::87ffa93d8de41fd0c3ea2f52378dab9c[]
			var response0 = new SearchResponse<object>();
			// end::87ffa93d8de41fd0c3ea2f52378dab9c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_multi"":{
			            ""match"":{
			                ""prefix"" : { ""user"" :  { ""value"" : ""ki"", ""boost"" : 1.08 } }
			            }
			        }
			    }
			}");
		}
	}
}
