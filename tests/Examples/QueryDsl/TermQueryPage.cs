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
using Examples.Models;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class TermQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/term-query.asciidoc:28")]
		public void Line28()
		{
			// tag::d0a8a938a2fa913b6fdbc871079a59dd[]
			var searchResponse = client.Search<Tweet>(s => s
				.Query(q => q
					.Term(f => f.User, "Kimchy", 1.0))
				.AllIndices()
			);
			// end::d0a8a938a2fa913b6fdbc871079a59dd[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""term"": {
			            ""user"": {
			                ""value"": ""Kimchy"",
			                ""boost"": 1.0
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/term-query.asciidoc:94")]
		public void Line94()
		{
			// tag::2a1de18774f9c68cafa169847832b2bc[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t.Name("full_text")))
				)
			);
			// end::2a1de18774f9c68cafa169847832b2bc[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""full_text"" : { ""type"" : ""text"" }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/term-query.asciidoc:113")]
		public void Line113()
		{
			// tag::d4b4cefba4318caeba7480187faf2b13[]
			var indexResponse = client.Index(new { full_text = "Quick Brown Foxes!" }, i => i.Index("my_index").Id(1));
			// end::d4b4cefba4318caeba7480187faf2b13[]

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""full_text"":   ""Quick Brown Foxes!""
			}");
		}

		[U]
		[Description("query-dsl/term-query.asciidoc:132")]
		public void Line132()
		{
			// tag::cdedd5f33f7e5f7acde561e97bff61de[]
			var searchResponse = client.Search<object>(s => s
				.Query(q => q
					.Term("full_text", "Quick Brown Foxes!"))
				.Index("my_index")
			);
			// end::cdedd5f33f7e5f7acde561e97bff61de[]

			searchResponse.MatchesExample(@"GET my_index/_search?pretty
			{
			  ""query"": {
			    ""term"": {
			      ""full_text"": ""Quick Brown Foxes!""
			    }
			  }
			}", (e, body) =>
			{
				body["query"]["term"]["full_text"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("query-dsl/term-query.asciidoc:165")]
		public void Line165()
		{
			// tag::a80f5db4357bb25b8704d374c18318ed[]
			var searchResponse = client.Search<object>(s => s
				.Query(q => q
					.Match(m => m.Field("full_text").Query("Quick Brown Foxes!")))
				.Index("my_index")
			);
			// end::a80f5db4357bb25b8704d374c18318ed[]

			searchResponse.MatchesExample(@"GET my_index/_search?pretty
			{
			  ""query"": {
			    ""match"": {
			      ""full_text"": ""Quick Brown Foxes!""
			    }
			  }
			}", (e, body) =>
			{
				body["query"]["match"]["full_text"].ToLongFormQuery();
			});
		}
	}
}
