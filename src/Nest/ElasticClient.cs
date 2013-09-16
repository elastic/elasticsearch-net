using System;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient : Nest.IElasticClient
	{
		protected IConnection Connection { get; set; }
		public IConnectionSettings Settings { get; protected set; }
		private bool _gotNodeInfo = false;
		private bool _IsValid { get; set; }
		private ElasticSearchVersionInfo _VersionInfo { get; set; }

		private TypeNameResolver TypeNameResolver { get; set; }
		private IdResolver IdResolver { get; set; }
		private IndexNameResolver IndexNameResolver { get; set; }
		private PathResolver PathResolver { get; set; }

		/// <summary>
		/// Validates the connection once and returns a bool whether NEST could connect to elasticsearch.
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (!this._gotNodeInfo)
					this.GetNodeInfo();
				return this._IsValid;
			}
		}
		/// <summary>
		/// Return the version info that was set when NEST did its one off sanity checks
		/// </summary>
		public IElasticSearchVersionInfo VersionInfo
		{
			get
			{
				if (!this._gotNodeInfo)
					this.GetNodeInfo();
				return this._VersionInfo;
			}
		}

		public IRawElasticClient Raw { get; private set; }

		public ElasticClient(IConnectionSettings settings)
			: this(settings, new Connection(settings))
		{

		}
		public ElasticClient(IConnectionSettings settings, IConnection connection)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = connection;
			this.TypeNameResolver = new TypeNameResolver();
			this.IdResolver = new IdResolver();
			this.IndexNameResolver = new IndexNameResolver(settings);
			this.PathResolver = new PathResolver(settings);

			//this._defaultConverters.Add(new TypeNameMarkerConverter(this.Settings));
			//this._defaultConverters.Add(new IndexSettingsConverter(this.Settings));

			this.SerializationSettings = this.CreateSettings();
			var indexSettings = this.CreateSettings();

			indexSettings.ContractResolver = new ElasticCamelCaseResolver(this.Settings);
			this.IndexSerializationSettings = indexSettings;
			this.PropertyNameResolver = new PropertyNameResolver();

			this.Raw = new RawElasticClient(this.Settings, connection);

		}

		public bool TryConnect(out ConnectionStatus status)
		{
			try
			{
				status = this.GetNodeInfo();
				return this.IsValid;
			}
			catch (Exception e)
			{
				status = new ConnectionStatus(e);
			}
			return false;
		}

		
		/// <summary>
		/// Returns a response of type R based on the connection status without parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		protected virtual R ToResponse<R>(ConnectionStatus status, bool allow404 = false) where R : BaseResponse
		{
			var isValid =
				(allow404)
				? (status.Error == null
					|| status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
				: (status.Error == null);
			var r = (R)Activator.CreateInstance(typeof(R));
			r.IsValid = isValid;
			r.ConnectionStatus = status;
			r.PropertyNameResolver = PropertyNameResolver;
			return r;
		}
		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		protected virtual R ToParsedResponse<R>(ConnectionStatus status, bool allow404 = false, IEnumerable<JsonConverter> extraConverters = null) where R : BaseResponse
		{
			var isValid =
				(allow404)
				? (status.Error == null
					|| status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
				: (status.Error == null);
			if (!isValid)
				return this.ToResponse<R>(status, allow404);

			var r = this.Deserialize<R>(status.Result, extraConverters: extraConverters);
			r.IsValid = isValid;
			r.ConnectionStatus = status;
			r.PropertyNameResolver = PropertyNameResolver;
			return r;
		}


		private ConnectionStatus GetNodeInfo()
		{
			ConnectionStatus response = null;
			try
			{
				response = this.Connection.GetSync("/");
				if (response.Success)
				{
					JObject o = JObject.Parse(response.Result);
					if (o["ok"] == null)
					{
						this._IsValid = false;
						return response;
					}

					this._IsValid = (bool)o["ok"];

					JObject version = o["version"] as JObject;
					this._VersionInfo = this.Deserialize<ElasticSearchVersionInfo>(version.ToString());

					this._gotNodeInfo = true;
				}
				return response;
			}
			catch (Exception e)
			{
				this._IsValid = false;
				return new ConnectionStatus(e);
			}

		}


	}
}
