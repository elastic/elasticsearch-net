// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Licensing
{
	public class GetTrialStatusPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/get-trial-status.asciidoc:41")]
		public void Line41()
		{
			// tag::88cf60d3310a56d8ae12704abc05b565[]
			var response0 = new SearchResponse<object>();
			// end::88cf60d3310a56d8ae12704abc05b565[]

			response0.MatchesExample(@"GET /_license/trial_status");
		}
	}
}
