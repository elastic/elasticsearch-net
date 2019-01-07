using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class ScriptFormatter : IJsonFormatter<IScript>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "inline", 0 },
			{ "source", 1 },
			{ "id", 2 },
			{ "lang", 3 },
			{ "params", 4 }
		};

		public IScript Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			IScript script = null;
			string language = null;
			Dictionary<string, object> parameters = null;

			while (reader.ReadIsInObject(ref count))
			{
				if (AutomataDictionary.TryGetValue(reader.ReadPropertyNameSegmentRaw(), out var value))
				{
					switch (value)
					{
						case 0:
						case 1:
							script = new InlineScript(reader.ReadString());
							break;
						case 2:
							script = new IndexedScript(reader.ReadString());
							break;
						case 3:
							language = reader.ReadString();
							break;
						case 4:
							parameters = formatterResolver.GetFormatter<Dictionary<string, object>>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
			}

			if (script == null)
				return null;

			script.Lang = language;
			script.Params = parameters;
			return script;
		}

		public void Serialize(ref JsonWriter writer, IScript value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value)
			{
				case IInlineScript inlineScript:
				{
					var formatter = formatterResolver.GetFormatter<IInlineScript>();
					formatter.Serialize(ref writer, inlineScript, formatterResolver);
					break;
				}
				case IIndexedScript indexedScript:
				{
					var formatter = formatterResolver.GetFormatter<IIndexedScript>();
					formatter.Serialize(ref writer, indexedScript, formatterResolver);
					break;
				}
				default:
				{
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IScript>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
				}
			}
		}
	}
}
