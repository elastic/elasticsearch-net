// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class TemplateExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/template-exists.asciidoc:12")]
		public void Line12()
		{
			// tag::a6be6c1cb4a556866fdccb0dee2f1dea[]
			var response0 = new SearchResponse<object>();
			// end::a6be6c1cb4a556866fdccb0dee2f1dea[]

			response0.MatchesExample(@"HEAD /_template/template_1");
		}
	}
}
