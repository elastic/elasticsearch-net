using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class ReadAsTypeJsonConverter<T> : JsonConverter
		where T : class, new()
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var t = new T();
			serializer.Populate(reader, t);
			return t;
		}
	}
	
}
