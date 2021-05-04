// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class WordDelimiterGraphTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-graph-tokenfilter.asciidoc:47")]
		public void Line47()
		{
			// tag::ffcf80e1094aa2d774f56f6b0bc54827[]
			var response0 = new SearchResponse<object>();
			// end::ffcf80e1094aa2d774f56f6b0bc54827[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""filter"": [ ""word_delimiter_graph"" ],
			  ""text"": ""Neil's-Super-Duper-XL500--42+AutoCoder""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-graph-tokenfilter.asciidoc:137")]
		public void Line137()
		{
			// tag::4620e88aa70944db528af43fead2b9ab[]
			var response0 = new SearchResponse<object>();
			// end::4620e88aa70944db528af43fead2b9ab[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""word_delimiter_graph"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-graph-tokenfilter.asciidoc:404")]
		public void Line404()
		{
			// tag::aee2ef858cd6bcc75ef97563cbe5ca5f[]
			var response0 = new SearchResponse<object>();
			// end::aee2ef858cd6bcc75ef97563cbe5ca5f[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""my_custom_word_delimiter_graph_filter"" ]
			        }
			      },
			      ""filter"": {
			        ""my_custom_word_delimiter_graph_filter"": {
			          ""type"": ""word_delimiter_graph"",
			          ""type_table"": [ ""- => ALPHA"" ],
			          ""split_on_case_change"": false,
			          ""split_on_numerics"": false,
			          ""stem_english_possessive"": true
			        }
			      }
			    }
			  }
			}");
		}
	}
}
