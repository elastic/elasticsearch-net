// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	public class ResolvableDictionaryProxy<TKey, TValue> : IIsAReadOnlyDictionary<TKey, TValue>
		where TKey : IUrlParameter
	{
		private readonly IConnectionConfigurationValues _connectionSettings;

		internal ResolvableDictionaryProxy(IConnectionConfigurationValues connectionSettings, IReadOnlyDictionary<TKey, TValue> backingDictionary)
		{
			_connectionSettings = connectionSettings;
			if (backingDictionary == null) return;

			Original = backingDictionary;

			var dictionary = new Dictionary<string, TValue>(backingDictionary.Count);
			foreach (var key in backingDictionary.Keys)
				dictionary[Sanitize(key)] = backingDictionary[key];

			BackingDictionary = dictionary;
		}

		public int Count => BackingDictionary.Count;

		public TValue this[TKey key] => BackingDictionary.TryGetValue(Sanitize(key), out var v) ? v : default(TValue);
		public TValue this[string key] => BackingDictionary.TryGetValue(key, out var v) ? v : default(TValue);

		public IEnumerable<TKey> Keys => Original.Keys;
		public IEnumerable<string> ResolvedKeys => BackingDictionary.Keys;

		public IEnumerable<TValue> Values => BackingDictionary.Values;
		protected internal IReadOnlyDictionary<string, TValue> BackingDictionary { get; } = EmptyReadOnly<string, TValue>.Dictionary;
		private IReadOnlyDictionary<TKey, TValue> Original { get; } = EmptyReadOnly<TKey, TValue>.Dictionary;

		IEnumerator IEnumerable.GetEnumerator() => Original.GetEnumerator();

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			Original.GetEnumerator();

		public bool ContainsKey(TKey key) => BackingDictionary.ContainsKey(Sanitize(key));

		public bool TryGetValue(TKey key, out TValue value) =>
			BackingDictionary.TryGetValue(Sanitize(key), out value);

		private string Sanitize(TKey key) => key?.GetString(_connectionSettings);
	}

	internal abstract class ResolvableDictionaryFormatterBase<TDictionary, TKey, TValue> : IJsonFormatter<TDictionary>
	{
		public TDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Dictionary<TKey, TValue>>();
			var d = formatter.Deserialize(ref reader, formatterResolver);
			var settings = formatterResolver.GetConnectionSettings();
			var dict = Create(settings, d);
			return dict;
		}

		public void Serialize(ref JsonWriter writer, TDictionary value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		protected abstract TDictionary Create(IConnectionSettingsValues settings, Dictionary<TKey, TValue> dictionary);
	}

	internal class ResolvableReadOnlyDictionaryFormatter<TKey, TValue>
		: ResolvableDictionaryFormatterBase<IReadOnlyDictionary<TKey, TValue>, TKey, TValue>
		where TKey : IUrlParameter
	{
		protected override IReadOnlyDictionary<TKey, TValue> Create(IConnectionSettingsValues settings, Dictionary<TKey, TValue> dictionary) =>
			new ResolvableDictionaryProxy<TKey, TValue>(settings, dictionary);
	}

	internal class ResolvableDictionaryResponseFormatter<TResponse, TKey, TValue> : IJsonFormatter<TResponse>
		where TResponse : ResponseBase, IDictionaryResponse<TKey, TValue>, new()
		where TKey : IUrlParameter
	{
		public TResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var response = new TResponse();
			var dictionary = new Dictionary<TKey, TValue>();
			var count = 0;
			var keyFormatter = formatterResolver.GetFormatter<TKey>();
			var valueFormatter = formatterResolver.GetFormatter<TValue>();

			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (ResponseFormatterHelpers.ServerErrorFields.TryGetValue(property, out var errorValue))
				{
					switch (errorValue)
					{
						case 0:
							if (reader.GetCurrentJsonToken() == JsonToken.String)
								response.Error = new Error { Reason = reader.ReadString() };
							else
							{
								var formatter = formatterResolver.GetFormatter<Error>();
								response.Error = formatter.Deserialize(ref reader, formatterResolver);
							}
							break;
						case 1:
							if (reader.GetCurrentJsonToken() == JsonToken.Number)
								response.StatusCode = reader.ReadInt32();
							else
								reader.ReadNextBlock();
							break;
					}
				}
				else
				{
					// include opening string quote in reader (offset - 1)
					var propertyReader = new JsonReader(property.Array, property.Offset - 1);
					var key = keyFormatter.Deserialize(ref propertyReader, formatterResolver);
					var value = valueFormatter.Deserialize(ref reader, formatterResolver);
					dictionary.Add(key, value);
				}
			}

			var settings = formatterResolver.GetConnectionSettings();
			var resolvableDictionary = new ResolvableDictionaryProxy<TKey, TValue>(settings, dictionary);
			response.BackingDictionary = resolvableDictionary;
			return response;
		}

		public void Serialize(ref JsonWriter writer, TResponse value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
