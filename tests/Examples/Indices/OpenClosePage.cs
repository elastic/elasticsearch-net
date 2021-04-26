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

namespace Examples.Indices
{
	public class OpenClosePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/open-close.asciidoc:11")]
		public void Line11()
		{
			// tag::7f36828a03e8cb5a028d9a6efb056b88[]
			var response0 = new SearchResponse<object>();
			// end::7f36828a03e8cb5a028d9a6efb056b88[]

			response0.MatchesExample(@"POST /twitter/_open");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/open-close.asciidoc:122")]
		public void Line122()
		{
			// tag::37e6177bf8803971d30a4252498c07a4[]
			var response0 = new SearchResponse<object>();
			// end::37e6177bf8803971d30a4252498c07a4[]

			response0.MatchesExample(@"POST /my_index/_open");
		}
	}
}
