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

namespace Examples.XPack.Docs.En.Watcher
{
	public class TroubleshootingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/troubleshooting.asciidoc:18")]
		public void Line18()
		{
			// tag::2856a5ceff1861aa9a78099f1c517fe7[]
			var response0 = new SearchResponse<object>();
			// end::2856a5ceff1861aa9a78099f1c517fe7[]

			response0.MatchesExample(@"GET .watches/_mapping");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/troubleshooting.asciidoc:33")]
		public void Line33()
		{
			// tag::e905543b281e9c41395304da76ed2ea3[]
			var response0 = new SearchResponse<object>();
			// end::e905543b281e9c41395304da76ed2ea3[]

			response0.MatchesExample(@"DELETE .watches");
		}
	}
}
