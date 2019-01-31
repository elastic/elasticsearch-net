using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	internal class SuggestDictionaryFormatter<T> : IJsonFormatter<SuggestDictionary<T>>
		where T : class
	{
		public SuggestDictionary<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Dictionary<string, Suggest<T>[]>>();
			var dict = formatter.Deserialize(ref reader, formatterResolver);
			return new SuggestDictionary<T>(dict);
		}

		public void Serialize(ref JsonWriter writer, SuggestDictionary<T> value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = new VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, Suggest<T>[]>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}

	[JsonFormatter(typeof(SuggestDictionaryFormatter<>))]
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
