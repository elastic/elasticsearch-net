using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient : Nest.IElasticClient
	{
		private readonly IConnectionSettings _connectionSettings;

		internal RawDispatch RawDispatch { get; private set; }

		private PathResolver PathResolver { get; set; }

		public IConnection Connection { get; protected set; }
		public ElasticSerializer Serializer { get; protected set; }
		public IRawElasticClient Raw { get; private set; }
		public ElasticInferrer Infer { get; private set; }

		public ElasticClient(IConnectionSettings settings)
			: this(settings, new Connection(settings))
		{

		}

		public ElasticClient(IConnectionSettings settings, IConnection connection)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");


			this._connectionSettings = settings;
			this.Connection = connection;

			this.PathResolver = new PathResolver(settings);

			this.PropertyNameResolver = new PropertyNameResolver();

			this.Serializer = new ElasticSerializer(this._connectionSettings);
			this.Raw = new RawElasticClient(this._connectionSettings, connection);
			this.RawDispatch = new RawDispatch(this.Raw);
			this.Infer = new ElasticInferrer(this._connectionSettings);

		}


		private R Dispatch<D, Q, R>(
			Func<D, D> selector
			, Func<ElasticSearchPathInfo<Q>, D, ConnectionStatus> dispatch
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return Dispatch<D, Q, R>(descriptor, dispatch);
		}

		private R Dispatch<D, Q, R>(
			D descriptor, 
			Func<ElasticSearchPathInfo<Q>, D, ConnectionStatus> dispatch
			) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : class
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return dispatch(pathInfo, descriptor)
				.Deserialize<R>();
		}

		internal Task<I> DispatchAsync<D, Q, R, I>(
			Func<D, D> selector
			, Func<ElasticSearchPathInfo<Q>, D, Task<ConnectionStatus>> dispatch
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : class, I
			where I : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return DispatchAsync<D, Q, R, I>(descriptor, dispatch);
		}

		private Task<I> DispatchAsync<D, Q, R, I>(
			D descriptor, 
			Func<ElasticSearchPathInfo<Q>, D, Task<ConnectionStatus>> dispatch) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : class, I 
			where I : class
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r => r.Result.Deserialize<R>());
		}


		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the too
		/// </summary>
		/// <returns></returns>
		public IRootInfoResponse RootNodeInfo()
		{

			var response = this.Connection.GetSync("/");
			return response.Deserialize<RootInfoResponse>();

		}

		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the too
		/// </summary>
		/// <returns></returns>
		public Task<IRootInfoResponse> RootNodeInfoAsync()
		{
			var response = this.Connection.Get("/");
			return response
				.ContinueWith(t => t.Result.Deserialize<RootInfoResponse>() as IRootInfoResponse);
		}
	}
}
