/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
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
