// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Apis.Enrich
{
	public class PutEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/enrich/put-enrich-policy.asciidoc:26")]
		public void Line26()
		{
			// tag::e15a5bb869d24668207b9b4629744be4[]
			var response0 = new SearchResponse<object>();
			// end::e15a5bb869d24668207b9b4629744be4[]

			response0.MatchesExample(@"PUT /_enrich/policy/my-policy
			{
			    ""match"": {
			        ""indices"": ""users"",
			        ""match_field"": ""email"",
			        ""enrich_fields"": [""first_name"", ""last_name"", ""city"", ""zip"", ""state""]
			    }
			}");
		}
	}
}
