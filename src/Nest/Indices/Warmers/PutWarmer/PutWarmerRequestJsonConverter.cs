using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class PutWarmerRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var searchRequest = serializer.Deserialize<SearchRequest>(reader);
			return new PutWarmerRequest("unknown name because its not on the body when deserializing") { Search =  searchRequest };
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IPutWarmerRequest;
			if (v != null) serializer.Serialize(writer, v.Search);
		}
	}
}