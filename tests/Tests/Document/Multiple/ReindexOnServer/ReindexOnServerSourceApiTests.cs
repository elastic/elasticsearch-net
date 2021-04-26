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
using Elasticsearch.Net;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexOnServer
{
	public class ReindexOnServerSourceApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor,
			ReindexOnServerRequest>
	{
		public ReindexOnServerSourceApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
				},
				source = new
				{
					index = CallIsolatedValue,
					_source = new[] { "id", "flag" },
				},
				conflicts = "proceed"
			};

		protected override int ExpectStatusCode => 200;

		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Index(CallIsolatedValue)
				.Source<Test>(f => f
					.Field(ff => ff.Id)
					.Field(ff => ff.Flag)
				)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
			)
			.Conflicts(Conflicts.Proceed)
			.Refresh();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue,
				Source = Fields<Test>(
					ff => ff.Id,
					ff => ff.Flag
				)
			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
			},
			Conflicts = Conflicts.Proceed,
			Refresh = true,
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
				Client.Bulk(b => b
					.Index(index)
					.IndexMany(new[]
					{
						new Test { Id = 1, Flag = "bar" },
						new Test { Id = 2, Flag = "bar" }
					})
					.Refresh(Refresh.WaitFor)
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReindexOnServer(f),
			(client, f) => client.ReindexOnServerAsync(f),
			(client, r) => client.ReindexOnServer(r),
			(client, r) => client.ReindexOnServerAsync(r)
		);

		protected override void ExpectResponse(ReindexOnServerResponse response) => response.ShouldBeValid();

		public class Test
		{
			public string Flag { get; set; }
			public long Id { get; set; }
		}
	}
}
