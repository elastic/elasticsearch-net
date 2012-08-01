using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient : Nest.IElasticClient
	{
		private IConnection Connection { get; set; }
		private IConnectionSettings Settings { get; set; }
		private bool _gotNodeInfo = false;
		private bool _IsValid { get; set; }
		private ElasticSearchVersionInfo _VersionInfo { get; set; }

		private TypeNameResolver TypeNameResolver { get; set; }

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

			this.DeserializeSettings = this.CreateDeserializeSettings();
			this.SerializationSettings = this.CreateSettings();
			var indexSettings = this.CreateSettings();
			indexSettings.ContractResolver = new ElasticCamelCaseResolver();
			this.IndexSerializationSettings = indexSettings;
			this.PropertyNameResolver = new PropertyNameResolver();

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

		public string Serialize(object @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, IndexSerializationSettings);
		}
		/// <summary>
		/// Returns a response of type R based on the connection status without parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		private R ToResponse<R>(ConnectionStatus status, bool allow404 = false) where R : BaseResponse
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
		private R ToParsedResponse<R>(ConnectionStatus status, bool allow404 = false) where R : BaseResponse
		{
			var isValid = 
				(allow404) 
				? (status.Error == null
					|| status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
				: (status.Error == null);
			if (!isValid)
				return this.ToResponse<R>(status, allow404);

			var r = JsonConvert.DeserializeObject<R>(status.Result, DeserializeSettings);
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
				response = this.Connection.GetSync("");
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
					this._VersionInfo = JsonConvert.DeserializeObject<ElasticSearchVersionInfo>(version.ToString(), DeserializeSettings);

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
