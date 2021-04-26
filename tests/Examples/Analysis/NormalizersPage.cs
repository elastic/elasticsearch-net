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

namespace Examples.Analysis
{
	public class NormalizersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/normalizers.asciidoc:27")]
		public void Line27()
		{
			// tag::966ff3a4c5b61ed1a36d44c17ce06157[]
			var response0 = new SearchResponse<object>();
			// end::966ff3a4c5b61ed1a36d44c17ce06157[]

			response0.MatchesExample(@"PUT index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""char_filter"": {
			        ""quote"": {
			          ""type"": ""mapping"",
			          ""mappings"": [
			            ""« => \"""",
			            ""» => \""""
			          ]
			        }
			      },
			      ""normalizer"": {
			        ""my_normalizer"": {
			          ""type"": ""custom"",
			          ""char_filter"": [""quote""],
			          ""filter"": [""lowercase"", ""asciifolding""]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""keyword"",
			        ""normalizer"": ""my_normalizer""
			      }
			    }
			  }
			}");
		}
	}
}
