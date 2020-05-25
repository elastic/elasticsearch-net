// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class GetTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/get-transform.asciidoc:96")]
		public void Line96()
		{
			// tag::c65b00a285f510dcd2865aa3539b4e03[]
			var response0 = new SearchResponse<object>();
			// end::c65b00a285f510dcd2865aa3539b4e03[]

			response0.MatchesExample(@"GET _transform?size=10");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/apis/get-transform.asciidoc:105")]
		public void Line105()
		{
			// tag::c8ebbecc372bcfa5f4a6e7242395ab5e[]
			var response0 = new SearchResponse<object>();
			// end::c8ebbecc372bcfa5f4a6e7242395ab5e[]

			response0.MatchesExample(@"GET _transform/ecommerce_transform");
		}
	}
}
