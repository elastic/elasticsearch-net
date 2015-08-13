using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : IElasticClient, IHighLevelToLowLevelDispatcher
	{

		internal IHighLevelToLowLevelDispatcher Dispatcher => this;

		internal LowLevelDispatch LowLevelDispatch { get; set; }
	
		private ITransport<IConnectionSettingsValues> Transport { get; }

		public IElasticsearchSerializer Serializer => this.Transport.Settings.Serializer;
		public ElasticInferrer Infer => this.Transport.Settings.Inferrer;
		public IConnectionSettingsValues ConnectionSettings => this.Transport.Settings;

		public IElasticsearchClient Raw { get; }

		public ElasticClient(Uri uri) : this(new ConnectionSettings(uri)) { }
		public ElasticClient(IConnectionSettingsValues connectionSettings) 
			: this(new Transport<IConnectionSettingsValues>(connectionSettings ?? new ConnectionSettings())) { }

		public ElasticClient(ITransport<IConnectionSettingsValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.Serializer.ThrowIfNull(nameof(transport.Settings.Serializer));
			transport.Settings.Inferrer.ThrowIfNull(nameof(transport.Settings.Inferrer));

			this.Transport = transport;
			this.Raw = new ElasticsearchClient(this.Transport);
			this.LowLevelDispatch = new LowLevelDispatch(this.Raw);
		}

		/// <summary>
		/// stPerform any request you want over the configured IConnection while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>An ElasticsearchResponse of T where T represents the JSON response body</returns>
		public ElasticsearchResponse<T> DoRequest<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			return this.Raw.DoRequest<T>(method, path, data, requestParameters);
		}

		/// <summary>
		/// Perform any request you want over the configured IConnection asynchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>A task of ElasticsearchResponse of T where T represents the JSON response body</returns>
		public Task<ElasticsearchResponse<T>> DoRequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			return this.Raw.DoRequestAsync<T>(method, path, data, requestParameters);
		}


		R IHighLevelToLowLevelDispatcher.Dispatch<D, Q, R>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
		{
			var pathInfo = descriptor.ToPathInfo(this.ConnectionSettings);
			var response = dispatch(pathInfo, descriptor);
			return ResultsSelector<D, Q, R>(response, descriptor);
		}

		R IHighLevelToLowLevelDispatcher.Dispatch<D, Q, R>(Func<D, D> selector, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.Dispatcher.Dispatch<D, Q, R>(descriptor, dispatch);
		}

		Task<I> IHighLevelToLowLevelDispatcher.DispatchAsync<D, Q, R, I>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch)
		{
			var pathInfo = descriptor.ToPathInfo(this.ConnectionSettings);
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r =>
				{
					if (r.IsFaulted && r.Exception != null)
					{
						var mr = r.Exception.InnerException as MaxRetryException;
						if (mr != null)
							mr.RethrowKeepingStackTrace();

						var ae = r.Exception.Flatten();
						if (ae.InnerException != null)
							ae.InnerException.RethrowKeepingStackTrace();

						ae.RethrowKeepingStackTrace();
					}
					return ResultsSelector<D, Q, R>(r.Result, descriptor);
				});
		}

		Task<I> IHighLevelToLowLevelDispatcher.DispatchAsync<D, Q, R, I>(Func<D, D> selector, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch)
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.Dispatcher.DispatchAsync<D, Q, R, I>(descriptor, dispatch);
		}

		private static R ResultsSelector<D, Q, R>(
			ElasticsearchResponse<R> c,
			D descriptor
			)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse
		{
			return c.Body ?? CreateInvalidInstance<R>(c);
		}

		private static R CreateInvalidInstance<R>(IApiCallDetails response) where R : BaseResponse
		{
			var r = typeof(R).CreateInstance<R>();
			((IBodyWithApiCallDetails)r).CallDetails = response;
			r.IsValid = false;
			return r;
		}

		private TRequest ForceConfiguration<TRequest>(
			Func<TRequest, TRequest> selector, Action<IRequestConfiguration> setter
			)
			where TRequest : class, IRequest, new()
		{
			selector = selector ?? (s => s);
			var request = selector(new TRequest());
			return ForceConfiguration(request, setter);
		}

		private TRequest ForceConfiguration<TRequest>(TRequest request, Action<IRequestConfiguration> setter)
			where TRequest : IRequest
		{
			var configuration = request.RequestConfiguration ?? new RequestConfiguration();
			setter(configuration);
			request.RequestConfiguration = configuration;
			return request;
		}
	}
}
