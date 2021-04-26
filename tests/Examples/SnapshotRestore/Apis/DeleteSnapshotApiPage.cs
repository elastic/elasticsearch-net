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
	public class DeleteSnapshotApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/delete-snapshot-api.asciidoc:30")]
		public void Line30()
		{
			// tag::f04e1284d09ceb4443d67b2ef9c7f476[]
			var response0 = new SearchResponse<object>();
			// end::f04e1284d09ceb4443d67b2ef9c7f476[]

			response0.MatchesExample(@"DELETE /_snapshot/my_repository/my_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/delete-snapshot-api.asciidoc:69")]
		public void Line69()
		{
			// tag::6dd4c02fe3d6b800648a04d3e2d29fc1[]
			var response0 = new SearchResponse<object>();
			// end::6dd4c02fe3d6b800648a04d3e2d29fc1[]

			response0.MatchesExample(@"DELETE /_snapshot/my_repository/snapshot_2,snapshot_3");
		}
	}
}