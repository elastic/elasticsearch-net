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

namespace Examples.Cluster
{
	public class PendingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::aa814309ad5f1630886ba75255b444f5[]
			var response0 = new SearchResponse<object>();
			// end::aa814309ad5f1630886ba75255b444f5[]

			response0.MatchesExample(@"GET /_cluster/pending_tasks");
		}
	}
}
