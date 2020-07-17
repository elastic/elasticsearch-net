// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
