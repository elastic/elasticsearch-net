using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	internal class VerbatimDictionaryKeysBaseFormatter<TDictionary, TKey, TValue> : IJsonFormatter<TDictionary>
		where TDictionary : IEnumerable<KeyValuePair<TKey, TValue>>
	{
		private readonly bool _keyIsField = typeof(TKey) == typeof(Field);
		private readonly bool _keyIsIndexName = typeof(TKey) == typeof(IndexName);
		private readonly bool _keyIsPropertyName = typeof(TKey) == typeof(PropertyName);
		private readonly bool _keyIsRelationName = typeof(TKey) == typeof(RelationName);
		private readonly bool _keyIsString = typeof(TKey) == typeof(string);
		private readonly bool _keyIsTypeName = typeof(TKey) == typeof(TypeName);

		public virtual TDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<TDictionary>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, TDictionary value, IJsonFormatterResolver formatterResolver)
		{
			var enumerable = (IEnumerable<KeyValuePair<TKey, TValue>>)value;
			if (enumerable == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var seenEntries = new Dictionary<string, TValue>(value.Count());

			foreach (var entry in enumerable)
			{
				if (SkipValue(entry)) // TODO: pass _connectionSettingsValues and configure Null Handling on this
					continue;

				string key;
				if (_keyIsString)
					key = entry.Key?.ToString();
				else if (settings == null)
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
				else if (_keyIsField)
				{
					var fieldName = entry.Key as Field;
					key = settings.Inferrer.Field(fieldName);
				}
				else if (_keyIsPropertyName)
				{
					var propertyName = entry.Key as PropertyName;
					if (propertyName?.Property != null && settings.PropertyMappings.TryGetValue(propertyName.Property, out var mapping)
						&& mapping.Ignore)
						continue;

					key = settings.Inferrer.PropertyName(propertyName);
				}
				else if (_keyIsIndexName)
				{
					var indexName = entry.Key as IndexName;
					key = settings.Inferrer.IndexName(indexName);
				}
				else if (_keyIsTypeName)
				{
					var typeName = entry.Key as TypeName;
					key = settings.Inferrer.TypeName(typeName);
				}
				else if (_keyIsRelationName)
				{
					var typeName = entry.Key as RelationName;
					key = settings.Inferrer.RelationName(typeName);
				}
				else
					key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);

				if (key != null)
					seenEntries[key] = entry.Value;
			}

			var formatter = formatterResolver.GetFormatter<Dictionary<string, TValue>>();
			formatter.Serialize(ref writer, seenEntries, formatterResolver);
		}

		protected virtual bool SkipValue(KeyValuePair<TKey, TValue> entry) =>
			entry.Value == null; //TODO: Check connection settings for allow nulls
	}

	internal class VerbatimInterfaceReadOnlyDictionaryKeysFormatter<TKey, TValue>
		: VerbatimDictionaryKeysBaseFormatter<IReadOnlyDictionary<TKey, TValue>, TKey, TValue>
	{
		public override IReadOnlyDictionary<TKey, TValue> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			// needed as Utf8Json does not handle the ctor for a IReadOnlyDictionary<TKey, TValue>
			var formatter = formatterResolver.GetFormatter<Dictionary<TKey, TValue>>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}

	internal class VerbatimDictionaryKeysFormatter<TKey, TValue>
		: VerbatimDictionaryKeysBaseFormatter<Dictionary<TKey, TValue>, TKey, TValue>
	{
	}

	internal class VerbatimDictionaryInterfaceKeysFormatter<TKey, TValue>
		: VerbatimDictionaryKeysBaseFormatter<IDictionary<TKey, TValue>, TKey, TValue>
	{
	}
}
