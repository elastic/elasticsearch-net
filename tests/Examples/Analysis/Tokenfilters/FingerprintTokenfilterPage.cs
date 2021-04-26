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
	public class FingerprintTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/fingerprint-tokenfilter.asciidoc:35")]
		public void Line35()
		{
			// tag::df82a9cb21a7557f3ddba2509f76f608[]
			var response0 = new SearchResponse<object>();
			// end::df82a9cb21a7557f3ddba2509f76f608[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""fingerprint""],
			  ""text"" : ""zebra jumps over resting resting dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/fingerprint-tokenfilter.asciidoc:76")]
		public void Line76()
		{
			// tag::0eb2c1284a9829224913a860190580d8[]
			var response0 = new SearchResponse<object>();
			// end::0eb2c1284a9829224913a860190580d8[]

			response0.MatchesExample(@"PUT fingerprint_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_fingerprint"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""fingerprint"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/fingerprint-tokenfilter.asciidoc:117")]
		public void Line117()
		{
			// tag::1b0f40959a7a4d124372f2bd3f7eac85[]
			var response0 = new SearchResponse<object>();
			// end::1b0f40959a7a4d124372f2bd3f7eac85[]

			response0.MatchesExample(@"PUT custom_fingerprint_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""fingerprint_plus_concat"" ]
			        }
			      },
			      ""filter"": {
			        ""fingerprint_plus_concat"": {
			          ""type"": ""fingerprint"",
			          ""max_output_size"": 100,
			          ""separator"": ""+""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
