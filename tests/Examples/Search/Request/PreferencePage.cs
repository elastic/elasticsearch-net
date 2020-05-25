// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class PreferencePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/preference.asciidoc:59")]
		public void Line59()
		{
			// tag::9405de6fd841c32ac510eb0a7eeed989[]
			var response0 = new SearchResponse<object>();
			// end::9405de6fd841c32ac510eb0a7eeed989[]

			response0.MatchesExample(@"GET /_search?preference=xyzabc123
			{
			    ""query"": {
			        ""match"": {
			            ""title"": ""elasticsearch""
			        }
			    }
			}");
		}
	}
}
