using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{

	public partial class ElasticClient
	{
		private IConnection Connection { get; set; }
		private IConnectionSettings Settings { get; set; }
		private bool _gotNodeInfo = false;
		private bool _IsValid { get; set; }
		private ElasticSearchVersionInfo _VersionInfo { get; set; }

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
			return JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);
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

			var r = JsonConvert.DeserializeObject<R>(status.Result, SerializationSettings);
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
