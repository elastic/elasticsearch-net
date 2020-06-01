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
		[Description("eql/search.asciidoc:22")]
		public void Line22()
		{
			// tag::21ee0b7d61d96fd3a72bad4c329979e5[]
			var response0 = new SearchResponse<object>();
			// end::21ee0b7d61d96fd3a72bad4c329979e5[]

			response0.MatchesExample(@"PUT /sec_logs/_bulk?refresh
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
		[Description("eql/search.asciidoc:53")]
		public void Line53()
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
		[Description("eql/search.asciidoc:156")]
		public void Line156()
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
		[Description("eql/search.asciidoc:258")]
		public void Line258()
		{
			// tag::412109709c109c228935f2217b04650b[]
			var response0 = new SearchResponse<object>();
			// end::412109709c109c228935f2217b04650b[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence
			      [ file where file.name == ""cmd.exe"" ] by agent.id
			      [ process where stringContains(process.name, ""regsvr32"") ] by agent.id
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:274")]
		public void Line274()
		{
			// tag::890cdad507a87fd175a12b9f0b683f46[]
			var response0 = new SearchResponse<object>();
			// end::890cdad507a87fd175a12b9f0b683f46[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""query"": """"""
			    sequence by agent.id
			      [ file where file.name == ""cmd.exe"" ]
			      [ process where stringContains(process.name, ""regsvr32"") ]
			  """"""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("eql/search.asciidoc:387")]
		public void Line387()
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
		[Description("eql/search.asciidoc:413")]
		public void Line413()
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
		[Description("eql/search.asciidoc:441")]
		public void Line441()
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
		[Description("eql/search.asciidoc:478")]
		public void Line478()
		{
			// tag::84c67d48beab6eaa209982314a6ed0be[]
			var response0 = new SearchResponse<object>();
			// end::84c67d48beab6eaa209982314a6ed0be[]

			response0.MatchesExample(@"GET /sec_logs/_eql/search
			{
			  ""case_sensitive"": true,
			  ""query"": """"""
			    process where stringContains(process.path, ""System32"")
			  """"""
			}");
		}
	}
}
