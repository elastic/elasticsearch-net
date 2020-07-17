// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class GetMappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/get-mapping.asciidoc:11")]
		public void Line11()
		{
			// tag::a8fba09a46b2c3524428aa3259b7124f[]
			var response0 = new SearchResponse<object>();
			// end::a8fba09a46b2c3524428aa3259b7124f[]

			response0.MatchesExample(@"GET /twitter/_mapping");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-mapping.asciidoc:68")]
		public void Line68()
		{
			// tag::cf02e3d8b371bd59f0224967c36330da[]
			var response0 = new SearchResponse<object>();
			// end::cf02e3d8b371bd59f0224967c36330da[]

			response0.MatchesExample(@"GET /twitter,kimchy/_mapping");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-mapping.asciidoc:78")]
		public void Line78()
		{
			// tag::5b7d6f1db88ca6f42c48fa3dbb4341e8[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5b7d6f1db88ca6f42c48fa3dbb4341e8[]

			response0.MatchesExample(@"GET /*/_mapping");

			response1.MatchesExample(@"GET /_all/_mapping");

			response2.MatchesExample(@"GET /_mapping");
		}
	}
}
