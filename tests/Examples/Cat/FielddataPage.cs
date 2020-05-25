// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class FielddataPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/fielddata.asciidoc:90")]
		public void Line90()
		{
			// tag::973f2d7fbff9f310b21108b31d7ad413[]
			var response0 = new SearchResponse<object>();
			// end::973f2d7fbff9f310b21108b31d7ad413[]

			response0.MatchesExample(@"GET /_cat/fielddata?v&fields=body");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/fielddata.asciidoc:114")]
		public void Line114()
		{
			// tag::62daf8e41b9e984d18d6cc51f247c7ad[]
			var response0 = new SearchResponse<object>();
			// end::62daf8e41b9e984d18d6cc51f247c7ad[]

			response0.MatchesExample(@"GET /_cat/fielddata/body,soul?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/fielddata.asciidoc:140")]
		public void Line140()
		{
			// tag::b26ff669b3c88fb0872fa0a923972f54[]
			var response0 = new SearchResponse<object>();
			// end::b26ff669b3c88fb0872fa0a923972f54[]

			response0.MatchesExample(@"GET /_cat/fielddata?v");
		}
	}
}
