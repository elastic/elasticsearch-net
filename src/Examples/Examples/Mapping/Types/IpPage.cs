using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class IpPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::ef38d941f9d914c095e729046a2e2d95[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::ef38d941f9d914c095e729046a2e2d95[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""ip_addr"": {
			        ""type"": ""ip""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""ip_addr"": ""192.168.1.1""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""ip_addr"": ""192.168.0.0/16""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line83()
		{
			// tag::96d3e3ee5d410507ca6ffb64a7e3d88e[]
			var response0 = new SearchResponse<object>();
			// end::96d3e3ee5d410507ca6ffb64a7e3d88e[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""ip_addr"": ""192.168.0.0/16""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line98()
		{
			// tag::f880cf334c8d355edc3abf196d9a8b67[]
			var response0 = new SearchResponse<object>();
			// end::f880cf334c8d355edc3abf196d9a8b67[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""ip_addr"": ""2001:db8::/48""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line116()
		{
			// tag::db5fe7de772a7607b8d104cc35a6bc6c[]
			var response0 = new SearchResponse<object>();
			// end::db5fe7de772a7607b8d104cc35a6bc6c[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""query_string"" : {
			      ""query"": ""ip_addr:\""2001:db8::/48\""""
			    }
			  }
			}");
		}
	}
}