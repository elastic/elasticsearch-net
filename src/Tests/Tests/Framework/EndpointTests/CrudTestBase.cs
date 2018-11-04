using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework
{
	public abstract class CrudWithNoDeleteTestBase<TCreateResponse, TReadResponse, TUpdateResponse>
		: CrudTestBase<TCreateResponse, TReadResponse, TUpdateResponse, AcknowledgedResponseBase>
		where TCreateResponse : class, IResponse
		where TReadResponse : class, IResponse
		where TUpdateResponse : class, IResponse
	{
		protected CrudWithNoDeleteTestBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeletes => false;
	}

	public abstract class CrudTestBase<TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse>
		: CrudTestBase<WritableCluster, TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse>
		where TCreateResponse : class, IResponse
		where TReadResponse : class, IResponse
		where TUpdateResponse : class, IResponse
		where TDeleteResponse : class, IResponse
	{
		protected CrudTestBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
	}

	public abstract class CrudTestBase<TCluster, TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse>
		: IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
		where TCreateResponse : class, IResponse
		where TReadResponse : class, IResponse
		where TUpdateResponse : class, IResponse
		where TDeleteResponse : class, IResponse
	{
		private readonly TCluster _cluster;
		private readonly LazyResponses _createGetResponse;
		private readonly LazyResponses _createResponse;
		private readonly LazyResponses _deleteGetResponse;
		private readonly LazyResponses _deleteResponse;
		private readonly LazyResponses _updateGetResponse;
		private readonly LazyResponses _updateResponse;

		[SuppressMessage("Potential Code Quality Issues", "RECS0021:Warns about calls to virtual member functions occuring in the constructor",
			Justification = "Expected behaviour")]
		protected CrudTestBase(TCluster cluster, EndpointUsage usage)
		{
			_cluster = cluster;
			_createResponse = usage.CallOnce(Create, 1);
			_createGetResponse = usage.CallOnce(Read, 2);
			_updateResponse = usage.CallOnce(Update, 3);
			_updateGetResponse = usage.CallOnce(Read, 4);
			_deleteResponse = usage.CallOnce(Delete, 5);
			_deleteGetResponse = usage.CallOnce(Read, 6);
		}

		protected virtual IElasticClient Client => _cluster.Client;

		protected virtual bool SupportsDeletes => true;

		private static string RandomFluent { get; } = $"fluent-{RandomString()}";
		private static string RandomFluentAsync { get; } = $"fluentasync-{RandomString()}";
		private static string RandomInitializer { get; } = $"ois-{RandomString()}";
		private static string RandomInitializerAsync { get; } = $"oisasync-{RandomString()}";

		protected abstract LazyResponses Create();

		protected abstract LazyResponses Read();

		protected abstract LazyResponses Update();

		protected virtual LazyResponses Delete() => LazyResponses.Empty;

		protected LazyResponses Calls<TDescriptor, TInitializer, TInterface, TResponse>(
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var client = Client;
			return new LazyResponses(async () =>
			{
				var dict = new Dictionary<ClientMethod, IResponse>();

				var sf = Sanitize(RandomFluent);
				dict.Add(ClientMethod.Fluent, fluent(sf, client, f => fluentBody(sf, f)));

				var sfa = Sanitize(RandomFluentAsync);
				dict.Add(ClientMethod.FluentAsync, await fluentAsync(sfa, client, f => fluentBody(sfa, f)));

				var si = Sanitize(RandomInitializer);
				dict.Add(ClientMethod.Initializer, request(si, client, initializerBody(si)));

				var sia = Sanitize(RandomInitializerAsync);
				dict.Add(ClientMethod.InitializerAsync, await requestAsync(sia, client, initializerBody(sia)));
				return dict;
			});
		}

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected virtual string Sanitize(string randomString) => randomString + "-" + GetType().Name.Replace("CrudTests", "").ToLowerInvariant();

		protected async Task AssertOnAllResponses<TResponse>(LazyResponses responses, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			//hack to make sure these are resolved in the right order, calling twice yields cached results so
			//should be fast
			await _createResponse;
			//this.WaitForYellow();
			await _createGetResponse;
			await _updateResponse;
			//this.WaitForYellow();
			await _updateGetResponse;
			if (SupportsDeletes)
			{
				await _deleteResponse;
				//this.WaitForYellow();
				await _deleteGetResponse;
			}

			foreach (var kv in await responses)
			{
				var response = kv.Value as TResponse;
				if (response == null)
					throw new Exception($"{kv.Value.GetType()} is not expected response type {typeof(TResponse)}");

				//try
				//{
				assert(response);
				//}
#pragma warning disable 7095
				//catch (Exception ex) when (false)
#pragma warning restore 7095
				//{
				//	throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				//}
			}
		}

		protected void WaitForYellow()
		{
			//this.Client.ClusterHealth(g => g.WaitForStatus(WaitForStatus.Yellow));
		}

		protected async Task AssertOnCreate(Action<TCreateResponse> assert) => await AssertOnAllResponses(_createResponse, assert);

		protected async Task AssertOnUpdate(Action<TUpdateResponse> assert) => await AssertOnAllResponses(_updateResponse, assert);

		protected async Task AssertOnDelete(Action<TDeleteResponse> assert)
		{
			if (!SupportsDeletes) return;

			await AssertOnAllResponses(_deleteResponse, assert);
		}

		protected async Task AssertOnGetAfterCreate(Action<TReadResponse> assert) => await AssertOnAllResponses(_createGetResponse, assert);

		protected async Task AssertOnGetAfterUpdate(Action<TReadResponse> assert) => await AssertOnAllResponses(_updateGetResponse, assert);

		protected async Task AssertOnGetAfterDelete(Action<TReadResponse> assert)
		{
			if (!SupportsDeletes) return;

			await AssertOnAllResponses(_deleteGetResponse, assert);
		}

		protected virtual void ExpectAfterCreate(TReadResponse response) { }

		protected virtual void ExpectAfterUpdate(TReadResponse response) { }

		[I] protected virtual async Task CreateCallIsValid() => await AssertOnCreate(r => r.ShouldBeValid());

		[I] protected virtual async Task GetAfterCreateIsValid() => await AssertOnGetAfterCreate(r =>
		{
			r.ShouldBeValid();
			ExpectAfterCreate(r);
		});

		[I] protected virtual async Task UpdateCallIsValid() => await AssertOnUpdate(r => r.ShouldBeValid());

		[I] protected virtual async Task GetAfterUpdateIsValid() => await AssertOnGetAfterUpdate(r =>
		{
			r.ShouldBeValid();
			ExpectAfterUpdate(r);
		});

		[I] protected virtual async Task DeleteCallIsValid() => await AssertOnDelete(r => r.ShouldBeValid());

		[I] protected virtual async Task GetAfterDeleteIsValid() => await AssertOnGetAfterDelete(r => r.ShouldNotBeValid());
	}
}
