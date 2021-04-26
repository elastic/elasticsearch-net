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
	public class UniqueTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/unique-tokenfilter.asciidoc:26")]
		public void Line26()
		{
			// tag::50d5c5b7e8ed9a95b8d9a25a32a77425[]
			var response0 = new SearchResponse<object>();
			// end::50d5c5b7e8ed9a95b8d9a25a32a77425[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""unique""],
			  ""text"" : ""the quick fox jumps the lazy fox""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/unique-tokenfilter.asciidoc:95")]
		public void Line95()
		{
			// tag::a428d518162918733d49261ffd65cfc1[]
			var response0 = new SearchResponse<object>();
			// end::a428d518162918733d49261ffd65cfc1[]

			response0.MatchesExample(@"PUT custom_unique_example
			{
			  ""settings"" : {
			    ""analysis"" : {
			      ""analyzer"" : {
			        ""standard_truncate"" : {
			        ""tokenizer"" : ""standard"",
			        ""filter"" : [""unique""]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/unique-tokenfilter.asciidoc:130")]
		public void Line130()
		{
			// tag::6e1157f3184fa192d47a3d0e3ea17a6c[]
			var response0 = new SearchResponse<object>();
			// end::6e1157f3184fa192d47a3d0e3ea17a6c[]

			response0.MatchesExample(@"PUT letter_unique_pos_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""letter_unique_pos"": {
			          ""tokenizer"": ""letter"",
			          ""filter"": [ ""unique_pos"" ]
			        }
			      },
			      ""filter"": {
			        ""unique_pos"": {
			          ""type"": ""unique"",
			          ""only_on_same_position"": true
			        }
			      }
			    }
			  }
			}");
		}
	}
}
