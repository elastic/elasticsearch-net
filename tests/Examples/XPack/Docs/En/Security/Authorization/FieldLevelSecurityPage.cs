// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Authorization
{
	public class FieldLevelSecurityPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:18")]
		public void Line18()
		{
			// tag::976e5f9baf81bd6ca0e9f80916a0a4f9[]
			var response0 = new SearchResponse<object>();
			// end::976e5f9baf81bd6ca0e9f80916a0a4f9[]

			response0.MatchesExample(@"POST /_security/role/test_role1
			{
			  ""indices"": [
			    {
			      ""names"": [ ""events-*"" ],
			      ""privileges"": [ ""read"" ],
			      ""field_security"" : {
			        ""grant"" : [ ""category"", ""@timestamp"", ""message"" ]
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:43")]
		public void Line43()
		{
			// tag::7c9076f3e93a8f61189783c736bf6082[]
			var response0 = new SearchResponse<object>();
			// end::7c9076f3e93a8f61189783c736bf6082[]

			response0.MatchesExample(@"POST /_security/role/test_role2
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""grant"" : [ ""event_*"" ]
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:77")]
		public void Line77()
		{
			// tag::d7d92816cac64b7c70d72b0000eeeeea[]
			var response0 = new SearchResponse<object>();
			// end::d7d92816cac64b7c70d72b0000eeeeea[]

			response0.MatchesExample(@"POST /_security/role/test_role3
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""grant"" : [ ""customer.handle"" ]
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:96")]
		public void Line96()
		{
			// tag::bb28d1f7f3f09f5061d7f4351aee89fc[]
			var response0 = new SearchResponse<object>();
			// end::bb28d1f7f3f09f5061d7f4351aee89fc[]

			response0.MatchesExample(@"POST /_security/role/test_role4
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""grant"" : [ ""customer.*"" ]
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:114")]
		public void Line114()
		{
			// tag::7a987cd13383bdc990155d7bd5fb221e[]
			var response0 = new SearchResponse<object>();
			// end::7a987cd13383bdc990155d7bd5fb221e[]

			response0.MatchesExample(@"POST /_security/role/test_role5
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""grant"" : [ ""*""],
			        ""except"": [ ""customer.handle"" ]
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:142")]
		public void Line142()
		{
			// tag::962e6187bbd71c5749376efed04b65ba[]
			var response0 = new SearchResponse<object>();
			// end::962e6187bbd71c5749376efed04b65ba[]

			response0.MatchesExample(@"POST /_security/role/test_role6
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""except"": [ ""customer.handle"" ],
			        ""grant"" : [ ""customer.*"" ]
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authorization/field-level-security.asciidoc:169")]
		public void Line169()
		{
			// tag::a1acf454bd6477183ce27ace872deb46[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a1acf454bd6477183ce27ace872deb46[]

			response0.MatchesExample(@"POST /_security/role/test_role7
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""grant"": [ ""a.*"" ],
			        ""except"" : [ ""a.b*"" ]
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"POST /_security/role/test_role8
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""*"" ],
			      ""privileges"" : [ ""read"" ],
			      ""field_security"" : {
			        ""grant"": [ ""a.b*"" ],
			        ""except"" : [ ""a.b.c*"" ]
			      }
			    }
			  ]
			}");
		}
	}
}
