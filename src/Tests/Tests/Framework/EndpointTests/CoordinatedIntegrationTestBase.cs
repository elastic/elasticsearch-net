using System;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework
{
	public abstract class CoordinatedIntegrationTestBase<TCluster>
		: IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		private readonly TCluster _cluster;
		private readonly CoordinatedUsage _coordinatedUsage;
		private readonly EndpointUsage _usage;

		protected CoordinatedIntegrationTestBase(CoordinatedUsage coordinatedUsage) => _coordinatedUsage = coordinatedUsage;

		protected async Task Assert<TResponse>(string name, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			var lazyResponses = await ExecuteOnceInOrderUntil(name);
			if (lazyResponses == null) throw new Exception($"{name} is defined but it yields no LazyResponses object");

			await AssertOnAllResponses(lazyResponses, assert);
		}

		protected async Task AssertRunsToCompletion(string name)
		{
			var lazyResponses = await ExecuteOnceInOrderUntil(name);
			if (lazyResponses == null) throw new Exception($"{name} is defined but it yields no LazyResponses object");
		}

		protected async Task AssertOnAllResponses<TResponse>(LazyResponses responses, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			foreach (var (_, value) in await responses)
			{
				if (!(value is TResponse response))
					throw new Exception($"{value.GetType()} is not expected response type {typeof(TResponse)}");

				assert(response);
			}
		}

		private async Task<LazyResponses> ExecuteOnceInOrderUntil(string name)
		{
			if (!_coordinatedUsage.Contains(name)) throw new Exception($"{name} is not a keyed after create response");

			foreach (var lazyResponses in _coordinatedUsage)
			{
				await lazyResponses;
				if (lazyResponses.Name == name) return lazyResponses;
			}
			return null;
		}
	}
}
