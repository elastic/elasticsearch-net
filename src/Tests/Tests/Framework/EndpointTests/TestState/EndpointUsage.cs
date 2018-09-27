using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;

namespace Tests.Framework.Integration
{
	public class EndpointUsage
	{
		private readonly object _lock = new object();
		private readonly ConcurrentDictionary<int, LazyResponses> _usages = new ConcurrentDictionary<int, LazyResponses>();

		public CallUniqueValues CallUniqueValues { get; }

		public bool CalledSetup { get; internal set; }
		public bool CalledTeardown { get; internal set; }

		public EndpointUsage() : this("nest") { }

		public EndpointUsage(string prefix) => this.CallUniqueValues = new CallUniqueValues(prefix);

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage, int k = 0)
		{
			if (_usages.TryGetValue(k, out var lazyResponses)) return lazyResponses;
			lock (_lock)
			{
				if (_usages.TryGetValue(k, out lazyResponses)) return lazyResponses;
				var response = clientUsage();
				_usages.TryAdd(k, response);
				return response;
			}
		}
	}

	public class SingleEndpointUsage<TResponse> : EndpointUsage
		where TResponse : class, IResponse
	{
		public SingleEndpointUsage(
			Func<string, IElasticClient, TResponse> fluent,
			Func<string, IElasticClient, Task<TResponse>> fluentAsync,
			Func<string, IElasticClient, TResponse> request,
			Func<string, IElasticClient, Task<TResponse>> requestAsync,
			string valuePrefix = null
			) : base(valuePrefix)
		{
			_fluent = fluent;
			_fluentAsync = fluentAsync;
			_request = request;
			_requestAsync = requestAsync;
		}

		private readonly Func<string, IElasticClient, TResponse> _fluent;
		private readonly Func<string, IElasticClient, Task<TResponse>> _fluentAsync;
		private readonly Func<string, IElasticClient, TResponse> _request;
		private readonly Func<string, IElasticClient, Task<TResponse>> _requestAsync;

		public Action<IElasticClient, CallUniqueValues> IntegrationSetup { get; set; }
		public Action<IElasticClient, CallUniqueValues> IntegrationTeardown { get; set; }
		public Action<IElasticClient> OnBeforeCall { get; set; }
		public Action<IElasticClient> OnAfterCall { get; set; }

		private LazyResponses Responses { get; set; }

		public void KickOffOnce(IElasticClient client) => this.Responses = this.CallOnce(()=> new LazyResponses(async () =>
		{
			if (TestClient.Configuration.RunIntegrationTests)
			{
				this.IntegrationSetup?.Invoke(client, this.CallUniqueValues);
				this.CalledSetup = true;
			}

			var dict = new Dictionary<ClientMethod, IResponse>();

			this.Call(client, dict, ClientMethod.Fluent, v => _fluent(v, client));

			await this.CallAsync(client, dict, ClientMethod.FluentAsync, v => _fluentAsync(v, client));

			this.Call(client, dict, ClientMethod.Initializer, v => _request(v, client));

			await this.CallAsync(client, dict, ClientMethod.InitializerAsync, v => _requestAsync(v, client));

			if (TestClient.Configuration.RunIntegrationTests)
			{
				foreach(var v in this.CallUniqueValues.Values.SelectMany(d=> d))
				this.IntegrationTeardown?.Invoke(client, this.CallUniqueValues);
				this.CalledTeardown = true;
			}

			return dict;
		}));

		private void Call(IElasticClient client, IDictionary<ClientMethod, IResponse> dict, ClientMethod method, Func<string, TResponse> call)
		{
			this.CallUniqueValues.CurrentView = method;
			this.OnBeforeCall?.Invoke(client);
			dict.Add(method, call(this.CallUniqueValues.Value));
			this.OnAfterCall?.Invoke(client);
		}
		private async Task CallAsync(IElasticClient client, IDictionary<ClientMethod, IResponse> dict, ClientMethod method, Func<string,Task<TResponse>> call)
		{
			this.CallUniqueValues.CurrentView = method;
			this.OnBeforeCall?.Invoke(client);
			dict.Add(method, await call(this.CallUniqueValues.Value));
			this.OnAfterCall?.Invoke(client);
		}

		public async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await this.Responses;
			foreach (var kv in responses)
			{
				var r = kv.Value as TResponse;

				//this is to make sure any unexpected exceptions on the response are rethrown and shown during testing
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
				    && !(r.ApiCall.OriginalException is ElasticsearchClientException))
				{
					var e = ExceptionDispatchInfo.Capture(r.ApiCall.OriginalException.Demystify());
					throw new ResponseAssertionException(e.SourceException, r);
				}

				try
				{
					assert(r);
				}
				catch (Exception e)
				{
					throw new ResponseAssertionException(e, r);
				}
			}
		}
	}

}
