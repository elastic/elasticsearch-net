/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
