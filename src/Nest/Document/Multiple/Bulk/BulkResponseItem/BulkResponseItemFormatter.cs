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
			IBulkResponseItem bulkResponseItem = null;

			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return bulkResponseItem;
			}

			reader.ReadIsBeginObjectWithVerify();
			var operation = reader.ReadPropertyNameSegmentRaw();
			if (Operations.TryGetValue(operation, out var value))
			{
				switch (value)
				{
					case 0:
						bulkResponseItem = formatterResolver.GetFormatter<BulkDeleteResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						bulkResponseItem = formatterResolver.GetFormatter<BulkUpdateResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 2:
						bulkResponseItem = formatterResolver.GetFormatter<BulkIndexResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 3:
						bulkResponseItem = formatterResolver.GetFormatter<BulkCreateResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}
			}
			else
			{
				reader.ReadNextBlock();
			}

			reader.ReadIsEndObjectWithVerify();
			return bulkResponseItem;
		}

		public void Serialize(ref JsonWriter writer, IBulkResponseItem value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
