using System;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Elastic.Xunit;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class ApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: RequestResponseApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>, IClusterFixture<TCluster>
		where TCluster : ClientTestClusterBase, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{

		protected abstract string UrlPath { get; }
		protected abstract HttpMethod HttpMethod { get; }

		protected ApiTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			this.SetupSerialization();
		}

		[U] protected async Task HitsTheCorrectUrl() =>
			await this.AssertOnAllResponses(r => this.AssertUrl(r.ApiCall.Uri));

		[U] protected async Task UsesCorrectHttpMethod() =>
			await this.AssertOnAllResponses(
				r => r.ApiCall.HttpMethod.Should().Be(this.HttpMethod,
					this.UniqueValues.CurrentView.GetStringValue()));

		[U] protected void SerializesInitializer() =>
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);

		[U] protected void SerializesFluent() =>
			this.AssertSerializesAndRoundTrips(this.Fluent?.Invoke(NewDescriptor()));

		private void AssertUrl(Uri u) => u.PathEquals(this.UrlPath, this.UniqueValues.CurrentView.GetStringValue());
	}

}
