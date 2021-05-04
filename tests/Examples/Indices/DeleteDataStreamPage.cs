// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class DeleteDataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/delete-data-stream.asciidoc:26")]
		public void Line26()
		{
			// tag::f823e4b87ed181b27f73ebc51351f0ee[]
			var response0 = new SearchResponse<object>();
			// end::f823e4b87ed181b27f73ebc51351f0ee[]

			response0.MatchesExample(@"DELETE /_data_stream/my-data-stream");
		}
	}
}