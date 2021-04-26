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
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Infer;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cluster/health")
					.Fluent(c => c.Cluster.Health())
					.Request(c => c.Cluster.Health(new ClusterHealthRequest()))
					.FluentAsync(c => c.Cluster.HealthAsync())
					.RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest()))
				;

			await GET("/_cluster/health/_all")
				.Fluent(c => c.Cluster.Health(AllIndices))
				.Request(c => c.Cluster.Health(new ClusterHealthRequest(AllIndices)))
				.FluentAsync(c => c.Cluster.HealthAsync(AllIndices))
				.RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest(AllIndices)));

			await GET("/_cluster/health/project")
					.Fluent(c => c.Cluster.Health(Index<Project>()))
					.Request(c => c.Cluster.Health(new ClusterHealthRequest(typeof(Project))))
					.FluentAsync(c => c.Cluster.HealthAsync(Index<Project>()))
					.RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest(typeof(Project))))
				;
		}
	}
}
