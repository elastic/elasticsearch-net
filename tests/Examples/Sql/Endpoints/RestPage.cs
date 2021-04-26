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

namespace Examples.Sql.Endpoints
{
	public class RestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:22")]
		public void Line22()
		{
			// tag::4870ece3455f2b5c34eccaa9492f3894[]
			var response0 = new SearchResponse<object>();
			// end::4870ece3455f2b5c34eccaa9492f3894[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC LIMIT 5""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:114")]
		public void Line114()
		{
			// tag::b649c4dc7d187a27d2112f59e62cecea[]
			var response0 = new SearchResponse<object>();
			// end::b649c4dc7d187a27d2112f59e62cecea[]

			response0.MatchesExample(@"POST /_sql?format=csv
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 5
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:139")]
		public void Line139()
		{
			// tag::8b8c48b5fcfaaec794875537d3be2e62[]
			var response0 = new SearchResponse<object>();
			// end::8b8c48b5fcfaaec794875537d3be2e62[]

			response0.MatchesExample(@"POST /_sql?format=json
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 5
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:174")]
		public void Line174()
		{
			// tag::92d82b9d1bda5a8ae1117d03413f4e67[]
			var response0 = new SearchResponse<object>();
			// end::92d82b9d1bda5a8ae1117d03413f4e67[]

			response0.MatchesExample(@"POST /_sql?format=tsv
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 5
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:200")]
		public void Line200()
		{
			// tag::a972c38ee41dc899708825790a113cb8[]
			var response0 = new SearchResponse<object>();
			// end::a972c38ee41dc899708825790a113cb8[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 5
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:227")]
		public void Line227()
		{
			// tag::d38b8ef18ca89eafb1e175ec9a393259[]
			var response0 = new SearchResponse<object>();
			// end::d38b8ef18ca89eafb1e175ec9a393259[]

			response0.MatchesExample(@"POST /_sql?format=yaml
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 5
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:282")]
		public void Line282()
		{
			// tag::212042898296f208dbf957f33c07e3b2[]
			var response0 = new SearchResponse<object>();
			// end::212042898296f208dbf957f33c07e3b2[]

			response0.MatchesExample(@"POST /_sql?format=json
			{
			    ""cursor"": ""sDXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAAEWYUpOYklQMHhRUEtld3RsNnFtYU1hQQ==:BAFmBGRhdGUBZgVsaWtlcwFzB21lc3NhZ2UBZgR1c2Vy9f///w8=""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:319")]
		public void Line319()
		{
			// tag::cc5dfc9aa125e3fd03f523fc2c356f63[]
			var response0 = new SearchResponse<object>();
			// end::cc5dfc9aa125e3fd03f523fc2c356f63[]

			response0.MatchesExample(@"POST /_sql/close
			{
			    ""cursor"": ""sDXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAAEWYUpOYklQMHhRUEtld3RsNnFtYU1hQQ==:BAFmBGRhdGUBZgVsaWtlcwFzB21lc3NhZ2UBZgR1c2Vy9f///w8=""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:346")]
		public void Line346()
		{
			// tag::683da0a8624bc03c79a3db8ffab43f0b[]
			var response0 = new SearchResponse<object>();
			// end::683da0a8624bc03c79a3db8ffab43f0b[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""filter"": {
			        ""range"": {
			            ""page_count"": {
			                ""gte"" : 100,
			                ""lte"" : 200
			            }
			        }
			    },
			    ""fetch_size"": 5
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:380")]
		public void Line380()
		{
			// tag::acc0bf5e777f8fc833b7928fdd17ea3e[]
			var response0 = new SearchResponse<object>();
			// end::acc0bf5e777f8fc833b7928fdd17ea3e[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
			    ""query"": ""SELECT * FROM library"",
			    ""filter"": {
			        ""terms"": {
			            ""_routing"": [""abc""]
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:404")]
		public void Line404()
		{
			// tag::c11dc94839b861235b4943f046e15997[]
			var response0 = new SearchResponse<object>();
			// end::c11dc94839b861235b4943f046e15997[]

			response0.MatchesExample(@"POST /_sql?format=json
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 5,
			    ""columnar"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:440")]
		public void Line440()
		{
			// tag::15089efd5a5a72234fdb91c111adb3c1[]
			var response0 = new SearchResponse<object>();
			// end::15089efd5a5a72234fdb91c111adb3c1[]

			response0.MatchesExample(@"POST /_sql?format=json
			{
			    ""cursor"": ""sDXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAAEWWWdrRlVfSS1TbDYtcW9lc1FJNmlYdw==:BAFmBmF1dGhvcgFmBG5hbWUBZgpwYWdlX2NvdW50AWYMcmVsZWFzZV9kYXRl+v///w8="",
			    ""columnar"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:473")]
		public void Line473()
		{
			// tag::172d150e56a225155a62c7b18bf8da67[]
			var response0 = new SearchResponse<object>();
			// end::172d150e56a225155a62c7b18bf8da67[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
				""query"": ""SELECT YEAR(release_date) AS year FROM library WHERE page_count > 300 AND author = 'Frank Herbert' GROUP BY year HAVING COUNT(*) > 0""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/rest.asciidoc:484")]
		public void Line484()
		{
			// tag::d9e0cba8e150681d861f5fd1545514e2[]
			var response0 = new SearchResponse<object>();
			// end::d9e0cba8e150681d861f5fd1545514e2[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
				""query"": ""SELECT YEAR(release_date) AS year FROM library WHERE page_count > ? AND author = ? GROUP BY year HAVING COUNT(*) > ?"",
				""params"": [300, ""Frank Herbert"", 0]
			}");
		}
	}
}
