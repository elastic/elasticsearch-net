// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch;

internal sealed class UnionConverter : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) => typeToConvert.Name == typeof(Union<,>).Name || (typeToConvert.BaseType is not null && typeToConvert.BaseType.Name == typeof(Union<,>).Name);

	public override JsonConverter CreateConverter(
		Type type,
		JsonSerializerOptions options)
	{
		if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Buckets<>))
		{
			// TODO - Could potentially cache an instance for each bucket type and reuse it
			var bucketType = type.GetGenericArguments()[0];
			return (JsonConverter)Activator.CreateInstance(typeof(BucketsConverter<>).MakeGenericType(bucketType));
		}

		// Fallback to generalised converter

		Type itemOneType, itemTwoType;

		if (type.Name == typeof(Union<,>).Name)
		{
			itemOneType = type.GetGenericArguments()[0];
			itemTwoType = type.GetGenericArguments()[1];
		}
		else
		{
			itemOneType = type.BaseType.GetGenericArguments()[0];
			itemTwoType = type.BaseType.GetGenericArguments()[1];
		}

		var converter = (JsonConverter)Activator.CreateInstance(typeof(UnionConverterInner<,>).MakeGenericType(itemOneType, itemTwoType));

		return converter;
	}

	private class UnionConverterInner<TItem1, TItem2> : JsonConverter<Union<TItem1, TItem2>>
	{
		public override Union<TItem1, TItem2>? Read(ref Utf8JsonReader reader, Type typeToConvert,
			JsonSerializerOptions options) => null;

		public override void Write(Utf8JsonWriter writer, Union<TItem1, TItem2> value,
			JsonSerializerOptions options)
		{
			if (value.Item1 is not null)
			{
				JsonSerializer.Serialize(writer, value.Item1, value.Item1.GetType(), options);
				return;
			}

			if (value.Item2 is not null)
			{
				JsonSerializer.Serialize(writer, value.Item2, value.Item2.GetType(), options);
				return;
			}

			throw new SerializationException("Invalid union type");
		}
	}

	private class BucketsConverter<TBucket> : JsonConverter<Buckets<TBucket>>
	{
		public override Buckets<TBucket>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// TODO - Read ahead to establish the type - For now, hardcoded for lists

			var bucketType = typeToConvert.GetGenericArguments()[0];

			var item = JsonSerializer.Deserialize(ref reader, typeof(IReadOnlyCollection<TBucket>), options);

			return (Buckets<TBucket>)Activator.CreateInstance(typeof(Buckets<>).MakeGenericType(bucketType), item);
		}

		public override void Write(Utf8JsonWriter writer, Buckets<TBucket> value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}

//public class UnionConverter<TConcrete> : JsonConverter<TConcrete> where TConcrete : class
//{
//	public override TConcrete Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//	{
//		var token = reader.TokenType;

//		switch (token)
//		{
//			case JsonTokenType.String:
//				{
//					var value = reader.GetString();
//					var result = (TConcrete)Activator.CreateInstance(typeof(TConcrete), value);
//					return result;
//				}
//			case JsonTokenType.Number:
//				{
//					var value = reader.GetInt32();
//					var result = (TConcrete)Activator.CreateInstance(typeof(TConcrete), value);
//					return result;
//				}
//		}

//		throw new SerializationException();
//	}

//	public override void Write(Utf8JsonWriter writer, TConcrete value, JsonSerializerOptions options) =>
//		throw new NotImplementedException();
//}
