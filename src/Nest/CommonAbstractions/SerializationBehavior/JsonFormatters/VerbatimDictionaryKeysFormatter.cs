// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class VerbatimDictionaryKeysFormatter<TDictionary, TInterface, TKey, TValue> : IJsonFormatter<TInterface>
		where TDictionary : TInterface, IIsADictionary<TKey, TValue>
		where TInterface : IIsADictionary<TKey, TValue>
	{
		private static readonly VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue> DictionaryFormatter =
			new VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue>();

		public void Serialize(ref JsonWriter writer, TInterface value, IJsonFormatterResolver formatterResolver) =>
			DictionaryFormatter.Serialize(ref writer, value, formatterResolver);

		public TInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dictionary = DictionaryFormatter.Deserialize(ref reader, formatterResolver);
			return typeof(TDictionary).CreateInstance<TDictionary>(dictionary);
		}
	}

	internal class VerbatimDictionaryInterfaceKeysPreservingNullFormatter<TKey, TValue> : VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue>
	{
		protected override bool SkipValue(KeyValuePair<TKey, TValue> entry) => false;
	}

	internal class VerbatimDictionaryKeysPreservingNullFormatter<TKey, TValue> : VerbatimDictionaryKeysFormatter<TKey, TValue>
	{
		protected override bool SkipValue(KeyValuePair<TKey, TValue> entry) => false;
	}
}
