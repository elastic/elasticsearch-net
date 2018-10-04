using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
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
		protected override bool SupportsExists => false;

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[I] protected override Task CreateCallIsValid() => base.CreateCallIsValid();
		[I] protected override Task GetAfterCreateIsValid() => base.GetAfterCreateIsValid();
		[I] protected override Task ExistsAfterCreateIsValid() => base.ExistsAfterCreateIsValid();
		[I] protected override Task UpdateCallIsValid() => base.UpdateCallIsValid();
		[I] protected override Task GetAfterUpdateIsValid() => base.GetAfterUpdateIsValid();
		[I] protected override Task DeleteCallIsValid() => base.DeleteCallIsValid();
		[I] protected override Task GetAfterDeleteIsValid() => base.GetAfterDeleteIsValid();
		[I] protected override Task ExistsAfterDeleteIsValid() => base.ExistsAfterDeleteIsValid();
		[I] protected override Task DeleteNotFoundIsNotValid() => base.DeleteNotFoundIsNotValid();
	}

	public abstract class CrudTestBase<TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse>
		: CrudTestBase<WritableCluster, TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse, ExistsResponse>
			where TCreateResponse : class, IResponse
			where TReadResponse : class, IResponse
			where TUpdateResponse : class, IResponse
			where TDeleteResponse : class, IResponse
	{
		protected CrudTestBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override bool SupportsExists => false;

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[I] protected override Task CreateCallIsValid() => base.CreateCallIsValid();
		[I] protected override Task GetAfterCreateIsValid() => base.GetAfterCreateIsValid();
		[I] protected override Task ExistsAfterCreateIsValid() => base.ExistsAfterCreateIsValid();
		[I] protected override Task UpdateCallIsValid() => base.UpdateCallIsValid();
		[I] protected override Task GetAfterUpdateIsValid() => base.GetAfterUpdateIsValid();
		[I] protected override Task DeleteCallIsValid() => base.DeleteCallIsValid();
		[I] protected override Task GetAfterDeleteIsValid() => base.GetAfterDeleteIsValid();
		[I] protected override Task ExistsAfterDeleteIsValid() => base.ExistsAfterDeleteIsValid();
		[I] protected override Task DeleteNotFoundIsNotValid() => base.DeleteNotFoundIsNotValid();
	}
	public abstract class CrudTestBase<TCluster, TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse>
		: CrudTestBase<TCluster, TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse, ExistsResponse>
			where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
			where TCreateResponse : class, IResponse
			where TReadResponse : class, IResponse
			where TUpdateResponse : class, IResponse
			where TDeleteResponse : class, IResponse
	{
		protected CrudTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override bool SupportsExists => false;

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[I] protected override Task CreateCallIsValid() => base.CreateCallIsValid();
		[I] protected override Task GetAfterCreateIsValid() => base.GetAfterCreateIsValid();
		[I] protected override Task ExistsAfterCreateIsValid() => base.ExistsAfterCreateIsValid();
		[I] protected override Task UpdateCallIsValid() => base.UpdateCallIsValid();
		[I] protected override Task GetAfterUpdateIsValid() => base.GetAfterUpdateIsValid();
		[I] protected override Task DeleteCallIsValid() => base.DeleteCallIsValid();
		[I] protected override Task GetAfterDeleteIsValid() => base.GetAfterDeleteIsValid();
		[I] protected override Task ExistsAfterDeleteIsValid() => base.ExistsAfterDeleteIsValid();
		[I] protected override Task DeleteNotFoundIsNotValid() => base.DeleteNotFoundIsNotValid();
	}

	public abstract class CrudTestBase<TCluster, TCreateResponse, TReadResponse, TUpdateResponse, TDeleteResponse, TExistsResponse>
		: IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
			where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster , new()
			where TCreateResponse : class, IResponse
			where TReadResponse : class, IResponse
			where TUpdateResponse : class, IResponse
			where TDeleteResponse : class, IResponse
			where TExistsResponse : class, IResponse, IExistsResponse
	{
		private readonly EndpointUsage _usage;
		private readonly LazyResponses _createResponse;
		private readonly LazyResponses _createGetResponse;
		private readonly Dictionary<string, LazyResponses> _afterCreateResponses = new Dictionary<string, LazyResponses>();
		private readonly LazyResponses _createExistsResponse;
		private readonly LazyResponses _updateResponse;
		private readonly LazyResponses _updateGetResponse;
		private readonly LazyResponses _deleteResponse;
		private readonly LazyResponses _deleteGetResponse;
		private readonly LazyResponses _deleteExistsResponse;
		private readonly LazyResponses _deleteNotFoundResponse;


		[SuppressMessage("Potential Code Quality Issues", "RECS0021:Warns about calls to virtual member functions occuring in the constructor", Justification = "Expected behaviour")]
		protected CrudTestBase(TCluster cluster, EndpointUsage usage)
		{
			this._usage = usage;
			this.Cluster = cluster;
			this._createResponse = usage.CallOnce(this.Create, 1);
			this._createGetResponse = usage.CallOnce(this.Read, 2);
			this._createExistsResponse = usage.CallOnce(this.Exists, 3);
			var i = 1;
			// ReSharper disable once VirtualMemberCallInConstructor
			foreach (var kv in this.AfterCreateCalls())
				this._afterCreateResponses[kv.Key] = usage.CallOnce(kv.Value, (3 * 10) + i++);

			this._updateResponse = usage.CallOnce(this.Update, 4);
			this._updateGetResponse = usage.CallOnce(this.Read, 5);
			this._deleteResponse = usage.CallOnce(this.Delete, 6);
			this._deleteGetResponse = usage.CallOnce(this.Read, 7);
			this._deleteExistsResponse = usage.CallOnce(this.Exists, 8);
			this._deleteNotFoundResponse = usage.CallOnce(this.Delete, 9);
		}
		protected abstract LazyResponses Create();
		protected abstract LazyResponses Read();
		protected abstract LazyResponses Update();
		protected virtual LazyResponses Exists() => LazyResponses.Empty;
		protected virtual LazyResponses Delete() => LazyResponses.Empty;
		protected virtual IDictionary<string, Func<LazyResponses>> AfterCreateCalls() => new Dictionary<string, Func<LazyResponses>>();

		private static string RandomFluent { get; } = $"fluent-{RandomString()}";
		private static string RandomFluentAsync { get; } = $"fluentasync-{RandomString()}";
		private static string RandomInitializer { get; } = $"ois-{RandomString()}";
		private static string RandomInitializerAsync { get; } = $"oisasync-{RandomString()}";

		protected virtual bool SupportsDeletes => true;
		protected virtual bool SupportsExists => true;
		protected virtual bool SupportsUpdates => true;
		/// <summary>Helpful if you want to capture a reproduce trace with e.g fiddler</summary>
		protected virtual bool TestOnlyOneMethod => false;

		protected virtual void IntegrationSetup(IElasticClient client) { }

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
			var client = this.Client;
			return new LazyResponses(async () =>
			{
				var dict = new Dictionary<ClientMethod, IResponse>();

				if (TestClient.Configuration.RunIntegrationTests)
				{
					this.IntegrationSetup(client);
					this._usage.CalledSetup = true;
				}

				var sf = Sanitize(RandomFluent);
				dict.Add(Integration.ClientMethod.Fluent, fluent(sf, client, f => fluentBody(sf, f)));
				if (this.TestOnlyOneMethod) return dict;

				var sfa = Sanitize(RandomFluentAsync);
				dict.Add(Integration.ClientMethod.FluentAsync, await fluentAsync(sfa, client, f => fluentBody(sfa, f)));

				var si = Sanitize(RandomInitializer);
				dict.Add(Integration.ClientMethod.Initializer, request(si, client, initializerBody(si)));

				var sia = Sanitize(RandomInitializerAsync);
				dict.Add(Integration.ClientMethod.InitializerAsync, await requestAsync(sia, client, initializerBody(sia)));
				return dict;
			});
		}
		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected virtual string Sanitize(string randomString) => randomString + "-" + this.GetType().Name.Replace("CrudTests", "").ToLowerInvariant();

		protected TCluster Cluster { get; }
		protected virtual IElasticClient Client => this.Cluster.Client;

		protected async Task AssertOnAfterCreateResponse<TResponse>(string name, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			await this.ExecuteOnceInOrder();
			if (this._afterCreateResponses.ContainsKey(name))
				await this.AssertOnAllResponses(this._afterCreateResponses[name], assert);
			else throw new Exception($"{name} is not a keyed after create response");
		}

		protected async Task AssertOnAllResponses<TResponse>(LazyResponses responses, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			await this.ExecuteOnceInOrder();

			foreach (var kv in await responses)
			{
				if (!(kv.Value is TResponse response))
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

		private async Task ExecuteOnceInOrder()
		{
			//hack to make sure these are resolved in the right order, calling twice yields cached results so  should be fast
			await this._createResponse;
			await this._createGetResponse;
			if (this.SupportsExists)
				await this._createExistsResponse;

			foreach (var kv in this._afterCreateResponses) await kv.Value;
			if (this.SupportsUpdates)
			{
				await this._updateResponse;
				await this._updateGetResponse;
			}

			if (this.SupportsDeletes)
			{
				await this._deleteResponse;
				await this._deleteGetResponse;
				if (this.SupportsExists)
					await this._deleteExistsResponse;
				await this._deleteNotFoundResponse;
			}
		}

		protected async Task AssertOnCreate(Action<TCreateResponse> assert) => await this.AssertOnAllResponses(this._createResponse, assert);
		protected async Task AssertOnUpdate(Action<TUpdateResponse> assert)
		{
			if (!this.SupportsUpdates) return;
			await this.AssertOnAllResponses(this._updateResponse, assert);
		}

		protected async Task AssertOnDelete(Action<TDeleteResponse> assert)
		{
			if (!this.SupportsDeletes) return;
			await this.AssertOnAllResponses(this._deleteResponse, assert);
		}

		protected async Task AssertOnGetAfterCreate(Action<TReadResponse> assert)
		{
			await this.AssertOnAllResponses(this._createGetResponse, assert);
		}

		protected async Task AssertOnGetAfterUpdate(Action<TReadResponse> assert)
		{
			if (!this.SupportsUpdates) return;
			await this.AssertOnAllResponses(this._updateGetResponse, assert);
		}

		protected async Task AssertOnGetAfterDelete(Action<TReadResponse> assert)
		{
			if (!this.SupportsDeletes) return;
			await this.AssertOnAllResponses(this._deleteGetResponse, assert);
		}

		protected async Task AssertOnExistsAfterCreate(Action<TExistsResponse> assert)
		{
			if (!this.SupportsExists) return;
			await this.AssertOnAllResponses(this._createExistsResponse, assert);
		}
		protected async Task AssertOnExistsAfterDelete(Action<TExistsResponse> assert)
		{
			if (!this.SupportsExists) return;
			await this.AssertOnAllResponses(this._deleteExistsResponse, assert);
		}

		protected async Task AssertOnDeleteNotFoundAfterDelete(Action<TDeleteResponse> assert)
		{
			if (!this.SupportsDeletes) return;
			await this.AssertOnAllResponses(this._deleteNotFoundResponse, assert);
		}

		protected virtual void ExpectAfterCreate(TReadResponse response) { }
		protected virtual void ExpectExistsAfterCreate(TExistsResponse response) { }
		protected virtual void ExpectAfterUpdate(TReadResponse response) { }
		protected virtual void ExpectDeleteNotFoundResponse(TDeleteResponse response) { }
		protected virtual void ExpectExistsAfterDelete(TExistsResponse response) { }

		[I] protected virtual async Task CreateCallIsValid() => await this.AssertOnCreate(r => r.ShouldBeValid());
		[I] protected virtual async Task GetAfterCreateIsValid() => await this.AssertOnGetAfterCreate(r => {
			r.ShouldBeValid();
			ExpectAfterCreate(r);
		});
		[I] protected virtual async Task ExistsAfterCreateIsValid() => await this.AssertOnExistsAfterCreate(r => {
			r.ShouldBeValid();
			r.Exists.Should().BeTrue();
			ExpectExistsAfterCreate(r);
		});

		[I] protected virtual async Task UpdateCallIsValid() => await this.AssertOnUpdate(r => r.ShouldBeValid());

		[I] protected virtual async Task GetAfterUpdateIsValid() => await this.AssertOnGetAfterUpdate(r => {
			r.ShouldBeValid();
			ExpectAfterUpdate(r);
		});

		[I] protected virtual async Task DeleteCallIsValid() => await this.AssertOnDelete(r => r.ShouldBeValid());
		[I] protected virtual async Task GetAfterDeleteIsValid() => await this.AssertOnGetAfterDelete(r => r.ShouldNotBeValid());
		[I] protected virtual async Task ExistsAfterDeleteIsValid() => await this.AssertOnExistsAfterDelete(r => {
			r.ShouldBeValid();
			r.Exists.Should().BeFalse();
			ExpectExistsAfterDelete(r);
		});
		[I] protected virtual async Task DeleteNotFoundIsNotValid() => await this.AssertOnDeleteNotFoundAfterDelete(r =>
		{
			r.ShouldNotBeValid();
			ExpectDeleteNotFoundResponse(r);
		});
	}
}
