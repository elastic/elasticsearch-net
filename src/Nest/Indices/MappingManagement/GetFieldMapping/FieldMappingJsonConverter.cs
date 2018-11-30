using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;
using Utf8Json;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class FieldMappingFormatter : IJsonFormatter<IReadOnlyDictionary<Field, IFieldMapping>>
	{
		public void Serialize(ref JsonWriter writer, IReadOnlyDictionary<Field, IFieldMapping> value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IReadOnlyDictionary<Field, IFieldMapping>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private static readonly AutomataDictionary _automataDictionary = new AutomataDictionary
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
				if (_automataDictionary.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							mapping = formatterResolver.GetFormatter<AllField>()
								.Deserialize(ref reader, formatterResolver);
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
							mapping = formatterResolver.GetFormatter<IndexField>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 4:
							mapping = formatterResolver.GetFormatter<SizeField>()
								.Deserialize(ref reader, formatterResolver);
							break;
						//TODO _field_names does not seem to have a special mapping (just returns like _uid) needs CONFIRMATION
					}
				}
				else
				{
					var property = formatterResolver.GetFormatter<IProperty>()
						.Deserialize(ref reader, formatterResolver);

					if (property != null)
						property.Name =
							Encoding.UTF8.GetString(propertyName.Array, propertyName.Offset, propertyName.Count);

					mapping = property;
				}

				if (mapping != null)
				{
					var name = Encoding.UTF8.GetString(propertyName.Array, propertyName.Offset, propertyName.Count);
					fieldMappings.Add(name, mapping);
				}
			}

			var settings = formatterResolver.GetConnectionSettings();
			var resolvableDictionary = new ResolvableDictionaryProxy<Field, IFieldMapping>(settings, fieldMappings);
			return resolvableDictionary;
		}
	}
}
