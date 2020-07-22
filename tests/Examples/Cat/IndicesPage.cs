// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class IndicesPage : ExampleBase
	{
		[U]
		[Description("cat/indices.asciidoc:100")]
		public void Line100()
		{
			// tag::073539a7e38be3cdf13008330b6a536a[]
			var catResponse = client.Cat.Indices(c => c
				.Index("twi*")
				.Verbose()
				.SortByColumns("index")
			);
			// end::073539a7e38be3cdf13008330b6a536a[]

			catResponse.MatchesExample(@"GET /_cat/indices/twi*?v&s=index");
		}
	}
}
