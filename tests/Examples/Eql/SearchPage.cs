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
		[Description("eql/search.asciidoc:48")]
		public void Line48()
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
		[Description("eql/search.asciidoc:136")]
		public void Line136()
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
		[Description("eql/search.asciidoc:158")]
		public void Line158()
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
		[Description("eql/search.asciidoc:182")]
		public void Line182()
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
