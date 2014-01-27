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

			this.PropertyNameResolver = new PropertyNameResolver();

			this.Serializer = new ElasticSerializer(this._connectionSettings);
			this.Raw = new RawElasticClient(this._connectionSettings, connection);
			this.RawDispatch = new RawDispatch(this.Raw);
			this.Infer = new ElasticInferrer(this._connectionSettings);

		}


		private R Dispatch<D, Q, R>(
			Func<D, D> selector
			, Func<ElasticSearchPathInfo<Q>, D, ConnectionStatus> dispatch
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return Dispatch<D, Q, R>(descriptor, dispatch, allow404);
		}

		private R Dispatch<D, Q, R>(
			D descriptor, 
			Func<ElasticSearchPathInfo<Q>, D, ConnectionStatus> dispatch,
			bool allow404 = false
			) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : class
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return dispatch(pathInfo, descriptor)
				.Deserialize<R>(allow404: allow404);
		}

		internal Task<I> DispatchAsync<D, Q, R, I>(
			Func<D, D> selector
			, Func<ElasticSearchPathInfo<Q>, D, Task<ConnectionStatus>> dispatch
			, bool allow404 = false
			)
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>, new()
			where R : class, I
			where I : class
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return DispatchAsync<D, Q, R, I>(descriptor, dispatch, allow404);
		}

		private Task<I> DispatchAsync<D, Q, R, I>(
			D descriptor, 
			Func<ElasticSearchPathInfo<Q>, D, Task<ConnectionStatus>> dispatch,
			bool allow404 = false
			) 
			where Q : FluentQueryString<Q>, new()
			where D : IPathInfo<Q>
			where R : class, I 
			where I : class
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r => r.Result.Deserialize<R>(allow404: allow404));
		}


		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the root
		/// </summary>
		/// <returns></returns>
		public IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? ((i) => i);
			return this.Dispatch<InfoDescriptor, InfoQueryString, RootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatch(p)
			);
		}

		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the root
		/// </summary>
		/// <returns></returns>
		public Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? ((i) => i);
			return this.DispatchAsync<InfoDescriptor, InfoQueryString, RootInfoResponse, IRootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatchAsync(p)
			);
		}
	}
}
