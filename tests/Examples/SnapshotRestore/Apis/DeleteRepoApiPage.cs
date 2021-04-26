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
	public class DeleteRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/delete-repo-api.asciidoc:27")]
		public void Line27()
		{
			// tag::ff56ded50c65998c70f3c5691ddc6f86[]
			var response0 = new SearchResponse<object>();
			// end::ff56ded50c65998c70f3c5691ddc6f86[]

			response0.MatchesExample(@"DELETE /_snapshot/my_repository");
		}
	}
}