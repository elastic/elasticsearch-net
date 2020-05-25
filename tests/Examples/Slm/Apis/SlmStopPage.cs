// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm.Apis
{
	public class SlmStopPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-stop.asciidoc:48")]
		public void Line48()
		{
			// tag::41195ef13af0465cdee1ae18f6c00fde[]
			var response0 = new SearchResponse<object>();
			// end::41195ef13af0465cdee1ae18f6c00fde[]

			response0.MatchesExample(@"POST _slm/stop");
		}
	}
}
