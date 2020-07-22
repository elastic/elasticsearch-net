// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class StandardTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/standard-tokenizer.asciidoc:16")]
		public void Line16()
		{
			// tag::88a08d0b15ef41324f5c23db533d47d1[]
			var response0 = new SearchResponse<object>();
			// end::88a08d0b15ef41324f5c23db533d47d1[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/standard-tokenizer.asciidoc:139")]
		public void Line139()
		{
			// tag::7375d4fe72c848ee3b0a799fda8bb0f0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::7375d4fe72c848ee3b0a799fda8bb0f0[]

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
			          ""type"": ""standard"",
			          ""max_token_length"": 5
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}
