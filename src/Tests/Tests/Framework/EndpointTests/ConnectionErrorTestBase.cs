using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class ConnectionErrorTestBase<TCluster>
		: RequestResponseApiTestBase<TCluster, IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		protected ConnectionErrorTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override IElasticClient Client => Cluster.Client;

		protected override object ExpectJson { get; } = null;
		protected override RootNodeInfoRequest Initializer => new RootNodeInfoRequest();

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.RootNodeInfo(f),
			(client, f) => client.RootNodeInfoAsync(f),
			(client, r) => client.RootNodeInfo(r),
			(client, r) => client.RootNodeInfoAsync(r)
		);

		//[I]
		public async Task IsValidIsFalse() => await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(false));

		//[I]
		public async Task AssertException() => await AssertOnAllResponses(r =>
		{
			var e = r.OriginalException;
			e.Should().NotBeNull();
			FindWebExceptionOrHttpRequestException(e, e);
		});

		private void FindWebExceptionOrHttpRequestException(Exception mainException, Exception currentException)
		{
			mainException.Should().NotBeNull();
			currentException.Should().NotBeNull();
			if (currentException is WebException exception) AssertWebException(exception);
			else if (currentException is HttpRequestException requestException) AssertHttpRequestException(requestException);
			else if (currentException.InnerException != null)
				FindWebExceptionOrHttpRequestException(mainException, currentException.InnerException);
			else
				throw new Exception("Unable to find WebException or HttpRequestException on" + mainException.GetType().FullName);
		}

		protected abstract void AssertWebException(WebException e);

		protected abstract void AssertHttpRequestException(HttpRequestException e);
	}
}
