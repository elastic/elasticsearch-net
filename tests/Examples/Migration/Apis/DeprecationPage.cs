// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Migration.Apis
{
	public class DeprecationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("migration/apis/deprecation.asciidoc:43")]
		public void Line43()
		{
			// tag::135819da3a4bde684357c57a49ad8e85[]
			var response0 = new SearchResponse<object>();
			// end::135819da3a4bde684357c57a49ad8e85[]

			response0.MatchesExample(@"GET /_migration/deprecations");
		}

		[U(Skip = "Example not implemented")]
		[Description("migration/apis/deprecation.asciidoc:122")]
		public void Line122()
		{
			// tag::69f8b0f2a9ba47e11f363d788cee9d6d[]
			var response0 = new SearchResponse<object>();
			// end::69f8b0f2a9ba47e11f363d788cee9d6d[]

			response0.MatchesExample(@"GET /logstash-*/_migration/deprecations");
		}
	}
}
