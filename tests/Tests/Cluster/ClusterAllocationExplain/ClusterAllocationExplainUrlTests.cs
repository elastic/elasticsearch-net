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

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;

namespace Tests.Cluster.ClusterAllocationExplain
{
	public class ClusterAllocationExplainUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("/_cluster/allocation/explain?include_yes_decisions=true")
			.Fluent(c => c.Cluster.AllocationExplain(s => s.Index<Project>().Shard(0).Primary().IncludeYesDecisions()))
			.Request(c => c.Cluster.AllocationExplain(new ClusterAllocationExplainRequest
				{ Index = typeof(Project), Shard = 0, Primary = true, IncludeYesDecisions = true }))
			.FluentAsync(c => c.Cluster.AllocationExplainAsync(s => s.Index<Project>().Shard(0).Primary().IncludeYesDecisions()))
			.RequestAsync(c => c.Cluster.AllocationExplainAsync(new ClusterAllocationExplainRequest
				{ Index = typeof(Project), Shard = 0, Primary = true, IncludeYesDecisions = true }));
	}
}
