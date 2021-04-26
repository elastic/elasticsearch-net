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
