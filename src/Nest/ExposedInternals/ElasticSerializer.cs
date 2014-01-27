using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class ElasticSerializer
	{
		private readonly IConnectionSettings _settings;
		private readonly PropertyNameResolver _propertyNameResolver;
		private readonly JsonSerializerSettings _serializationSettings;

		public ElasticSerializer(IConnectionSettings settings)
		{
			this._settings = settings;
			this._serializationSettings = this.CreateSettings();
			this._propertyNameResolver = new PropertyNameResolver();
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
				r = Activator.CreateInstance<R>();
			else
				r = JsonConvert.DeserializeObject<R>(status.Result, jsonSettings);

			var baseResponse = r as BaseResponse;
			if (baseResponse == null)
				return null;
			baseResponse.IsValid = isValid;
			baseResponse.ConnectionStatus = status;
			baseResponse.PropertyNameResolver = this._propertyNameResolver;
			return r;
		}

		/// <summary>
		/// serialize an object using the internal registered converters without camelcasing properties as is done 
		/// while indexing objects
		/// </summary>
		public string Serialize(object @object, Formatting formatting = Formatting.Indented)
		{
			return JsonConvert.SerializeObject(@object, formatting, this._serializationSettings);
		}

		/// <summary>
		/// Deserialize an object 
		/// </summary>
		/// <param name="notFoundIsValid">When deserializing a ConnectionStatus to a BaseResponse type this controls whether a 404 is a valid response</param>
		public T Deserialize<T>(object value, IList<JsonConverter> extraConverters = null, bool notFoundIsValidResponse = false) where T : class
		{
			return this.DeserializeInternal<T>(value, null, extraConverters, notFoundIsValidResponse);
		}

		public IQueryResponse<TResult> DeserializeSearchResponse<T, TResult>(ConnectionStatus status, SearchDescriptor<T> originalSearchDescriptor)
			where TResult : class
			where T : class
		{
			var types = (originalSearchDescriptor._Types ?? Enumerable.Empty<TypeNameMarker>())
				.Where(t => t.Type != null);
			var partialFields = originalSearchDescriptor._PartialFields.EmptyIfNull().Select(x => x.Key);
			if (originalSearchDescriptor._ConcreteTypeSelector == null && (
				types.Any(t => t.Type != typeof(TResult)))
				|| partialFields.Any())
			{
				var typeDictionary = types
					.ToDictionary(t => t.Resolve(this._settings), t => t.Type);

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
				piggyBackJsonConverter: new ConcreteTypeConverter<TResult>(originalSearchDescriptor._ConcreteTypeSelector, partialFields)
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

		/// <summary>
		/// _msearch needs a specialized json format in the body
		/// </summary>
		internal string SerializeMultiSearch(MultiSearchDescriptor multiSearchDescriptor)
		{
			var sb = new StringBuilder();
			foreach (var operation in multiSearchDescriptor._Operations.Values)
			{
				var indeces = operation._Indices.HasAny() ? string.Join(",", operation._Indices) : null;
				if (operation._AllIndices.GetValueOrDefault(false))
					indeces = "_all";
				
				var index = indeces ??
							multiSearchDescriptor._FixedIndex ??
				            new IndexNameResolver(this._settings).GetIndexForType(operation._ClrType);
				
				var types =  operation._Types.HasAny() ? string.Join(",", operation._Types.Select(x => x.Resolve(this._settings)) ) : null;
				var typeName = types
							   ?? multiSearchDescriptor._FixedType
				               ?? TypeNameMarker.Create(operation._ClrType);
				if (operation._AllTypes.GetValueOrDefault(false))
					typeName = null; //force empty typename so we'll query all types.

				var op = new { index = index, type = typeName, search_type = this.GetSearchType(operation, multiSearchDescriptor), 
					preference = operation._Preference, routing = operation._Routing };
				var opJson =  JsonConvert.SerializeObject(op, Formatting.None);

				var action = "{0}\n".F(opJson);
				sb.Append(action);
				var searchJson = JsonConvert.SerializeObject(operation, Formatting.None);
				sb.Append(searchJson + "\n");

			}
			var json = sb.ToString();
			return json;
		}
		private string GetSearchType(SearchDescriptorBase descriptor, MultiSearchDescriptor multiSearchDescriptor)
		{
			if (!descriptor._SearchType.HasValue)
				return null;
			switch (descriptor._SearchType.Value)
			{
				case SearchType.Count:
					return "count";
				case SearchType.DfsQueryThenFetch:
					return "dfs_query_then_fetch";
				case SearchType.DfsQueryAndFetch:
					return "dfs_query_and_fetch";
				case SearchType.QueryThenFetch:
					return "query_then_fetch";
				case SearchType.QueryAndFetch:
					return "query_and_fetch";
				case SearchType.Scan:
					return "scan";
			}
			return multiSearchDescriptor._QueryString.ContainsKey("search_type") 
				? multiSearchDescriptor._QueryString.NameValueCollection["search_type"] 
				: null;
			
		}
	}
	/// <summary>
	/// Registerering global jsonconverters is very costly,
	/// The best thing is to specify them as a contract (see ElasticContractResolver)
	/// This however prevents a way to give a jsonconverter state which for some calls is needed i.e:
	/// A multiget and multisearch need access to the descriptor that describes what types are used.
	/// When NEST knows it has to piggyback this it has to pass serialization state it will create a new 
	/// serializersettings object with a new contract resolver which holds this state. Its ugly but it does boost
	/// massive performance gains.
	/// </summary>
	internal class JsonConverterPiggyBackState
	{
		public JsonConverter ActualJsonConverter { get; set; }
	}
}
