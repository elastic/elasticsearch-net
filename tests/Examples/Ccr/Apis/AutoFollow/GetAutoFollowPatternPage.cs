// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class GetAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/get-auto-follow-pattern.asciidoc:41")]
		public void Line41()
		{
			// tag::5e124875d97c27362ae858160ae1c6d5[]
			var response0 = new SearchResponse<object>();
			// end::5e124875d97c27362ae858160ae1c6d5[]

			response0.MatchesExample(@"GET /_ccr/auto_follow/");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/get-auto-follow-pattern.asciidoc:46")]
		public void Line46()
		{
			// tag::d56a9d89282df56adbbc34b91390ac17[]
			var response0 = new SearchResponse<object>();
			// end::d56a9d89282df56adbbc34b91390ac17[]

			response0.MatchesExample(@"GET /_ccr/auto_follow/<auto_follow_pattern_name>");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/get-auto-follow-pattern.asciidoc:79")]
		public void Line79()
		{
			// tag::79f33e05b203eb46eef7958fbc95ef77[]
			var response0 = new SearchResponse<object>();
			// end::79f33e05b203eb46eef7958fbc95ef77[]

			response0.MatchesExample(@"GET /_ccr/auto_follow/my_auto_follow_pattern");
		}
	}
}
