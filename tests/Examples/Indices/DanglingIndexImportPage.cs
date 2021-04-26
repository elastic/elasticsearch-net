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

namespace Examples.Indices
{
	public class DanglingIndexImportPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/dangling-index-import.asciidoc:13")]
		public void Line13()
		{
			// tag::a3d943ac9d45b4eff4aa0c679b4eceb3[]
			var response0 = new SearchResponse<object>();
			// end::a3d943ac9d45b4eff4aa0c679b4eceb3[]

			response0.MatchesExample(@"POST /_dangling/<index-uuid>?accept_data_loss=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/dangling-index-import.asciidoc:53")]
		public void Line53()
		{
			// tag::ca98afbd6a90f63e02f62239d225313b[]
			var response0 = new SearchResponse<object>();
			// end::ca98afbd6a90f63e02f62239d225313b[]

			response0.MatchesExample(@"POST /_dangling/zmM4e0JtBkeUjiHD-MihPQ?accept_data_loss=true");
		}
	}
}