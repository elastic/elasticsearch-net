// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	internal class DynamicTemplatesInterfaceFormatter : IJsonFormatter<IDynamicTemplateContainer>
	{
		public IDynamicTemplateContainer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<DynamicTemplateContainer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, IDynamicTemplateContainer value, IJsonFormatterResolver formatterResolver)
		{
			if (!value.HasAny())
				return;

			writer.WriteBeginArray();
			var formatter = formatterResolver.GetFormatter<IDynamicTemplate>();
			var count = 0;
			foreach (var p in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WriteBeginObject();
				writer.WritePropertyName(p.Key);
				formatter.Serialize(ref writer, p.Value, formatterResolver);
				writer.WriteEndObject();

				count++;
			}
			writer.WriteEndArray();
		}
	}

	internal class DynamicTemplatesFormatter : IJsonFormatter<DynamicTemplateContainer>
	{
		public DynamicTemplateContainer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dynamicTemplateContainer = new DynamicTemplateContainer();
			var count = 0;
			var formatter = formatterResolver.GetFormatter<DynamicTemplate>();

			while (reader.ReadIsInArray(ref count))
			{
				var objectCount = 0;
				string name = null;
				IDynamicTemplate template = null;
				while (reader.ReadIsInObject(ref objectCount))
				{
					name = reader.ReadPropertyName();
					template = formatter.Deserialize(ref reader, formatterResolver);
				}

				dynamicTemplateContainer.Add(name, template);
			}

			return dynamicTemplateContainer;
		}

		public void Serialize(ref JsonWriter writer, DynamicTemplateContainer value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<IDynamicTemplateContainer>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
