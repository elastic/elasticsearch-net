using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	internal class QueryContainerJsonConverter: ReserializeJsonConverter<QueryContainer, IQueryContainer>
	{

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject) return base.ReadJson(reader, objectType, existingValue, serializer);
			if (reader.TokenType != JsonToken.String) return null;

			//Some API's return the stored queries as escaped string, e.g the get shield role API
			var escapedJson = reader.Value as string;
			if (string.IsNullOrWhiteSpace(escapedJson)) return null;
			using (var sr = new StringReader(escapedJson))
			using (var escapedReader = new JsonTextReader(sr))
			{
				escapedReader.Read();
				return base.ReadJson(escapedReader, objectType, existingValue, serializer);
			}
		}


		protected override void SerializeJson(JsonWriter writer, object value, IQueryContainer castValue, JsonSerializer serializer)
		{
			var rawQuery = castValue.RawQuery;
			if (!rawQuery?.Raw.IsNullOrEmpty() ?? false && rawQuery.IsWritable)
			{
				writer.WriteRawValue(rawQuery.Raw);
				return;
			}

			base.SerializeJson(writer, value, castValue, serializer);
		}
	}

	internal class QueryContainerCollectionJsonConverter : JsonConverter
	{
		public override bool CanWrite => true;
		public override bool CanRead => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var collection = (IEnumerable<QueryContainer>) value;

			if (collection == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteStartArray();
			foreach (var queryContainer in collection)
			{
				if (queryContainer != null && queryContainer.IsWritable)
					serializer.Serialize(writer, queryContainer);
			}
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
