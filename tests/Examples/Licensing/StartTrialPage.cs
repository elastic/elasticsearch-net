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

namespace Examples.Licensing
{
	public class StartTrialPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/start-trial.asciidoc:45")]
		public void Line45()
		{
			// tag::37f1f2e75ed95308ae436bbbb8d5645e[]
			var response0 = new SearchResponse<object>();
			// end::37f1f2e75ed95308ae436bbbb8d5645e[]

			response0.MatchesExample(@"POST /_license/start_trial?acknowledge=true");
		}
	}
}
