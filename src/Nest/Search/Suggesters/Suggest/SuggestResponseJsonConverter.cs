using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class SuggestResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			var shards = jsonObject.Property("_shards").Value.ToObject<ShardsMetaData>();
			jsonObject.Remove("_shards");

			var genericType = objectType.GetTypeInfo().GenericTypeArguments.First();
			var o = serializer.Deserialize(jsonObject.CreateReader(), genericType);
			var suggestType = typeof(Suggest<>).MakeGenericType(genericType).MakeArrayType();

			var dict = new Dictionary<string, object>();

			foreach (var prop in jsonObject.Properties())
			{
				var value = prop.Value.ToObject(suggestType, serializer);
				dict.Add(prop.Name, value);
			}
			var r = typeof(SuggestResponse<>).CreateGenericInstance(genericType, shards, dict);
			return r;
		}
	}
}
