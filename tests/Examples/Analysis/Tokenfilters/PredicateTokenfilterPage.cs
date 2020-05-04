// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class PredicateTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/predicate-tokenfilter.asciidoc:22")]
		public void Line22()
		{
			// tag::10338787b66a7f93270c3b88dd6197f8[]
			var response0 = new SearchResponse<object>();
			// end::10338787b66a7f93270c3b88dd6197f8[]

			response0.MatchesExample(@"PUT /condition_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""my_script_filter"" ]
			                }
			            },
			            ""filter"" : {
			                ""my_script_filter"" : {
			                    ""type"" : ""predicate_token_filter"",
			                    ""script"" : {
			                        ""source"" : ""token.getTerm().length() > 5""  \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/predicate-tokenfilter.asciidoc:51")]
		public void Line51()
		{
			// tag::e20493a20d3992a97238b87c6930f08d[]
			var response0 = new SearchResponse<object>();
			// end::e20493a20d3992a97238b87c6930f08d[]

			response0.MatchesExample(@"POST /condition_example/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""What Flapdoodle""
			}");
		}
	}
}