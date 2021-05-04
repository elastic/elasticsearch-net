// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class IdsQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/ids-query.asciidoc:13")]
		public void Line13()
		{
			// tag::84cdb6a7a5464af7ef95b3d546883870[]
			var response0 = new SearchResponse<object>();
			// end::84cdb6a7a5464af7ef95b3d546883870[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""ids"" : {
			            ""values"" : [""1"", ""4"", ""100""]
			        }
			    }
			}");
		}
	}
}
