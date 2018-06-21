using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ScriptQueryConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IScriptQuery;
			if (v == null) return;

			writer.WriteStartObject();
			if (!v.Name.IsNullOrEmpty()) writer.WriteProperty(serializer, "_name", v.Name);
			if (v.Boost != null) writer.WriteProperty(serializer, "boost", v.Boost);
			writer.WritePropertyName("script");
			writer.WriteStartObject();
			{
				if (v.Id != null) writer.WriteProperty(serializer, "id", v.Id);
				if (v.Source != null) writer.WriteProperty(serializer, "source", v.Source);
				if (v.Lang != null) writer.WriteProperty(serializer, "lang", v.Lang);
				if (v.Params != null) writer.WriteProperty(serializer, "params", v.Params);
			}
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new ScriptQuery();
			var o = JObject.Load(reader);
			var properties = o.Properties().ToListOrNullIfEmpty();
			var scriptProperty = properties.FirstOrDefault(p => p.Name == "script");
			if (scriptProperty != null)
				properties.AddRange(scriptProperty.Value.Value<JObject>().Properties());

			foreach (var p in properties)
			{
				switch (p.Name)
				{
					case "_name":
						r.Name = p.Value.Value<string>();
						break;
					case "boost":
						r.Boost = p.Value.Value<double>();
						break;
					case "id":
						r.Id = p.Value.Value<string>();
						break;
					case "inline":
					case "source":
						r.Source = p.Value.Value<string>();
						break;
					case "lang":
						r.Lang = p.Value.Value<string>();
						break;
					case "params":
						r.Params = p.Value.ToObject<Dictionary<string, object>>();
						break;
				}
			}
			return r;
		}
	}
}
