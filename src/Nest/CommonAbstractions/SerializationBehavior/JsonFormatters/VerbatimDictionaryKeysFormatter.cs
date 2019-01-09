using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	internal class VerbatimDictionaryKeysFormatter<TDictionary, TInterface, TKey, TValue> : IJsonFormatter<TInterface>
		where TDictionary : TInterface, IIsADictionary<TKey, TValue>
		where TInterface : IIsADictionary<TKey, TValue>
	{
		private static readonly VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue> DictionaryFormatter =
			new VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue>();

		public void Serialize(ref JsonWriter writer, TInterface value, IJsonFormatterResolver formatterResolver)
		{
			DictionaryFormatter.Serialize(ref writer, value, formatterResolver);
		}

		public TInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dictionary = DictionaryFormatter.Deserialize(ref reader, formatterResolver);
			return typeof(TDictionary).CreateInstance<TDictionary>(dictionary);
		}
	}

	internal class VerbatimDictionaryKeysPreservingNullFormatter<TKey, TValue> : VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue>
	{
		protected override bool SkipValue(KeyValuePair<TKey, TValue> entry) => false;
	}
}
