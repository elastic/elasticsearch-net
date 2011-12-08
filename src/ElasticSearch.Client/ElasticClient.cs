using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Resolvers.Converters;
using ElasticSearch.Client.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using ElasticSearch;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ElasticSearch.Client
{

	public partial class ElasticClient
	{
		private IConnection Connection { get; set; }
		private IConnectionSettings Settings { get; set; }
		private bool _gotNodeInfo = false;
		private bool _IsValid { get; set; }
		private ElasticSearchVersionInfo _VersionInfo { get; set; }
		private JsonSerializerSettings SerializationSettings { get; set; }
		private PropertyNameResolver PropertyNameResolver { get; set; }


		public bool IsValid
		{
			get
			{
				if (!this._gotNodeInfo)
					this.GetNodeInfo();
				return this._IsValid;
			}
		}
	
		public ElasticSearchVersionInfo VersionInfo
		{
			get
			{
				if (!this._gotNodeInfo)
					this.GetNodeInfo();
				return this._VersionInfo;
			}
		}

		public ElasticClient(IConnectionSettings settings) : this(settings, false)
		{

		}
		public ElasticClient(IConnectionSettings settings,bool  useThrift)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			if (useThrift)
				this.Connection = new ThriftConnection(settings);
			else 
				this.Connection = new Connection(settings);

			this.SerializationSettings = new JsonSerializerSettings()
			{
				ContractResolver = new ElasticResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> 
				{ 
					new IsoDateTimeConverter(), 
					new QueryJsonConverter(), 
					new FacetConverter(),
					new IndexSettingsConverter(),
					new ShardsSegmentConverter()
				}
			};
			this.PropertyNameResolver = new PropertyNameResolver(this.SerializationSettings);
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
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
		}
		/// <summary>
		/// Returns an response of type R based on the connection status without parsing status.Result into R
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
			r.PropertyNameResolver = this.PropertyNameResolver;
			return r;
		}
		/// <summary>
		/// Returns an response of type R based on the connection status by trying parsing status.Result into R
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

			var r = JsonConvert.DeserializeObject<R>(status.Result, this.SerializationSettings);
			r.IsValid = isValid;
			r.ConnectionStatus = status;
			r.PropertyNameResolver = this.PropertyNameResolver;
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
					this._VersionInfo = JsonConvert.DeserializeObject<ElasticSearchVersionInfo>(version.ToString());

					this._gotNodeInfo = true;
				}
			}
			catch 
			{
				this._IsValid = false;
			}
			return response;
		}
		
		
	

	
	}
}
