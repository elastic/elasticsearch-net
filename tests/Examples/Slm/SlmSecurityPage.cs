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

namespace Examples.Slm
{
	public class SlmSecurityPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/slm-security.asciidoc:25")]
		public void Line25()
		{
			// tag::1d7abeea98f6ed64cc8371794c90a921[]
			var response0 = new SearchResponse<object>();
			// end::1d7abeea98f6ed64cc8371794c90a921[]

			response0.MatchesExample(@"POST /_security/role/slm-admin
			{
			  ""cluster"": [""manage_slm"", ""cluster:admin/snapshot/*""],
			  ""indices"": [
			    {
			      ""names"": ["".slm-history-*""],
			      ""privileges"": [""all""]
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/slm-security.asciidoc:46")]
		public void Line46()
		{
			// tag::ef76d0e4cdc2881c161a5557a98a3446[]
			var response0 = new SearchResponse<object>();
			// end::ef76d0e4cdc2881c161a5557a98a3446[]

			response0.MatchesExample(@"POST /_security/role/slm-read-only
			{
			  ""cluster"": [""read_slm""],
			  ""indices"": [
			    {
			      ""names"": ["".slm-history-*""],
			      ""privileges"": [""read""]
			    }
			  ]
			}");
		}
	}
}