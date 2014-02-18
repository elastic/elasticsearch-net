using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public enum SerializationFormatting
	{
		None,
		Indented
	}

	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(byte[] bytes) where T : class;
		byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented);

	}


	public interface INestSerializer : IElasticsearchSerializer
	{
		/// <summary>
		/// Deserialize an object 
		/// </summary>
		/// <param name="notFoundIsValid">When deserializing a ConnectionStatus to a BaseResponse type this controls whether a 404 is a valid response</param>
		T Deserialize<T>(string value, bool notFoundIsValidResponse = false) where T : class;

		T Deserialize<T>(ConnectionStatus value, bool notFoundIsValidResponse = false) where T : class;
		IQueryResponse<TResult> DeserializeSearchResponse<T, TResult>(ConnectionStatus status, SearchDescriptor<T> originalSearchDescriptor)
			where TResult : class
			where T : class;

		string SerializeBulkDescriptor(BulkDescriptor bulkDescriptor);

		/// <summary>
		/// _msearch needs a specialized json format in the body
		/// </summary>
		string SerializeMultiSearch(MultiSearchDescriptor multiSearchDescriptor);

		TemplateResponse DeserializeTemplateResponse(ConnectionStatus c, GetTemplateDescriptor d);
		GetMappingResponse DeserializeGetMappingResponse(ConnectionStatus c);
		MultiGetResponse DeserializeMultiGetResponse(ConnectionStatus c, MultiGetDescriptor d);
		MultiSearchResponse DeserializeMultiSearchResponse(ConnectionStatus c, MultiSearchDescriptor d);
		WarmerResponse DeserializeWarmerResponse(ConnectionStatus connectionStatus, GetWarmerDescriptor getWarmerDescriptor);
		IndexSettingsResponse DeserializeIndexSettingsResponse(ConnectionStatus status);
	}

	public class NestSerializer : INestSerializer
	{
		private static readonly Lazy<Regex> StripIndex = new Lazy<Regex>(() => new Regex(@"^index\."), LazyThreadSafetyMode.PublicationOnly);
		private readonly IConnectionSettings _settings;
		private readonly JsonSerializerSettings _serializationSettings;

		public NestSerializer(IConnectionSettings settings)
		{
			this._settings = settings;
			this._serializationSettings = this.CreateSettings();
		}

		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		protected virtual R ToParsedResponse<R>(ConnectionStatus status, JsonSerializerSettings jsonSettings, bool allow404 = false) where R : class
		{
			var isValid =
				(allow404)
				? (status.Error == null
					|| status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
				: (status.Error == null);

			R r;
			if (!isValid)
				r = (R)typeof (R).CreateInstance();
			else
				r = JsonConvert.DeserializeObject<R>(status.Result, jsonSettings);

			var baseResponse = r as BaseResponse;
			if (baseResponse == null)
				return null;
			baseResponse.IsValid = isValid;
			baseResponse.ConnectionStatus = status;
			return r;
		}

		public virtual T Deserialize<T>(byte[] bytes) where T : class
		{
			return this.Deserialize<T>(bytes.Utf8String());
		}

		public virtual byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var format = formatting == SerializationFormatting.None ? Formatting.None : Formatting.Indented;
			var serialized = JsonConvert.SerializeObject(data, format, this._serializationSettings);
			return serialized.Utf8Bytes();
		}

		/// <summary>
		/// Deserialize an object 
		/// </summary>
		/// <param name="notFoundIsValid">When deserializing a ConnectionStatus to a BaseResponse type this controls whether a 404 is a valid response</param>
		public T Deserialize<T>(string value, bool notFoundIsValidResponse = false) where T : class
		{
			return this.DeserializeInternal<T>(value, null, null, notFoundIsValidResponse);
		}
		public T Deserialize<T>(ConnectionStatus value, bool notFoundIsValidResponse = false) where T : class
		{
			return this.DeserializeInternal<T>(value, null, null, notFoundIsValidResponse);
		}
		public IQueryResponse<TResult> DeserializeSearchResponse<T, TResult>(ConnectionStatus status, SearchDescriptor<T> originalSearchDescriptor)
			where TResult : class
			where T : class
		{
			var types = (originalSearchDescriptor._Types ?? Enumerable.Empty<TypeNameMarker>())
				.Where(t => t.Type != null);
			if (originalSearchDescriptor._ConcreteTypeSelector == null && types.Any(t => t.Type != typeof(TResult)))
			{
				var inferrer = new ElasticInferrer(this._settings);
				var typeDictionary = types
					.ToDictionary(inferrer.TypeName, t => t.Type);

				originalSearchDescriptor._ConcreteTypeSelector = (o, h) =>
				{
					Type t;
					if (!typeDictionary.TryGetValue(h.Type, out t))
						return typeof(TResult);
					return t;
				};
			}

			if (originalSearchDescriptor._ConcreteTypeSelector == null)
				return this.Deserialize<QueryResponse<TResult>>(status);

			return this.DeserializeInternal<QueryResponse<TResult>>(
				status,
				piggyBackJsonConverter: new ConcreteTypeConverter<TResult>(originalSearchDescriptor._ConcreteTypeSelector)
			);
		}

		internal T DeserializeInternal<T>(
			object value,
			JsonConverter piggyBackJsonConverter,
			IList<JsonConverter> extraConverters = null,

			bool notFoundIsValidResponse = false) where T : class
		{
			var jsonSettings = extraConverters.HasAny() || piggyBackJsonConverter != null
				? this.CreateSettings(extraConverters, piggyBackJsonConverter)
				: this._serializationSettings;

			var jTokenValue = value as JToken;
			if (jTokenValue != null)
				return JsonSerializer.Create(jsonSettings).Deserialize<T>(jTokenValue.CreateReader());

			var status = value as ConnectionStatus;
			if (status == null || !typeof(BaseResponse).IsAssignableFrom(typeof(T)))
				return JsonConvert.DeserializeObject<T>(value.ToString(), jsonSettings);

			return this.ToParsedResponse<T>(status, jsonSettings, notFoundIsValidResponse);
		}

		internal JsonSerializerSettings CreateSettings(IList<JsonConverter> extraConverters = null, JsonConverter piggyBackJsonConverter = null)
		{
			var converters = extraConverters.HasAny()
				? extraConverters.ToList()
				: null;
			var piggyBackState = new JsonConverterPiggyBackState { ActualJsonConverter = piggyBackJsonConverter };
			var settings = new JsonSerializerSettings()
			{
				ContractResolver = new ElasticContractResolver(this._settings) { PiggyBackState = piggyBackState },
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore,
				Converters = converters,
			};

			if (_settings.ModifyJsonSerializerSettings != null)
				_settings.ModifyJsonSerializerSettings(settings);

			return settings;
		}

		public string SerializeBulkDescriptor(BulkDescriptor bulkDescriptor)
		{
			bulkDescriptor.ThrowIfNull("bulkDescriptor");
			bulkDescriptor._Operations.ThrowIfEmpty("Bulk descriptor does not define any operations");
			var sb = new StringBuilder();
			var inferrer = new ElasticInferrer(this._settings);

			foreach (var operation in bulkDescriptor._Operations)
			{
				var command = operation._Operation;
				var index = operation._Index
					?? inferrer.IndexName(bulkDescriptor._Index)
					?? inferrer.IndexName(operation._ClrType);
				var typeName = operation._Type
					?? inferrer.TypeName(bulkDescriptor._Type)
					?? inferrer.TypeName(operation._ClrType);

				var id = operation.GetIdForObject(inferrer);
				operation._Index = index;
				operation._Type = typeName;
				operation._Id = id;

				var opJson = this.Serialize(operation, SerializationFormatting.None).Utf8String();

				var action = "{{ \"{0}\" :  {1} }}\n".F(command, opJson);
				sb.Append(action);

				if (command == "index" || command == "create")
				{
					var jsonCommand = this.Serialize(operation._Object, SerializationFormatting.None).Utf8String();
					sb.Append(jsonCommand + "\n");
				}
				else if (command == "update")
				{
					var jsonCommand = this.Serialize(operation.GetBody(), SerializationFormatting.None).Utf8String();
					sb.Append(jsonCommand + "\n");
				}
			}
			var json = sb.ToString();
			return json;
		}
	
		/// <summary>
		/// _msearch needs a specialized json format in the body
		/// </summary>
		public string SerializeMultiSearch(MultiSearchDescriptor multiSearchDescriptor)
		{
			var sb = new StringBuilder();
			var inferrer = new ElasticInferrer(this._settings);
			foreach (var operation in multiSearchDescriptor._Operations.Values)
			{
				var indices = inferrer.IndexNames(operation._Indices);
				if (operation._AllIndices.GetValueOrDefault(false))
					indices = "_all";

				var index = indices 
					?? inferrer.IndexName(multiSearchDescriptor._Index)
					?? inferrer.IndexName(operation._ClrType);

				var types = inferrer.TypeNames(operation._Types);
				var typeName = types
					?? inferrer.TypeName(multiSearchDescriptor._Type)
					?? inferrer.TypeName(operation._ClrType);
				if (operation._AllTypes.GetValueOrDefault(false))
					typeName = null; //force empty typename so we'll query all types.

				var op = new
				{
					index = index,
					type = typeName,
					search_type = this.GetSearchType(operation, multiSearchDescriptor),
					preference = operation._Preference,
					routing = operation._Routing
				};
				var opJson = this.Serialize(op, SerializationFormatting.None).Utf8String();

				var action = "{0}\n".F(opJson);
				sb.Append(action);
				var searchJson = this.Serialize(operation, SerializationFormatting.None).Utf8String();
				sb.Append(searchJson + "\n");

			}
			var json = sb.ToString();
			return json;
		}

		public TemplateResponse DeserializeTemplateResponse(ConnectionStatus c, GetTemplateDescriptor d)
		{
			if (!c.Success) return new TemplateResponse { ConnectionStatus = c, IsValid = false };

			var dict = c.Deserialize<Dictionary<string, TemplateMapping>>();
			if (dict.Count == 0)
				throw new DslException("Could not deserialize TemplateMapping");

			return new TemplateResponse
			{
				ConnectionStatus = c,
				IsValid = true,
				Name = dict.First().Key,
				TemplateMapping = dict.First().Value
			};
		}
		

		public GetMappingResponse DeserializeGetMappingResponse(ConnectionStatus c)
		{
			var dict = c.Success
				? c.Deserialize<GetRootObjectMappingWrapping>()
				: null;
			return new GetMappingResponse(c, dict);

		}

		public MultiGetResponse DeserializeMultiGetResponse(ConnectionStatus c, MultiGetDescriptor d)
		{
			var multiGetHitConverter = new MultiGetHitConverter(d);
			var multiGetResponse = this.DeserializeInternal<MultiGetResponse>(c, piggyBackJsonConverter: multiGetHitConverter);
			return multiGetResponse;

		}

		public MultiSearchResponse DeserializeMultiSearchResponse(ConnectionStatus c, MultiSearchDescriptor d)
		{
			var multiSearchConverter = new MultiSearchConverter(this._settings, d);
			var multiSearchResponse = this.DeserializeInternal<MultiSearchResponse>(c, piggyBackJsonConverter: multiSearchConverter);
			return multiSearchResponse;

		}

		public WarmerResponse DeserializeWarmerResponse(ConnectionStatus connectionStatus, GetWarmerDescriptor getWarmerDescriptor)
		{
			if (!connectionStatus.Success)
				return new WarmerResponse() { ConnectionStatus = connectionStatus, IsValid = false };

			var dict = connectionStatus.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>>();
			var indices = new Dictionary<string, Dictionary<string, WarmerMapping>>();
			foreach (var kv in dict)
			{
				var indexDict = kv.Value;
				Dictionary<string, WarmerMapping> warmers;
				if (indexDict == null || !indexDict.TryGetValue("warmers", out warmers) || warmers == null)
					continue;
				foreach (var kvW in warmers)
				{
					kvW.Value.Name = kvW.Key;
				}
				indices.Add(kv.Key, warmers);
			}

			return new WarmerResponse
			{
				ConnectionStatus = connectionStatus,
				IsValid = true,
				Indices = indices
			};
		}
		
		protected string GetSearchType(SearchDescriptorBase descriptor, MultiSearchDescriptor multiSearchDescriptor)
		{
			if (descriptor._SearchType != null)
			{
				switch (descriptor._SearchType.Value)
				{
					case SearchTypeOptions.Count:
						return "count";
					case SearchTypeOptions.DfsQueryThenFetch:
						return "dfs_query_then_fetch";
					case SearchTypeOptions.DfsQueryAndFetch:
						return "dfs_query_and_fetch";
					case SearchTypeOptions.QueryThenFetch:
						return "query_then_fetch";
					case SearchTypeOptions.QueryAndFetch:
						return "query_and_fetch";
					case SearchTypeOptions.Scan:
						return "scan";
				}
			}
			return multiSearchDescriptor._QueryString.ContainsKey("search_type")
				? multiSearchDescriptor._QueryString._QueryStringDictionary["search_type"] as string
				: null;
		}

		public IndexSettingsResponse DeserializeIndexSettingsResponse(ConnectionStatus status)
		{
			var response = new IndexSettingsResponse { IsValid = false };
			try
			{
				var settingsContainer = SettingsContainer(status);
				response.Settings = settingsContainer.ToObject<IndexSettings>();
				response.IsValid = true;
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
			response.ConnectionStatus = status;
			return response;
		}

		//TODO although this gets the job done this looks a bit iffy, refactor
		private JObject SettingsContainer(ConnectionStatus status)
		{
			var o = JObject.Parse(status.Result);
			var settingsObject = o.First.First.First.First;

			var settingsContainer = new JObject();
			// In indexsettings response all analyzers etc are delivered as settings so need to split up the settings key and make proper json
			foreach (JProperty s in settingsObject.Children<JProperty>())
			{
				var name = StripIndex.Value.Replace(s.Name, "");
				if (name.StartsWith("analysis."))
				{
					var keys = name.Split('.');
					RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, keys, s.Value);
				}
				else if (name.StartsWith("similarity."))
				{
					var keys = name.Split('.');
					var similaryKeys = new[] { keys[0], keys[1], string.Join(".", keys.Skip(2).ToArray()) };
					RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, similaryKeys, s.Value);
				}
				else
				{
					RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, new[] { name }, s.Value);
				}
			}
			return settingsContainer;
		}

		/// <summary>
		/// Rewrites the index settings response to index settings json.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		private void RewriteIndexSettingsResponseToIndexSettingsJSon(JContainer container, string[] key, JToken value)
		{
			var thisKey = key.First();
			int indexer;

			if (key.Length > 2 || (key.Length == 2 && !int.TryParse(key.Last(), out indexer)))
			{
				var property = (JContainer)((JObject)container).GetValue(thisKey);
				if (property == null)
				{
					property = new JObject();
					((JObject)container).Add(thisKey, property);
				}
				RewriteIndexSettingsResponseToIndexSettingsJSon(property, key.Skip(1).ToArray(), value);
			}
			else if (key.Length == 2 && int.TryParse(key.Last(), out indexer))
			{
				var property = ((JObject)container).Property(thisKey);
				if (property == null)
				{
					property = new JProperty(thisKey, new JArray());
					container.Add(property);
				}
				var jArray = (JArray)property.Value;
				jArray.Add(value);
			}
			else
			{
				var property = new JProperty(thisKey, value);
				container.Add(property);
			}
		}

	}
}
