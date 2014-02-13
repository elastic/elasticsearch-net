using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient : Nest.IElasticClient
	{
		protected readonly IConnectionSettings _connectionSettings;

		internal RawDispatch RawDispatch { get; set; }

		public IConnection Connection { get; protected set; }
		public INestSerializer Serializer { get; protected set; }
		public IRawElasticClient Raw { get; protected set; }
		public ElasticInferrer Infer { get; protected set; }

		public ElasticClient(IConnectionSettings settings, IConnection connection = null, INestSerializer serializer = null)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this._connectionSettings = settings;
			this.Connection = connection ?? new Connection(settings);

			this.Serializer = serializer ?? new NestSerializer(this._connectionSettings);
			this.Raw = new RawElasticClient(this._connectionSettings, this.Connection, this.Serializer);
			this.RawDispatch = new RawDispatch(this.Raw);
			this.Infer = new ElasticInferrer(this._connectionSettings);

		}


		private R Dispatch<D, Q, R>(
			Func<D, D> selector
			, Func<ElasticsearchPathInfo<Q>, D, ConnectionStatus> dispatch
			, Func<ConnectionStatus, D, R> resultSelector = null
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return Dispatch<D, Q, R>(descriptor, dispatch, resultSelector, allow404);
		}

		private R Dispatch<D, Q, R>(
			D descriptor
			, Func<ElasticsearchPathInfo<Q>, D, ConnectionStatus> dispatch
			, Func<ConnectionStatus, D, R> resultSelector = null
			, bool allow404 = false
			) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : class
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			resultSelector = resultSelector ?? ((c, d) => c.Deserialize<R>(allow404: allow404));
			return resultSelector(dispatch(pathInfo, descriptor), descriptor);
		}

		internal Task<I> DispatchAsync<D, Q, R, I>(
			Func<D, D> selector
			, Func<ElasticsearchPathInfo<Q>, D, Task<ConnectionStatus>> dispatch
			, Func<ConnectionStatus, D, R> resultSelector = null
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : class, I
			where I : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return DispatchAsync<D, Q, R, I>(descriptor, dispatch, resultSelector, allow404);
		}

		private Task<I> DispatchAsync<D, Q, R, I>(
			D descriptor 
			, Func<ElasticsearchPathInfo<Q>, D, Task<ConnectionStatus>> dispatch
			, Func<ConnectionStatus, D, R> resultSelector = null
			, bool allow404 = false
			) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : class, I 
			where I : class
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			resultSelector = resultSelector ?? ((c, d) => c.Deserialize<R>(allow404: allow404));
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r => resultSelector(r.Result, descriptor));
		}


	}
}
