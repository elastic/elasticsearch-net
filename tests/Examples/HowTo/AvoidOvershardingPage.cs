// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.HowTo
{
	public class AvoidOvershardingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("how-to/avoid-oversharding.asciidoc:136")]
		public void Line136()
		{
			// tag::3574da9ffe6372071d9bc9e79e3de3f0[]
			var response0 = new SearchResponse<object>();
			// end::3574da9ffe6372071d9bc9e79e3de3f0[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""foo-2019.10.*""
			  },
			  ""dest"": {
			    ""index"": ""foo-2019.10""
			  }
			}");
		}
	}
}