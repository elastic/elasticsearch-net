// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class HasPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/has-privileges.asciidoc:68")]
		public void Line68()
		{
			// tag::9684e5fa8c22a07a372feb6fc1f5f7c0[]
			var response0 = new SearchResponse<object>();
			// end::9684e5fa8c22a07a372feb6fc1f5f7c0[]

			response0.MatchesExample(@"GET /_security/user/_has_privileges
			{
			  ""cluster"": [ ""monitor"", ""manage"" ],
			  ""index"" : [
			    {
			      ""names"": [ ""suppliers"", ""products"" ],
			      ""privileges"": [ ""read"" ]
			    },
			    {
			      ""names"": [ ""inventory"" ],
			      ""privileges"" : [ ""read"", ""write"" ]
			    }
			  ],
			  ""application"": [
			    {
			      ""application"": ""inventory_manager"",
			      ""privileges"" : [ ""read"", ""data:write/inventory"" ],
			      ""resources"" : [ ""product/1852563"" ]
			    }
			  ]
			}");
		}
	}
}
