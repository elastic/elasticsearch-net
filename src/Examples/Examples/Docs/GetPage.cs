using System;
using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using Nest;

namespace Examples.Docs
{
	public class GetPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::fbcf5078a6a9e09790553804054c36b3[]
			var response0 = new SearchResponse<object>();
			// end::fbcf5078a6a9e09790553804054c36b3[]

			response0.MatchesExample(@"GET twitter/_doc/0");
		}

		[U(Skip = "Example not implemented")]
		public void Line46()
		{
			// tag::98234499cfec70487cec5d013e976a84[]
			var response0 = new SearchResponse<object>();
			// end::98234499cfec70487cec5d013e976a84[]

			response0.MatchesExample(@"HEAD twitter/_doc/0");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::138ccd89f72aa7502dd9578403dcc589[]
			var response0 = new SearchResponse<object>();
			// end::138ccd89f72aa7502dd9578403dcc589[]

			response0.MatchesExample(@"GET twitter/_doc/0?_source=false");
		}

		[U(Skip = "Example not implemented")]
		public void Line84()
		{
			// tag::8fdf2344c4fb3de6902ad7c5735270df[]
			var response0 = new SearchResponse<object>();
			// end::8fdf2344c4fb3de6902ad7c5735270df[]

			response0.MatchesExample(@"GET twitter/_doc/0?_source_includes=*.id&_source_excludes=entities");
		}

		[U(Skip = "Example not implemented")]
		public void Line93()
		{
			// tag::745f9b8cdb8e91073f6e520e1d9f8c05[]
			var response0 = new SearchResponse<object>();
			// end::745f9b8cdb8e91073f6e520e1d9f8c05[]

			response0.MatchesExample(@"GET twitter/_doc/0?_source=*.id,retweeted");
		}

		[U(Skip = "Example not implemented")]
		public void Line109()
		{
			// tag::913770050ebbf3b9b549a899bc11060a[]
			var response0 = new SearchResponse<object>();
			// end::913770050ebbf3b9b549a899bc11060a[]

			response0.MatchesExample(@"PUT twitter
			{
			   ""mappings"": {
			       ""properties"": {
			          ""counter"": {
			             ""type"": ""integer"",
			             ""store"": false
			          },
			          ""tags"": {
			             ""type"": ""keyword"",
			             ""store"": true
			          }
			       }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line131()
		{
			// tag::5eabcdbf61bfcb484dc694f25c2bba36[]
			var response0 = new SearchResponse<object>();
			// end::5eabcdbf61bfcb484dc694f25c2bba36[]

			response0.MatchesExample(@"PUT twitter/_doc/1
			{
			    ""counter"" : 1,
			    ""tags"" : [""red""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line144()
		{
			// tag::710c7871f20f176d51209b1574b0d61b[]
			var response0 = new SearchResponse<object>();
			// end::710c7871f20f176d51209b1574b0d61b[]

			response0.MatchesExample(@"GET twitter/_doc/1?stored_fields=tags,counter");
		}

		[U(Skip = "Example not implemented")]
		public void Line178()
		{
			// tag::0ba0b2db24852abccb7c0fc1098d566e[]
			var response0 = new SearchResponse<object>();
			// end::0ba0b2db24852abccb7c0fc1098d566e[]

			response0.MatchesExample(@"PUT twitter/_doc/2?routing=user1
			{
			    ""counter"" : 1,
			    ""tags"" : [""white""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line189()
		{
			// tag::69a7be47f85138b10437113ab2f0d72d[]
			var response0 = new SearchResponse<object>();
			// end::69a7be47f85138b10437113ab2f0d72d[]

			response0.MatchesExample(@"GET twitter/_doc/2?routing=user1&stored_fields=tags,counter");
		}

		[U(Skip = "Example not implemented")]
		public void Line229()
		{
			// tag::89a8ac1509936acc272fc2d72907bc45[]
			var response0 = new SearchResponse<object>();
			// end::89a8ac1509936acc272fc2d72907bc45[]

			response0.MatchesExample(@"GET twitter/_source/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line238()
		{
			// tag::d222c6a6ec7a3beca6c97011b0874512[]
			var response0 = new SearchResponse<object>();
			// end::d222c6a6ec7a3beca6c97011b0874512[]

			response0.MatchesExample(@"GET twitter/_source/1/?_source_includes=*.id&_source_excludes=entities");
		}

		[U(Skip = "Example not implemented")]
		public void Line248()
		{
			// tag::2468ab381257d759d8a88af1141f6f9c[]
			var response0 = new SearchResponse<object>();
			// end::2468ab381257d759d8a88af1141f6f9c[]

			response0.MatchesExample(@"HEAD twitter/_source/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line262()
		{
			// tag::1d65cb6d055c46a1bde809687d835b71[]
			var response0 = new SearchResponse<object>();
			// end::1d65cb6d055c46a1bde809687d835b71[]

			response0.MatchesExample(@"GET twitter/_doc/2?routing=user1");
		}
	}
}
