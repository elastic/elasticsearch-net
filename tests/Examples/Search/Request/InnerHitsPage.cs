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

namespace Examples.Search.Request
{
	public class InnerHitsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/inner-hits.asciidoc:86")]
		public void Line86()
		{
			// tag::2a91e1fb8ad93a188fa9d77ec01bc431[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::2a91e1fb8ad93a188fa9d77ec01bc431[]

			response0.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""properties"": {
			      ""comments"": {
			        ""type"": ""nested""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT test/_doc/1?refresh
			{
			  ""title"": ""Test title"",
			  ""comments"": [
			    {
			      ""author"": ""kimchy"",
			      ""number"": 1
			    },
			    {
			      ""author"": ""nik9000"",
			      ""number"": 2
			    }
			  ]
			}");

			response2.MatchesExample(@"POST test/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""comments"",
			      ""query"": {
			        ""match"": {""comments.number"" : 2}
			      },
			      ""inner_hits"": {} \<1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/inner-hits.asciidoc:207")]
		public void Line207()
		{
			// tag::983fbb78e57e8fe98db38cf2d217e943[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::983fbb78e57e8fe98db38cf2d217e943[]

			response0.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""properties"": {
			      ""comments"": {
			        ""type"": ""nested""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT test/_doc/1?refresh
			{
			  ""title"": ""Test title"",
			  ""comments"": [
			    {
			      ""author"": ""kimchy"",
			      ""text"": ""comment text""
			    },
			    {
			      ""author"": ""nik9000"",
			      ""text"": ""words words words""
			    }
			  ]
			}");

			response2.MatchesExample(@"POST test/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""comments"",
			      ""query"": {
			        ""match"": {""comments.text"" : ""words""}
			      },
			      ""inner_hits"": {
			        ""_source"" : false,
			        ""docvalue_fields"" : [
			          ""comments.text.keyword""
			        ]
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/inner-hits.asciidoc:318")]
		public void Line318()
		{
			// tag::79feb4a0c0a21b7015a52f9736cd4683[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::79feb4a0c0a21b7015a52f9736cd4683[]

			response0.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""properties"": {
			      ""comments"": {
			        ""type"": ""nested"",
			        ""properties"": {
			          ""votes"": {
			            ""type"": ""nested""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT test/_doc/1?refresh
			{
			  ""title"": ""Test title"",
			  ""comments"": [
			    {
			      ""author"": ""kimchy"",
			      ""text"": ""comment text"",
			      ""votes"": []
			    },
			    {
			      ""author"": ""nik9000"",
			      ""text"": ""words words words"",
			      ""votes"": [
			        {""value"": 1 , ""voter"": ""kimchy""},
			        {""value"": -1, ""voter"": ""other""}
			      ]
			    }
			  ]
			}");

			response2.MatchesExample(@"POST test/_search
			{
			  ""query"": {
			    ""nested"": {
			      ""path"": ""comments.votes"",
			        ""query"": {
			          ""match"": {
			            ""comments.votes.voter"": ""kimchy""
			          }
			        },
			        ""inner_hits"" : {}
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/inner-hits.asciidoc:435")]
		public void Line435()
		{
			// tag::3f5b5bee692e7d4b0992dc0a64e95a60[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::3f5b5bee692e7d4b0992dc0a64e95a60[]

			response0.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_join_field"": {
			        ""type"": ""join"",
			        ""relations"": {
			          ""my_parent"": ""my_child""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT test/_doc/1?refresh
			{
			  ""number"": 1,
			  ""my_join_field"": ""my_parent""
			}");

			response2.MatchesExample(@"PUT test/_doc/2?routing=1&refresh
			{
			  ""number"": 1,
			  ""my_join_field"": {
			    ""name"": ""my_child"",
			    ""parent"": ""1""
			  }
			}");

			response3.MatchesExample(@"POST test/_search
			{
			  ""query"": {
			    ""has_child"": {
			      ""type"": ""my_child"",
			      ""query"": {
			        ""match"": {
			          ""number"": 1
			        }
			      },
			      ""inner_hits"": {}    \<1>
			    }
			  }
			}");
		}
	}
}
