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

		private TypeNameResolver TypeNameResolver { get; set; }
		private IdResolver IdResolver { get; set; }
		private IndexNameResolver IndexNameResolver { get; set; }
		private PathResolver PathResolver { get; set; }

		public IConnection Connection { get; protected set; }
		public ElasticSerializer Serializer { get; protected set; }
		public IRawElasticClient Raw { get; private set; }

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
			this.TypeNameResolver = new TypeNameResolver();
			this.IdResolver = new IdResolver();
			this.IndexNameResolver = new IndexNameResolver(settings);
			this.PathResolver = new PathResolver(settings);

			this.PropertyNameResolver = new PropertyNameResolver();

			this.Serializer = new ElasticSerializer(this._connectionSettings);
			this.Raw = new RawElasticClient(this._connectionSettings, connection);

		}

		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the too
		/// </summary>
		/// <returns></returns>
		public IRootInfoResponse RootNodeInfo()
		{
			var response = this.Connection.GetSync("/");
			return response.ToParsedResponse<RootInfoResponse>();

		}

		/// <summary>
		/// Get the data when you hit the elasticsearch endpoint at the too
		/// </summary>
		/// <returns></returns>
		public Task<IRootInfoResponse> RootNodeInfoAsync()
		{
			var response = this.Connection.Get("/");
			return response
				.ContinueWith(t => t.Result.ToParsedResponse<RootInfoResponse>() as IRootInfoResponse);

		}

	}
}
