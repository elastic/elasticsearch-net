// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class EdgengramTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/edgengram-tokenizer.asciidoc:28")]
		public void Line28()
		{
			// tag::a512e4dd8880ce0395937db1bab1d205[]
			var response0 = new SearchResponse<object>();
			// end::a512e4dd8880ce0395937db1bab1d205[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""edge_ngram"",
			  ""text"": ""Quick Fox""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/edgengram-tokenizer.asciidoc:144")]
		public void Line144()
		{
			// tag::a61389da4033bd7b73a63ff2ee258125[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a61389da4033bd7b73a63ff2ee258125[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""my_tokenizer""
			        }
			      },
			      ""tokenizer"": {
			        ""my_tokenizer"": {
			          ""type"": ""edge_ngram"",
			          ""min_gram"": 2,
			          ""max_gram"": 10,
			          ""token_chars"": [
			            ""letter"",
			            ""digit""
			          ]
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""2 Quick Foxes.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/edgengram-tokenizer.asciidoc:264")]
		public void Line264()
		{
			// tag::b8893e8f2b1aea4b093e0c4f037cfff7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::b8893e8f2b1aea4b093e0c4f037cfff7[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""autocomplete"": {
			          ""tokenizer"": ""autocomplete"",
			          ""filter"": [
			            ""lowercase""
			          ]
			        },
			        ""autocomplete_search"": {
			          ""tokenizer"": ""lowercase""
			        }
			      },
			      ""tokenizer"": {
			        ""autocomplete"": {
			          ""type"": ""edge_ngram"",
			          ""min_gram"": 2,
			          ""max_gram"": 10,
			          ""token_chars"": [
			            ""letter""
			          ]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"": ""text"",
			        ""analyzer"": ""autocomplete"",
			        ""search_analyzer"": ""autocomplete_search""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""title"": ""Quick Foxes"" \<1>
			}");

			response2.MatchesExample(@"POST my_index/_refresh");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""title"": {
			        ""query"": ""Quick Fo"", \<2>
			        ""operator"": ""and""
			      }
			    }
			  }
			}");
		}
	}
}
