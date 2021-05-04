// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class IgnoreAbovePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/ignore-above.asciidoc:10")]
		public void Line10()
		{
			// tag::17a77b9c39526c865d7bd6b72cf4a79f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::17a77b9c39526c865d7bd6b72cf4a79f[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""message"": {
			        ""type"": ""keyword"",
			        ""ignore_above"": 20 \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1 \<2>
			{
			  ""message"": ""Syntax error""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2 \<3>
			{
			  ""message"": ""Syntax error with some long stacktrace""
			}");

			response3.MatchesExample(@"GET my_index/_search \<4>
			{
			  ""aggs"": {
			    ""messages"": {
			      ""terms"": {
			        ""field"": ""message""
			      }
			    }
			  }
			}");
		}
	}
}
