// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.Indices
{
	public class DeleteIndexPage : ExampleBase
	{
		[U]
		[Description("indices/delete-index.asciidoc:10")]
		public void Line10()
		{
			// tag::98f14fddddea54a7d6149ab7b92e099d[]
			var deleteIndexResponse = client.DeleteIndex("twitter");
			// end::98f14fddddea54a7d6149ab7b92e099d[]

			deleteIndexResponse.MatchesExample(@"DELETE /twitter");
		}
	}
}
