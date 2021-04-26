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

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.NestSpecific.Raw
{
	/**
	 * Allows a query represented as a string of JSON to be passed to NEST's Fluent API or Object Initializer syntax.
	 * This can be useful when porting over a query expressed in the query DSL over to NEST.
	 */
	public class RawUsageTests : QueryDslUsageTestsBase
	{
		private static readonly string RawTermQuery = @"{""term"": { ""fieldname"":""value"" } }";

		public RawUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new RawQuery(RawTermQuery);

		protected override object QueryJson => new
		{
			term = new { fieldname = "value" }
		};

		protected override bool SupportsDeserialization => false;

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Raw(RawTermQuery);
	}
}
