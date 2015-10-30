using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class FilterAggregationJsonConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IFilterAggregation;
			if (f == null || f.Filter == null)
			{
				writer.WriteNull();
				return;
			};

			serializer.Serialize(writer, f.Filter);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}

}
	