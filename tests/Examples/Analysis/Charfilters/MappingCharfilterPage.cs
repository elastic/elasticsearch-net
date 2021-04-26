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

namespace Examples.Analysis.Charfilters
{
	public class MappingCharfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/mapping-charfilter.asciidoc:26")]
		public void Line26()
		{
			// tag::02853293a5b7cd9cc7a886eb413bbeb6[]
			var response0 = new SearchResponse<object>();
			// end::02853293a5b7cd9cc7a886eb413bbeb6[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""char_filter"": [
			    {
			      ""type"": ""mapping"",
			      ""mappings"": [
			        ""٠ => 0"",
			        ""١ => 1"",
			        ""٢ => 2"",
			        ""٣ => 3"",
			        ""٤ => 4"",
			        ""٥ => 5"",
			        ""٦ => 6"",
			        ""٧ => 7"",
			        ""٨ => 8"",
			        ""٩ => 9""
			      ]
			    }
			  ],
			  ""text"": ""My license plate is ٢٥٠١٥""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/mapping-charfilter.asciidoc:109")]
		public void Line109()
		{
			// tag::9965aa724b58eff630d8347fd4453f5b[]
			var response0 = new SearchResponse<object>();
			// end::9965aa724b58eff630d8347fd4453f5b[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""char_filter"": [
			            ""my_mappings_char_filter""
			          ]
			        }
			      },
			      ""char_filter"": {
			        ""my_mappings_char_filter"": {
			          ""type"": ""mapping"",
			          ""mappings"": [
			            "":) => _happy_"",
			            "":( => _sad_""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/mapping-charfilter.asciidoc:141")]
		public void Line141()
		{
			// tag::7df01e191f592ecdcd3934cc1479391a[]
			var response0 = new SearchResponse<object>();
			// end::7df01e191f592ecdcd3934cc1479391a[]

			response0.MatchesExample(@"GET /my_index/_analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""char_filter"": [ ""my_mappings_char_filter"" ],
			  ""text"": ""I'm delighted about it :(""
			}");
		}
	}
}
