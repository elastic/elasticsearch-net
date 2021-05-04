// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class JoinFieldFormatter : IJsonFormatter<JoinField>
	{
		public JoinField Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var parent = reader.ReadString();
				return new JoinField(new JoinField.Parent(parent));
			}

			var count = 0;
			Id parentId = null;
			string name = null;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case "parent":
						parentId = formatterResolver.GetFormatter<Id>().Deserialize(ref reader, formatterResolver);
						break;
					case "name":
						name = reader.ReadString();
						break;
				}
			}

			return parentId != null
				? new JoinField(new JoinField.Child(name, parentId))
				: new JoinField(new JoinField.Parent(name));
		}

		public void Serialize(ref JsonWriter writer, JoinField value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			// ref cannot be used inside lambda expression body
			switch (value.Tag)
			{
				case 0:
				{
					var relationNameFormatter = formatterResolver.GetFormatter<RelationName>();
					relationNameFormatter.Serialize(ref writer, value.ParentOption.Name, formatterResolver);
					break;
				}
				case 1:
				{
					var child = value.ChildOption;
					writer.WriteBeginObject();
					writer.WritePropertyName("name");
					var relationNameFormatter = formatterResolver.GetFormatter<RelationName>();
					relationNameFormatter.Serialize(ref writer, child.Name, formatterResolver);
					writer.WriteValueSeparator();
					writer.WritePropertyName("parent");
					var id = (child.ParentId as IUrlParameter)?.GetString(formatterResolver.GetConnectionSettings());
					writer.WriteString(id);
					writer.WriteEndObject();
					break;
				}
			}
		}
	}
}
