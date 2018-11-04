using System;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Framework
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
