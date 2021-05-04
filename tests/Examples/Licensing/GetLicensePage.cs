// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Licensing
{
	public class GetLicensePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/get-license.asciidoc:47")]
		public void Line47()
		{
			// tag::11c395d1649733bcab853fe31ec393b2[]
			var response0 = new SearchResponse<object>();
			// end::11c395d1649733bcab853fe31ec393b2[]

			response0.MatchesExample(@"GET /_license");
		}
	}
}
