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
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class TrimTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/trim-tokenfilter.asciidoc:34")]
		public void Line34()
		{
			// tag::c318fde926842722825a51e5c9c326a9[]
			var response0 = new SearchResponse<object>();
			// end::c318fde926842722825a51e5c9c326a9[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""text"" : "" fox ""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/trim-tokenfilter.asciidoc:65")]
		public void Line65()
		{
			// tag::a3a14f7f0e80725f695a901a7e1d579d[]
			var response0 = new SearchResponse<object>();
			// end::a3a14f7f0e80725f695a901a7e1d579d[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""filter"" : [""trim""],
			  ""text"" : "" fox ""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/trim-tokenfilter.asciidoc:99")]
		public void Line99()
		{
			// tag::fd26bfdbe95b2d2db374385d12849f77[]
			var response0 = new SearchResponse<object>();
			// end::fd26bfdbe95b2d2db374385d12849f77[]

			response0.MatchesExample(@"PUT trim_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""keyword_trim"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""trim"" ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
