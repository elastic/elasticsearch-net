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

		private readonly ReadOnlyCollection<JsonConverter> _extraConverters = new List<JsonConverter>().AsReadOnly();

		public ElasticSerializer(IConnectionSettings settings)
		{
			this._settings = settings;
			this._serializationSettings = this.CreateSettings();
			this._propertyNameResolver = new PropertyNameResolver();
			this._extraConverters = settings.ExtraConverters;
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
		public T Deserialize<T>(object value, bool notFoundIsValidResponse = false) where T : class
		{
			return this.DeserializeInternal<T>(value, null, notFoundIsValidResponse);
		}

		internal T DeserializeInternal<T>(
			object value, 
			IList<JsonConverter> extraConverters = null, 

			bool notFoundIsValidResponse = false) where T : class
		{
			var jsonSettings = extraConverters.HasAny() ? this.CreateSettings(extraConverters) : this._serializationSettings;
			var status = value as ConnectionStatus;
			if (status == null || !typeof(BaseResponse).IsAssignableFrom(typeof(T)))
				return JsonConvert.DeserializeObject<T>(value.ToString(), jsonSettings);

			return this.ToParsedResponse<T>(status, jsonSettings, notFoundIsValidResponse);
		}

		private JsonSerializerSettings CreateSettings(IList<JsonConverter> extraConverters = null)
		{
			var converters = extraConverters.HasAny() ? extraConverters.Concat(extraConverters).ToList() : extraConverters;
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticContractResolver(this._settings) { HasExtraConverters = extraConverters.HasAny() },
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include,
				Converters = converters,
			};
		}
	}

}
