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
using Nest;

namespace Examples.IndexModules
{
	public class BlocksPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/blocks.asciidoc:76")]
		public void Line76()
		{
			// tag::fcdc59a15a8f6da6e8f30905cae0525c[]
			var response0 = new SearchResponse<object>();
			// end::fcdc59a15a8f6da6e8f30905cae0525c[]

			response0.MatchesExample(@"PUT /twitter/_block/write");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/blocks.asciidoc:143")]
		public void Line143()
		{
			// tag::9bc4ea0bd452bade2feb275883f45861[]
			var response0 = new SearchResponse<object>();
			// end::9bc4ea0bd452bade2feb275883f45861[]

			response0.MatchesExample(@"PUT /my_index/_block/write");
		}
	}
}