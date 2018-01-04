using System;
using System.Collections;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class ResolvableDictionaryProxy<TKey, TValue> : IIsAReadOnlyDictionary<TKey, TValue>
		where TKey : IUrlParameter
	{
		private readonly IConnectionConfigurationValues _connectionSettings;
		private IReadOnlyDictionary<TKey, TValue> Original { get; } = EmptyReadOnly<TKey, TValue>.Dictionary;
		protected internal IReadOnlyDictionary<string, TValue> BackingDictionary { get; } = EmptyReadOnly<string, TValue>.Dictionary;

		internal ResolvableDictionaryProxy(IConnectionConfigurationValues connectionSettings, IReadOnlyDictionary<TKey, TValue> backingDictionary)
		{
			this._connectionSettings = connectionSettings;
			if (backingDictionary == null) return;

			this.Original = backingDictionary;

			var dictionary = new Dictionary<string, TValue>();
			foreach (var key in backingDictionary.Keys)
				dictionary[Sanitize(key)] = backingDictionary[key];

			this.BackingDictionary = dictionary;
		}

		private string Sanitize(TKey key) => key?.GetString(_connectionSettings);

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			this.Original.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.Original.GetEnumerator();

		public int Count => this.BackingDictionary.Count;

		public bool ContainsKey(TKey key) => this.BackingDictionary.ContainsKey(Sanitize(key));

		public bool TryGetValue(TKey key, out TValue value) =>
			this.BackingDictionary.TryGetValue(Sanitize(key), out value);

		public TValue this[TKey key] => this.BackingDictionary.TryGetValue(Sanitize(key), out var v) ? v : default(TValue);
		public TValue this[string key] => this.BackingDictionary.TryGetValue(key, out var v) ? v : default(TValue);

		public IEnumerable<TKey> Keys => this.Original.Keys;
		public IEnumerable<string> ResolvedKeys => this.BackingDictionary.Keys;

		public IEnumerable<TValue> Values => this.BackingDictionary.Values;
	}

	internal abstract class ResolvableDictionaryJsonConverterBase<TDictionary, TKey, TValue> : JsonConverter
		where TDictionary : ResolvableDictionaryProxy<TKey, TValue>
		where TKey : IUrlParameter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var d = new Dictionary<TKey, TValue>();
			serializer.Populate(reader, d);
			var settings = serializer.GetConnectionSettings();
			var dict = Create(settings, d);
			return dict;
		}

		protected abstract TDictionary Create(IConnectionSettingsValues settings, Dictionary<TKey, TValue> dictionary);

	}

	internal class ResolvableDictionaryJsonConverter<TKey, TValue>
		: ResolvableDictionaryJsonConverterBase<ResolvableDictionaryProxy<TKey, TValue>,TKey, TValue>
		where TKey : IUrlParameter
	{
		protected override ResolvableDictionaryProxy<TKey, TValue> Create(IConnectionSettingsValues s, Dictionary<TKey, TValue> d) =>
			new ResolvableDictionaryProxy<TKey, TValue>(s, d);
	}

	internal class ResolvableDictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter
		where TResponse : ResponseBase, IDictionaryResponse<TKey, TValue> , new() where TKey : IUrlParameter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = DictionaryResponseJsonConverterHelpers.ReadServerErrorFirst(reader, out var error, out var statusCode);

			var response = new TResponse();
			var d = new Dictionary<TKey, TValue>();
			serializer.Populate(j.CreateReader(), d);
			var settings = serializer.GetConnectionSettings();
			var dict = new ResolvableDictionaryProxy<TKey, TValue>(settings, d);
			response.BackingDictionary = dict;
			response.Error = error;
			response.StatusCode = statusCode;
			return response;
		}


		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }
	}
}
