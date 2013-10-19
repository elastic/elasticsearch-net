using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public class ElasticSerializer
	{
		private readonly IConnectionSettings _settings;
		private readonly PropertyNameResolver _propertyNameResolver;
		private readonly JsonSerializerSettings _serializationSettings;

		private static readonly List<JsonConverter> _extraConverters = new List<JsonConverter>();
		private static readonly List<JsonConverter> _defaultConverters = new List<JsonConverter>
		{
			new IsoDateTimeConverter(),
			new FacetConverter(),
			new DictionaryKeysAreNotPropertyNamesJsonConverter()
		};

		public ElasticSerializer(IConnectionSettings settings)
		{
			this._settings = settings;
			this._serializationSettings = this.CreateSettings();
			this._propertyNameResolver = new PropertyNameResolver();
		}

		/// <summary>
		/// Allows you to adjust the buildin JsonSerializerSettings to your liking
		/// </summary>
		public void ModifyJsonSerializationSettings(Action<JsonSerializerSettings> modifier)
		{
			modifier(this._serializationSettings);
		}
			
		/// <summary>
		/// Add a JsonConverter to the build in serialization
		/// </summary>
		public void AddConverter(JsonConverter converter)
		{
			this._serializationSettings.Converters.Add(converter);
			_extraConverters.Add(converter);
		}

		/// <summary>
		/// Returns a response of type R based on the connection status without parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		protected virtual R ToResponse<R>(ConnectionStatus status, bool allow404 = false) where R : class
		{
			var isValid =
				(allow404)
				? (status.Error == null
					|| status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
				: (status.Error == null);
			var r = (R)Activator.CreateInstance(typeof(R));
      var baseResponse = r as BaseResponse;
      if (baseResponse == null)
        return null;

			baseResponse.IsValid = isValid;
			baseResponse.ConnectionStatus = status;
			baseResponse.PropertyNameResolver = this._propertyNameResolver;
			return r;
		}
		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		protected virtual R ToParsedResponse<R>(ConnectionStatus status, bool allow404 = false, IEnumerable<JsonConverter> extraConverters = null) where R : class
		{
			var isValid =
				(allow404)
				? (status.Error == null
					|| status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
				: (status.Error == null);
			if (!isValid)
				return this.ToResponse<R>(status, allow404);

			var r = this.Deserialize<R>(status.Result, extraConverters: extraConverters);
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
		/// <param name="notFoundIsValid">When deserializing a ConnectionStatus to a BaseResponse this controls whether a 404 is a valid response</param>
		public T Deserialize<T>(object value, IEnumerable<JsonConverter> extraConverters = null, bool notFoundIsValidResponse = false) where T : class
		{
      
			var settings = this._serializationSettings;
			if (extraConverters.HasAny())
			{
				settings = this.CreateSettings();
				var concrete = extraConverters.OfType<ConcreteTypeConverter>().FirstOrDefault();
				if (concrete != null)
				{
					((ElasticContractResolver)settings.ContractResolver).ConcreteTypeConverter = concrete;
				}
				else
					settings.Converters = settings.Converters.Concat(extraConverters).ToList();

			}
      var status = value as ConnectionStatus;
      if (status == null || !typeof(BaseResponse).IsAssignableFrom(typeof(T)))
        return JsonConvert.DeserializeObject<T>(value.ToString(), settings);

      return this.ToParsedResponse<T>(status, notFoundIsValidResponse, extraConverters);

		}
		private JsonSerializerSettings CreateSettings()
		{
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticContractResolver(this._settings),
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include,
				Converters = _defaultConverters.Concat(_extraConverters).ToList()
			};
		}
	}
}
