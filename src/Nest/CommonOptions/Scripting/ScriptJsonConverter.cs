using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Internal;

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
			var formatter = formatterResolver;
		}
	}
}
