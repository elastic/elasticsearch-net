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
using Examples.Models;
using Nest;
using static Nest.Infer;
using System.ComponentModel;

namespace Examples.Root
{
	public class MappingPage : ExampleBase
	{
		[U]
		[Description("mapping.asciidoc:147")]
		public void Line147()
		{
			// tag::d8b2a88b5eca99d3691ad3cd40266736[]
			var createIndexResponse = client.Indices.Create("my-index", c => c
				.Map<Employee>(m => m
					.Properties(props => props
						.Number(n => n.Name(p => p.Age).Type(NumberType.Integer))
						.Keyword(k => k.Name(p => p.Email))
						.Text(k => k.Name(p => p.Name))
					)
				)
			);
			// end::d8b2a88b5eca99d3691ad3cd40266736[]

			createIndexResponse.MatchesExample(@"PUT /my-index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""age"":    { ""type"": ""integer"" },  <1>
			      ""email"":  { ""type"": ""keyword""  }, <2>
			      ""name"":   { ""type"": ""text""  }     <3>
			    }
			  }
			}");
		}

		[U]
		[Description("mapping.asciidoc:176")]
		public void Line176()
		{
			// tag::71ba9033107882f61cdc3b32fc73568d[]
			var mapResponse = client.Map<Employee>(m => m
				.Index("my-index")
				.Properties(props => props
					.Keyword(k => k
						.Name(p => p.EmployeeId)
						.Index(false)
					)
				)
			);
			// end::71ba9033107882f61cdc3b32fc73568d[]

			mapResponse.MatchesExample(@"PUT /my-index/_mapping
			{
			  ""properties"": {
			    ""employee-id"": {
			      ""type"": ""keyword"",
			      ""index"": false
			    }
			  }
			}");
		}

		[U]
		[Description("mapping.asciidoc:217")]
		public void Line217()
		{
			// tag::609260ad1d5998be2ca09ff1fe237efa[]
			var getMappingResponse = client.Indices.GetMapping<Employee>(m => m.Index("my-index"));
			// end::609260ad1d5998be2ca09ff1fe237efa[]

			getMappingResponse.MatchesExample(@"GET /my-index/_mapping");
		}

		[U]
		[Description("mapping.asciidoc:263")]
		public void Line263()
		{
			// tag::99a52be903945b17e734a1d02a57e958[]
			var getMappingResponse = client.Indices.GetFieldMapping<Employee>(
				Field<Employee>(p => p.EmployeeId),
				m => m.Index("my-index")
			);
			// end::99a52be903945b17e734a1d02a57e958[]

			getMappingResponse.MatchesExample(@"GET /my-index/_mapping/field/employee-id");
		}
	}
}
