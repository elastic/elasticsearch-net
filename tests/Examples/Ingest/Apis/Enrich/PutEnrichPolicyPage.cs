using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis.Enrich
{
	public class PutEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
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