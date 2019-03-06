using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ScriptJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => typeof(IScript).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var dict = o.Properties().ToDictionary(p => p.Name, p => p.Value);
			if (!dict.HasAny()) return null;

			IScript script = null;
			if (dict.TryGetValue("inline", out JToken inlineToken))
			{
				var inline = inlineToken.ToString();
				script = new InlineScript(inline);
			}
			else if (dict.TryGetValue("file", out JToken fileToken))
			{
				var file = fileToken.ToString();
				script = new FileScript(file);
			}
			else if (dict.TryGetValue("id", out JToken idToken))
			{
				var id = idToken.ToString();
				script = new IndexedScript(id);
			}

			if (script == null) return null;

			if (dict.TryGetValue("lang", out JToken langToken))
				script.Lang = langToken.ToString();
			if (dict.TryGetValue("params", out JToken paramsToken))
				script.Params = paramsToken.ToObject<Dictionary<string, object>>();

			return script;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
