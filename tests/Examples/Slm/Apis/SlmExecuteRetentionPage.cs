// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm.Apis
{
	public class SlmExecuteRetentionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-execute-retention.asciidoc:35")]
		public void Line35()
		{
			// tag::e71d300cd87f09a9527cf45395dd7eb1[]
			var response0 = new SearchResponse<object>();
			// end::e71d300cd87f09a9527cf45395dd7eb1[]

			response0.MatchesExample(@"POST /_slm/_execute_retention");
		}
	}
}
