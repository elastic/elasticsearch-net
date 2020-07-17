// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class GetIndexPage : ExampleBase
	{
		[U]
		[Description("indices/get-index.asciidoc:11")]
		public void Line11()
		{
			// tag::be8f28f31207b173de61be032fcf239c[]
			var getIndexResponse = client.Indices.Get("twitter");
			// end::be8f28f31207b173de61be032fcf239c[]

			getIndexResponse.MatchesExample(@"GET /twitter");
		}
	}
}
