using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis.Enrich
{
	public class PutEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line19()
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

		[U(Skip = "Example not implemented")]
		public void Line182()
		{
			// tag::0c8bce944c1189a8551e8dbd99c365f2[]
			var response0 = new SearchResponse<object>();
			// end::0c8bce944c1189a8551e8dbd99c365f2[]

			response0.MatchesExample(@"PUT /postal_codes
			{
			    ""mappings"": {
			        ""properties"": {
			            ""location"": {
			                ""type"": ""geo_shape""
			            },
			            ""postal_code"": {
			                ""type"": ""keyword""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line202()
		{
			// tag::497a51622ef123efc44e54ba2106385e[]
			var response0 = new SearchResponse<object>();
			// end::497a51622ef123efc44e54ba2106385e[]

			response0.MatchesExample(@"PUT /postal_codes/_doc/1?refresh=wait_for
			{
			    ""location"": {
			        ""type"": ""envelope"",
			        ""coordinates"": [[13.0, 53.0], [14.0, 52.0]]
			    },
			    ""postal_code"": ""96598""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line227()
		{
			// tag::99da25a3d63f98c16df47f21acbf37e7[]
			var response0 = new SearchResponse<object>();
			// end::99da25a3d63f98c16df47f21acbf37e7[]

			response0.MatchesExample(@"PUT /_enrich/policy/postal_policy
			{
			    ""geo_match"": {
			        ""indices"": ""postal_codes"",
			        ""match_field"": ""location"",
			        ""enrich_fields"": [""location"",""postal_code""]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line245()
		{
			// tag::207c04ccbdce0e8a289070a3b0a79ecb[]
			var response0 = new SearchResponse<object>();
			// end::207c04ccbdce0e8a289070a3b0a79ecb[]

			response0.MatchesExample(@"POST /_enrich/policy/postal_policy/_execute");
		}

		[U(Skip = "Example not implemented")]
		public void Line267()
		{
			// tag::83b4a737514a047d31f12f110bed0b5e[]
			var response0 = new SearchResponse<object>();
			// end::83b4a737514a047d31f12f110bed0b5e[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/postal_lookup
			{
			  ""description"": ""Enrich postal codes"",
			  ""processors"": [
			    {
			      ""enrich"": {
			        ""policy_name"": ""postal_policy"",
			        ""field"": ""geo_location"",
			        ""target_field"": ""geo_data"",
			        ""shape_relation"": ""INTERSECTS""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line292()
		{
			// tag::ad3b9d676187bebdb62e0f1de9a202e0[]
			var response0 = new SearchResponse<object>();
			// end::ad3b9d676187bebdb62e0f1de9a202e0[]

			response0.MatchesExample(@"PUT /users/_doc/0?pipeline=postal_lookup
			{
			    ""first_name"": ""Mardy"",
			    ""last_name"": ""Brown"",
			    ""geo_location"": ""POINT (13.5 52.5)""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line308()
		{
			// tag::a3f3c1f3f31dbd225da5fd14633bc4a0[]
			var response0 = new SearchResponse<object>();
			// end::a3f3c1f3f31dbd225da5fd14633bc4a0[]

			response0.MatchesExample(@"GET /users/_doc/0");
		}
	}
}