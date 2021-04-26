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

namespace Examples.Autoscaling.Apis
{
	public class GetAutoscalingPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/get-autoscaling-policy.asciidoc:15")]
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
		[Description("autoscaling/apis/get-autoscaling-policy.asciidoc:29")]
		public void Line29()
		{
			// tag::fb4799d2fe4011bf6084f89d97d9a4a5[]
			var response0 = new SearchResponse<object>();
			// end::fb4799d2fe4011bf6084f89d97d9a4a5[]

			response0.MatchesExample(@"GET /_autoscaling/policy/<name>");
		}

		[U(Skip = "Example not implemented")]
		[Description("autoscaling/apis/get-autoscaling-policy.asciidoc:52")]
		public void Line52()
		{
			// tag::0ea146b178561bc8b9002bed8a35641f[]
			var response0 = new SearchResponse<object>();
			// end::0ea146b178561bc8b9002bed8a35641f[]

			response0.MatchesExample(@"GET /_autoscaling/policy/my_autoscaling_policy");
		}
	}
}
