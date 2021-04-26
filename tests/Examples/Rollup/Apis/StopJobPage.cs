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

namespace Examples.Rollup.Apis
{
	public class StopJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/stop-job.asciidoc:76")]
		public void Line76()
		{
			// tag::07a5fdeb7805cec1d28ba288b28f5ff5[]
			var response0 = new SearchResponse<object>();
			// end::07a5fdeb7805cec1d28ba288b28f5ff5[]

			response0.MatchesExample(@"POST _rollup/job/sensor/_stop?wait_for_completion=true&timeout=10s");
		}
	}
}
