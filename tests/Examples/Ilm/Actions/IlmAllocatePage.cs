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

namespace Examples.Ilm.Actions
{
	public class IlmAllocatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-allocate.asciidoc:54")]
		public void Line54()
		{
			// tag::1116c769f39f0c7fe86ec2a4871efcd5[]
			var response0 = new SearchResponse<object>();
			// end::1116c769f39f0c7fe86ec2a4871efcd5[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""allocate"" : {
			            ""number_of_replicas"" : 2
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-allocate.asciidoc:82")]
		public void Line82()
		{
			// tag::0518c673094fb18ecb491a3b78af4695[]
			var response0 = new SearchResponse<object>();
			// end::0518c673094fb18ecb491a3b78af4695[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""allocate"" : {
			            ""include"" : {
			              ""box_type"": ""hot,warm""
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-allocate.asciidoc:112")]
		public void Line112()
		{
			// tag::9d461ae140ddc018efd2650559800cd1[]
			var response0 = new SearchResponse<object>();
			// end::9d461ae140ddc018efd2650559800cd1[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""allocate"" : {
			            ""number_of_replicas"": 1,
			            ""require"" : {
			              ""box_type"": ""cold""
			            }
			        }
			        }
			      }
			    }
			  }
			}");
		}
	}
}