/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
