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

namespace Examples.Modules.Indices
{
	public class RequestCachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/indices/request_cache.asciidoc:47")]
		public void Line47()
		{
			// tag::00629ea43db6ee1704183170df085495[]
			var response0 = new SearchResponse<object>();
			// end::00629ea43db6ee1704183170df085495[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_cache/clear?request=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/indices/request_cache.asciidoc:59")]
		public void Line59()
		{
			// tag::adebfecf7485326e9f7fae9de9169abc[]
			var response0 = new SearchResponse<object>();
			// end::adebfecf7485326e9f7fae9de9169abc[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.requests.cache.enable"": false
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/indices/request_cache.asciidoc:72")]
		public void Line72()
		{
			// tag::f22e069bc0c6f9dae57e084c662a86fd[]
			var response0 = new SearchResponse<object>();
			// end::f22e069bc0c6f9dae57e084c662a86fd[]

			response0.MatchesExample(@"PUT /my_index/_settings
			{ ""index.requests.cache.enable"": true }");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/indices/request_cache.asciidoc:86")]
		public void Line86()
		{
			// tag::13e9c7cdd43161f1336c94fd70a0db0c[]
			var response0 = new SearchResponse<object>();
			// end::13e9c7cdd43161f1336c94fd70a0db0c[]

			response0.MatchesExample(@"GET /my_index/_search?request_cache=true
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""popular_colors"": {
			      ""terms"": {
			        ""field"": ""colors""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/indices/request_cache.asciidoc:139")]
		public void Line139()
		{
			// tag::36da9668fef56910370f16bfb772cc40[]
			var response0 = new SearchResponse<object>();
			// end::36da9668fef56910370f16bfb772cc40[]

			response0.MatchesExample(@"GET /_stats/request_cache?human");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/indices/request_cache.asciidoc:146")]
		public void Line146()
		{
			// tag::90631797c7fbda43902abf2cc0ea8304[]
			var response0 = new SearchResponse<object>();
			// end::90631797c7fbda43902abf2cc0ea8304[]

			response0.MatchesExample(@"GET /_nodes/stats/indices/request_cache?human");
		}
	}
}
