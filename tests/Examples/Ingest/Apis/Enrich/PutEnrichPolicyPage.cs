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
