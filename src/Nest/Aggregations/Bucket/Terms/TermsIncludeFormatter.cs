// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	internal class TermsIncludeFormatter : IJsonFormatter<TermsInclude>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "partition", 0 },
			{ "num_partitions", 1 }
		};

		public TermsInclude Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			TermsInclude termsInclude;
			switch (token)
			{
				case JsonToken.BeginArray:
					var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
					termsInclude = new TermsInclude(formatter.Deserialize(ref reader, formatterResolver));
					break;
				case JsonToken.BeginObject:
					long partition = 0;
					long numberOfPartitions = 0;
					var count = 0;
					while (reader.ReadIsInObject(ref count))
					{
						var propertyName = reader.ReadPropertyNameSegmentRaw();
						if (AutomataDictionary.TryGetValue(propertyName, out var value))
						{
							switch (value)
							{
								case 0:
									partition = reader.ReadInt64();
									break;
								case 1:
									numberOfPartitions = reader.ReadInt64();
									break;
							}
						}
					}

					termsInclude = new TermsInclude(partition, numberOfPartitions);
					break;
				case JsonToken.String:
					termsInclude = new TermsInclude(reader.ReadString());
					break;
				default:
					throw new Exception($"Unexpected token {token} when deserializing {nameof(TermsInclude)}");
			}

			return termsInclude;
		}

		public void Serialize(ref JsonWriter writer, TermsInclude value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else if (value.Values != null)
			{
				var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
				formatter.Serialize(ref writer, value.Values, formatterResolver);
			}
			else if (value.Partition.HasValue && value.NumberOfPartitions.HasValue)
			{
				writer.WriteBeginObject();
				writer.WritePropertyName("partition");
				writer.WriteInt64(value.Partition.Value);
				writer.WriteValueSeparator();
				writer.WritePropertyName("num_partitions");
				writer.WriteInt64(value.NumberOfPartitions.Value);
				writer.WriteEndObject();
			}
			else
				writer.WriteString(value.Pattern);
		}
	}
}
