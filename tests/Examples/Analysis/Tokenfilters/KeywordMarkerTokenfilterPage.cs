// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class KeywordMarkerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keyword-marker-tokenfilter.asciidoc:35")]
		public void Line35()
		{
			// tag::26f237f9bf14e8b972cc33ff6aebefa2[]
			var response0 = new SearchResponse<object>();
			// end::26f237f9bf14e8b972cc33ff6aebefa2[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [ ""stemmer"" ],
			  ""text"": ""fox running and jumping""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keyword-marker-tokenfilter.asciidoc:95")]
		public void Line95()
		{
			// tag::5302f4f2bcc0f400ff71c791e6f68d7b[]
			var response0 = new SearchResponse<object>();
			// end::5302f4f2bcc0f400ff71c791e6f68d7b[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""keyword_marker"",
			      ""keywords"": [ ""jumping"" ]
			    },
			    ""stemmer""
			  ],
			  ""text"": ""fox running and jumping""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keyword-marker-tokenfilter.asciidoc:163")]
		public void Line163()
		{
			// tag::059e04aaf093379401f665c33ac796dc[]
			var response0 = new SearchResponse<object>();
			// end::059e04aaf093379401f665c33ac796dc[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""keyword_marker"",
			      ""keywords"": [ ""jumping"" ]
			    },
			    ""stemmer""
			  ],
			  ""text"": ""fox running and jumping"",
			  ""explain"": true,
			  ""attributes"": ""keyword""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keyword-marker-tokenfilter.asciidoc:365")]
		public void Line365()
		{
			// tag::f8c1fa91443573fcc0a5f2e22b7b4582[]
			var response0 = new SearchResponse<object>();
			// end::f8c1fa91443573fcc0a5f2e22b7b4582[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_analyzer"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""my_custom_keyword_marker_filter"",
			            ""porter_stem""
			          ]
			        }
			      },
			      ""filter"": {
			        ""my_custom_keyword_marker_filter"": {
			          ""type"": ""keyword_marker"",
			          ""keywords"": ""analysis/example_word_list.txt""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
