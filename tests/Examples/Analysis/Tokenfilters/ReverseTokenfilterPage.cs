// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class ReverseTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/reverse-tokenfilter.asciidoc:24")]
		public void Line24()
		{
			// tag::e09d30195108bd6a1f6857394a6123ea[]
			var response0 = new SearchResponse<object>();
			// end::e09d30195108bd6a1f6857394a6123ea[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""reverse""],
			  ""text"" : ""quick fox jumps""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/reverse-tokenfilter.asciidoc:79")]
		public void Line79()
		{
			// tag::aa5fbb68d3a8e0d0c894791cb6cf0b13[]
			var response0 = new SearchResponse<object>();
			// end::aa5fbb68d3a8e0d0c894791cb6cf0b13[]

			response0.MatchesExample(@"PUT reverse_example
			{
			  ""settings"" : {
			    ""analysis"" : {
			      ""analyzer"" : {
			        ""whitespace_reverse"" : {
			          ""tokenizer"" : ""whitespace"",
			          ""filter"" : [""reverse""]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
