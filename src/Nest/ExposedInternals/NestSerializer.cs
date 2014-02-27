using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public class NestSerializer : INestSerializer
	{
		private readonly IConnectionSettingsValues _settings;
		private readonly JsonSerializerSettings _serializationSettings;

		public NestSerializer(IConnectionSettingsValues settings)
		{
			this._settings = settings;
			this._serializationSettings = this.CreateSettings();
		}

		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		public virtual R ToParsedResponse<R>(
			ElasticsearchResponse status, 
			bool notFoundIsAValidResponse = false,
			JsonConverter piggyBackJsonConverter = null
			) where R : BaseResponse
		{
			var jsonSettings =piggyBackJsonConverter != null
				? this.CreateSettings(piggyBackJsonConverter)
				: this._serializationSettings;
			
			var isValid =
				(notFoundIsAValidResponse)
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
		public virtual T Deserialize<T>(byte[] bytes) where T : class
		{
			if (bytes == null) return null;
			return JsonConvert.DeserializeObject<T>(bytes.Utf8String(), this._serializationSettings);
		}
		
		public IQueryResponse<TResult> DeserializeSearchResponse<T, TResult>(ElasticsearchResponse status, SearchDescriptor<T> originalSearchDescriptor)
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
				return this.ToParsedResponse<QueryResponse<TResult>>(status, piggyBackJsonConverter: null, notFoundIsAValidResponse: true);

			var concreteTypeConverter = new ConcreteTypeConverter<TResult>(originalSearchDescriptor._ConcreteTypeSelector);
			return this.ToParsedResponse<QueryResponse<TResult>>(status, piggyBackJsonConverter: concreteTypeConverter, notFoundIsAValidResponse: true);
		}

		internal JsonSerializerSettings CreateSettings(JsonConverter piggyBackJsonConverter = null)
		{
			var piggyBackState = new JsonConverterPiggyBackState { ActualJsonConverter = piggyBackJsonConverter };
			var settings = new JsonSerializerSettings()
			{
				ContractResolver = new ElasticContractResolver(this._settings) { PiggyBackState = piggyBackState },
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
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

		public TemplateResponse DeserializeTemplateResponse(ElasticsearchResponse c, GetTemplateDescriptor d)
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

		public GetMappingResponse DeserializeGetMappingResponse(ElasticsearchResponse c)
		{
			var dict = c.Success
				? c.Deserialize<GetRootObjectMappingWrapping>()
				: null;
			return new GetMappingResponse(c, dict);

		}

		public MultiGetResponse DeserializeMultiGetResponse(ElasticsearchResponse c, MultiGetDescriptor d)
		{
			var multiGetHitConverter = new MultiGetHitConverter(d);
			var multiGetResponse = this.ToParsedResponse<MultiGetResponse>(c, piggyBackJsonConverter: multiGetHitConverter);
			return multiGetResponse;
		}

		public MultiSearchResponse DeserializeMultiSearchResponse(ElasticsearchResponse c, MultiSearchDescriptor d)
		{
			var multiSearchConverter = new MultiSearchConverter(this._settings, d);
			var multiSearchResponse = this.ToParsedResponse<MultiSearchResponse>(c, piggyBackJsonConverter: multiSearchConverter);
			return multiSearchResponse;
		}

		public WarmerResponse DeserializeWarmerResponse(ElasticsearchResponse connectionStatus, GetWarmerDescriptor getWarmerDescriptor)
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

	}
}