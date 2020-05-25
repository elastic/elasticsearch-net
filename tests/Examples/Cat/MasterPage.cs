// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class MasterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/master.asciidoc:39")]
		public void Line39()
		{
			// tag::45bde49f35ffae3f3dabc77a592241b4[]
			var response0 = new SearchResponse<object>();
			// end::45bde49f35ffae3f3dabc77a592241b4[]

			response0.MatchesExample(@"GET /_cat/master?v");
		}
	}
}
