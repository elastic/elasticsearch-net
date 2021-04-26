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

namespace Examples.Mapping.Types
{
	public class IpPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/ip.asciidoc:11")]
		public void Line11()
		{
			// tag::ef38d941f9d914c095e729046a2e2d95[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::ef38d941f9d914c095e729046a2e2d95[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""ip_addr"": {
			        ""type"": ""ip""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""ip_addr"": ""192.168.1.1""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""ip_addr"": ""192.168.0.0/16""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/ip.asciidoc:77")]
		public void Line77()
		{
			// tag::96d3e3ee5d410507ca6ffb64a7e3d88e[]
			var response0 = new SearchResponse<object>();
			// end::96d3e3ee5d410507ca6ffb64a7e3d88e[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""ip_addr"": ""192.168.0.0/16""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/ip.asciidoc:91")]
		public void Line91()
		{
			// tag::f880cf334c8d355edc3abf196d9a8b67[]
			var response0 = new SearchResponse<object>();
			// end::f880cf334c8d355edc3abf196d9a8b67[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""ip_addr"": ""2001:db8::/48""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/ip.asciidoc:108")]
		public void Line108()
		{
			// tag::db5fe7de772a7607b8d104cc35a6bc6c[]
			var response0 = new SearchResponse<object>();
			// end::db5fe7de772a7607b8d104cc35a6bc6c[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""query_string"" : {
			      ""query"": ""ip_addr:\""2001:db8::/48\""""
			    }
			  }
			}");
		}
	}
}
