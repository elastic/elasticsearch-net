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
using Examples.Models;
using Newtonsoft.Json.Linq;

namespace Examples.Search
{
	public class SuggestersPage : ExampleBase
	{
		[U]
		[Description("search/suggesters.asciidoc:8")]
		public void Line8()
		{
			// tag::626f8c4b3e2cd3d9beaa63a7f5799d7a[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Message)
						.Query("tring out Elasticsearch")
					)
				)
				.Suggest(su => su
					.Term("my-suggestion", t => t
						.Text("tring out Elasticsearch")
						.Field(f => f.Message)
					)
				)
			);
			// end::626f8c4b3e2cd3d9beaa63a7f5799d7a[]

			searchResponse.MatchesExample(@"POST twitter/_search
			{
			  ""query"" : {
			    ""match"": {
			      ""message"": ""tring out Elasticsearch""
			    }
			  },
			  ""suggest"" : {
			    ""my-suggestion"" : {
			      ""text"" : ""tring out Elasticsearch"",
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    }
			  }
			}", (e, b) => b["query"]["match"]["message"].ToLongFormQuery());
		}

		[U]
		[Description("search/suggesters.asciidoc:51")]
		public void Line51()
		{
			// tag::2533e4b36ae837eaecda08407ecb6383[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.Suggest(su => su
					.Term("my-suggest-1", t => t
						.Text("tring out Elasticsearch")
						.Field(f => f.Message)
					)
					.Term("my-suggest-2", t => t
						.Text("kmichy")
						.Field(f => f.User)
					)
				)
			);
			// end::2533e4b36ae837eaecda08407ecb6383[]

			searchResponse.MatchesExample(@"POST _search
			{
			  ""suggest"": {
			    ""my-suggest-1"" : {
			      ""text"" : ""tring out Elasticsearch"",
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-suggest-2"" : {
			      ""text"" : ""kmichy"",
			      ""term"" : {
			        ""field"" : ""user""
			      }
			    }
			  }
			}");
		}

		[U]
		[Description("search/suggesters.asciidoc:127")]
		public void Line127()
		{
			// tag::5275842787967b6db876025f4a1c6942[]
			var searchResponse = client.Search<Tweet>(s => s
				.AllIndices()
				.Suggest(su => su
					.Term("my-suggest-1", t => t
						.Text("tring out Elasticsearch")
						.Field(f => f.Message)
					)
					.Term("my-suggest-2", t => t
						.Text("tring out Elasticsearch")
						.Field(f => f.User)
					)
				)
			);
			// end::5275842787967b6db876025f4a1c6942[]

			searchResponse.MatchesExample(@"POST _search
			{
			  ""suggest"": {
			    ""text"" : ""tring out Elasticsearch"",
			    ""my-suggest-1"" : {
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-suggest-2"" : {
			       ""term"" : {
			        ""field"" : ""user""
			       }
			    }
			  }
			}", (e, b) =>
			{
				// client does not support global suggest text
				((JObject)b["suggest"]).Remove("text");
				((JObject)b["suggest"]["my-suggest-1"]).Add("text", "tring out Elasticsearch");
				((JObject)b["suggest"]["my-suggest-2"]).Add("text", "tring out Elasticsearch");
			});
		}
	}
}
