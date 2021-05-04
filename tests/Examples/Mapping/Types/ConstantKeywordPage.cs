// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Mapping.Types
{
	public class ConstantKeywordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/constant-keyword.asciidoc:14")]
		public void Line14()
		{
			// tag::6b0d492c0f50103fefeab385a7bebd01[]
			var response0 = new SearchResponse<object>();
			// end::6b0d492c0f50103fefeab385a7bebd01[]

			response0.MatchesExample(@"PUT logs-debug
			{
			  ""mappings"": {
			    ""properties"": {
			      ""@timestamp"": {
			        ""type"": ""date""
			      },
			      ""message"": {
			        ""type"": ""text""
			      },
			      ""level"": {
			        ""type"": ""constant_keyword"",
			        ""value"": ""debug""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/constant-keyword.asciidoc:43")]
		public void Line43()
		{
			// tag::134384b8c63cfbd8d762fb01757bb3f9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::134384b8c63cfbd8d762fb01757bb3f9[]

			response0.MatchesExample(@"POST logs-debug/_doc
			{
			  ""date"": ""2019-12-12"",
			  ""message"": ""Starting up Elasticsearch"",
			  ""level"": ""debug""
			}");

			response1.MatchesExample(@"POST logs-debug/_doc
			{
			  ""date"": ""2019-12-12"",
			  ""message"": ""Starting up Elasticsearch""
			}");
		}
	}
}
