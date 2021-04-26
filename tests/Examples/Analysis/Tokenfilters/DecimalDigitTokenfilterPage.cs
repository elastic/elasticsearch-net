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
	public class DecimalDigitTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/decimal-digit-tokenfilter.asciidoc:20")]
		public void Line20()
		{
			// tag::a21319c9eff1ac47d7fe7490f1ef2efa[]
			var response0 = new SearchResponse<object>();
			// end::a21319c9eff1ac47d7fe7490f1ef2efa[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""decimal_digit""],
			  ""text"" : ""१-one two-२ ३""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/decimal-digit-tokenfilter.asciidoc:75")]
		public void Line75()
		{
			// tag::121b8bc28620095dfa570a989bcdb04e[]
			var response0 = new SearchResponse<object>();
			// end::121b8bc28620095dfa570a989bcdb04e[]

			response0.MatchesExample(@"PUT /decimal_digit_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""whitespace_decimal_digit"" : {
			                    ""tokenizer"" : ""whitespace"",
			                    ""filter"" : [""decimal_digit""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}
