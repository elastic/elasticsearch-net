using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Exceptions;

namespace Nest
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : IElasticClient, IHighLevelToLowLevelDispatcher
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal IHighLevelToLowLevelDispatcher Dispatcher => this;
		internal LowLevelDispatch LowLevelDispatch { get; set; }
		
		public IConnection Connection { get; protected set; }
		public INestSerializer Serializer { get; protected set; }
		public IElasticsearchClient Raw { get; protected set; }
		public ElasticInferrer Infer { get; protected set; }


		/// <summary>
		/// Instantiate a new strongly typed connection to elasticsearch
		/// </summary>
		/// <param name="settings">An optional settings object telling the client how and where to connect to.
		/// <para>Defaults to a static single node connection pool to http://localhost:9200</para>
		/// <para>It's recommended to pass an explicit 'new ConnectionSettings()' instance</para>
		/// </param>
		/// <param name="connection">Optionally provide a different connection handler, defaults to http using HttpWebRequest</param>
		/// <param name="serializer">Optionally provide a custom serializer responsible for taking a stream and turning into T</param>
		/// <param name="transport">The transport coordinates requests between the client and the connection pool and the connection</param>
		public ElasticClient(
			IConnectionSettingsValues settings = null,
			IConnection connection = null,
			INestSerializer serializer = null,
			ITransport transport = null)
		{
			this._connectionSettings = settings ?? new ConnectionSettings();
			this.Connection = connection ?? new HttpConnection(this._connectionSettings);

			this.Serializer = serializer ?? new NestSerializer(this._connectionSettings);
			this.Raw = new ElasticsearchClient(
				this._connectionSettings,
				this.Connection,
				transport, //default transport
				this.Serializer
			);
			this.LowLevelDispatch = new LowLevelDispatch(this.Raw);
			this.Infer = this._connectionSettings.Inferrer;

		}

		/// <summary>
		/// Perform any request you want over the configured IConnection while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>An ElasticsearchResponse of T where T represents the JSON response body</returns>
		public ElasticsearchResponse<T> DoRequest<T>(string method, string path, object data = null, IRequestParameters requestParameters = null)
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
		public Task<ElasticsearchResponse<T>> DoRequestAsync<T>(string method, string path, object data = null, IRequestParameters requestParameters = null)
		{
			return this.Raw.DoRequestAsync<T>(method, path, data, requestParameters);
		}


		R IHighLevelToLowLevelDispatcher.Dispatch<D, Q, R>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
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
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
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
			var config = descriptor.RequestConfiguration;
			var statusCodeAllowed = config != null && config.AllowedStatusCodes.HasAny(i => i == c.HttpStatusCode);

			if (c.Success || statusCodeAllowed)
			{
				c.Response.IsValid = true;
				return c.Response;
			}
			var badResponse = CreateInvalidInstance<R>(c);
			return badResponse;
		}

		private static R CreateInvalidInstance<R>(IElasticsearchResponse response) where R : BaseResponse
		{
			var r = (R)typeof(R).CreateInstance();
			((IResponseWithRequestInformation)r).RequestInformation = response;
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
