// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm.Apis
{
	public class SlmDeletePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-delete.asciidoc:66")]
		public void Line66()
		{
			// tag::1a1f3421717ff744ed83232729289bb0[]
			var response0 = new SearchResponse<object>();
			// end::1a1f3421717ff744ed83232729289bb0[]

			response0.MatchesExample(@"DELETE /_slm/policy/daily-snapshots");
		}
	}
}
