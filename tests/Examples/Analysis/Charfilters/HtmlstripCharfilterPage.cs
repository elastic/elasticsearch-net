// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Charfilters
{
	public class HtmlstripCharfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/htmlstrip-charfilter.asciidoc:12")]
		public void Line12()
		{
			// tag::d6de3491f5787f739d5cd8c2ff3dddfa[]
			var response0 = new SearchResponse<object>();
			// end::d6de3491f5787f739d5cd8c2ff3dddfa[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"":      ""keyword"", \<1>
			  ""char_filter"":  [ ""html_strip"" ],
			  ""text"": ""\<p>I&apos;m so \<b>happy</b>!</p>""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/htmlstrip-charfilter.asciidoc:74")]
		public void Line74()
		{
			// tag::426f95b13a5b6042b5273d74ad8ee708[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::426f95b13a5b6042b5273d74ad8ee708[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""char_filter"": [""my_char_filter""]
			        }
			      },
			      ""char_filter"": {
			        ""my_char_filter"": {
			          ""type"": ""html_strip"",
			          ""escaped_tags"": [""b""]
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""\<p>I&apos;m so \<b>happy</b>!</p>""
			}");
		}
	}
}