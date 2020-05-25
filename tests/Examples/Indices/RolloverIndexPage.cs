// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class RolloverIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:11")]
		public void Line11()
		{
			// tag::84e8a4cf3453ed2fd4aeecdbdb02b813[]
			var response0 = new SearchResponse<object>();
			// end::84e8a4cf3453ed2fd4aeecdbdb02b813[]

			response0.MatchesExample(@"POST /alias1/_rollover/twitter
			{
			  ""conditions"": {
			    ""max_age"":   ""7d"",
			    ""max_docs"":  1000,
			    ""max_size"": ""5gb""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:160")]
		public void Line160()
		{
			// tag::593c11e8a9f88ec2629f2eb33cded9b7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::593c11e8a9f88ec2629f2eb33cded9b7[]

			response0.MatchesExample(@"PUT /logs-000001 \<1>
			{
			  ""aliases"": {
			    ""logs_write"": {}
			  }
			}");

			response1.MatchesExample(@"# Add > 1000 documents to logs-000001");

			response2.MatchesExample(@"POST /logs_write/_rollover \<2>
			{
			  ""conditions"": {
			    ""max_age"":   ""7d"",
			    ""max_docs"":  1000,
			    ""max_size"":  ""5gb""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:220")]
		public void Line220()
		{
			// tag::75f887596c4972bc679929ca996698f2[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::75f887596c4972bc679929ca996698f2[]

			response0.MatchesExample(@"PUT /logs-000001
			{
			  ""aliases"": {
			    ""logs_write"": {}
			  }
			}");

			response1.MatchesExample(@"POST /logs_write/_rollover
			{
			  ""conditions"" : {
			    ""max_age"": ""7d"",
			    ""max_docs"": 1000,
			    ""max_size"": ""5gb""
			  },
			  ""settings"": {
			    ""index.number_of_shards"": 2
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:254")]
		public void Line254()
		{
			// tag::659247d91f61ceb17cbcc60801fd3456[]
			var response0 = new SearchResponse<object>();
			// end::659247d91f61ceb17cbcc60801fd3456[]

			response0.MatchesExample(@"POST /my_alias/_rollover/my_new_index_name
			{
			  ""conditions"": {
			    ""max_age"":   ""7d"",
			    ""max_docs"":  1000,
			    ""max_size"": ""5gb""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:278")]
		public void Line278()
		{
			// tag::8f6ef669c09e0c8bfc2731f422471770[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();
			// end::8f6ef669c09e0c8bfc2731f422471770[]

			response0.MatchesExample(@"# PUT /<logs-{now/d}-1> with URI encoding:");

			response1.MatchesExample(@"PUT /%3Clogs-%7Bnow%2Fd%7D-1%3E \<1>
			{
			  ""aliases"": {
			    ""logs_write"": {}
			  }
			}");

			response2.MatchesExample(@"PUT logs_write/_doc/1
			{
			  ""message"": ""a dummy log""
			}");

			response3.MatchesExample(@"POST logs_write/_refresh");

			response4.MatchesExample(@"# Wait for a day to pass");

			response5.MatchesExample(@"POST /logs_write/_rollover \<2>
			{
			  ""conditions"": {
			    ""max_docs"":   ""1""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:337")]
		public void Line337()
		{
			// tag::03584e88046614ec7727db506d866f48[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::03584e88046614ec7727db506d866f48[]

			response0.MatchesExample(@"# GET /<logs-{now/d}-*>,<logs-{now/d-1d}-*>,<logs-{now/d-2d}-*>/_search");

			response1.MatchesExample(@"GET /%3Clogs-%7Bnow%2Fd%7D-*%3E%2C%3Clogs-%7Bnow%2Fd-1d%7D-*%3E%2C%3Clogs-%7Bnow%2Fd-2d%7D-*%3E/_search");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:352")]
		public void Line352()
		{
			// tag::896eb7487a512fc43a2af7e16717f40d[]
			var response0 = new SearchResponse<object>();
			// end::896eb7487a512fc43a2af7e16717f40d[]

			response0.MatchesExample(@"POST /logs_write/_rollover?dry_run
			{
			  ""conditions"" : {
			    ""max_age"": ""7d"",
			    ""max_docs"": 1000,
			    ""max_size"": ""5gb""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/rollover-index.asciidoc:380")]
		public void Line380()
		{
			// tag::9e9a3ad495e6305563a88dd4c74a5fda[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();
			// end::9e9a3ad495e6305563a88dd4c74a5fda[]

			response0.MatchesExample(@"PUT my_logs_index-000001
			{
			  ""aliases"": {
			    ""logs"": { ""is_write_index"": true } \<1>
			  }
			}");

			response1.MatchesExample(@"PUT logs/_doc/1
			{
			  ""message"": ""a dummy log""
			}");

			response2.MatchesExample(@"POST logs/_refresh");

			response3.MatchesExample(@"POST /logs/_rollover
			{
			  ""conditions"": {
			    ""max_docs"":   ""1""
			  }
			}");

			response4.MatchesExample(@"PUT logs/_doc/2 \<2>
			{
			  ""message"": ""a newer log""
			}");
		}
	}
}
