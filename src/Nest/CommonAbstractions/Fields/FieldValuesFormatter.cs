using System;
using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	internal class FieldValuesFormatter : IJsonFormatter<FieldValues>
	{
		private readonly IConnectionSettingsValues _settings;

		public FieldValuesFormatter(IConnectionSettingsValues settings) => _settings = settings;

		public FieldValues Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			var fields = new Dictionary<string, LazyDocument>();
			var lazyDocumentFormatter = formatterResolver.GetFormatter<LazyDocument>();

			while (reader.ReadIsInObject(ref count))
			{
				var savedReader = reader;
				var propertyName = reader.ReadPropertyName();
				var lazyDocument = lazyDocumentFormatter.Deserialize(ref savedReader, formatterResolver);
				fields[propertyName] = lazyDocument;
			}

			return new FieldValues(_settings.Inferrer, fields);
		}

		public void Serialize(ref JsonWriter writer, FieldValues value, IJsonFormatterResolver formatterResolver) =>
			throw new NotImplementedException();
	}
}
