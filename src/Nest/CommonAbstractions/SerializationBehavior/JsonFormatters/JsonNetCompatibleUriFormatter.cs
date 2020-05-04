// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class JsonNetCompatibleUriFormatter : IJsonFormatter<Uri>
	{
		public void Serialize(ref JsonWriter writer, Uri value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			// JSON.NET uses .OriginalString and not the canonical form returned by .ToString()
			// See https://github.com/JamesNK/Newtonsoft.Json/blob/0ce23ff92459619fde10a5cec0a336ab00a08b4c/Src/Newtonsoft.Json/JsonTextWriter.cs#L769
			writer.WriteString(value.OriginalString);
		}

		public Uri Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new Uri(reader.ReadString(), UriKind.RelativeOrAbsolute);
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				default:
					throw new Exception($"Cannot deserialize {typeof(Uri).FullName} from {token}");
			}
		}
	}
}
