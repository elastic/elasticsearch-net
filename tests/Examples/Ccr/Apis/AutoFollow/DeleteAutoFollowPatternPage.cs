// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class DeleteAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/delete-auto-follow-pattern.asciidoc:34")]
		public void Line34()
		{
			// tag::2f2580ea420e1836d922fe48fa8ada97[]
			var response0 = new SearchResponse<object>();
			// end::2f2580ea420e1836d922fe48fa8ada97[]

			response0.MatchesExample(@"DELETE /_ccr/auto_follow/<auto_follow_pattern_name>");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/delete-auto-follow-pattern.asciidoc:66")]
		public void Line66()
		{
			// tag::d4ef6ac034c4d42cb75d830ec69146e6[]
			var response0 = new SearchResponse<object>();
			// end::d4ef6ac034c4d42cb75d830ec69146e6[]

			response0.MatchesExample(@"DELETE /_ccr/auto_follow/my_auto_follow_pattern");
		}
	}
}
