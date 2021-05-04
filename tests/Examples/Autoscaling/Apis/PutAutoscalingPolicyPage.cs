// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Autoscaling.Apis
{
	public class PutAutoscalingPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/put-autoscaling-policy.asciidoc:15")]
		public void Line15()
		{
			// tag::4015b1eb9a15be7a3468bca965c52958[]
			var response0 = new SearchResponse<object>();
			// end::4015b1eb9a15be7a3468bca965c52958[]

			response0.MatchesExample(@"PUT /_autoscaling/policy/<name>
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
		[Description("autoscaling/apis/put-autoscaling-policy.asciidoc:47")]
		public void Line47()
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
	}
}
