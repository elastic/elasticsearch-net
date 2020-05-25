// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class StartTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/start-transform.asciidoc:64")]
		public void Line64()
		{
			// tag::01bc0f2ed30eb3dd23511d01ce0ac6e1[]
			var response0 = new SearchResponse<object>();
			// end::01bc0f2ed30eb3dd23511d01ce0ac6e1[]

			response0.MatchesExample(@"POST _transform/ecommerce_transform/_start");
		}
	}
}
