// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm.Apis
{
	public class MoveToStepPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/apis/move-to-step.asciidoc:145")]
		public void Line145()
		{
			// tag::e3c5f93b3c85e8519f801defc20b0ce0[]
			var response0 = new SearchResponse<object>();
			// end::e3c5f93b3c85e8519f801defc20b0ce0[]

			response0.MatchesExample(@"POST _ilm/move/my_index
			{
			  ""current_step"": { \<1>
			    ""phase"": ""new"",
			    ""action"": ""complete"",
			    ""name"": ""complete""
			  },
			  ""next_step"": { \<2>
			    ""phase"": ""warm"",
			    ""action"": ""forcemerge"",
			    ""name"": ""forcemerge""
			  }
			}");
		}
	}
}
