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

namespace Examples.Ingest.Apis.Enrich
{
	public class GetEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/enrich/get-enrich-policy.asciidoc:44")]
		public void Line44()
		{
			// tag::af18f5c5fb2364ae23c6a14431820aba[]
			var response0 = new SearchResponse<object>();
			// end::af18f5c5fb2364ae23c6a14431820aba[]

			response0.MatchesExample(@"GET /_enrich/policy/my-policy");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/enrich/get-enrich-policy.asciidoc:125")]
		public void Line125()
		{
			// tag::8684589e31d96ab229e8c4feb4d704bb[]
			var response0 = new SearchResponse<object>();
			// end::8684589e31d96ab229e8c4feb4d704bb[]

			response0.MatchesExample(@"GET /_enrich/policy/my-policy,other-policy");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/enrich/get-enrich-policy.asciidoc:177")]
		public void Line177()
		{
			// tag::c97fd95ebdcf56cc973582e37f732ed2[]
			var response0 = new SearchResponse<object>();
			// end::c97fd95ebdcf56cc973582e37f732ed2[]

			response0.MatchesExample(@"GET /_enrich/policy");
		}
	}
}
