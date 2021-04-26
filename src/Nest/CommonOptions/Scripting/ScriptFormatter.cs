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
using Nest.Utf8Json;
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
			{
				reader.ReadNextBlock();
				return null;
			}

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
