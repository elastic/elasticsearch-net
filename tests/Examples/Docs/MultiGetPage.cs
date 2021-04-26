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

namespace Examples.Docs
{
	public class MultiGetPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:10")]
		public void Line10()
		{
			// tag::ccfaeef928ba7dd4b5de0c518151fd7c[]
			var response0 = new SearchResponse<object>();
			// end::ccfaeef928ba7dd4b5de0c518151fd7c[]

			response0.MatchesExample(@"GET /_mget
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""twitter"",
			            ""_id"" : ""1""
			        },
			        {
			            ""_index"" : ""twitter"",
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:130")]
		public void Line130()
		{
			// tag::53cf7d3731f50620b3277b80e2fbfd56[]
			var response0 = new SearchResponse<object>();
			// end::53cf7d3731f50620b3277b80e2fbfd56[]

			response0.MatchesExample(@"GET /twitter/_mget
			{
			    ""docs"" : [
			        {
			            ""_id"" : ""1""
			        },
			        {
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:148")]
		public void Line148()
		{
			// tag::81095ba46e4d8c5da3623f5ea8c54a34[]
			var response0 = new SearchResponse<object>();
			// end::81095ba46e4d8c5da3623f5ea8c54a34[]

			response0.MatchesExample(@"GET /twitter/_mget
			{
			    ""ids"" : [""1"", ""2""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:170")]
		public void Line170()
		{
			// tag::6b1ab3f273c6e425067cd5889b0c258f[]
			var response0 = new SearchResponse<object>();
			// end::6b1ab3f273c6e425067cd5889b0c258f[]

			response0.MatchesExample(@"GET /_mget
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""1"",
			            ""_source"" : false
			        },
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""2"",
			            ""_source"" : [""field3"", ""field4""]
			        },
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""3"",
			            ""_source"" : {
			                ""include"": [""user""],
			                ""exclude"": [""user.location""]
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:208")]
		public void Line208()
		{
			// tag::c4272ba35b81125a805fb1a7292f3d25[]
			var response0 = new SearchResponse<object>();
			// end::c4272ba35b81125a805fb1a7292f3d25[]

			response0.MatchesExample(@"GET /_mget
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""1"",
			            ""stored_fields"" : [""field1"", ""field2""]
			        },
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""2"",
			            ""stored_fields"" : [""field3"", ""field4""]
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:231")]
		public void Line231()
		{
			// tag::27fac828d28ab065524dd1ce148840c0[]
			var response0 = new SearchResponse<object>();
			// end::27fac828d28ab065524dd1ce148840c0[]

			response0.MatchesExample(@"GET /test/_mget?stored_fields=field1,field2
			{
			    ""docs"" : [
			        {
			            ""_id"" : ""1""
			        },
			        {
			            ""_id"" : ""2"",
			            ""stored_fields"" : [""field3"", ""field4""]
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/multi-get.asciidoc:254")]
		public void Line254()
		{
			// tag::1b37488d0a79d3c950029851b7cd623e[]
			var response0 = new SearchResponse<object>();
			// end::1b37488d0a79d3c950029851b7cd623e[]

			response0.MatchesExample(@"GET /_mget?routing=key1
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""1"",
			            ""routing"" : ""key2""
			        },
			        {
			            ""_index"" : ""test"",
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}
	}
}
