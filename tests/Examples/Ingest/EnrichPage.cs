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

namespace Examples.Ingest
{
	public class EnrichPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:321")]
		public void Line321()
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
		[Description("ingest/enrich.asciidoc:340")]
		public void Line340()
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
		[Description("ingest/enrich.asciidoc:362")]
		public void Line362()
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
		[Description("ingest/enrich.asciidoc:378")]
		public void Line378()
		{
			// tag::207c04ccbdce0e8a289070a3b0a79ecb[]
			var response0 = new SearchResponse<object>();
			// end::207c04ccbdce0e8a289070a3b0a79ecb[]

			response0.MatchesExample(@"POST /_enrich/policy/postal_policy/_execute");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:397")]
		public void Line397()
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
		[Description("ingest/enrich.asciidoc:419")]
		public void Line419()
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
		[Description("ingest/enrich.asciidoc:433")]
		public void Line433()
		{
			// tag::a3f3c1f3f31dbd225da5fd14633bc4a0[]
			var response0 = new SearchResponse<object>();
			// end::a3f3c1f3f31dbd225da5fd14633bc4a0[]

			response0.MatchesExample(@"GET /users/_doc/0");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:494")]
		public void Line494()
		{
			// tag::927dd38daa489175a5008799452e870a[]
			var response0 = new SearchResponse<object>();
			// end::927dd38daa489175a5008799452e870a[]

			response0.MatchesExample(@"PUT /users/_doc/1?refresh=wait_for
			{
			    ""email"": ""mardy.brown@asciidocsmith.com"",
			    ""first_name"": ""Mardy"",
			    ""last_name"": ""Brown"",
			    ""city"": ""New Orleans"",
			    ""county"": ""Orleans"",
			    ""state"": ""LA"",
			    ""zip"": 70116,
			    ""web"": ""mardy.asciidocsmith.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:518")]
		public void Line518()
		{
			// tag::9ab4e8a564e13475cb3a0376be56bb8e[]
			var response0 = new SearchResponse<object>();
			// end::9ab4e8a564e13475cb3a0376be56bb8e[]

			response0.MatchesExample(@"PUT /_enrich/policy/users-policy
			{
			    ""match"": {
			        ""indices"": ""users"",
			        ""match_field"": ""email"",
			        ""enrich_fields"": [""first_name"", ""last_name"", ""city"", ""zip"", ""state""]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:534")]
		public void Line534()
		{
			// tag::af4f82ce86672a9bafd834f334c8e1c9[]
			var response0 = new SearchResponse<object>();
			// end::af4f82ce86672a9bafd834f334c8e1c9[]

			response0.MatchesExample(@"POST /_enrich/policy/users-policy/_execute");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:551")]
		public void Line551()
		{
			// tag::958b661d89b2beeb0cfefe8edbe3e408[]
			var response0 = new SearchResponse<object>();
			// end::958b661d89b2beeb0cfefe8edbe3e408[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/user_lookup
			{
			  ""description"" : ""Enriching user details to messages"",
			  ""processors"" : [
			    {
			      ""enrich"" : {
			        ""policy_name"": ""users-policy"",
			        ""field"" : ""email"",
			        ""target_field"": ""user"",
			        ""max_matches"": ""1""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:573")]
		public void Line573()
		{
			// tag::7495d7e8d99e4f5ac8034988b706e09d[]
			var response0 = new SearchResponse<object>();
			// end::7495d7e8d99e4f5ac8034988b706e09d[]

			response0.MatchesExample(@"PUT /my_index/_doc/my_id?pipeline=user_lookup
			{
			  ""email"": ""mardy.brown@asciidocsmith.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/enrich.asciidoc:585")]
		public void Line585()
		{
			// tag::ce20ab8067b6e4ad68e8ad7a5a0b73fd[]
			var response0 = new SearchResponse<object>();
			// end::ce20ab8067b6e4ad68e8ad7a5a0b73fd[]

			response0.MatchesExample(@"GET /my_index/_doc/my_id");
		}
	}
}
