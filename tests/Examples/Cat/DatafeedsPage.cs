// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class DatafeedsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/datafeeds.asciidoc:118")]
		public void Line118()
		{
			// tag::4f57307b38318e9d8416923e12ff47ed[]
			var response0 = new SearchResponse<object>();
			// end::4f57307b38318e9d8416923e12ff47ed[]

			response0.MatchesExample(@"GET _cat/ml/datafeeds?v");
		}
	}
}
