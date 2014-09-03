using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers.Converters
{
	public class TemplateSectionConverter : JsonConverter
	{
		public override bool CanWrite { get { return true; } }

		public override bool CanRead { get { return false; } }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as ITemplateSection;
			if (v == null) writer.WriteNull();

			writer.WriteRaw("{{#" + v.Variable + "}}");
			serializer.Serialize(writer, v.Instance);
			writer.WriteRaw("{{/" + v.Variable + "}}");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
