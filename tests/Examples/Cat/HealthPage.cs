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
using System.ComponentModel;

namespace Examples.Cat
{
	public class HealthPage : ExampleBase
	{
		[U]
		[Description("cat/health.asciidoc:69")]
		public void Line69()
		{
			// tag::f8cc4b331a19ff4df8e4a490f906ee69[]
			var catResponse = client.Cat.Health(h => h.Verbose());
			// end::f8cc4b331a19ff4df8e4a490f906ee69[]

			catResponse.MatchesExample(@"GET /_cat/health?v");
		}

		[U]
		[Description("cat/health.asciidoc:89")]
		public void Line89()
		{
			// tag::ccd9e2cf7181de67cf9ab0df1a02c575[]
			var catResponse = client.Cat.Health(h => h.Verbose().IncludeTimestamp(false));
			// end::ccd9e2cf7181de67cf9ab0df1a02c575[]

			catResponse.MatchesExample(@"GET /_cat/health?v&ts=false");
		}
	}
}
