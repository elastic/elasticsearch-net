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
using System.Diagnostics;
using System.Text.RegularExpressions;

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
		

		public bool TryConnect(out ConnectionStatus status)
		{
			status = this.GetNodeInfo();
			return this.IsValid;


		}


		
		public ElasticClient(IConnectionSettings settings)
		{
			this.Settings = settings;
			this.Connection = new Connection(settings);
			this.SerializationSettings = new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> { new IsoDateTimeConverter(), new QueryJsonConverter() }
			};
		}
		
		public string Serialize(object @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
		}

		private ConnectionStatus GetNodeInfo()
		{
			var response = this.Connection.GetSync("");
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
			return response;
		}
		
		private string createPath(string index, string type)
		{
			return "{0}/{1}/".F(index, type);
		}
		private string createPath(string index, string type, string id)
		{
			return "{0}/{1}/{2}".F(index, type, id);
		}

		public ConnectionStatus Index<T>(string index, string type, T @object) where T : class
		{
			return this.Index<T>(@object, this.createPath(index, type));
		}
		public ConnectionStatus Index<T>(string index, string type, string id, T @object) where T : class
		{
			return this.Index<T>(@object, this.createPath(index, type, id));
		}
		public ConnectionStatus Index<T>(string index, string type, int id, T @object) where T : class
		{
			return this.Index<T>(@object, this.createPath(index, type, id.ToString()));
		}
		public ConnectionStatus Index<T>(T @object) where T : class
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
			return this.Index<T>(@object, path);
		}
		private ConnectionStatus Index<T>(T @object, string path) where T : class
		{
			string json = JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);

			return this.Connection.PostSync(path, json);
		}

		public void IndexAsync<T>(T @object, string path, Action<ConnectionStatus> continuation) where T : class
		{
			string json = JsonConvert.SerializeObject(@object, Formatting.None, this.SerializationSettings);
			this.Connection.Post(path, json, continuation);
		}
		public void IndexAsync<T>(IEnumerable<T> @objects) where T : class
		{
			objects = @objects.ToList().ConvertAll((@object) => @object);
			var json = this.GenerateBulkCommand(@objects);
			if (!json.IsNullOrEmpty())
				this.Connection.Post("_bulk", json, null);
		}
		public void IndexAsync<T>(IEnumerable<T> @objects, Action<ConnectionStatus> continuation) where T : class
		{
			objects = @objects.ToList().ConvertAll((@object) => @object);
			var json = this.GenerateBulkCommand(@objects);
			if (!json.IsNullOrEmpty())
				this.Connection.Post("_bulk", json, continuation);
		}
		public void Index<T>(IEnumerable<T> @objects) where T : class
		{
			var json = this.GenerateBulkCommand(@objects);
			if (!json.IsNullOrEmpty())
				this.Connection.PostSync("_bulk", json);
		}
		private string GenerateBulkCommand<T>(IEnumerable<T> @objects)
		{
			@objects.ThrowIfNull("objects");

			var index = this.Settings.DefaultIndex;
			if (string.IsNullOrEmpty(index))
				throw new NullReferenceException("Cannot infer default index for current connection.");

			if (@objects.Count() == 0)
				return null;

			var type = typeof(T);
			var typeName = Inflector.MakePlural(type.Name).ToLower();

			Func<T, string> idSelector = null;
			var idProperty = type.GetProperty("Id");
			if (idProperty != null)
			{
				if (idProperty.PropertyType == typeof(int))
					idSelector = (@object) => ((int)@object.TryGetPropertyValue("Id")).ToString();
				else if (idProperty.PropertyType == typeof(int?))
					idSelector = (@object) =>
					{
						int? val = (int?)@object.TryGetPropertyValue("Id");
						return (val.HasValue) ? val.Value.ToString() : string.Empty;
					};
				else if (idProperty.PropertyType == typeof(string))
					idSelector = (@object) => (string)@object.TryGetPropertyValue("Id");
			}

			var sb = new StringBuilder();
			var command = "{{ \"index\" : {{ \"_index\" : \"{0}\", \"_type\" : \"{1}\", \"_id\" : \"{2}\" }} }}\n";

			//if we can't reflect id let ES create one.
			if (idSelector == null)
				command = "{{ \"index\" : {{ \"_index\" : \"{0}\", \"_type\" : \"{1}\" }} }}\n".F(index, typeName);

			foreach (var @object in objects)
			{
				string jsonCommand = JsonConvert.SerializeObject(@object, Formatting.None, this.SerializationSettings);
				if (idSelector == null)
					sb.Append(command);
				else
					sb.Append(command.F(index, typeName, idSelector(@object)));
				sb.Append(jsonCommand + "\n");
			}
			var json = sb.ToString();
			return json;
		}



		private Regex _bulkReplace = new Regex(@",\n|^\[", RegexOptions.Compiled | RegexOptions.Multiline);

		public void IndexAsync<T>(IEnumerable<T> objects, string path, Action<ConnectionStatus> continuation)
		{
			var sb = new StringBuilder();
			var command = "{ \"index\" : { \"_index\" : \"mpdreamz\", \"_type\" : \"posts\", \"_id\" : \"1\" } }\n";
			foreach (var @object in objects)
			{
				string jsonCommand = JsonConvert.SerializeObject(@object, Formatting.None, this.SerializationSettings);
				sb.Append(command);
				sb.Append(jsonCommand + "\n");
			}
			var json = sb.ToString();
			this.Connection.Post("_bulk", json, continuation);  
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

		public QueryResponse<T> Query<T>(string query) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var type = typeof(T);
			var typeName = Inflector.MakePlural(type.Name).ToLower();
			string path = this.createPath(index, typeName) + "_search";

			var result = this.Connection.PostSync(path, query);

			var response = JsonConvert.DeserializeObject<QueryResponse<T>>(result.Result);

			return response;
		}

		public QueryResponse<T> Search<T>(Search search) where T : class
		{
			var rawQuery = this.Serialize(search);
			return this.Query<T>(rawQuery);
		}
		public QueryResponse<T> Search<T>(IQuery query) where T : class
		{
			return this.Search<T>(new Search()
			{
				Query = new Query(query)

			}.Skip(0).Take(10)
			);
		}
		
	}
}
