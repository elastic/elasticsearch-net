using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface IDictionaryResponse<TKey, TValue> : IResponse
	{
		IReadOnlyDictionary<TKey, TValue> BackingDictionary { get; set; }
	}

	public abstract class DictionaryResponseBase<TKey, TValue> : ResponseBase, IDictionaryResponse<TKey, TValue>
	{
		protected IDictionaryResponse<TKey, TValue> Self => this;
		IReadOnlyDictionary<TKey, TValue> IDictionaryResponse<TKey, TValue>.BackingDictionary { get; set; }
	}

	internal class DictionaryResponseJsonConverterHelpers
	{
		public static JObject ReadServerErrorFirst(JsonReader reader, out Error error, out int? statusCode)
		{
			var j = JObject.Load(reader);
			var errorProperty = j.Property("error");
			error = null;
			if (errorProperty?.Value?.Type == JTokenType.String)
			{
				var reason = errorProperty.Value.Value<string>();
				error = new Error {Reason = reason};
				errorProperty.Remove();
			}
			else if (errorProperty?.Value?.Type == JTokenType.Object && ((JObject) errorProperty.Value)["reason"] != null)
			{
				error = errorProperty.Value.ToObject<Error>();
				errorProperty.Remove();
			}
			var statusProperty = j.Property("status");
			statusCode = null;
			if (statusProperty?.Value?.Type == JTokenType.Integer)
			{
				statusCode = statusProperty.Value.Value<int>();
				statusProperty.Remove();
			}
			return j;
		}
	}

	internal class DictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter
		where TResponse : ResponseBase, IDictionaryResponse<TKey, TValue>, new()
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = DictionaryResponseJsonConverterHelpers.ReadServerErrorFirst(reader, out var error, out var statusCode);
			var response = new TResponse();
			var dict = new Dictionary<TKey, TValue>();
			serializer.Populate(j.CreateReader(), dict);
			response.BackingDictionary = dict;
			response.Error = error;
			response.StatusCode = statusCode;
			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

	}
}
