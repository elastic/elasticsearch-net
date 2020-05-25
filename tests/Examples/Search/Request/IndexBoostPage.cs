// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class IndexBoostPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/index-boost.asciidoc:11")]
		public void Line11()
		{
			// tag::69dce2801f824f61e4f3ea9ee9371e31[]
			var response0 = new SearchResponse<object>();
			// end::69dce2801f824f61e4f3ea9ee9371e31[]

			response0.MatchesExample(@"GET /_search
			{
			    ""indices_boost"" : [
			        { ""index1"" : 1.4 },
			        { ""index2"" : 1.3 }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/index-boost.asciidoc:25")]
		public void Line25()
		{
			// tag::fb8a4322825d26c4e7b41bd763b3d392[]
			var response0 = new SearchResponse<object>();
			// end::fb8a4322825d26c4e7b41bd763b3d392[]

			response0.MatchesExample(@"GET /_search
			{
			    ""indices_boost"" : [
			        { ""alias1"" : 1.4 },
			        { ""index*"" : 1.3 }
			    ]
			}");
		}
	}
}
