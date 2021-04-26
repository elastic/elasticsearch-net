/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
