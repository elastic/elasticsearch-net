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

namespace Examples.Analysis.Analyzers
{
	public class WhitespaceAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/whitespace-analyzer.asciidoc:14")]
		public void Line14()
		{
			// tag::262a778d754add491fbc9c721ac25bf0[]
			var response0 = new SearchResponse<object>();
			// end::262a778d754add491fbc9c721ac25bf0[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""whitespace"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/whitespace-analyzer.asciidoc:131")]
		public void Line131()
		{
			// tag::31aed390c30bd4f42a5c56253695e53f[]
			var response0 = new SearchResponse<object>();
			// end::31aed390c30bd4f42a5c56253695e53f[]

			response0.MatchesExample(@"PUT /whitespace_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""rebuilt_whitespace"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [         \<1>
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
