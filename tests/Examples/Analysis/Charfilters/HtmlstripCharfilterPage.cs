// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Charfilters
{
	public class HtmlstripCharfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/htmlstrip-charfilter.asciidoc:21")]
		public void Line21()
		{
			// tag::affc7ff234dc3acccb2bf7dc51f54813[]
			var response0 = new SearchResponse<object>();
			// end::affc7ff234dc3acccb2bf7dc51f54813[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""char_filter"": [
			    ""html_strip""
			  ],
			  ""text"": ""<p>I&apos;m so <b>happy</b>!</p>""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/htmlstrip-charfilter.asciidoc:64")]
		public void Line64()
		{
			// tag::1e1db5745eefa984b2cf2f693dbb9943[]
			var response0 = new SearchResponse<object>();
			// end::1e1db5745eefa984b2cf2f693dbb9943[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""char_filter"": [
			            ""html_strip""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/htmlstrip-charfilter.asciidoc:106")]
		public void Line106()
		{
			// tag::b7f42ed5e0469dd79f7f599e447a7e25[]
			var response0 = new SearchResponse<object>();
			// end::b7f42ed5e0469dd79f7f599e447a7e25[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""char_filter"": [
			            ""my_custom_html_strip_char_filter""
			          ]
			        }
			      },
			      ""char_filter"": {
			        ""my_custom_html_strip_char_filter"": {
			          ""type"": ""html_strip"",
			          ""escaped_tags"": [
			            ""b""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
