// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class KstemTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/kstem-tokenfilter.asciidoc:29")]
		public void Line29()
		{
			// tag::68a891f609ca3a379d2d64e4914f3067[]
			var response0 = new SearchResponse<object>();
			// end::68a891f609ca3a379d2d64e4914f3067[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [ ""kstem"" ],
			  ""text"": ""the foxes jumping quickly""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/kstem-tokenfilter.asciidoc:98")]
		public void Line98()
		{
			// tag::e56ea22e3555d7c6de248e0327200b2e[]
			var response0 = new SearchResponse<object>();
			// end::e56ea22e3555d7c6de248e0327200b2e[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [
			            ""lowercase"",
			            ""kstem""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}