// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class PauseAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/pause-auto-follow-pattern.asciidoc:76")]
		public void Line76()
		{
			// tag::b25256ed615cd837461b0bfa590526b7[]
			var response0 = new SearchResponse<object>();
			// end::b25256ed615cd837461b0bfa590526b7[]

			response0.MatchesExample(@"POST /_ccr/auto_follow/my_auto_follow_pattern/pause");
		}
	}
}
