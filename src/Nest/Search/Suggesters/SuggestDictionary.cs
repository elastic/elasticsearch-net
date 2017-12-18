using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class SuggestDictionaryConverter<T> : VerbatimDictionaryKeysJsonConverter<string, Suggest<T>[]>
		where T : class
	{
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dict = serializer.Deserialize<Dictionary<string, Suggest<T>[]>>(reader);
			return new SuggestDictionary<T>(dict);
		}
	}

	public class SuggestDictionary<T> : IsAReadOnlyDictionaryBase<string, Suggest<T>[]>
		where T : class
	{
		public static SuggestDictionary<T> Default { get; } = new SuggestDictionary<T>(EmptyReadOnly<string, Suggest<T>[]>.Dictionary);

		public SuggestDictionary(IReadOnlyDictionary<string, Suggest<T>[]> backingDictionary) : base(backingDictionary) { }

		private static readonly char[] TypedKeysSeparator = {'#'};
		protected override string Sanitize(string key)
		{
			//typed_keys = true results in suggest keys being returned as "<type>#<name>"
			var tokens = key.Split(TypedKeysSeparator, 2, StringSplitOptions.RemoveEmptyEntries);
			return tokens.Length > 1 ? tokens[1] : tokens[0];
		}

	}
}
