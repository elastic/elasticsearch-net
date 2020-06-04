// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Modules
{
	public class CrossClusterSearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/cross-cluster-search.asciidoc:35")]
		public void Line35()
		{
			// tag::a8d39396d741e768083808bb11443e9b[]
			var response0 = new SearchResponse<object>();
			// end::a8d39396d741e768083808bb11443e9b[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_one"": {
			          ""seeds"": [
			            ""127.0.0.1:9300""
			          ]
			        },
			        ""cluster_two"": {
			          ""seeds"": [
			            ""127.0.0.1:9301""
			          ]
			        },
			        ""cluster_three"": {
			          ""seeds"": [
			            ""127.0.0.1:9302""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/cross-cluster-search.asciidoc:72")]
		public void Line72()
		{
			// tag::972c0c1b6c0b8327fadd77cc1c71b532[]
			var response0 = new SearchResponse<object>();
			// end::972c0c1b6c0b8327fadd77cc1c71b532[]

			response0.MatchesExample(@"GET /cluster_one:twitter/_search
			{
			  ""query"": {
			    ""match"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/cross-cluster-search.asciidoc:143")]
		public void Line143()
		{
			// tag::6a570214755e38fd587e406d5b19b371[]
			var response0 = new SearchResponse<object>();
			// end::6a570214755e38fd587e406d5b19b371[]

			response0.MatchesExample(@"GET /twitter,cluster_one:twitter,cluster_two:twitter/_search
			{
			  ""query"": {
			    ""match"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/cross-cluster-search.asciidoc:242")]
		public void Line242()
		{
			// tag::9949bcc64c9cd6f4041819546d7fce78[]
			var response0 = new SearchResponse<object>();
			// end::9949bcc64c9cd6f4041819546d7fce78[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.remote.cluster_two.skip_unavailable"": true
			  }
			}");
		}
	}
}
