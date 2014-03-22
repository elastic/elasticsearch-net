using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

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
			INestSerializer serializer = null)
		{

			this._connectionSettings = settings ?? new ConnectionSettings();
			this.Connection = connection ?? new HttpConnection(settings);

			this.Serializer = serializer ?? new NestSerializer(this._connectionSettings);
			var stringifier = new NestStringifier(settings);
			this.Raw = new ElasticsearchClient(
				this._connectionSettings, 
				this.Connection, 
				null, //default transport
				this.Serializer, 
				stringifier
			);
			this.RawDispatch = new RawDispatch(this.Raw);
			this.Infer = new ElasticInferrer(this._connectionSettings);

		}


		private R Dispatch<D, Q, R>(
			Func<D, D> selector
			, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : BaseResponse
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.Dispatch<D, Q, R>(descriptor, dispatch, allow404);
		}


		private R Dispatch<D, Q, R>(
			D descriptor
			, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : BaseResponse
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			Func<ElasticsearchResponse<R>, D, R> resultSelector = 
				((c, d) => c.Success || allow404 && c.HttpStatusCode == 404  ? c.Response : CreateInvalidInstance<R>(c));
			var response = dispatch(pathInfo, descriptor);
			return resultSelector(response, descriptor);
		}



		private static R CreateInvalidInstance<R>(IElasticsearchResponse response) where R : BaseResponse
		{
			var r = (R)typeof(R).CreateInstance();
			r.ConnectionStatus = response;
			r.IsValid = false;
			return r;
		}

		internal Task<I> DispatchAsync<D, Q, R, I>(
			Func<D, D> selector
			, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : BaseResponse, I
			where I : IResponse
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.DispatchAsync<D, Q, R, I>(descriptor, dispatch,  allow404);
		}

		private Task<I> DispatchAsync<D, Q, R, I>(
			D descriptor 
			, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch
			, bool allow404 = false
			) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : BaseResponse, I 
			where I : IResponse
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			Func<ElasticsearchResponse<R>, D, R> resultSelector =
				((c, d) => c.Success || allow404 && c.HttpStatusCode == 404  ? c.Response : CreateInvalidInstance<R>(c));
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r => resultSelector(r.Result, descriptor));
		}


	}
}
