// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace Examples.Search.Request
{
	public class StoredFieldsPage : ExampleBase
	{
		[U]
		[Description("search/request/stored-fields.asciidoc:13")]
		public void Line13()
		{
			// tag::2eeb3e55a7d3955e084bb369f1539009[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.StoredFields(sf => sf
					.Fields("user", "postDate")
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::2eeb3e55a7d3955e084bb369f1539009[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""stored_fields"" : [""user"", ""postDate""],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}

		[U]
		[Description("search/request/stored-fields.asciidoc:29")]
		public void Line29()
		{
			// tag::2af86a6ebbb834fbcf6fa7268f87a3a5[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.StoredFields(sf => sf)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::2af86a6ebbb834fbcf6fa7268f87a3a5[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""stored_fields"" : [],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["term"]["user"].ToLongFormTermQuery()));
		}

		[U]
		[Description("search/request/stored-fields.asciidoc:55")]
		public void Line55()
		{
			// tag::ccec437aed7a10d9111724ffd929fe00[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.StoredFields(sf => sf.Field("_none_"))
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::ccec437aed7a10d9111724ffd929fe00[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""stored_fields"": ""_none_"",
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json["stored_fields"] = new JArray("_none_");
				json["query"]["term"]["user"].ToLongFormTermQuery();
			}));
		}
	}
}
