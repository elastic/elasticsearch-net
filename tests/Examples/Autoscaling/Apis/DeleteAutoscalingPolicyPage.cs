// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Autoscaling.Apis
{
	public class DeleteAutoscalingPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/delete-autoscaling-policy.asciidoc:15")]
		public void Line15()
		{
			// tag::e7e47d671e68046629a7969c55eed8b6[]
			var response0 = new SearchResponse<object>();
			// end::e7e47d671e68046629a7969c55eed8b6[]

			response0.MatchesExample(@"PUT /_autoscaling/policy/my_autoscaling_policy
			{
			  ""policy"": {
			    ""deciders"": {
			      ""always"": {
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/delete-autoscaling-policy.asciidoc:29")]
		public void Line29()
		{
			// tag::50a9623c153cabe64101efb633e10e6c[]
			var response0 = new SearchResponse<object>();
			// end::50a9623c153cabe64101efb633e10e6c[]

			response0.MatchesExample(@"DELETE /_autoscaling/policy/<name>");
		}

		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/delete-autoscaling-policy.asciidoc:52")]
		public void Line52()
		{
			// tag::b620ef4400d2f660fe2c67835938442c[]
			var response0 = new SearchResponse<object>();
			// end::b620ef4400d2f660fe2c67835938442c[]

			response0.MatchesExample(@"DELETE /_autoscaling/policy/my_autoscaling_policy");
		}
	}
}
