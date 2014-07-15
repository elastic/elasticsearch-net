using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class GeoShapeQueryConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return false; } }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
