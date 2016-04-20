using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDictionaryResponse<TKey, TValue> : IResponse
	{
		IDictionary<TKey, TValue> BackingDictionary { get; set; }
	}

	public abstract class DictionaryResponseBase<TKey, TValue> : ResponseBase, IDictionaryResponse<TKey, TValue>
	{
		protected IDictionaryResponse<TKey, TValue> Self => this;
		IDictionary<TKey, TValue> IDictionaryResponse<TKey, TValue>.BackingDictionary { get; set; }
	}

	public class DictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter
		where TResponse : IDictionaryResponse<TKey, TValue>, new()
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new TResponse();
			var dict = new Dictionary<TKey, TValue>();
			serializer.Populate(reader, dict);
			response.BackingDictionary = dict;
			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }
	}
}
