// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class ClassicTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/classic-tokenizer.asciidoc:25")]
		public void Line25()
		{
			// tag::c6d39d22188dc7bbfdad811a94cbcc2b[]
			var response0 = new SearchResponse<object>();
			// end::c6d39d22188dc7bbfdad811a94cbcc2b[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""classic"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/classic-tokenizer.asciidoc:148")]
		public void Line148()
		{
			// tag::326f5bc3013c80c2ee005c676a877ecf[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::326f5bc3013c80c2ee005c676a877ecf[]

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
			          ""type"": ""classic"",
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
