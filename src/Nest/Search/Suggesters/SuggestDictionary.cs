using System;
using System.Collections.Generic;
using Utf8Json;
using JsonReader = Newtonsoft.Json.JsonReader;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Nest
{
	internal class SuggestDictionaryConverter<T> : IJsonFormatter<SuggestDictionary<T>>
		where T : class
	{
		public void Serialize(ref JsonWriter writer, SuggestDictionary<T> value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = new VerbatimReadOnlyDictionaryKeysFormatter<string, Suggest<T>[]>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		public SuggestDictionary<T> Deserialize(ref Utf8Json.JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Dictionary<string, Suggest<T>[]>>();
			var dict = formatter.Deserialize(ref reader, formatterResolver);
			return new SuggestDictionary<T>(dict);
		}
	}

	public class SuggestDictionary<T> : IsAReadOnlyDictionaryBase<string, Suggest<T>[]>
		where T : class
	{
		[SerializationConstructor]
		public SuggestDictionary(IReadOnlyDictionary<string, Suggest<T>[]> backingDictionary) : base(backingDictionary) { }

		public static SuggestDictionary<T> Default { get; } = new SuggestDictionary<T>(EmptyReadOnly<string, Suggest<T>[]>.Dictionary);

		protected override string Sanitize(string key)
		{
			//typed_keys = true results in suggest keys being returned as "<type>#<name>"
			var hashIndex = key.IndexOf('#');
			return hashIndex > -1 ? key.Substring(hashIndex + 1) : key;
		}
	}
}
