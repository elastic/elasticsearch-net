// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class CjkWidthTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/cjk-width-tokenfilter.asciidoc:28")]
		public void Line28()
		{
			// tag::76b279835936ee4b546a171c671c3cd7[]
			var response0 = new SearchResponse<object>();
			// end::76b279835936ee4b546a171c671c3cd7[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""cjk_width""],
			  ""text"" : ""ｼｰｻｲﾄﾞﾗｲﾅｰ""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/cjk-width-tokenfilter.asciidoc:69")]
		public void Line69()
		{
			// tag::3df1aa197f7498a534b0536f49aea28b[]
			var response0 = new SearchResponse<object>();
			// end::3df1aa197f7498a534b0536f49aea28b[]

			response0.MatchesExample(@"PUT /cjk_width_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_cjk_width"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""cjk_width""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}
