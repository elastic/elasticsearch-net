// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(IncludeExcludeFormatter))]
	public class IncludeExclude
	{
		public IncludeExclude(string pattern) => Pattern = pattern;

		public IncludeExclude(IEnumerable<string> values) => Values = values;

		[IgnoreDataMember]
		public string Pattern { get; set; }

		[IgnoreDataMember]
		public IEnumerable<string> Values { get; set; }
	}

	internal class IncludeExcludeFormatter : IJsonFormatter<IncludeExclude>
	{
		public void Serialize(ref JsonWriter writer, IncludeExclude value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else if (value.Values != null)
			{
				var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
				formatter.Serialize(ref writer, value.Values, formatterResolver);
			}
			else
				writer.WriteString(value.Pattern);
		}

		public IncludeExclude Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			IncludeExclude termsInclude;

			switch (token)
			{
				case JsonToken.BeginArray:
					var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
					termsInclude = new IncludeExclude(formatter.Deserialize(ref reader, formatterResolver));
					break;
				case JsonToken.String:
					termsInclude = new IncludeExclude(reader.ReadString());
					break;
				default:
					throw new Exception($"Unexpected token {token} when deserializing {nameof(IncludeExclude)}");
			}

			return termsInclude;
		}
	}
}
