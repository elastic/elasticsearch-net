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

namespace Examples.DataFrames.Apis
{
	public class DeleteTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::c0632f2983704d482200d4900e722534[]
			var response0 = new SearchResponse<object>();
			// end::c0632f2983704d482200d4900e722534[]

			response0.MatchesExample(@"DELETE _data_frame/transforms/ecommerce_transform");
		}
	}
}
