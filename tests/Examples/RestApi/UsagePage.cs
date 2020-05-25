// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.RestApi
{
	public class UsagePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rest-api/usage.asciidoc:32")]
		public void Line32()
		{
			// tag::43fe75fa9f3fca846598fdad58fd98cb[]
			var response0 = new SearchResponse<object>();
			// end::43fe75fa9f3fca846598fdad58fd98cb[]

			response0.MatchesExample(@"GET /_xpack/usage");
		}
	}
}
