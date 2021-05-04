// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class KeepWordsTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keep-words-tokenfilter.asciidoc:26")]
		public void Line26()
		{
			// tag::9a036a792be1d39af9fd0d1adb5f3402[]
			var response0 = new SearchResponse<object>();
			// end::9a036a792be1d39af9fd0d1adb5f3402[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""keep"",
			      ""keep_words"": [ ""dog"", ""elephant"", ""fox"" ]
			    }
			  ],
			  ""text"": ""the quick fox jumps over the lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keep-words-tokenfilter.asciidoc:118")]
		public void Line118()
		{
			// tag::642c0c1c76e9bf226cd216ebae9ab958[]
			var response0 = new SearchResponse<object>();
			// end::642c0c1c76e9bf226cd216ebae9ab958[]

			response0.MatchesExample(@"PUT keep_words_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_keep_word_array"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""keep_word_array"" ]
			        },
			        ""standard_keep_word_file"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""keep_word_file"" ]
			        }
			      },
			      ""filter"": {
			        ""keep_word_array"": {
			          ""type"": ""keep"",
			          ""keep_words"": [ ""one"", ""two"", ""three"" ]
			        },
			        ""keep_word_file"": {
			          ""type"": ""keep"",
			          ""keep_words_path"": ""analysis/example_word_list.txt""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
