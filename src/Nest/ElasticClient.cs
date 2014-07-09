using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Nest
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : Nest.IElasticClient
	{
		protected readonly IConnectionSettingsValues _connectionSettings;

		internal RawDispatch RawDispatch { get; set; }

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
			this.RawDispatch = new RawDispatch(this.Raw);
			this.Infer = new ElasticInferrer(this._connectionSettings);

		}


		private R Dispatch<D, Q, R>(
			Func<D, D> selector
			, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch
			)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>,  new()
			where R : BaseResponse
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.Dispatch<D, Q, R>(descriptor, dispatch);
		}

		private R Dispatch<D, Q, R>(
			D descriptor
			, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch
			)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			var response = dispatch(pathInfo, descriptor);
			return ResultsSelector<D, Q, R>(response, descriptor);
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


		private static R CreateValidInstance<R>(IElasticsearchResponse response) where R : BaseResponse
		{
			var r = (R)typeof(R).CreateInstance();
			((IResponseWithRequestInformation)r).RequestInformation = response;
			r.IsValid = true;
			return r;
		}
		private static R CreateInvalidInstance<R>(IElasticsearchResponse response) where R : BaseResponse
		{
			var r = (R)typeof(R).CreateInstance();
			((IResponseWithRequestInformation)r).RequestInformation = response;
			r.IsValid = false;
			return r;
		}

		internal Task<I> DispatchAsync<D, Q, R, I>(
			Func<D, D> selector
			, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch
			)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>, new()
			where R : BaseResponse, I
			where I : IResponse
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.DispatchAsync<D, Q, R, I>(descriptor, dispatch);
		}

		private Task<I> DispatchAsync<D, Q, R, I>(
			D descriptor 
			, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch
			) 
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse, I 
			where I : IResponse
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r => ResultsSelector<D, Q, R>(r.Result, descriptor));
		}


	}
}
