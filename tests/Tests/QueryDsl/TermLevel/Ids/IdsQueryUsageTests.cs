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

using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Ids
{
	public class IdsQueryUsageTests : QueryDslUsageTestsBase
	{
		public IdsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IIdsQuery>(a => a.Ids)
		{
			q => q.Values = null,
			q => q.Values = Enumerable.Empty<Id>()
		};

		protected override QueryContainer QueryInitializer => new IdsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Values = new List<Id> { 1, 2, 3, 4 },
		};

		protected override object QueryJson => new
		{
			ids = new
			{
				_name = "named_query",
				boost = 1.1,
				values = new[] { 1, 2, 3, 4 }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Ids(c => c
				.Name("named_query")
				.Boost(1.1)
				.Values(1, 2, 3, 4)
			);
	}
}
