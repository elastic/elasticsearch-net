using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Eql
{
	public class SearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:17")]
		public void Line17()
		{
			// tag::423f422b18fea34486a77579b2b12d72[]
			var response0 = new SearchResponse<object>();
			// end::423f422b18fea34486a77579b2b12d72[]

			response0.MatchesExample(@"PUT sec_logs/_bulk?refresh
			{""index"":{""_index"" : ""sec_logs"", ""_id"" : ""1""}}
			{ ""@timestamp"": ""2020-12-06T11:04:05.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"" }, ""process"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{""_index"" : ""sec_logs"", ""_id"" : ""2""}}
			{ ""@timestamp"": ""2020-12-06T11:04:07.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""file"" }, ""file"": { ""accessed"": ""2020-12-07T11:07:08.000Z"", ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"", ""type"": ""file"", ""size"": 16384 }, ""process"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{""_index"" : ""sec_logs"", ""_id"" : ""3""}}
			{ ""@timestamp"": ""2020-12-07T11:06:07.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"" }, ""process"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{""_index"" : ""sec_logs"", ""_id"" : ""4""}}
			{ ""@timestamp"": ""2020-12-07T11:07:08.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""file"" }, ""file"": { ""accessed"": ""2020-12-07T11:07:08.000Z"", ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"", ""type"": ""file"", ""size"": 16384 }, ""process"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{""_index"" : ""sec_logs"", ""_id"" : ""5""}}
			{ ""@timestamp"": ""2020-12-07T11:07:09.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"" }, ""process"": { ""name"": ""regsvr32.exe"", ""path"": ""C:\\Windows\\System32\\regsvr32.exe"" } }");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:39")]
		public void Line39()
		{
			// tag::39e711af23a7eee61a1e13cf2ef7c360[]
			var response0 = new SearchResponse<object>();
			// end::39e711af23a7eee61a1e13cf2ef7c360[]

			response0.MatchesExample(@"GET sec_logs/_eql/search
			{
			  ""query"": """"""
			    process where process.name == ""cmd.exe""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:127")]
		public void Line127()
		{
			// tag::6f915983b4c12bcd1a8ca1c9cf8feed1[]
			var response0 = new SearchResponse<object>();
			// end::6f915983b4c12bcd1a8ca1c9cf8feed1[]

			response0.MatchesExample(@"GET sec_logs/_eql/search
			{
			   ""event_category_field"": ""file.type"",
			  ""query"": """"""
			    file where agent.id == ""8a4f500d""
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:149")]
		public void Line149()
		{
			// tag::382f6056cfbc3a113f675c0fbc59aaf3[]
			var response0 = new SearchResponse<object>();
			// end::382f6056cfbc3a113f675c0fbc59aaf3[]

			response0.MatchesExample(@"GET sec_logs/_eql/search
			{
			  ""timestamp_field"": ""file.accessed"",
			  ""query"": """"""
			    file where (file.size > 1 and file.type == ""file"")
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:173")]
		public void Line173()
		{
			// tag::bfdf5997fe6e1fc4e938a28fcd6c8683[]
			var response0 = new SearchResponse<object>();
			// end::bfdf5997fe6e1fc4e938a28fcd6c8683[]

			response0.MatchesExample(@"GET sec_logs/_eql/search
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
	}
}