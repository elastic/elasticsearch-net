using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Sql.Endpoints
{
	public class RestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line21()
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
		public void Line110()
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
		public void Line135()
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
		public void Line170()
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
		public void Line196()
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
		public void Line223()
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
		public void Line277()
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
		public void Line314()
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
		public void Line341()
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
		public void Line379()
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
		public void Line415()
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
	}
}