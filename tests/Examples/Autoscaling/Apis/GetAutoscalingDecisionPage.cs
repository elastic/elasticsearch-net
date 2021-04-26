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

namespace Examples.Autoscaling.Apis
{
	public class GetAutoscalingDecisionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/get-autoscaling-decision.asciidoc:15")]
		public void Line15()
		{
			// tag::0d142c34961983f6aab5f12965407bab[]
			var response0 = new SearchResponse<object>();
			// end::0d142c34961983f6aab5f12965407bab[]

			response0.MatchesExample(@"GET /_autoscaling/decision/");
		}

		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/get-autoscaling-decision.asciidoc:40")]
		public void Line40()
		{
			// tag::f1335bacda32b273ecc9603498eff65c[]
			var response0 = new SearchResponse<object>();
			// end::f1335bacda32b273ecc9603498eff65c[]

			response0.MatchesExample(@"GET /_autoscaling/decision");
		}
	}
}
