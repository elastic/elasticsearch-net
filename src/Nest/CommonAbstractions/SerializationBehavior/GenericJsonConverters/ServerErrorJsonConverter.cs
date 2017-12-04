using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ServerErrorJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType) => objectType == typeof(Error);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			//TODO MAKE DEDICATED SERVER ERROR PARSER FOR HIGH LEVEL CLIENT
			var token = JToken.Load(reader);
			token.TryParseServerError(serializer, out var error);
			return error?.Error;
		}
	}
}
