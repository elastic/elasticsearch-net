using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class ScriptJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(IScript).IsAssignableFrom(objectType);
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var dict = o.Properties().ToDictionary(p => p.Name, p => p.Value);
			if (!dict.HasAny()) return null;

			IScript script = null;
			if (dict.ContainsKey("inline"))
			{
				var inline = dict["inline"].ToString();
				script = new InlineScript(inline);
			}
			if (dict.ContainsKey("file"))
			{
				var file = dict["file"].ToString();
				script = new FileScript(file);
			}
			if (dict.ContainsKey("id"))
			{
				var id = dict["id"].ToString();
				script = new IndexedScript(id);
			}

			if (script == null) return null;

			if (dict.ContainsKey("lang"))
				script.Lang = dict["lang"].ToString();
			if (dict.ContainsKey("params"))
				script.Params = dict["params"].ToObject<Dictionary<string, object>>();

			return script;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
