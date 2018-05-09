using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class ConnectionErrorTestBase<TCluster>
		: RequestResponseApiTestBase<TCluster, IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster , new()
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

		[I] public async Task IsValidIsFalse() => await this.AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(false));

		[I] public async Task AssertException() => await this.AssertOnAllResponses(r =>
		{
			var e = r.OriginalException;
			e.Should().NotBeNull();
			FindUnderlyingException(e, e);
		});

		private void FindUnderlyingException(Exception mainException, Exception currentException)
		{
			mainException.Should().NotBeNull();
			currentException.Should().NotBeNull();
			if (currentException is WebException exception) this.AssertWebException(exception);
			else if (currentException is HttpRequestException requestException) this.AssertHttpRequestException(requestException);
			else if (currentException.InnerException != null)
				FindUnderlyingException(mainException, currentException.InnerException);
			else
				throw new Exception("Unable to find WebException or HttpRequestException on" + mainException.GetType().FullName);
		}

		protected abstract void AssertWebException(WebException e);
		protected abstract void AssertHttpRequestException(System.Net.Http.HttpRequestException e);

	}
}
