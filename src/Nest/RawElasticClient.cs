using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	public partial class RawElasticClient : IRawElasticClient
	{
		protected IConnection Connection { get; set; }
		public IConnectionSettings Settings { get; protected set; }

		public RawElasticClient(IConnectionSettings settings)
			: this(settings, new Connection(settings))
		{

		}

		internal readonly JsonSerializerSettings SerializationSettings;
		internal readonly JsonSerializerSettings IndexSerializationSettings;
		internal readonly PropertyNameResolver PropertyNameResolver;
		private readonly List<JsonConverter> _extraConverters = new List<JsonConverter>();

		private readonly List<JsonConverter> _defaultConverters = new List<JsonConverter>
		{
			new IsoDateTimeConverter(),
			new FacetConverter()
		};

		private JsonSerializerSettings CreateSettings()
		{
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticResolver(this.Settings),
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include,
				Converters = _defaultConverters.Concat(_extraConverters).ToList()
			};
		}
		public void AddConverter(JsonConverter converter)
		{
			this.IndexSerializationSettings.Converters.Add(converter);
			this.SerializationSettings.Converters.Add(converter);
			_extraConverters.Add(converter);
		}

		public void ModifyJsonSerializationSettings(Action<JsonSerializerSettings> modifier)
		{
			modifier(this.IndexSerializationSettings);
			modifier(this.SerializationSettings);
		}

		/// <summary>
		/// serialize an object using the internal registered converters without camelcasing properties as is done 
		/// while indexing objects
		/// </summary>
		public string Serialize(object @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
		}

		public RawElasticClient(IConnectionSettings settings, IConnection connection)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
			this.Connection = connection;

			this.SerializationSettings = this.CreateSettings();
			var indexSettings = this.CreateSettings();

			indexSettings.ContractResolver = new ElasticCamelCaseResolver(this.Settings);
			this.IndexSerializationSettings = indexSettings;
		}

		protected ConnectionStatus DoRequest(string method, string path, object data = null,  NameValueCollection queryString = null)
		{
			var postData = string.Empty;
			var s = data as string;
			if (s != null)
				postData = s;
			else if (data != null)
				postData = this.Serialize(data);

			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.PostSync(path, postData);
				case "put": return this.Connection.PutSync(path, postData);
				case "delete": 
					return string.IsNullOrWhiteSpace(postData) 
						? this.Connection.DeleteSync(path) 
						: this.Connection.DeleteSync(path, postData);
				case "head": return this.Connection.HeadSync(path);
				case "get": return this.Connection.GetSync(path);
			}
			throw new DslException("Unknown HTTP method " + method);
		}

		protected Task<ConnectionStatus> DoRequestAsync(string method, string path, object data = null, NameValueCollection queryString = null)
		{
			var postData = string.Empty;
			var s = data as string;
			if (s != null)
				postData = s;
			else if (data != null)
				postData = this.Serialize(data);

			switch (method.ToLowerInvariant())
			{
				case "post": return this.Connection.Post(path, postData);
				case "put": return this.Connection.Put(path, postData);
				case "delete":
					return string.IsNullOrWhiteSpace(postData)
						? this.Connection.Delete(path)
						: this.Connection.Delete(path, postData);
				case "head": return this.Connection.Head(path);
				case "get": return this.Connection.Get(path);
			}
			throw new DslException("Unknown HTTP method " + method);
		}
	}
}
