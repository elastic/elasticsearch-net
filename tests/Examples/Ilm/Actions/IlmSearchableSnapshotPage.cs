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
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmSearchableSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-searchable-snapshot.asciidoc:36")]
		public void Line36()
		{
			// tag::3f2e5132e35b9e8b3203a4a0541cf0d4[]
			var response0 = new SearchResponse<object>();
			// end::3f2e5132e35b9e8b3203a4a0541cf0d4[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""cold"": {
			        ""actions"": {
			          ""searchable_snapshot"" : {
			            ""snapshot_repository"" : ""backing_repo""
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}