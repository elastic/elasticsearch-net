// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class SamlAuthenticateApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-authenticate-api.asciidoc:79")]
		public void Line79()
		{
			// tag::8e208098a0156c4c92afe0a06960b230[]
			var response0 = new SearchResponse<object>();
			// end::8e208098a0156c4c92afe0a06960b230[]

			response0.MatchesExample(@"POST /_security/saml/authenticate
			{
			  ""content"" : ""PHNhbWxwOlJlc3BvbnNlIHhtbG5zOnNhbWxwPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6cHJvdG9jb2wiIHhtbG5zOnNhbWw9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMD....."",
			  ""ids"" : [""4fee3b046395c4e751011e97f8900b5273d56685""]
			}");
		}
	}
}
