using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class FieldValuesJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var fields = o.Properties().ToDictionary(p => p.Name, p => p.Value.ToObject<object>());
			var inferrer = serializer.GetConnectionSettings().Inferrer;
			var fieldValues = new FieldValues(inferrer, fields);
			return fieldValues;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
