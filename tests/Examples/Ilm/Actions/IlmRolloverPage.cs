using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmRolloverPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-rollover.asciidoc:30")]
		public void Line30()
		{
			// tag::d7e7489b7d176aa854dfc785a12feab3[]
			var response0 = new SearchResponse<object>();
			// end::d7e7489b7d176aa854dfc785a12feab3[]

			response0.MatchesExample(@"PUT my_index-000001
			{
			  ""settings"": {
			    ""index.lifecycle.name"": ""my_policy"",
			    ""index.lifecycle.rollover_alias"": ""my_data""
			  },
			  ""aliases"": {
			    ""my_data"": {
			      ""is_write_index"": true
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-rollover.asciidoc:79")]
		public void Line79()
		{
			// tag::19211ccf772f1dee7b500c21f4a9a805[]
			var response0 = new SearchResponse<object>();
			// end::19211ccf772f1dee7b500c21f4a9a805[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_size"": ""100GB""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-rollover.asciidoc:102")]
		public void Line102()
		{
			// tag::cfd4b34f35e531a20739a3b308d57134[]
			var response0 = new SearchResponse<object>();
			// end::cfd4b34f35e531a20739a3b308d57134[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_docs"": 100000000
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-rollover.asciidoc:125")]
		public void Line125()
		{
			// tag::d4a41fb74b41b41a0ee114a2311f2815[]
			var response0 = new SearchResponse<object>();
			// end::d4a41fb74b41b41a0ee114a2311f2815[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_age"": ""7d""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-rollover.asciidoc:150")]
		public void Line150()
		{
			// tag::8940f2b911220acc9afef6360b6c13c4[]
			var response0 = new SearchResponse<object>();
			// end::8940f2b911220acc9afef6360b6c13c4[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_age"": ""7d"",
			            ""max_size"": ""100GB""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-rollover.asciidoc:178")]
		public void Line178()
		{
			// tag::f6c79fa1c01bb4539d0cba0bd62c1ce0[]
			var response0 = new SearchResponse<object>();
			// end::f6c79fa1c01bb4539d0cba0bd62c1ce0[]

			response0.MatchesExample(@"PUT /_ilm/policy/rollover_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""50G""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""1d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}
	}
}