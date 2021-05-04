// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
