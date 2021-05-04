// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Cat
{
	public class TransformsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/transforms.asciidoc:172")]
		public void Line172()
		{
			// tag::da8c3c635ea1101d6a1a5eb1db2ffebd[]
			var response0 = new SearchResponse<object>();
			// end::da8c3c635ea1101d6a1a5eb1db2ffebd[]

			response0.MatchesExample(@"GET /_cat/transforms?v&format=json");
		}
	}
}
