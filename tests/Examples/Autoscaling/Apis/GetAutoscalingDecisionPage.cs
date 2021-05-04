// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
