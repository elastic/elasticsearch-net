using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class QueryContainerJsonConverter: ReserializeJsonConverter<QueryContainer, IQueryContainer>
	{
		protected override void SerializeJson(JsonWriter writer, object value, IQueryContainer castValue, JsonSerializer serializer)
		{
			var rawQuery = castValue.RawQuery;
			if (!rawQuery?.Raw.IsNullOrEmpty() ?? false)
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
				if (queryContainer != null && (queryContainer.IsStrict || !queryContainer.IsConditionless))
					serializer.Serialize(writer, queryContainer);
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType) => true;
	}
}