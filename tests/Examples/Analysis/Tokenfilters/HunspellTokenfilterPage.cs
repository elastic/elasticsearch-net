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
	public class HunspellTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/hunspell-tokenfilter.asciidoc:74")]
		public void Line74()
		{
			// tag::62f1ec1bb5cc5a9c2efd536a7474f549[]
			var response0 = new SearchResponse<object>();
			// end::62f1ec1bb5cc5a9c2efd536a7474f549[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""hunspell"",
			      ""locale"": ""en_US""
			    }
			  ],
			  ""text"": ""the foxes jumping quickly""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/hunspell-tokenfilter.asciidoc:203")]
		public void Line203()
		{
			// tag::6c42e05f48b2c0d58860a1f3a3f63829[]
			var response0 = new SearchResponse<object>();
			// end::6c42e05f48b2c0d58860a1f3a3f63829[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""en"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""my_en_US_dict_stemmer"" ]
			        }
			      },
			      ""filter"": {
			        ""my_en_US_dict_stemmer"": {
			          ""type"": ""hunspell"",
			          ""locale"": ""en_US"",
			          ""dedup"": false
			        }
			      }
			    }
			  }
			}");
		}
	}
}
