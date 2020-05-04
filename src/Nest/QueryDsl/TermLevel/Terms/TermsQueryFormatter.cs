// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	internal class TermsQueryFormatter : IJsonFormatter<ITermsQuery>
	{
		private static readonly AutomataDictionary FieldLookups = new AutomataDictionary
		{
			{ "id", 0 },
			{ "index", 1 },
			{ "path", 2 },
			{ "routing", 3 }
		};

		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "boost", 0 },
			{ "_name", 1 }
		};

		private static readonly SourceWriteFormatter<object> SourceWriteFormatter =
			new SourceWriteFormatter<object>();

		public ITermsQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			ITermsQuery query = new TermsQuery();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							query.Boost = reader.ReadDouble();
							break;
						case 1:
							query.Name = reader.ReadString();
							break;
					}
				}
				else
				{
					query.Field = property.Utf8String();
					ReadTerms(ref reader, query, formatterResolver);
				}
			}

			return query;
		}

		public void Serialize(ref JsonWriter writer, ITermsQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var field = settings.Inferrer.Field(value.Field);
			var written = false;
			writer.WriteBeginObject();

			if (!value.Name.IsNullOrEmpty())
			{
				writer.WritePropertyName("_name");
				writer.WriteString(value.Name);
				written = true;
			}

			if (value.Boost.HasValue)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("boost");
				writer.WriteDouble(value.Boost.Value);
				written = true;
			}

			if (written)
				writer.WriteValueSeparator();

			if (value.IsVerbatim)
			{
				if (value.TermsLookup != null)
				{
					writer.WritePropertyName(field);
					var formatter = formatterResolver.GetFormatter<IFieldLookup>();
					formatter.Serialize(ref writer, value.TermsLookup, formatterResolver);
				}
				else if (value.Terms != null)
				{
					writer.WritePropertyName(field);
					writer.WriteBeginArray();
					var count = 0;
					foreach (var o in value.Terms)
					{
						if (count > 0)
							writer.WriteValueSeparator();

						SourceWriteFormatter.Serialize(ref writer, o, formatterResolver);
						count++;
					}
					writer.WriteEndArray();
				}
			}
			else
			{
				if (value.Terms.HasAny())
				{
					writer.WritePropertyName(field);
					writer.WriteBeginArray();
					var count = 0;
					foreach (var o in value.Terms)
					{
						if (count > 0)
							writer.WriteValueSeparator();

						SourceWriteFormatter.Serialize(ref writer, o, formatterResolver);
						count++;
					}
					writer.WriteEndArray();
				}
				else if (value.TermsLookup != null)
				{
					writer.WritePropertyName(field);
					var formatter = formatterResolver.GetFormatter<IFieldLookup>();
					formatter.Serialize(ref writer, value.TermsLookup, formatterResolver);
				}
			}

			writer.WriteEndObject();
		}

		private void ReadTerms(ref JsonReader reader, ITermsQuery termsQuery, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.BeginObject)
			{
				var fieldLookup = new FieldLookup();

				var count = 0;
				while (reader.ReadIsInObject(ref count))
				{
					var property = reader.ReadPropertyNameSegmentRaw();
					if (FieldLookups.TryGetValue(property, out var value))
					{
						switch (value)
						{
							case 0:
								fieldLookup.Id = formatterResolver.GetFormatter<Id>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 1:
								fieldLookup.Index = formatterResolver.GetFormatter<IndexName>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 2:
								fieldLookup.Path = formatterResolver.GetFormatter<Field>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 3:
								fieldLookup.Routing = formatterResolver.GetFormatter<Routing>()
									.Deserialize(ref reader, formatterResolver);
								break;
						}
					}
				}

				termsQuery.TermsLookup = fieldLookup;
			}
			else if (token == JsonToken.BeginArray)
			{
				var values = formatterResolver.GetFormatter<IEnumerable<object>>()
					.Deserialize(ref reader, formatterResolver);
				termsQuery.Terms = values;
			}
		}
	}
}
