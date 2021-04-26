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

namespace Examples.Indices
{
	public class DeleteIndexPage : ExampleBase
	{
		[U]
		[Description("indices/delete-index.asciidoc:10")]
		public void Line10()
		{
			// tag::98f14fddddea54a7d6149ab7b92e099d[]
			var deleteIndexResponse = client.DeleteIndex("twitter");
			// end::98f14fddddea54a7d6149ab7b92e099d[]

			deleteIndexResponse.MatchesExample(@"DELETE /twitter");
		}
	}
}
