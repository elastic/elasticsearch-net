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

namespace Examples.Transform.Apis
{
	public class GetTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/get-transform.asciidoc:96")]
		public void Line96()
		{
			// tag::c65b00a285f510dcd2865aa3539b4e03[]
			var response0 = new SearchResponse<object>();
			// end::c65b00a285f510dcd2865aa3539b4e03[]

			response0.MatchesExample(@"GET _transform?size=10");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/apis/get-transform.asciidoc:105")]
		public void Line105()
		{
			// tag::c8ebbecc372bcfa5f4a6e7242395ab5e[]
			var response0 = new SearchResponse<object>();
			// end::c8ebbecc372bcfa5f4a6e7242395ab5e[]

			response0.MatchesExample(@"GET _transform/ecommerce_transform");
		}
	}
}
