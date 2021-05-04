// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmWaitForSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-wait-for-snapshot.asciidoc:21")]
		public void Line21()
		{
			// tag::83062a543163370328cf2e21a68c1bd3[]
			var response0 = new SearchResponse<object>();
			// end::83062a543163370328cf2e21a68c1bd3[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""delete"": {
			        ""actions"": {
			          ""wait_for_snapshot"" : {
			            ""policy"": ""slm-policy-name""
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}