using System.Collections.Generic;
using Elasticsearch.Net;



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
