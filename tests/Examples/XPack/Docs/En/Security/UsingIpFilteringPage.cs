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

namespace Examples.XPack.Docs.En.Security
{
	public class UsingIpFilteringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/using-ip-filtering.asciidoc:118")]
		public void Line118()
		{
			// tag::d453198d420e84e4200f8f4f0ed6b83c[]
			var response0 = new SearchResponse<object>();
			// end::d453198d420e84e4200f8f4f0ed6b83c[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			    ""persistent"" : {
			        ""xpack.security.transport.filter.allow"" : ""172.16.0.0/24""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/using-ip-filtering.asciidoc:130")]
		public void Line130()
		{
			// tag::da9ffa564574978ea2b1e2bdb36bfd93[]
			var response0 = new SearchResponse<object>();
			// end::da9ffa564574978ea2b1e2bdb36bfd93[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			    ""persistent"" : {
			        ""xpack.security.transport.filter.enabled"" : false
			    }
			}");
		}
	}
}
