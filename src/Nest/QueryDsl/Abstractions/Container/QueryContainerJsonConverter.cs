using System;
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
}