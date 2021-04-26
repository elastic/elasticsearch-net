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
	public class MinhashTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/minhash-tokenfilter.asciidoc:134")]
		public void Line134()
		{
			// tag::7fc0039434c1833d7efd65d85870abfb[]
			var response0 = new SearchResponse<object>();
			// end::7fc0039434c1833d7efd65d85870abfb[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""my_shingle_filter"": {      <1>
			          ""type"": ""shingle"",
			          ""min_shingle_size"": 5,
			          ""max_shingle_size"": 5,
			          ""output_unigrams"": false
			        },
			        ""my_minhash_filter"": {
			          ""type"": ""min_hash"",
			          ""hash_count"": 1,          <2>
			          ""bucket_count"": 512,      <3>
			          ""hash_set_size"": 1,       <4>
			          ""with_rotation"": true     <5>
			        }
			      },
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""my_shingle_filter"",
			            ""my_minhash_filter""
			          ]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""fingerprint"": {
			        ""type"": ""text"",
			        ""analyzer"": ""my_analyzer""
			      }
			    }
			  }
			}");
		}
	}
}