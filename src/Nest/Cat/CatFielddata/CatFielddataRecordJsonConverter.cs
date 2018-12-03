using System;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class CatFielddataRecordFormatter : IJsonFormatter<CatFielddataRecord>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "id", 0 },
			{ "node", 1 },
			{ "n", 1 },
			{ "host", 2 },
			{ "ip", 3 },
			{ "field", 4 },
			{ "size", 5 }
		};

		public CatFielddataRecord Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var record = new CatFielddataRecord();
			var count = 0;

			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (AutomataDictionary.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							record.Id = reader.ReadString();
							break;
						case 1:
							record.Node = reader.ReadString();
							break;
						case 2:
							record.Host = reader.ReadString();
							break;
						case 3:
							record.Ip = reader.ReadString();
							break;
						case 4:
							record.Field = reader.ReadString();
							break;
						case 5:
							record.Size = reader.ReadString();
							break;
					}
				}
			}

			return record;
		}

		public void Serialize(ref JsonWriter writer, CatFielddataRecord value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
