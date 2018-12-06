using System;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class BulkResponseItemFormatter : IJsonFormatter<IBulkResponseItem>
	{
		private static readonly AutomataDictionary Operations = new AutomataDictionary
		{
			{ "delete", 0 },
			{ "update", 1 },
			{ "index", 2 },
			{ "create", 3 }
		};

		public IBulkResponseItem Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			// read to first property
			reader.ReadNext();

			var operation = reader.ReadPropertyNameSegmentRaw();
			if (Operations.TryGetValue(operation, out var value))
			{
				switch (value)
				{
					case 0:
						return formatterResolver.GetFormatter<BulkDeleteResponseItem>()
							.Deserialize(ref reader, formatterResolver);
					case 1:
						return formatterResolver.GetFormatter<BulkUpdateResponseItem>()
							.Deserialize(ref reader, formatterResolver);
					case 2:
						return formatterResolver.GetFormatter<BulkIndexResponseItem>()
							.Deserialize(ref reader, formatterResolver);
					case 3:
						return formatterResolver.GetFormatter<BulkCreateResponseItem>()
							.Deserialize(ref reader, formatterResolver);
				}
			}

			reader.ReadNextBlock();
			return null;
		}

		public void Serialize(ref JsonWriter writer, IBulkResponseItem value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
