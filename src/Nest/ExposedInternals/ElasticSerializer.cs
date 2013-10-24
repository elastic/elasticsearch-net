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

		internal T DeserializeInternal<T>(
			object value, 
			JsonConverter piggyBackJsonConverter,
			IList<JsonConverter> extraConverters = null, 

			bool notFoundIsValidResponse = false) where T : class
		{
			var jsonSettings = extraConverters.HasAny() || piggyBackJsonConverter != null 
				? this.CreateSettings(extraConverters, piggyBackJsonConverter) 
				: this._serializationSettings;
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
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticContractResolver(this._settings) { PiggyBackState = piggyBackState },
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore,
				Converters = converters,
			};
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
