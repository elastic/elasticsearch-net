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
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
 using Elastic.Elasticsearch.Ephemeral;
 using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Framework.EndpointTests
{
	public abstract class ApiIntegrationTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected ApiIntegrationTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override IElasticClient Client => Cluster.Client;
		protected abstract bool ExpectIsValid { get; }
		protected abstract int ExpectStatusCode { get; }
		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		protected virtual void ExpectResponse(TResponse response) { }

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public virtual async Task ReturnsExpectedStatusCode() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(ExpectStatusCode));

		[I] public virtual async Task ReturnsExpectedIsValid() =>
			await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(ExpectIsValid));

		[I] public virtual async Task ReturnsExpectedResponse() => await AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert) =>
			base.AssertOnAllResponses((r) =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
					&& !(r.ApiCall.OriginalException is ElasticsearchClientException))
				{
					var e = ExceptionDispatchInfo.Capture(r.ApiCall.OriginalException.Demystify());
					throw new ResponseAssertionException(e.SourceException, r).Demystify();
				}

				try
				{
					assert(r);
				}
				catch (Exception e)
				{
					var ex = ExceptionDispatchInfo.Capture(e.Demystify());
					throw new ResponseAssertionException(ex.SourceException, r).Demystify();
				}
			});
	}

	public class ResponseAssertionException : Exception
	{

		public ResponseAssertionException(Exception innerException, IResponse response)
			: base(ResponseInMessage(innerException.Message, response), innerException) { }

		private static string ResponseInMessage(string innerExceptionMessage, IResponse r) => $@"{innerExceptionMessage}
Response Under Test:
{r.DebugInformation}";
	}
}
