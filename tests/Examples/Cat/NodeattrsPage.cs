// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class NodeattrsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/nodeattrs.asciidoc:69")]
		public void Line69()
		{
			// tag::e20e2e6f949ac660a77840a9263fadef[]
			var response0 = new SearchResponse<object>();
			// end::e20e2e6f949ac660a77840a9263fadef[]

			response0.MatchesExample(@"GET /_cat/nodeattrs?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/nodeattrs.asciidoc:100")]
		public void Line100()
		{
			// tag::0c69c638073cc8518187b678dd33443c[]
			var response0 = new SearchResponse<object>();
			// end::0c69c638073cc8518187b678dd33443c[]

			response0.MatchesExample(@"GET /_cat/nodeattrs?v&h=name,pid,attr,value");
		}
	}
}
