using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class ShapePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line84()
		{
			// tag::04409304cd13f4cfa8efbed87aea9b15[]
			var response0 = new SearchResponse<object>();
			// end::04409304cd13f4cfa8efbed87aea9b15[]

			response0.MatchesExample(@"PUT /example
			{
			    ""mappings"": {
			        ""properties"": {
			            ""geometry"": {
			                ""type"": ""shape""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line149()
		{
			// tag::a55bdc75b139d947d64b32dc9824e558[]
			var response0 = new SearchResponse<object>();
			// end::a55bdc75b139d947d64b32dc9824e558[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""point"",
			        ""coordinates"" : [-377.03653, 389.897676]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line163()
		{
			// tag::8fb11f30a609b13c1373ce4a26124159[]
			var response0 = new SearchResponse<object>();
			// end::8fb11f30a609b13c1373ce4a26124159[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""POINT (-377.03653 389.897676)""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line181()
		{
			// tag::bff745b32238691bae88de22530643cb[]
			var response0 = new SearchResponse<object>();
			// end::bff745b32238691bae88de22530643cb[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""linestring"",
			        ""coordinates"" : [[-377.03653, 389.897676], [-377.009051, 389.889939]]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line195()
		{
			// tag::c4f62c66f967c6e0da3616957efbeccf[]
			var response0 = new SearchResponse<object>();
			// end::c4f62c66f967c6e0da3616957efbeccf[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""LINESTRING (-377.03653 389.897676, -377.009051 389.889939)""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line212()
		{
			// tag::567829f263dd472bf76500db05d2200a[]
			var response0 = new SearchResponse<object>();
			// end::567829f263dd472bf76500db05d2200a[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""polygon"",
			        ""coordinates"" : [
			            [ [1000.0, -1001.0], [1001.0, -1001.0], [1001.0, -1000.0], [1000.0, -1000.0], [1000.0, -1001.0] ]
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line228()
		{
			// tag::ae5f9956a525e976bfc37dcb4e7414ae[]
			var response0 = new SearchResponse<object>();
			// end::ae5f9956a525e976bfc37dcb4e7414ae[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""POLYGON ((1000.0 -1001.0, 1001.0 -1001.0, 1001.0 -1000.0, 1000.0 -1000.0, 1000.0 -1001.0))""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line241()
		{
			// tag::4f869e56eb25586ac402ccfb00aa0359[]
			var response0 = new SearchResponse<object>();
			// end::4f869e56eb25586ac402ccfb00aa0359[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""polygon"",
			        ""coordinates"" : [
			            [ [1000.0, -1001.0], [1001.0, -1001.0], [1001.0, -1000.0], [1000.0, -1000.0], [1000.0, -1001.0] ],
			            [ [1000.2, -1001.2], [1000.8, -1001.2], [1000.8, -1001.8], [1000.2, -1001.8], [1000.2, -1001.2] ]
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line258()
		{
			// tag::a5816a58c1fa769c23c6211ab449e6f3[]
			var response0 = new SearchResponse<object>();
			// end::a5816a58c1fa769c23c6211ab449e6f3[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""POLYGON ((1000.0 1000.0, 1001.0 1000.0, 1001.0 1001.0, 1000.0 1001.0, 1000.0 1000.0), (1000.2 1000.2, 1000.8 1000.2, 1000.8 1000.8, 1000.2 1000.8, 1000.2 1000.2))""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line281()
		{
			// tag::1f1ccd9af526b2251bf960a85288fc97[]
			var response0 = new SearchResponse<object>();
			// end::1f1ccd9af526b2251bf960a85288fc97[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""polygon"",
			        ""orientation"" : ""clockwise"",
			        ""coordinates"" : [
			            [ [1000.0, 1000.0], [1000.0, 1001.0], [1001.0, 1001.0], [1001.0, 1000.0], [1000.0, 1000.0] ]
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line302()
		{
			// tag::02da8c5d098d9e7cc263efac344a96de[]
			var response0 = new SearchResponse<object>();
			// end::02da8c5d098d9e7cc263efac344a96de[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""multipoint"",
			        ""coordinates"" : [
			            [1002.0, 1002.0], [1003.0, 2000.0]
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line318()
		{
			// tag::577b09f45256ff855252d29e1d1cd433[]
			var response0 = new SearchResponse<object>();
			// end::577b09f45256ff855252d29e1d1cd433[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""MULTIPOINT (1002.0 2000.0, 1003.0 2000.0)""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line333()
		{
			// tag::76c551d13c3d907ad6dc56b85bec76de[]
			var response0 = new SearchResponse<object>();
			// end::76c551d13c3d907ad6dc56b85bec76de[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""multilinestring"",
			        ""coordinates"" : [
			            [ [1002.0, 200.0], [1003.0, 200.0], [1003.0, 300.0], [1002.0, 300.0] ],
			            [ [1000.0, 100.0], [1001.0, 100.0], [1001.0, 100.0], [1000.0, 100.0] ],
			            [ [1000.2, 100.2], [1000.8, 100.2], [1000.8, 100.8], [1000.2, 100.8] ]
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line351()
		{
			// tag::9aeca1d56bb2ff0701587b269163311e[]
			var response0 = new SearchResponse<object>();
			// end::9aeca1d56bb2ff0701587b269163311e[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""MULTILINESTRING ((1002.0 200.0, 1003.0 200.0, 1003.0 300.0, 1002.0 300.0), (1000.0 100.0, 1001.0 100.0, 1001.0 100.0, 1000.0 100.0), (1000.2 0.2, 1000.8 100.2, 1000.8 100.8, 1000.2 100.8))""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line366()
		{
			// tag::9d2464f0dce99d47f2699d953ee55b37[]
			var response0 = new SearchResponse<object>();
			// end::9d2464f0dce99d47f2699d953ee55b37[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""multipolygon"",
			        ""coordinates"" : [
			            [ [[1002.0, 200.0], [1003.0, 200.0], [1003.0, 300.0], [1002.0, 300.0], [1002.0, 200.0]] ],
			            [ [[1000.0, 200.0], [1001.0, 100.0], [1001.0, 100.0], [1000.0, 100.0], [1000.0, 100.0]],
			              [[1000.2, 200.2], [1000.8, 100.2], [1000.8, 100.8], [1000.2, 100.8], [1000.2, 100.2]] ]
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line384()
		{
			// tag::e7f366d76e3e53b4c0c30f7b0c21fbc0[]
			var response0 = new SearchResponse<object>();
			// end::e7f366d76e3e53b4c0c30f7b0c21fbc0[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""MULTIPOLYGON (((1002.0 200.0, 1003.0 200.0, 1003.0 300.0, 1002.0 300.0, 102.0 200.0)), ((1000.0 100.0, 1001.0 100.0, 1001.0 100.0, 1000.0 100.0, 1000.0 100.0), (1000.2 100.2, 1000.8 100.2, 1000.8 100.8, 1000.2 100.8, 1000.2 100.2)))""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line399()
		{
			// tag::4b3ef0f1d3cb9598a3fb94c03948e9e2[]
			var response0 = new SearchResponse<object>();
			// end::4b3ef0f1d3cb9598a3fb94c03948e9e2[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"": ""geometrycollection"",
			        ""geometries"": [
			            {
			                ""type"": ""point"",
			                ""coordinates"": [1000.0, 100.0]
			            },
			            {
			                ""type"": ""linestring"",
			                ""coordinates"": [ [1001.0, 100.0], [1002.0, 100.0] ]
			            }
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line422()
		{
			// tag::72ef8c634b3594963f203d2b3631c12e[]
			var response0 = new SearchResponse<object>();
			// end::72ef8c634b3594963f203d2b3631c12e[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""GEOMETRYCOLLECTION (POINT (1000.0 100.0), LINESTRING (1001.0 100.0, 1002.0 100.0))""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line438()
		{
			// tag::6dd3c5a716302fdd39fcf5c150b826bc[]
			var response0 = new SearchResponse<object>();
			// end::6dd3c5a716302fdd39fcf5c150b826bc[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : {
			        ""type"" : ""envelope"",
			        ""coordinates"" : [ [1000.0, 100.0], [1001.0, 100.0] ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line454()
		{
			// tag::70932f56df27fb502d2095fefcaa83d6[]
			var response0 = new SearchResponse<object>();
			// end::70932f56df27fb502d2095fefcaa83d6[]

			response0.MatchesExample(@"POST /example/_doc
			{
			    ""location"" : ""BBOX (1000.0, 1002.0, 2000.0, 1000.0)""
			}");
		}
	}
}