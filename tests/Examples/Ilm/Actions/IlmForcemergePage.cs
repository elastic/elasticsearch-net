// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmForcemergePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-forcemerge.asciidoc:37")]
		public void Line37()
		{
			// tag::eb5486d2fe4283475bf9e0e09280be16[]
			var response0 = new SearchResponse<object>();
			// end::eb5486d2fe4283475bf9e0e09280be16[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""forcemerge"" : {
			            ""max_num_segments"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}