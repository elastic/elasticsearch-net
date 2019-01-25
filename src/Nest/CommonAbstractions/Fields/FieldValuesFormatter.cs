using System;
using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	internal class FieldValuesFormatter : IJsonFormatter<FieldValues>
	{
		private static readonly LazyDocumentFormatter LazyDocumentFormatter = new LazyDocumentFormatter();
		
		public FieldValues Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			var fields = new Dictionary<string, LazyDocument>();
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				var lazyDocument = LazyDocumentFormatter.Deserialize(ref reader, formatterResolver);
				fields[propertyName] = lazyDocument;
			}

			return new FieldValues(formatterResolver.GetConnectionSettings().Inferrer, fields);
		}

		public void Serialize(ref JsonWriter writer, FieldValues value, IJsonFormatterResolver formatterResolver) =>
			throw new NotImplementedException();
	}
}
