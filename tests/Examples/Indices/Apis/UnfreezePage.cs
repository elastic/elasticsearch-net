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

namespace Examples.Indices.Apis
{
	public class UnfreezePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/apis/unfreeze.asciidoc:42")]
		public void Line42()
		{
			// tag::ffea06f77c9df5720412aa06be964118[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ffea06f77c9df5720412aa06be964118[]

			response0.MatchesExample(@"POST /my_index/_freeze");

			response1.MatchesExample(@"POST /my_index/_unfreeze");
		}
	}
}
