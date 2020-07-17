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
		[U]
		[Description("indices/get-mapping.asciidoc:11")]
		public void Line11()
		{
			// tag::a8fba09a46b2c3524428aa3259b7124f[]
			var getMappingResponse = client.Indices.GetMapping<object>(m => m
				.Index("twitter")
			);
			// end::a8fba09a46b2c3524428aa3259b7124f[]

			getMappingResponse.MatchesExample(@"GET /twitter/_mapping");
		}

		[U]
		[Description("indices/get-mapping.asciidoc:68")]
		public void Line68()
		{
			// tag::cf02e3d8b371bd59f0224967c36330da[]
			var getMappingResponse = client.Indices.GetMapping<object>(m => m
				.Index("twitter,kimchy")
			);
			// end::cf02e3d8b371bd59f0224967c36330da[]

			getMappingResponse.MatchesExample(@"GET /twitter,kimchy/_mapping");
		}

		[U]
		[Description("indices/get-mapping.asciidoc:78")]
		public void Line78()
		{
			// tag::5b7d6f1db88ca6f42c48fa3dbb4341e8[]
			var getMappingResponse1 = client.Indices.GetMapping<object>(m => m
				.Index("*")
			);

			var getMappingResponse2 = client.Indices.GetMapping<object>(m => m
				.AllIndices()
			);

			var getMappingResponse3 = client.Indices.GetMapping<object>(m => m
				.Index("")
			);
			// end::5b7d6f1db88ca6f42c48fa3dbb4341e8[]

			getMappingResponse1.MatchesExample(@"GET /*/_mapping");

			getMappingResponse2.MatchesExample(@"GET /_all/_mapping");

			getMappingResponse3.MatchesExample(@"GET /_mapping");
		}
	}
}
