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
	public class DelimitedPayloadTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/delimited-payload-tokenfilter.asciidoc:47")]
		public void Line47()
		{
			// tag::7dc82f7d36686fd57a47e34cbda39a4e[]
			var response0 = new SearchResponse<object>();
			// end::7dc82f7d36686fd57a47e34cbda39a4e[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [""delimited_payload""],
			  ""text"": ""the|0 brown|10 fox|5 is|0 quick|10""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/delimited-payload-tokenfilter.asciidoc:120")]
		public void Line120()
		{
			// tag::d443db2755fde3b49ca3a9d296c4a96f[]
			var response0 = new SearchResponse<object>();
			// end::d443db2755fde3b49ca3a9d296c4a96f[]

			response0.MatchesExample(@"PUT delimited_payload
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_delimited_payload"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""delimited_payload"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/delimited-payload-tokenfilter.asciidoc:173")]
		public void Line173()
		{
			// tag::27f9f604e7a48799fa30529cbc0ff619[]
			var response0 = new SearchResponse<object>();
			// end::27f9f604e7a48799fa30529cbc0ff619[]

			response0.MatchesExample(@"PUT delimited_payload_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_plus_delimited"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""plus_delimited"" ]
			        }
			      },
			      ""filter"": {
			        ""plus_delimited"": {
			          ""type"": ""delimited_payload"",
			          ""delimiter"": ""+"",
			          ""encoding"": ""int""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/delimited-payload-tokenfilter.asciidoc:206")]
		public void Line206()
		{
			// tag::50c2cea2adbe9523458c2686ab11df54[]
			var response0 = new SearchResponse<object>();
			// end::50c2cea2adbe9523458c2686ab11df54[]

			response0.MatchesExample(@"PUT text_payloads
			{
			  ""mappings"": {
			    ""properties"": {
			      ""text"": {
			        ""type"": ""text"",
			        ""term_vector"": ""with_positions_payloads"",
			        ""analyzer"": ""payload_delimiter""
			      }
			    }
			  },
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""payload_delimiter"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""delimited_payload"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/delimited-payload-tokenfilter.asciidoc:234")]
		public void Line234()
		{
			// tag::d2f6fb271e97fde8685d7744e6718cc7[]
			var response0 = new SearchResponse<object>();
			// end::d2f6fb271e97fde8685d7744e6718cc7[]

			response0.MatchesExample(@"POST text_payloads/_doc/1
			{
			  ""text"": ""the|0 brown|3 fox|4 is|0 quick|10""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/delimited-payload-tokenfilter.asciidoc:246")]
		public void Line246()
		{
			// tag::b24a374c0ad264abbcacb5686f5ed61c[]
			var response0 = new SearchResponse<object>();
			// end::b24a374c0ad264abbcacb5686f5ed61c[]

			response0.MatchesExample(@"GET text_payloads/_termvectors/1
			{
			  ""fields"": [ ""text"" ],
			  ""payloads"": true
			}");
		}
	}
}
