// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.StatusManagement.SyncedFlush
{
	public class SyncedFlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, SyncedFlushResponse, ISyncedFlushRequest, SyncedFlushDescriptor, SyncedFlushRequest>
	{
		public SyncedFlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<SyncedFlushDescriptor, ISyncedFlushRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SyncedFlushRequest Initializer => new SyncedFlushRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush/synced?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.SyncedFlush(CallIsolatedValue, f),
			(client, f) => client.Indices.SyncedFlushAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.SyncedFlush(r),
			(client, r) => client.Indices.SyncedFlushAsync(r)
		);
	}
}
