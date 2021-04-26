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
	public class AsciifoldingTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/asciifolding-tokenfilter.asciidoc:21")]
		public void Line21()
		{
			// tag::00d65f7b9daa1c6b18eedd8ace206bae[]
			var response0 = new SearchResponse<object>();
			// end::00d65f7b9daa1c6b18eedd8ace206bae[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""asciifolding""],
			  ""text"" : ""açaí à la carte""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/asciifolding-tokenfilter.asciidoc:83")]
		public void Line83()
		{
			// tag::a976bdf566730e35c5277740c1e3a7f2[]
			var response0 = new SearchResponse<object>();
			// end::a976bdf566730e35c5277740c1e3a7f2[]

			response0.MatchesExample(@"PUT /asciifold_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_asciifolding"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""asciifolding""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/asciifolding-tokenfilter.asciidoc:118")]
		public void Line118()
		{
			// tag::c5a0248213307f8e036a26e3294ad611[]
			var response0 = new SearchResponse<object>();
			// end::c5a0248213307f8e036a26e3294ad611[]

			response0.MatchesExample(@"PUT /asciifold_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_asciifolding"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""my_ascii_folding""]
			                }
			            },
			            ""filter"" : {
			                ""my_ascii_folding"" : {
			                    ""type"" : ""asciifolding"",
			                    ""preserve_original"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
