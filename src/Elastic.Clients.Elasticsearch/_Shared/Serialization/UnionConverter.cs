// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class UnionConverter : JsonConverterFactory
{
	// Because converters registered on JsonSerializerOptions take priority over the JsonConverter attribute on the type, we need a way to
	// mark those types we don't want to use the default union converter. This set is used for that purpose, until a better option can be
	// found.
	private static readonly HashSet<Type> TypesToSkip = new()
	{
		typeof(Core.Search.SourceConfig),
		typeof(Script)
	};

	public override bool CanConvert(Type typeToConvert) => !TypesToSkip.Contains(typeToConvert) &&
		(typeToConvert.Name == typeof(Union<,>).Name || (typeToConvert.BaseType is not null && typeToConvert.BaseType.Name == typeof(Union<,>).Name));

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

		JsonConverter converter;

		if (type.Name == typeof(Union<,>).Name)
		{
			converter = (JsonConverter)Activator.CreateInstance(typeof(UnionConverterInner<,>).MakeGenericType(itemOneType, itemTwoType));
		}
		else
		{
			converter = (JsonConverter)Activator.CreateInstance(typeof(DerivedUnionConverterInner<,,>).MakeGenericType(type, itemOneType, itemTwoType));
		}

		return converter;
	}

	private class DerivedUnionConverterInner<TType, TItem1, TItem2> : JsonConverter<TType>
		where TType : Union<TItem1, TItem2>
	{
		public override TType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// TODO - Aggregate Exception if both fail

			var readerCopy = reader;

			try
			{
				var itemOne = JsonSerializer.Deserialize<TItem1>(ref readerCopy, options);

				if (itemOne is IUnionVerifiable verifiable)
				{
					if (verifiable.IsSuccessful)
					{
						reader = readerCopy;
						return (TType)Activator.CreateInstance(typeof(TType), itemOne);
					}
				}
				else if (itemOne is not null)
				{
					reader = readerCopy;
					return (TType)Activator.CreateInstance(typeof(TType), itemOne);
				}
			}
			catch
			{
				// TODO - Store for aggregate exception
			}

			//if (reader.TokenType == JsonTokenType.StartObject)
			//{
			//	requiresEndObject = true;
			//	reader.Read();
			//}

			try
			{
				var itemTwo = JsonSerializer.Deserialize<TItem2>(ref reader, options);

				if (itemTwo is not null)
				{
					return (TType)Activator.CreateInstance(typeof(TType), itemTwo);
				}
			}
			catch
			{
				// TODO - Store for aggregate exception
			}

			throw new JsonException("Unable to deserialize union."); // TODO - Add inner aggregate exception.
		}

		public override void Write(Utf8JsonWriter writer, TType value,
			JsonSerializerOptions options)
		{
			switch (value.Tag)
			{
				case 0:
					JsonSerializer.Serialize(writer, value.Item1, value.Item1!.GetType(), options);
					return;

				case 1:
					JsonSerializer.Serialize(writer, value.Item2, value.Item2!.GetType(), options);
					return;
			}

			throw new JsonException("Invalid union type.");
		}
	}

	private class UnionConverterInner<TItem1, TItem2> : JsonConverter<Union<TItem1, TItem2>>
	{
		public override Union<TItem1, TItem2>? Read(ref Utf8JsonReader reader, Type typeToConvert,
			JsonSerializerOptions options)
		{
			var readerCopy = reader;

			try
			{
				var itemOne = JsonSerializer.Deserialize<TItem1>(ref readerCopy, options);

				if (itemOne is IUnionVerifiable verifiable)
				{
					if (verifiable.IsSuccessful)
					{
						reader = readerCopy;
						return (Union<TItem1, TItem2>)Activator.CreateInstance(typeof(Union<TItem1, TItem2>), itemOne);
					}
				}
				else if (itemOne is not null)
				{
					reader = readerCopy;
					return (Union<TItem1, TItem2>)Activator.CreateInstance(typeof(Union<TItem1, TItem2>), itemOne);
				}
			}
			catch
			{
				// TODO - Store for aggregate exception
			}

			try
			{
				var itemTwo = JsonSerializer.Deserialize<TItem2>(ref reader, options);

				if (itemTwo is not null)
				{
					return (Union<TItem1, TItem2>)Activator.CreateInstance(typeof(Union<TItem1, TItem2>), itemTwo);
				}
			}
			catch
			{
				// TODO - Store for aggregate exception
			}

			throw new JsonException("Unable to deserialize union."); // TODO - Add inner aggregate exception.
		}

		public override void Write(Utf8JsonWriter writer, Union<TItem1, TItem2> value,
			JsonSerializerOptions options)
		{
			switch (value.Tag)
			{
				case 0:
					JsonSerializer.Serialize(writer, value.Item1, value.Item1!.GetType(), options);
					return;

				case 1:
					JsonSerializer.Serialize(writer, value.Item2, value.Item2!.GetType(), options);
					return;
			}

			throw new JsonException("Invalid union type");
		}
	}

	private class BucketsConverter<TBucket> : JsonConverter<Buckets<TBucket>>
	{
		public override Buckets<TBucket>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.TokenType switch
			{
				JsonTokenType.Null => null,
				JsonTokenType.StartArray => new(JsonSerializer.Deserialize<IReadOnlyCollection<TBucket>>(ref reader, options)),
				JsonTokenType.StartObject => new(JsonSerializer.Deserialize<IReadOnlyDictionary<string, TBucket>>(ref reader, options)),
				_ => throw new JsonException("Invalid bucket type")
			};
		}

		public override void Write(Utf8JsonWriter writer, Buckets<TBucket> value, JsonSerializerOptions options)
		{
			if (value.Item1 is { } item1)
			{
				JsonSerializer.Serialize(writer, item1, options);
				return;
			}

			if (value.Item2 is { } item2)
			{
				JsonSerializer.Serialize(writer, item2, options);
				return;
			}

			writer.WriteNullValue();
		}
	}
}
