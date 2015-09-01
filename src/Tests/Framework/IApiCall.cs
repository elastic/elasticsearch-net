using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework
{
	public abstract class CrudExample<TCreateResponse>
			where TCreateResponse : class, IResponse
	{
		private LazyResponses _createResponse;
		private LazyResponses _createGetResponse;
		private LazyResponses _updateResponse;
		private LazyResponses _updateGetResponse;
		private LazyResponses _deleteResponse;
		private LazyResponses _deleteGetResponse;

		readonly IIntegrationCluster _cluster;

		[SuppressMessage("Potential Code Quality Issues", "RECS0021:Warns about calls to virtual member functions occuring in the constructor", Justification = "Expected behaviour")]
		public CrudExample(IIntegrationCluster cluster, ApiUsage usage)
		{
			this._cluster = cluster;
			this.IntegrationPort = cluster.Node.Port;
			this._createResponse = usage.CallOnce(this.Create);
			this._createGetResponse = usage.CallOnce(this.Read);
			this._updateResponse = usage.CallOnce(this.Update);
			this._updateGetResponse = usage.CallOnce(this.Read);
			this._deleteResponse = usage.CallOnce(this.Delete);
			this._deleteGetResponse = usage.CallOnce(this.Read);
		}
		protected abstract LazyResponses Create();
		protected abstract LazyResponses Read();
		protected abstract LazyResponses Update();
		protected abstract LazyResponses Delete();

		protected static string RandomFluent { get; } = RandomString();
		protected static string RandomFluentAsync { get; } = RandomString();
		protected static string RandomInitializer { get; } = RandomString();
		protected static string RandomInitializerAsync { get; } = RandomString();

		protected LazyResponses Calls<TDescriptor, TInitializer, TInterface, TResponse>(
			Func<string, TInitializer> initializerBody,
			Func<string, TDescriptor, TInterface> fluentBody,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<string, IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TInitializer, TResponse> request,
			Func<string, IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
			where TResponse : class, IResponse
			where TDescriptor : class, TInterface, new()
			where TInitializer : class, TInterface
			where TInterface : class
		{
			var client = this.Client;
			return new LazyResponses(async () =>
			{
				var dict = new Dictionary<string, IResponse>();
				dict.Add("fluent", fluent(RandomFluent, client, f => fluentBody(RandomFluent, f)));
				dict.Add("fluentAsync", await fluentAsync(RandomFluentAsync, client, f => fluentBody(RandomFluentAsync, f)));
				dict.Add("request", request(RandomInitializer, client, initializerBody(RandomInitializer)));
				dict.Add("requestAsync", await requestAsync(RandomInitializerAsync, client, initializerBody(RandomInitializerAsync)));
				return dict;
			});
		}
		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
		protected int IntegrationPort { get; set; } = 9200;
		protected virtual ConnectionSettings GetConnectionSettings(ConnectionSettings settings) => settings;
		protected virtual IElasticClient Client => this._cluster.Client(GetConnectionSettings);

		protected async Task AssertOnAllResponses<TResponse>(LazyResponses responses, Action<TResponse> assert)
			where TResponse : class, IResponse
		{
			foreach (var kv in await responses)
			{
				var response = kv.Value as TResponse;
				try
				{
					assert(response);
				}
				catch (Exception ex) when (false)
				{
					throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				}
			}
		}

		protected async Task AssertOnCreate(Action<TCreateResponse> assert) => await this.AssertOnAllResponses(this._createResponse, assert);

		[I]
		protected async Task CreateCallIsValid() => await this.AssertOnCreate(r => r.IsValid.Should().Be(true));
	}
}