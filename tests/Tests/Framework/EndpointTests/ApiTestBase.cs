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
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Extensions;

namespace Tests.Framework.EndpointTests
{
	public abstract class ApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: RequestResponseApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected ApiTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = null;
		protected abstract HttpMethod HttpMethod { get; }
		protected abstract string UrlPath { get; }

		[U] protected virtual async Task HitsTheCorrectUrl() => await AssertOnAllResponses(r => AssertUrl(r.ApiCall.Uri));

		[U] protected virtual async Task UsesCorrectHttpMethod() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpMethod.Should().Be(HttpMethod, UniqueValues.CurrentView.GetStringValue()));

		[U] protected virtual void SerializesInitializer() => RoundTripsOrSerializes<TInterface>(Initializer);

		[U] protected virtual void SerializesFluent() => RoundTripsOrSerializes(Fluent?.Invoke(NewDescriptor()));

		private void AssertUrl(Uri u) => u.PathEquals(UrlPath, UniqueValues.CurrentView.GetStringValue());
	}
}
