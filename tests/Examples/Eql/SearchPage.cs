// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Eql
{
	public class SearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:24")]
		public void Line24()
		{
			// tag::8460edb65562b5cac801a8e91a52ad00[]
			var response0 = new SearchResponse<object>();
			// end::8460edb65562b5cac801a8e91a52ad00[]

			response0.MatchesExample(@"PUT /sec_logs/_bulk?refresh
			{""index"":{ }}
			{ ""@timestamp"": ""2020-12-06T11:04:05.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"", ""id"": ""edwCRnyD"", ""sequence"": 1 }, ""process"": { ""name"": ""cmd.exe"", ""executable"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{ }}
			{ ""@timestamp"": ""2020-12-06T11:04:07.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""file"", ""id"": ""dGCHwoeS"", ""sequence"": 2 }, ""file"": { ""accessed"": ""2020-12-07T11:07:08.000Z"", ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"", ""type"": ""file"", ""size"": 16384 }, ""process"": { ""name"": ""cmd.exe"", ""executable"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{ }}
			{ ""@timestamp"": ""2020-12-07T11:06:07.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"", ""id"": ""cMyt5SZ2"", ""sequence"": 3 }, ""process"": { ""name"": ""cmd.exe"", ""executable"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{ }}
			{ ""@timestamp"": ""2020-12-07T11:07:08.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""file"", ""id"": ""bYA7gPay"", ""sequence"": 4 }, ""file"": { ""accessed"": ""2020-12-07T11:07:08.000Z"", ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"", ""type"": ""file"", ""size"": 16384 }, ""process"": { ""name"": ""cmd.exe"", ""executable"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{ }}
			{ ""@timestamp"": ""2020-12-07T11:07:09.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"", ""id"": ""aR3NWVOs"", ""sequence"": 5 }, ""process"": { ""name"": ""regsvr32.exe"", ""executable"": ""C:\\Windows\\System32\\regsvr32.exe"" } }
			{""index"":{ }}
			{ ""@timestamp"": ""2020-12-07T11:07:10.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"", ""id"": ""GTSmSqgz0U"", ""sequence"": 6, ""type"": ""termination"" }, ""process"": { ""name"": ""regsvr32.exe"", ""executable"": ""C:\\Windows\\System32\\regsvr32.exe"" } }");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:57")]
		public void Line57()
		{
			// tag::975c71db4a64e43901e11e580b685ad8[]
			var response0 = new SearchResponse<object>();
			// end::975c71db4a64e43901e11e580b685ad8[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    process where process.name == ""cmd.exe""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:163")]
		public void Line163()
		{
			// tag::faccdd71120690698f663e794a85aa71[]
			var response0 = new SearchResponse<object>();
			// end::faccdd71120690698f663e794a85aa71[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence
			      [ file where file.name == ""cmd.exe"" ]
			      [ process where stringContains(process.name, ""regsvr32"") ]
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:265")]
		public void Line265()
		{
			// tag::91efe8eb8e1feb9e77732e42e13edad8[]
			var response0 = new SearchResponse<object>();
			// end::91efe8eb8e1feb9e77732e42e13edad8[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence with maxspan=1h
			      [ file where file.name == ""cmd.exe"" ]
			      [ process where stringContains(process.name, ""regsvr32"") ]
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:283")]
		public void Line283()
		{
			// tag::d046eb2a6a8382532b9729e4d63eedde[]
			var response0 = new SearchResponse<object>();
			// end::d046eb2a6a8382532b9729e4d63eedde[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence with maxspan=1h
			      [ file where file.name == ""cmd.exe"" ] by agent.id
			      [ process where stringContains(process.name, ""regsvr32"") ] by agent.id
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:299")]
		public void Line299()
		{
			// tag::6096e72461cc84018c4938aac975512c[]
			var response0 = new SearchResponse<object>();
			// end::6096e72461cc84018c4938aac975512c[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence by agent.id with maxspan=1h
			      [ file where file.name == ""cmd.exe"" ]
			      [ process where stringContains(process.name, ""regsvr32"") ]
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:402")]
		public void Line402()
		{
			// tag::c86cd3e0fe0c09462bdb5508f3961fde[]
			var response0 = new SearchResponse<object>();
			// end::c86cd3e0fe0c09462bdb5508f3961fde[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence by agent.id with maxspan=1h
			      [ file where file.name == ""cmd.exe"" ]
			      [ process where stringContains(process.name, ""regsvr32"") ]
			    until [ process where event.type == ""termination"" ]
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:430")]
		public void Line430()
		{
			// tag::54b76e8d2063652b2cc85aa9b554704e[]
			var response0 = new SearchResponse<object>();
			// end::54b76e8d2063652b2cc85aa9b554704e[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""event_category_field"": ""file.type"",
			  ""query"": """"""
			    file where agent.id == ""8a4f500d""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:456")]
		public void Line456()
		{
			// tag::0ccf234774b8263b26cb8a6149ef8855[]
			var response0 = new SearchResponse<object>();
			// end::0ccf234774b8263b26cb8a6149ef8855[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""timestamp_field"": ""file.accessed"",
			  ""query"": """"""
			    file where (file.size > 1 and file.type == ""file"")
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:484")]
		public void Line484()
		{
			// tag::86a4902f1967847e6b9c658a0612a028[]
			var response0 = new SearchResponse<object>();
			// end::86a4902f1967847e6b9c658a0612a028[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""tiebreaker_field"": ""event.id"",
			  ""query"": """"""
			    process where process.name == ""cmd.exe"" and stringContains(process.executable, ""System32"")
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:577")]
		public void Line577()
		{
			// tag::578e5759595f0322170ea0a12dbf2e77[]
			var response0 = new SearchResponse<object>();
			// end::578e5759595f0322170ea0a12dbf2e77[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""filter"": {
			    ""range"" : {
			      ""file.size"" : {
			        ""gte"" : 1,
			        ""lte"" : 1000000
			      }
			    }
			  },
			  ""query"": """"""
			    file where (file.type == ""file"" and file.name == ""cmd.exe"")
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:642")]
		public void Line642()
		{
			// tag::064c537d2f3b930ef835f5d3ff39e69e[]
			var response0 = new SearchResponse<object>();
			// end::064c537d2f3b930ef835f5d3ff39e69e[]

			response0.MatchesExample(@"GET /frozen_sec_logs/_eql/search
			{
			  ""wait_for_completion_timeout"": ""2s"",
			  ""query"": """"""
			    process where process.name == ""cmd.exe""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:692")]
		public void Line692()
		{
			// tag::1cca4bb2f0ea7e43181be8bd965149d4[]
			var response0 = new SearchResponse<object>();
			// end::1cca4bb2f0ea7e43181be8bd965149d4[]

			response0.MatchesExample(@"GET /_eql/search/FmNJRUZ1YWZCU3dHY1BIOUhaenVSRkEaaXFlZ3h4c1RTWFNocDdnY2FSaERnUTozNDE=?wait_for_completion_timeout=2s");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:737")]
		public void Line737()
		{
			// tag::eacb7a2758eca6898f79b87d471f91c2[]
			var response0 = new SearchResponse<object>();
			// end::eacb7a2758eca6898f79b87d471f91c2[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""keep_alive"": ""2d"",
			  ""wait_for_completion_timeout"": ""2s"",
			  ""query"": """"""
			    process where process.name == ""cmd.exe""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:761")]
		public void Line761()
		{
			// tag::6693f0ffa0de3229b5dedda197810e70[]
			var response0 = new SearchResponse<object>();
			// end::6693f0ffa0de3229b5dedda197810e70[]

			response0.MatchesExample(@"GET /_eql/search/FmNJRUZ1YWZCU3dHY1BIOUhaenVSRkEaaXFlZ3h4c1RTWFNocDdnY2FSaERnUTozNDE=?keep_alive=5d");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:778")]
		public void Line778()
		{
			// tag::636e5683c31983de60a48599ede620f9[]
			var response0 = new SearchResponse<object>();
			// end::636e5683c31983de60a48599ede620f9[]

			response0.MatchesExample(@"DELETE /_eql/search/FmNJRUZ1YWZCU3dHY1BIOUhaenVSRkEaaXFlZ3h4c1RTWFNocDdnY2FSaERnUTozNDE=?keep_alive=5d");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:803")]
		public void Line803()
		{
			// tag::ff9c97030694613b932f46b464eacb4a[]
			var response0 = new SearchResponse<object>();
			// end::ff9c97030694613b932f46b464eacb4a[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""keep_on_completion"": true,
			  ""wait_for_completion_timeout"": ""2s"",
			  ""query"": """"""
			    process where process.name == ""cmd.exe""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:837")]
		public void Line837()
		{
			// tag::a669e9d56e34c95ef4c780e92ed307f1[]
			var response0 = new SearchResponse<object>();
			// end::a669e9d56e34c95ef4c780e92ed307f1[]

			response0.MatchesExample(@"GET /_eql/search/FjlmbndxNmJjU0RPdExBTGg0elNOOEEaQk9xSjJBQzBRMldZa1VVQ2pPa01YUToxMDY=");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:869")]
		public void Line869()
		{
			// tag::bf685177fd636022d5e15b82f9975e46[]
			var response0 = new SearchResponse<object>();
			// end::bf685177fd636022d5e15b82f9975e46[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""keep_on_completion"": true,
			  ""case_sensitive"": true,
			  ""query"": """"""
			    process where stringContains(process.executable, ""System32"")
			  """"""
			}");
		}
	}
}
