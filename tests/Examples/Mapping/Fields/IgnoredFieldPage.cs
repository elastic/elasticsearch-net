// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Fields
{
	public class IgnoredFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/ignored-field.asciidoc:18")]
		public void Line18()
		{
			// tag::3fe0fb38f75d2a34fb1e6ac9bedbcdbc[]
			var response0 = new SearchResponse<object>();
			// end::3fe0fb38f75d2a34fb1e6ac9bedbcdbc[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""exists"": {
			      ""field"": ""_ignored""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/fields/ignored-field.asciidoc:33")]
		public void Line33()
		{
			// tag::cf47cd4a39cd62a3ecad919e54a67bca[]
			var response0 = new SearchResponse<object>();
			// end::cf47cd4a39cd62a3ecad919e54a67bca[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""term"": {
			      ""_ignored"": ""@timestamp""
			    }
			  }
			}");
		}
	}
}
