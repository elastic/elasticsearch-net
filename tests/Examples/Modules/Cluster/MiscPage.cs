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

namespace Examples.Modules.Cluster
{
	public class MiscPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/cluster/misc.asciidoc:78")]
		public void Line78()
		{
			// tag::4207219a892339e8f3abe0df8723dd27[]
			var response0 = new SearchResponse<object>();
			// end::4207219a892339e8f3abe0df8723dd27[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""persistent"": {
			    ""cluster.metadata.administrator"": ""sysadmin@example.com""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/cluster/misc.asciidoc:122")]
		public void Line122()
		{
			// tag::c3fa14da3d0b0f93fb59bb5386b7e776[]
			var response0 = new SearchResponse<object>();
			// end::c3fa14da3d0b0f93fb59bb5386b7e776[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""transient"": {
			    ""logger.org.elasticsearch.indices.recovery"": ""DEBUG""
			  }
			}");
		}
	}
}
