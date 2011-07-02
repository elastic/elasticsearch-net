using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


		
		public ElasticClient(IConnectionSettings settings)
		{
            if (settings == null)
                throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = new Connection(settings);
			this.SerializationSettings = new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> { new IsoDateTimeConverter(), new QueryJsonConverter(), new FacetsMetaDataConverter() }
			};
			this.PropertyNameResolver = new PropertyNameResolver(this.SerializationSettings);
		}
		public ElasticClient(IConnectionSettings settings,bool  useThrift)
		{
			this.Settings = settings;
			this.Connection = new ThriftConnection(settings);
			this.SerializationSettings = new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> { new IsoDateTimeConverter(), new QueryJsonConverter(), new FacetsMetaDataConverter() }
			};
			this.PropertyNameResolver = new PropertyNameResolver(this.SerializationSettings);
		}
		
		public string Serialize(object @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
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
