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

namespace Examples.Ilm.Actions
{
	public class IlmShrinkPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-shrink.asciidoc:49")]
		public void Line49()
		{
			// tag::f3b4ddce8ff21fc1a76a7c0d9c36650e[]
			var response0 = new SearchResponse<object>();
			// end::f3b4ddce8ff21fc1a76a7c0d9c36650e[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""shrink"" : {
			            ""number_of_shards"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}