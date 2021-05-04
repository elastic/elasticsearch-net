// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class ChildrenFormatter : IJsonFormatter<Children>
	{
		public Children Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var children = new Children();
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
				{
					var type = reader.ReadString();
					children.Add(type);
					return children;
				}
				case JsonToken.BeginArray:
				{
					var types = new List<RelationName>();
					var count = 0;
					while (reader.ReadIsInArray(ref count))
					{
						var type = reader.ReadString();
						types.Add(type);
					}

					children.AddRange(types);
					return children;
				}
				default:
					reader.ReadNextBlock();
					return children;
			}
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
