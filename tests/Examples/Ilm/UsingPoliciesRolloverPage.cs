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

namespace Examples.Ilm
{
	public class UsingPoliciesRolloverPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/using-policies-rollover.asciidoc:38")]
		public void Line38()
		{
			// tag::aed01ec7b6368fa2c8f86434e176c907[]
			var response0 = new SearchResponse<object>();
			// end::aed01ec7b6368fa2c8f86434e176c907[]

			response0.MatchesExample(@"PUT /_ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/using-policies-rollover.asciidoc:66")]
		public void Line66()
		{
			// tag::f29c02d259065033bd557519d1b21481[]
			var response0 = new SearchResponse<object>();
			// end::f29c02d259065033bd557519d1b21481[]

			response0.MatchesExample(@"PUT _template/my_template
			{
			  ""index_patterns"": [""test-*""], \<1>
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""my_policy"", \<2>
			    ""index.lifecycle.rollover_alias"": ""test-alias"" \<3>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/using-policies-rollover.asciidoc:97")]
		public void Line97()
		{
			// tag::454e0e11e2bbb4718109a53662f8c45d[]
			var response0 = new SearchResponse<object>();
			// end::454e0e11e2bbb4718109a53662f8c45d[]

			response0.MatchesExample(@"PUT test-000001 \<1>
			{
			  ""aliases"": {
			    ""test-alias"":{
			      ""is_write_index"": true \<2>
			    }
			  }
			}");
		}
	}
}
