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

namespace Examples.Search
{
	public class ProfilePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/profile.asciidoc:32")]
		public void Line32()
		{
			// tag::f6e300010478e5cbbeb2e589bc16fce7[]
			var response0 = new SearchResponse<object>();
			// end::f6e300010478e5cbbeb2e589bc16fce7[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""profile"": true,\<1>
			  ""query"" : {
			    ""match"" : { ""message"" : ""some number"" }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/profile.asciidoc:533")]
		public void Line533()
		{
			// tag::d8621790a416f05557c8df037a3722ac[]
			var response0 = new SearchResponse<object>();
			// end::d8621790a416f05557c8df037a3722ac[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""profile"": true,
			  ""query"": {
			    ""term"": {
			      ""user"": {
			        ""value"": ""test""
			      }
			    }
			  },
			  ""aggs"": {
			    ""my_scoped_agg"": {
			      ""terms"": {
			        ""field"": ""likes""
			      }
			    },
			    ""my_global_agg"": {
			      ""global"": {},
			      ""aggs"": {
			        ""my_level_agg"": {
			          ""terms"": {
			            ""field"": ""likes""
			          }
			        }
			      }
			    }
			  },
			  ""post_filter"": {
			    ""match"": {
			      ""message"": ""some""
			    }
			  }
			}");
		}
	}
}
