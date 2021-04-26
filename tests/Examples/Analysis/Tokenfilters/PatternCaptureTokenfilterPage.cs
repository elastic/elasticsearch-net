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
	public class PatternCaptureTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/pattern-capture-tokenfilter.asciidoc:51")]
		public void Line51()
		{
			// tag::f733b25cd4c448b226bb76862974eef2[]
			var response0 = new SearchResponse<object>();
			// end::f733b25cd4c448b226bb76862974eef2[]

			response0.MatchesExample(@"PUT test
			{
			   ""settings"" : {
			      ""analysis"" : {
			         ""filter"" : {
			            ""code"" : {
			               ""type"" : ""pattern_capture"",
			               ""preserve_original"" : true,
			               ""patterns"" : [
			                  ""(\\p{Ll}+|\\p{Lu}\\p{Ll}+|\\p{Lu}+)"",
			                  ""(\\d+)""
			               ]
			            }
			         },
			         ""analyzer"" : {
			            ""code"" : {
			               ""tokenizer"" : ""pattern"",
			               ""filter"" : [ ""code"", ""lowercase"" ]
			            }
			         }
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/pattern-capture-tokenfilter.asciidoc:91")]
		public void Line91()
		{
			// tag::080c34d8151d02b760571e3a2899fa97[]
			var response0 = new SearchResponse<object>();
			// end::080c34d8151d02b760571e3a2899fa97[]

			response0.MatchesExample(@"PUT test
			{
			   ""settings"" : {
			      ""analysis"" : {
			         ""filter"" : {
			            ""email"" : {
			               ""type"" : ""pattern_capture"",
			               ""preserve_original"" : true,
			               ""patterns"" : [
			                  ""([^@]+)"",
			                  ""(\\p{L}+)"",
			                  ""(\\d+)"",
			                  ""@(.+)""
			               ]
			            }
			         },
			         ""analyzer"" : {
			            ""email"" : {
			               ""tokenizer"" : ""uax_url_email"",
			               ""filter"" : [ ""email"", ""lowercase"",  ""unique"" ]
			            }
			         }
			      }
			   }
			}");
		}
	}
}
