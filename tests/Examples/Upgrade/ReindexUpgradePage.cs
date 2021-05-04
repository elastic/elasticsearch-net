// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Upgrade
{
	public class ReindexUpgradePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("upgrade/reindex_upgrade.asciidoc:160")]
		public void Line160()
		{
			// tag::acd65c045139fef38ef5cd20c8c1cfc1[]
			var response0 = new SearchResponse<object>();
			// end::acd65c045139fef38ef5cd20c8c1cfc1[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""remote"": {
			      ""host"": ""http://oldhost:9200"",
			      ""username"": ""user"",
			      ""password"": ""pass""
			    },
			    ""index"": ""source"",
			    ""query"": {
			      ""match"": {
			        ""test"": ""data""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest""
			  }
			}");
		}
	}
}
