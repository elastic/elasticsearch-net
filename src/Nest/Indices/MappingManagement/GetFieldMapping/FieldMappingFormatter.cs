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
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Nest
{
	internal class FieldMappingFormatter : IJsonFormatter<IReadOnlyDictionary<Field, IFieldMapping>>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "_all", 0 },
			{ "_source", 1 },
			{ "_routing", 2 },
			{ "_index", 3 },
			{ "_size", 4 }
		};

		public IReadOnlyDictionary<Field, IFieldMapping> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var fieldMappings = new Dictionary<Field, IFieldMapping>();

			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNext();
				return fieldMappings;
			}

			var count = 0;
			IFieldMapping mapping = null;

			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
#pragma warning disable 618
							mapping = formatterResolver.GetFormatter<AllField>()
								.Deserialize(ref reader, formatterResolver);
#pragma warning restore 618
							break;
						case 1:
							mapping = formatterResolver.GetFormatter<SourceField>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 2:
							mapping = formatterResolver.GetFormatter<RoutingField>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 3:
#pragma warning disable 618
							mapping = formatterResolver.GetFormatter<IndexField>()
								.Deserialize(ref reader, formatterResolver);
#pragma warning restore 618
							break;
						case 4:
							mapping = formatterResolver.GetFormatter<SizeField>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
				{
					var property = formatterResolver.GetFormatter<IProperty>()
						.Deserialize(ref reader, formatterResolver);

					if (property != null)
						property.Name =
							propertyName.Utf8String();

					mapping = property;
				}

				if (mapping != null)
				{
					var name = propertyName.Utf8String();
					fieldMappings.Add(name, mapping);
				}
			}

			var settings = formatterResolver.GetConnectionSettings();
			var resolvableDictionary = new ResolvableDictionaryProxy<Field, IFieldMapping>(settings, fieldMappings);
			return resolvableDictionary;
		}

		public void Serialize(ref JsonWriter writer, IReadOnlyDictionary<Field, IFieldMapping> value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IReadOnlyDictionary<Field, IFieldMapping>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
