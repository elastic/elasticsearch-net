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

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3907 : IClusterFixture<IntrusiveOperationCluster>
	{
		private readonly IntrusiveOperationCluster _cluster;

		// use intrusive operation cluster because we're changing the underlying http handler
		// and this cluster runs with a max concurrency of 1, so changing http handler
		// will not affect other integration tests
		public GithubIssue3907(IntrusiveOperationCluster cluster) => _cluster = cluster;

		[I]
		public void NotUsingSocketsHttpHandlerDoesNotCauseException()
		{
			AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);

			var response = _cluster.Client.Indices.Exists("non_existent_index");
			response.ApiCall.HttpStatusCode.Should().Be(404);
			response.OriginalException.Should().BeNull();

			AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", true);
		}
	}
}
