using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using ElasticSearch;
using Newtonsoft.Json.Converters;
using ElasticSearch.DSL;

namespace ElasticSearch.Client
{

	public class ElasticClient
	{
		private IConnection Connection { get; set; }
		private IConnectionSettings Settings { get; set; }
		private bool _gotNodeInfo = false;
		private bool _IsValid { get; set; }
		private ElasticSearchVersionInfo _VersionInfo { get; set; }
		private JsonSerializerSettings SerializationSettings { get; set; }
		
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
		
		
		public ElasticClient(IConnectionSettings settings)
		{
			this.Settings = settings;
			this.Connection = new Connection(settings);
			this.SerializationSettings = new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> { new IsoDateTimeConverter() }
			};
		}
		
		private void GetNodeInfo()
		{
			var response = this.Connection.GetSync("");
			if (response.Success)
			{
				JObject o = JObject.Parse(response.Result);
				if (o["ok"] == null)
				{
					this._IsValid = false;
					return;
				}
				
				this._IsValid = (bool)o["ok"];
				
				JObject version = o["version"] as JObject;
				this._VersionInfo = JsonConvert.DeserializeObject<ElasticSearchVersionInfo>(version.ToString());
				
				this._gotNodeInfo = true;
			}
		}
		
		private string createPath(string index, string type)
		{
			return "{0}/{1}/".F(index, type);
		}
		private string createPath(string index, string type, string id)
		{
			return "{0}/{1}/{2}".F(index, type, id);
		}
		
		public void IndexSync<T>(string index, string type, T @object) where T : class
		{
			this.IndexSync<T>(@object, this.createPath(index, type));
		}
		public void IndexSync<T>(string index, string type, string id, T @object) where T : class
		{
			this.IndexSync<T>(@object, this.createPath(index, type, id));
		}
		public void IndexSync<T>(string index, string type, int id, T @object) where T : class
		{
			this.IndexSync<T>(@object, this.createPath(index, type, id.ToString()));
		}
		public void IndexSync<T>(T @object) where T : class
		{
			@object.ThrowIfNull("object");
		
			var index = this.Settings.DefaultIndex;
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");
			
			var type = typeof(T);
			var typeName = Inflector.MakePlural(type.Name).ToLower();
			var path = this.createPath(index,typeName);
			
			var idProperty = type.GetProperty("Id");
			int? id = null;
			string idString = string.Empty;
			if (idProperty != null)
			{
				if (idProperty.PropertyType == typeof(int))
					id = (int?)@object.TryGetPropertyValue("Id");
				if (idProperty.PropertyType == typeof(string))
					idString = (string)@object.TryGetPropertyValue("Id");
				if (id.HasValue)
					idString = id.Value.ToString();
				if (!string.IsNullOrEmpty(idString))
					path = this.createPath(index, typeName, idString);
			}
			this.IndexSync<T>(@object, path);
		}
		private void IndexSync<T>(T @object, string path) where T : class
		{

			
			
			
			string json = JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
			var response = this.Connection.PostSync(path, json);
		}

		public T Get<T>(int id) where T : class
		{
			return this.Get<T>(id.ToString());
		}

		public T Get<T>(string id) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var type = typeof(T);
			var typeName = Inflector.MakePlural(type.Name).ToLower();
			return this.Get<T>(id, this.createPath(index, typeName));
			
			
		}
		public T Get<T>(string index, string type, string id) where T : class
		{
			return this.Get<T>(id, index + "/" + type + "/");
		}
		public T Get<T>(string index, string type, int id) where T : class
		{
			return this.Get<T>(id.ToString(), index + "/" + type + "/");
		}
		private T Get<T>(string id, string path) where T : class
		{
			var response = this.Connection.GetSync(path + id);
			var o = JObject.Parse(response.Result);
			var source = o["_source"];
			if (source != null)
			{
				return JsonConvert.DeserializeObject<T>(source.ToString());
			}
			
			return null;
		}
		
		public void Search<T>(IQuery<T> query) where T : class
		{
			var search = new Search<T>(query);
		}
		
	}
}
