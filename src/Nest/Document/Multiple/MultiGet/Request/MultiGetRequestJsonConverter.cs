using System;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	internal class MultiGetRequestFormatter : IJsonFormatter<IMultiGetRequest>
	{
		private static readonly IdFormatter IdFormatter = new IdFormatter();

		public IMultiGetRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IMultiGetRequest value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();
			if (!(value?.Documents.HasAny()).GetValueOrDefault(false))
			{
				writer.WriteEndObject();
				return;
			}

			var docs = value.Documents.Select(d =>
				{
					if (value.Index != null) d.Index = null;
					return d;
				})
				.ToList();

			var flatten = docs.All(p => p.CanBeFlattened);

			writer.WritePropertyName(flatten ? "ids" : "docs");

			IJsonFormatter<IMultiGetOperation> formatter = null;
			if (!flatten)
				formatter = formatterResolver.GetFormatter<IMultiGetOperation>();

			writer.WriteBeginArray();
			for (var index = 0; index < docs.Count; index++)
			{
				if (index > 0)
					writer.WriteValueSeparator();

				var id = docs[index];
				if (flatten)
					IdFormatter.Serialize(ref writer, id.Id, formatterResolver);
				else
					formatter.Serialize(ref writer, id, formatterResolver);
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
