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
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.VotingConfigExclusions.PostVotingConfigExclusions
{
	public class PostingVotingConfigExclusionsTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_cluster/voting_config_exclusions?node_names=node1%2Cnode2")
				.Fluent(c => c.Cluster.PostVotingConfigExclusions(f => f.NodeNames("node1,node2")))
				.Request(c => c.Cluster.PostVotingConfigExclusions(new PostVotingConfigExclusionsRequest{ NodeNames = "node1,node2" }))
				.FluentAsync(c => c.Cluster.PostVotingConfigExclusionsAsync(f => f.NodeNames("node1,node2")))
				.RequestAsync(c => c.Cluster.PostVotingConfigExclusionsAsync(new PostVotingConfigExclusionsRequest{ NodeNames = "node1,node2" }));
	}
}
