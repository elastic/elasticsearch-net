// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public interface IContainer
{
}


//public class QueryContainerConverter : JsonConverter<QueryContainer>
//{
//	public override QueryContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//	{
//		reader.Read();

//		if (reader.TokenType != JsonTokenType.PropertyName)
//		{
//			throw new JsonException();
//		}

//		var propertyName = reader.GetString();

//		if (propertyName == "bool")
//		{
//			var boolQuery = JsonSerializer.Deserialize<BoolQuery>(ref reader, options);
//			reader.Read();
//			return new QueryContainer(boolQuery);
//		}

//		return null;
//	}

//	public override void Write(Utf8JsonWriter writer, QueryContainer value, JsonSerializerOptions options)
//	{
//		if (value is null || value.Variant is null)
//		{
//			writer.WriteNullValue();
//			return;
//		}

//		writer.WriteStartObject();

//		writer.WritePropertyName(value.Variant.QueryContainerVariantName);

//		switch (value.Variant)
//		{
//			case BoolQuery boolQuery:
//				JsonSerializer.Serialize(writer, boolQuery, options);
//				break;
//			case BoostingQuery boostingQuery:
//				JsonSerializer.Serialize(writer, boostingQuery, options);
//				break;
//		}

//		writer.WriteEndObject();
//	}
//}
