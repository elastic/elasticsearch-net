// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	[JsonConverter(typeof(FieldTypeConverter))]
	public enum FieldType
	{
		Date,
		Text,
		Long
	}

    internal class FieldTypeConverter : JsonConverter<FieldType>
	{
		public override FieldType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "date":
					return FieldType.Date;
				case "long":
					return FieldType.Long;
				case "text":
					return FieldType.Text;
			}

			ThrowHelper.ThrowJsonException("Unexpected field type value.");
			return default;
		}

		public override void Write(Utf8JsonWriter writer, FieldType value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case FieldType.Date:
					writer.WriteStringValue("date");
					return;
				case FieldType.Long:
					writer.WriteStringValue("long");
					return;
				case FieldType.Text:
					writer.WriteStringValue("text");
					return;
			}

			writer.WriteNullValue();
		}
	}
}
