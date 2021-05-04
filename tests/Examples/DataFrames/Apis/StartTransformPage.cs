// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class StartTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line65()
		{
			// tag::811a0ff3a0e65bbb869c5654a47892cd[]
			var response0 = new SearchResponse<object>();
			// end::811a0ff3a0e65bbb869c5654a47892cd[]

			response0.MatchesExample(@"POST _data_frame/transforms/ecommerce_transform/_start");
		}
	}
}
