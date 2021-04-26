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
	public class GetSnapshotApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-snapshot-api.asciidoc:28")]
		public void Line28()
		{
			// tag::a811b82ba4632bdd9065829085188bc9[]
			var response0 = new SearchResponse<object>();
			// end::a811b82ba4632bdd9065829085188bc9[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/my_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-snapshot-api.asciidoc:194")]
		public void Line194()
		{
			// tag::bfb8a15cd05b43094ffbce8078bad3e1[]
			var response0 = new SearchResponse<object>();
			// end::bfb8a15cd05b43094ffbce8078bad3e1[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/snapshot_2");
		}
	}
}