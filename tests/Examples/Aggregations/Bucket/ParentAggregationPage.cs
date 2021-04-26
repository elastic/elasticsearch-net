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

namespace Examples.Aggregations.Bucket
{
	public class ParentAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/parent-aggregation.asciidoc:13")]
		public void Line13()
		{
			// tag::1db086021e83205b6eab3b7765911cc2[]
			var response0 = new SearchResponse<object>();
			// end::1db086021e83205b6eab3b7765911cc2[]

			response0.MatchesExample(@"PUT parent_example
			{
			  ""mappings"": {
			     ""properties"": {
			       ""join"": {
			         ""type"": ""join"",
			         ""relations"": {
			           ""question"": ""answer""
			         }
			       }
			     }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/parent-aggregation.asciidoc:36")]
		public void Line36()
		{
			// tag::c9afa715021f2e6450e72ac73271960c[]
			var response0 = new SearchResponse<object>();
			// end::c9afa715021f2e6450e72ac73271960c[]

			response0.MatchesExample(@"PUT parent_example/_doc/1
			{
			  ""join"": {
			    ""name"": ""question""
			  },
			  ""body"": ""\<p>I have Windows 2003 server and i bought a new Windows 2008 server..."",
			  ""title"": ""Whats the best way to file transfer my site from server to a newer one?"",
			  ""tags"": [
			    ""windows-server-2003"",
			    ""windows-server-2008"",
			    ""file-transfer""
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/parent-aggregation.asciidoc:56")]
		public void Line56()
		{
			// tag::d8310e5606c61e7a6e64a90838b1a830[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::d8310e5606c61e7a6e64a90838b1a830[]

			response0.MatchesExample(@"PUT parent_example/_doc/2?routing=1
			{
			  ""join"": {
			    ""name"": ""answer"",
			    ""parent"": ""1""
			  },
			  ""owner"": {
			    ""location"": ""Norfolk, United Kingdom"",
			    ""display_name"": ""Sam"",
			    ""id"": 48
			  },
			  ""body"": ""\<p>Unfortunately you're pretty much limited to FTP..."",
			  ""creation_date"": ""2009-05-04T13:45:37.030""
			}");

			response1.MatchesExample(@"PUT parent_example/_doc/3?routing=1&refresh
			{
			  ""join"": {
			    ""name"": ""answer"",
			    ""parent"": ""1""
			  },
			  ""owner"": {
			    ""location"": ""Norfolk, United Kingdom"",
			    ""display_name"": ""Troll"",
			    ""id"": 49
			  },
			  ""body"": ""\<p>Use Linux..."",
			  ""creation_date"": ""2009-05-05T13:45:37.030""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/parent-aggregation.asciidoc:92")]
		public void Line92()
		{
			// tag::686bc640b877de845c46bef372a9866c[]
			var response0 = new SearchResponse<object>();
			// end::686bc640b877de845c46bef372a9866c[]

			response0.MatchesExample(@"POST parent_example/_search?size=0
			{
			  ""aggs"": {
			    ""top-names"": {
			      ""terms"": {
			        ""field"": ""owner.display_name.keyword"",
			        ""size"": 10
			      },
			      ""aggs"": {
			        ""to-questions"": {
			          ""parent"": {
			            ""type"" : ""answer"" \<1>
			          },
			          ""aggs"": {
			            ""top-tags"": {
			              ""terms"": {
			                ""field"": ""tags.keyword"",
			                ""size"": 10
			              }
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
