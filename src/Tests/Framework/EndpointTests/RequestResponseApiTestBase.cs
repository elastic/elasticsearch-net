using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Managed;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Nest;
using Tests.Framework.Integration;
using Elastic.Xunit;
using Elastic.Xunit.Sdk;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Framework
{
	public abstract class RequestResponseApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: SerializationTestBase, IClusterFixture<TCluster>, IClassFixture<EndpointUsage>
		where TCluster : ICluster<EphemeralClusterConfiguration> , new()
		where TResponse : class, IResponse
		where TInterface : class
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
	{
		private readonly EndpointUsage _usage;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
		protected bool RanIntegrationSetup => this._usage?.CalledSetup ?? false;
	    protected string U(string s) => Uri.EscapeDataString(s);

		protected string CallIsolatedValue => UniqueValues.Value;
		protected T ExtendedValue<T>(string key) where T : class => this.UniqueValues.ExtendedValue<T>(key);
		protected void ExtendedValue<T>(string key, T value) where T : class => this.UniqueValues.ExtendedValue(key, value);

		protected virtual TDescriptor NewDescriptor() => Activator.CreateInstance<TDescriptor>();
		protected virtual Func<TDescriptor, TInterface> Fluent { get; } = null;
		protected virtual TInitializer Initializer { get; } = null;

		protected virtual void IntegrationSetup(IElasticClient client, CallUniqueValues values) { }
		protected virtual void IntegrationTeardown(IElasticClient client, CallUniqueValues values) { }
		protected virtual void OnBeforeCall(IElasticClient client) { }
		protected virtual void OnAfterCall(IElasticClient client) { }

		protected CallUniqueValues UniqueValues { get; }
		protected LazyResponses Responses { get; }

		public override IElasticClient Client => TestClient.DefaultInMemoryClient;

		protected abstract LazyResponses ClientUsage();

        public TCluster Cluster { get; }

		protected RequestResponseApiTestBase(TCluster cluster, EndpointUsage usage) : base(usage)
		{
			this._usage = usage ?? throw new ArgumentNullException(nameof(usage));

			if (cluster == null) throw new ArgumentNullException(nameof(cluster));
			this.Cluster = cluster;
			this.Responses = usage.CallOnce(this.ClientUsage);
			this.UniqueValues = usage.CallUniqueValues;
		}

		protected LazyResponses Calls(
			Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<IElasticClient, TInitializer, TResponse> request,
			Func<IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
		{
			return new LazyResponses(async () =>
			{
				var client = this.Client;
				if (TestClient.Configuration.RunIntegrationTests)
				{
					this.IntegrationSetup(client, UniqueValues);
					this._usage.CalledSetup = true;
				}

				var dict = new Dictionary<ClientMethod, IResponse>();
				UniqueValues.CurrentView = ClientMethod.Fluent;

				OnBeforeCall(client);
				dict.Add(ClientMethod.Fluent, fluent(client, this.Fluent));
				OnAfterCall(client);

				UniqueValues.CurrentView = ClientMethod.FluentAsync;
				OnBeforeCall(client);
				dict.Add(ClientMethod.FluentAsync, await fluentAsync(client, this.Fluent));
				OnAfterCall(client);

				UniqueValues.CurrentView = ClientMethod.Initializer;
				OnBeforeCall(client);
				dict.Add(ClientMethod.Initializer, request(client, this.Initializer));
				OnAfterCall(client);

				UniqueValues.CurrentView = ClientMethod.InitializerAsync;
				OnBeforeCall(client);
				dict.Add(ClientMethod.InitializerAsync, await requestAsync(client, this.Initializer));
				OnAfterCall(client);

				if (TestClient.Configuration.RunIntegrationTests)
				{
					this.IntegrationTeardown(client, UniqueValues);
					this._usage.CalledTeardown = true;
				}

				return dict;
			});
		}

		protected virtual async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await this.Responses;
			foreach (var kv in responses)
			{
				var response = kv.Value as TResponse;
				try
				{
					this.UniqueValues.CurrentView = kv.Key;
					assert(response);
				}
#pragma warning disable 7095 //enable this if you expect a single overload to act up
#pragma warning disable 8360
				catch (Exception ex) when (false)
#pragma warning restore 7095
#pragma warning restore 8360
#pragma warning disable 0162 //dead code while the previous exception filter is false
				{
					throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				}
#pragma warning restore 0162
			}
		}
	}
}
