// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.IndexModules
{
	public class StorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/store.asciidoc:30")]
		public void Line30()
		{
			// tag::fba99da14d4323c91794703438979912[]
			var response0 = new SearchResponse<object>();
			// end::fba99da14d4323c91794703438979912[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.type"": ""hybridfs""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/store.asciidoc:120")]
		public void Line120()
		{
			// tag::9ba2e779fe3e9d12ed5fca1ba3f8be97[]
			var response0 = new SearchResponse<object>();
			// end::9ba2e779fe3e9d12ed5fca1ba3f8be97[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.preload"": [""nvd"", ""dvd""]
			  }
			}");
		}
	}
}
