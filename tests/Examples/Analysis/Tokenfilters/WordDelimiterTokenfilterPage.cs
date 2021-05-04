// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class WordDelimiterTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-tokenfilter.asciidoc:58")]
		public void Line58()
		{
			// tag::c42bc6e74afc3d43cd032ec2bfd77385[]
			var response0 = new SearchResponse<object>();
			// end::c42bc6e74afc3d43cd032ec2bfd77385[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""filter"": [ ""word_delimiter"" ],
			  ""text"": ""Neil's-Super-Duper-XL500--42+AutoCoder""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-tokenfilter.asciidoc:148")]
		public void Line148()
		{
			// tag::0daa91e512cb2009925b5efb49e926f7[]
			var response0 = new SearchResponse<object>();
			// end::0daa91e512cb2009925b5efb49e926f7[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""word_delimiter"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-tokenfilter.asciidoc:359")]
		public void Line359()
		{
			// tag::83a1cb2fd02a76f2766d7b186002859e[]
			var response0 = new SearchResponse<object>();
			// end::83a1cb2fd02a76f2766d7b186002859e[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""my_custom_word_delimiter_filter"" ]
			        }
			      },
			      ""filter"": {
			        ""my_custom_word_delimiter_filter"": {
			          ""type"": ""word_delimiter"",
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
