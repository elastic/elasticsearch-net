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

namespace Examples.Analysis.Tokenfilters
{
	public class LowercaseTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/lowercase-tokenfilter.asciidoc:20")]
		public void Line20()
		{
			// tag::aa3284717241ed79d3d1d3bdbbdce598[]
			var response0 = new SearchResponse<object>();
			// end::aa3284717241ed79d3d1d3bdbbdce598[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""lowercase""],
			  ""text"" : ""THE Quick FoX JUMPs""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/lowercase-tokenfilter.asciidoc:82")]
		public void Line82()
		{
			// tag::bf173db2ec48059c47eb8a7268545add[]
			var response0 = new SearchResponse<object>();
			// end::bf173db2ec48059c47eb8a7268545add[]

			response0.MatchesExample(@"PUT lowercase_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""whitespace_lowercase"" : {
			                    ""tokenizer"" : ""whitespace"",
			                    ""filter"" : [""lowercase""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/lowercase-tokenfilter.asciidoc:131")]
		public void Line131()
		{
			// tag::f268416813befd13c604642c6fe6eda9[]
			var response0 = new SearchResponse<object>();
			// end::f268416813befd13c604642c6fe6eda9[]

			response0.MatchesExample(@"PUT custom_lowercase_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""greek_lowercase_example"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""greek_lowercase""]
			        }
			      },
			      ""filter"": {
			        ""greek_lowercase"": {
			          ""type"": ""lowercase"",
			          ""language"": ""greek""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
