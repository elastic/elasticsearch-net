// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Licensing
{
	public class DeleteLicensePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/delete-license.asciidoc:36")]
		public void Line36()
		{
			// tag::4f8a4ad49e2bca6784c88ede18a1a709[]
			var response0 = new SearchResponse<object>();
			// end::4f8a4ad49e2bca6784c88ede18a1a709[]

			response0.MatchesExample(@"DELETE /_license");
		}
	}
}
