// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class StopTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/stop-transform.asciidoc:95")]
		public void Line95()
		{
			// tag::654882f545eca8d7047695f867c63072[]
			var response0 = new SearchResponse<object>();
			// end::654882f545eca8d7047695f867c63072[]

			response0.MatchesExample(@"POST _transform/ecommerce_transform/_stop");
		}
	}
}
