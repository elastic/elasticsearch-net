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
	public class SimpleAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/simple-analyzer.asciidoc:15")]
		public void Line15()
		{
			// tag::1ea24f67fbbb6293d53caf2fe0c4b984[]
			var response0 = new SearchResponse<object>();
			// end::1ea24f67fbbb6293d53caf2fe0c4b984[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""simple"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/simple-analyzer.asciidoc:134")]
		public void Line134()
		{
			// tag::27bb04d77cbaab09d25fed6dec70835e[]
			var response0 = new SearchResponse<object>();
			// end::27bb04d77cbaab09d25fed6dec70835e[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_simple_analyzer"": {
			          ""tokenizer"": ""lowercase"",
			          ""filter"": [                          <1>
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
