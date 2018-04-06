using System;
using System.Net;
using System.Threading.Tasks;
using Elastic.Managed;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class ConnectionErrorTestBase<TCluster>
		: ApiTestBase<TCluster, IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration> , new()
	{
		protected ConnectionErrorTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RootNodeInfo(f),
			fluentAsync: (client, f) => client.RootNodeInfoAsync(f),
			request: (client, r) => client.RootNodeInfo(r),
			requestAsync: (client, r) => client.RootNodeInfoAsync(r)
		);

		public override IElasticClient Client => this.Cluster.Client;
		protected override RootNodeInfoRequest Initializer => new RootNodeInfoRequest();

		protected override string UrlPath => "";
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		[I] public async Task IsValidIsFalse() => await this.AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(false));

		[I] public async Task AssertException() => await this.AssertOnAllResponses(r =>
		{
			var e = r.OriginalException;
			e.Should().NotBeNull();
			if (e is WebException) this.AssertWebException((WebException) e);
			else if (e is System.Net.Http.HttpRequestException)
				this.AssertHttpRequestException((System.Net.Http.HttpRequestException) e);
			else throw new Exception("Response orginal exception is not one of the expected connection exception but" + e.GetType().FullName);
		});

		protected abstract void AssertWebException(WebException e);
		protected abstract void AssertHttpRequestException(System.Net.Http.HttpRequestException e);

	}
}
