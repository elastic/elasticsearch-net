using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using HttpMethod = Elasticsearch.Net.HttpMethod;

namespace Tests.Framework
{
	public abstract class ConnectionErrorTestBase<TCluster>
		: ApiTestBase<TCluster, IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster , new()
	{
		protected ConnectionErrorTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RootNodeInfo(f),
			fluentAsync: (client, f) => client.RootNodeInfoAsync(f),
			request: (client, r) => client.RootNodeInfo(r),
			requestAsync: (client, r) => client.RootNodeInfoAsync(r)
		);

		protected override object ExpectJson { get; } = null;

		protected override RootNodeInfoRequest Initializer => new RootNodeInfoRequest();

		protected override string UrlPath => "";
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		[I] public async Task IsValidIsFalse() => await this.AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(false));

		[I] public async Task AssertException() => await this.AssertOnAllResponses(r =>
		{
			var e = r.OriginalException;
			e.Should().NotBeNull();
			//TODO build seed:85405 integrate 5.6.0 "badcertgenca,denyallcertificates" "workingwithcertificates+badcertgenca,workingwithcertificates+denyallsslcertificates"
			//This is fixed in 6.x and master but due to differences in RequestPipeline.cs this warrants a deeper investigation on 5.x
			//FindWebExceptionOrHttpRequestException(e, e);
		});

		private bool FindWebExceptionOrHttpRequestException(Exception mainException, Exception currentException)
		{
			mainException.Should().NotBeNull();
			currentException.Should().NotBeNull();
			switch (currentException)
			{
				case WebException exception:
					this.AssertWebException(exception);
					return true;
				case HttpRequestException requestException:
					this.AssertHttpRequestException(requestException);
					return true;
				default:
					if (currentException.InnerException != null)
					{
						if (currentException.InnerException is AggregateException ae)
						{
							ae.Flatten();
							if (ae.InnerExceptions.Any(e => FindWebExceptionOrHttpRequestException(mainException, e))) return true;
						}
						else
						{
							if (FindWebExceptionOrHttpRequestException(mainException, currentException.InnerException)) return true;
						}

					}
					if (mainException == currentException)
						throw new Exception("Unable to find WebException or HttpRequestException on" + mainException.GetType().FullName);
					return false;
			}
		}
		protected abstract void AssertWebException(WebException e);
		protected abstract void AssertHttpRequestException(HttpRequestException e);

	}
}
