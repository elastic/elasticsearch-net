// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Framework.EndpointTests
{
	public abstract class ApiIntegrationTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		: ApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
		where TResponse : class, IResponse
		where TDescriptor : class
		where TInitializer : class
	{
		protected ApiIntegrationTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override IElasticClient Client => Cluster.Client;
		protected abstract bool ExpectIsValid { get; }
		protected abstract int ExpectStatusCode { get; }
		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		protected virtual void ExpectResponse(TResponse response) { }

		[I] public virtual async Task ReturnsExpectedStatusCode() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(ExpectStatusCode));

		[I] public virtual async Task ReturnsExpectedIsValid() =>
			await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(ExpectIsValid));

		[I] public virtual async Task ReturnsExpectedResponse() => await AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert) =>
			base.AssertOnAllResponses(r =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
					&& r.ApiCall.OriginalException is not TransportException)
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

	public abstract class NdJsonApiIntegrationTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		: NdJsonApiTestBase<TCluster, TResponse, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, ITestCluster, new()
		where TResponse : class, IResponse
		where TDescriptor : class
		where TInitializer : class
	{
		protected NdJsonApiIntegrationTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override IElasticClient Client => Cluster.Client;
		protected abstract bool ExpectIsValid { get; }
		protected abstract int ExpectStatusCode { get; }
		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		protected virtual void ExpectResponse(TResponse response) { }

		[I]
		public virtual async Task ReturnsExpectedStatusCode() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(ExpectStatusCode));

		[I]
		public virtual async Task ReturnsExpectedIsValid() =>
			await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(ExpectIsValid));

		[I] public virtual async Task ReturnsExpectedResponse() => await AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert) =>
			base.AssertOnAllResponses(r =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
					&& r.ApiCall.OriginalException is not TransportException)
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
