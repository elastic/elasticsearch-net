using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class ApiTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		: SerializationTestBase
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		private readonly EndpointUsage _usage;
		private readonly LazyResponses _responses;
		private readonly int _port;
		private readonly CallUniqueValues _uniqueValues;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
		protected bool RanIntegrationSetup => this._usage?.CalledSetup ?? false;

		protected IIntegrationCluster Cluster { get; }

		protected string CallIsolatedValue => _uniqueValues.Value;
		protected T ExtendedValue<T>(string key) where T : class => this._uniqueValues.ExtendedValue<T>(key);
		protected void ExtendedValue<T>(string key, T value) where T : class => this._uniqueValues.ExtendedValue(key, value);
		protected virtual void IntegrationSetup(IElasticClient client, CallUniqueValues values) { }
		protected virtual void OnBeforeCall(IElasticClient client) { }
		protected virtual void OnAfterCall(IElasticClient client) { }

		protected virtual bool ForceInMemory => true;
		protected virtual ConnectionSettings GetConnectionSettings(ConnectionSettings settings) => settings;
		protected override IElasticClient Client => this.Cluster.Client(GetConnectionSettings, this.ForceInMemory);

		protected virtual TDescriptor NewDescriptor() => Activator.CreateInstance<TDescriptor>();
		protected virtual Func<TDescriptor, TInterface> Fluent { get; }
		protected virtual TInitializer Initializer { get; }

		protected abstract LazyResponses ClientUsage();

		protected abstract string UrlPath { get; }
		protected abstract HttpMethod HttpMethod { get; }

		protected ApiTestBase(IIntegrationCluster cluster, EndpointUsage usage) : base(cluster)
		{
			this._usage = usage;
			this.Cluster = cluster;

			this._responses = usage.CallOnce(this.ClientUsage);
			this._port = cluster.Node.Port;
			this._uniqueValues = usage.CallUniqueValues;
			this.SetupSerialization();
		}

		[U] protected async Task HitsTheCorrectUrl() =>
			await this.AssertOnAllResponses(r => UrlTester.ComparePathAndQuerystring(this.UrlPath, r.ApiCall.Uri));

		[U] protected async Task UsesCorrectHttpMethod() =>
			await this.AssertOnAllResponses(r => r.CallDetails.HttpMethod.Should().Be(this.HttpMethod));

		[U] protected void SerializesInitializer() =>
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);

		[U] protected void SerializesFluent() =>
			this.AssertSerializesAndRoundTrips(this.Fluent?.Invoke(NewDescriptor()));

		protected LazyResponses Calls(
			Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<IElasticClient, TInitializer, TResponse> request,
			Func<IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
		{
			//this client is outside the lambda so that the callstack is one where we can get the method name
			//of the current running test and send that as a header, great for e.g fiddler to relate requests with the test that sent it
			var client = this.Client;
			return new LazyResponses(async () =>
			{
				if (TestClient.Configuration.RunIntegrationTests)
				{
					this.IntegrationSetup(client, _uniqueValues);
					this._usage.CalledSetup = true;
				}

				var dict = new Dictionary<ClientMethod, IResponse>();
				_uniqueValues.CurrentView = ClientMethod.Fluent;

				OnBeforeCall(client);
				dict.Add(ClientMethod.Fluent, fluent(client, this.Fluent));
				OnAfterCall(client);

				_uniqueValues.CurrentView = ClientMethod.FluentAsync;
				OnBeforeCall(client);
				dict.Add(ClientMethod.FluentAsync, await fluentAsync(client, this.Fluent));
				OnAfterCall(client);

				_uniqueValues.CurrentView = ClientMethod.Initializer;
				OnBeforeCall(client);
				dict.Add(ClientMethod.Initializer, request(client, this.Initializer));
				OnAfterCall(client);

				_uniqueValues.CurrentView = ClientMethod.InitializerAsync;
				OnBeforeCall(client);
				dict.Add(ClientMethod.InitializerAsync, await requestAsync(client, this.Initializer));
				OnAfterCall(client);
				return dict;
			});
		}

		protected virtual async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await this._responses;
			foreach (var kv in responses)
			{
				var response = kv.Value as TResponse;
				try
				{
					this._uniqueValues.CurrentView = kv.Key;
					assert(response);
				}
#pragma warning disable 7095 //enable this if you expect a single overload to act up
				catch (Exception ex) when (false)
#pragma warning restore 7095
				{
					throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				}
			}
		}

	}
}
