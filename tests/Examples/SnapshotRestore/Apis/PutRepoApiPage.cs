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

namespace Examples.SnapshotRestore.Apis
{
	public class PutRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/put-repo-api.asciidoc:10")]
		public void Line10()
		{
			// tag::89b72dd7f747f6297c2b089e8bc807be[]
			var response0 = new SearchResponse<object>();
			// end::89b72dd7f747f6297c2b089e8bc807be[]

			response0.MatchesExample(@"PUT /_snapshot/my_repository
			{
			  ""type"": ""fs"",
			  ""settings"": {
			    ""location"": ""my_backup_location""
			  }
			}");
		}
	}
}