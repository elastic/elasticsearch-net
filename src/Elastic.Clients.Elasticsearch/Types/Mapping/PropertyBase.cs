// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Mapping;

[JsonConverter(typeof(PropertyInterfaceConverter))]
public partial interface IProperty
{
}

// FUTURE GENERATED
internal sealed partial class PropertyInterfaceConverter : JsonConverter<IProperty>
{
	public override IProperty? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var copiedReader = reader;

		string? type = null;

		using var jsonDoc = JsonDocument.ParseValue(ref copiedReader);

		if (jsonDoc is not null)
		{
			var root = jsonDoc.RootElement;

			if (root.TryGetProperty("type", out var readType) && readType.ValueKind == JsonValueKind.String)
			{
				type = readType.ToString();
			}
		}

		// object is the default when no type is specified in the property object
		if (type is null)
			return JsonSerializer.Deserialize<ObjectProperty>(ref reader, options);

		return DeserializeVariant(type, ref reader, options);
	}

	public override void Write(Utf8JsonWriter writer, IProperty value, JsonSerializerOptions options) => throw new NotImplementedException();

	//private static PropertyBase DeserializeVariant(string type, ref Utf8JsonReader reader, JsonSerializerOptions options)
	//{
	//	switch (type)
	//	{
	//		case "text":
	//			return JsonSerializer.Deserialize<TextProperty>(ref reader, options);

	//		case "ip":
	//			return JsonSerializer.Deserialize<IpProperty>(ref reader, options);

	//		default:
	//			throw new JsonException("Encounted an unknown property type which could not be deserialised.");
	//	}
	//}
}
