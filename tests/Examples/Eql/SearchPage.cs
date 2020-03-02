using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Eql
{
	public class SearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line17()
		{
			// tag::b34d7bd889b73989ea905a4565274ea3[]
			var response0 = new SearchResponse<object>();
			// end::b34d7bd889b73989ea905a4565274ea3[]

			response0.MatchesExample(@"PUT sec_logs/_bulk?refresh
			{""index"":{""_index"" : ""sec_logs""}}
			{ ""@timestamp"": ""2020-12-07T11:06:07.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"" }, ""process"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{""_index"" : ""sec_logs""}}
			{ ""@timestamp"": ""2020-12-07T11:07:08.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""image_load"" }, ""file"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" }, ""process"": { ""name"": ""cmd.exe"", ""path"": ""C:\\Windows\\System32\\cmd.exe"" } }
			{""index"":{""_index"" : ""sec_logs""}}
			{ ""@timestamp"": ""2020-12-07T11:07:09.000Z"", ""agent"": { ""id"": ""8a4f500d"" }, ""event"": { ""category"": ""process"" }, ""process"": { ""name"": ""regsvr32.exe"", ""path"": ""C:\\Windows\\System32\\regsvr32.exe"" } }");
		}

		[U(Skip = "Example not implemented")]
		public void Line34()
		{
			// tag::6022b11c1f1e3bbfb44395554c78827f[]
			var response0 = new SearchResponse<object>();
			// end::6022b11c1f1e3bbfb44395554c78827f[]

			response0.MatchesExample(@"GET sec_logs/_eql/search
			{
			  ""event_type_field"": ""event.category"",
			  ""rule"": """"""
			    process where process.name == ""cmd.exe""
			  """"""
			}");
		}
	}
}