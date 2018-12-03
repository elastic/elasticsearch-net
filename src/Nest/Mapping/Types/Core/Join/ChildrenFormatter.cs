using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	internal class ChildrenFormatter : IJsonFormatter<Children>
	{
		public Children Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var children = new Children();
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var type = reader.ReadString();
				children.Add(type);
				return children;
			}

			if (token != JsonToken.BeginArray)
				return null;

			var types = new List<RelationName>();
			var count = 0;
			while (reader.ReadIsInArray(ref count))
			{
				var type = reader.ReadString();
				if (reader.GetCurrentJsonToken() == JsonToken.String)
					types.Add(type);
			}

			children.AddRange(types);
			return children;
		}

		public void Serialize(ref JsonWriter writer, Children value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null || value.Count == 0)
			{
				writer.WriteNull();
				return;
			}
			var settings = formatterResolver.GetConnectionSettings();
			var resolved = value.Cast<IUrlParameter>().ToList();
			if (resolved.Count == 1)
			{
				writer.WriteString(resolved[0].GetString(settings));
				return;
			}
			writer.WriteBeginArray();
			for (var index = 0; index < resolved.Count; index++)
			{
				if (index > 0)
					writer.WriteValueSeparator();
				var r = resolved[index];
				writer.WriteString(r.GetString(settings));
			}
			writer.WriteEndArray();
		}
	}
}
