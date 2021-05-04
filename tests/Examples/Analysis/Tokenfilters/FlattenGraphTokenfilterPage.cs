// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class FlattenGraphTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/flatten-graph-tokenfilter.asciidoc:39")]
		public void Line39()
		{
			// tag::2c27a8eb6528126f37a843d434cd88b6[]
			var response0 = new SearchResponse<object>();
			// end::2c27a8eb6528126f37a843d434cd88b6[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""synonym_graph"",
			      ""synonyms"": [ ""dns, domain name system"" ]
			    }
			  ],
			  ""text"": ""domain name system is fragile""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/flatten-graph-tokenfilter.asciidoc:118")]
		public void Line118()
		{
			// tag::ef10e8d07d9fae945e035d5dee1e9754[]
			var response0 = new SearchResponse<object>();
			// end::ef10e8d07d9fae945e035d5dee1e9754[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""synonym_graph"",
			      ""synonyms"": [ ""dns, domain name system"" ]
			    },
			    ""flatten_graph""
			  ],
			  ""text"": ""domain name system is fragile""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/flatten-graph-tokenfilter.asciidoc:203")]
		public void Line203()
		{
			// tag::4b6312348785cb57de3467f82ec252a5[]
			var response0 = new SearchResponse<object>();
			// end::4b6312348785cb57de3467f82ec252a5[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_index_analyzer"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""my_custom_word_delimiter_graph_filter"",
			            ""flatten_graph""
			          ]
			        }
			      },
			      ""filter"": {
			        ""my_custom_word_delimiter_graph_filter"": {
			          ""type"": ""word_delimiter_graph"",
			          ""catenate_all"": true
			        }
			      }
			    }
			  }
			}");
		}
	}
}
