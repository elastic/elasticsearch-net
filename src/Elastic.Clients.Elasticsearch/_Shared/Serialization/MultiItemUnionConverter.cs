// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Core;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A base converter for any multi-item (>2 items) unions. The code-generator creates a
/// derived type which populates the fields in its constructor.
/// IMPORTANT: This is a MVP implementation which meets the requirements for the currently
/// generated multi-item unions. Additional logic may be needed when we first encounter
/// other item types. In the interests of time, we are not covering all cases for now.
/// </summary>
internal abstract class MultiItemUnionConverter<TUnion, TEnum> : JsonConverter<TUnion>
	where TUnion : IComplexUnion<TEnum>
	where TEnum : Enum
{
	// Used when serializing to specify the type for each enum kind.
	protected Dictionary<TEnum, Type> _types;

	// Used when creating an instance of the TUnion for a specific type.
	protected Dictionary<Type, Func<object, TUnion>> _factories;

	// Used when deserializing objects, to determine which type we have.
	protected Dictionary<string, Type> _uniquePropertyToType;

#pragma warning disable CS0649
	protected Type _arrayType; // For now, we handle only unions with one item being defined as an array
#pragma warning restore CS0649

	public override TUnion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		const string exceptionMessage = "Unable to match JSON object to union item.";

		// Plan of attack!!
		// - If the token is start object:
		// - For types which are objects, we need to identify required properties
		// - Determine if each object has a unique required property (if not we need to find unique combinations)
		// - If none are unique, we may struggle, but we can also check for unique optional properties
		// - If the token is a literal value, see if we have a matching literal available
		// - If the token is the start of an array, do we have an array type identified (we currently only support one array-based item).

		if (_factories is null)
			ThrowHelper.ThrowJsonException("No factories have been registered for deserialization.");

		// We'll be handling an object
		if (reader.TokenType == JsonTokenType.StartObject)
		{
			var readerCopy = reader; // We need a copy to use when reading ahead

			if (_uniquePropertyToType is null)
				ThrowHelper.ThrowJsonException(exceptionMessage);

			using var jsonDoc = JsonDocument.ParseValue(ref readerCopy);

			if (jsonDoc is null)
				ThrowHelper.ThrowJsonException(exceptionMessage);

			Type? matchedType = null;

			// Loop through the unique properties of each possible object.
			// Once we find a match we can stop checking any further.
			foreach (var item in _uniquePropertyToType)
			{
				if (jsonDoc.RootElement.TryGetProperty(item.Key, out _))
				{
					// We've matched a unique property in the JSON object, so now know the type
					matchedType = item.Value;
					break;
				}
			}

			if (matchedType is null)
				ThrowHelper.ThrowJsonException(exceptionMessage);

			if (!_factories.TryGetValue(matchedType, out var factory))
				ThrowHelper.ThrowJsonException("Unable to locate factory for multi-item union object item.");

			// Since we now know the type and have the factory for that type, we can deserialize the object
			// and pass it to the factory to create the instance.

			var value = JsonSerializer.Deserialize(ref reader, matchedType, options);

			return factory.Invoke(value);
		}

		if (reader.TokenType == JsonTokenType.String)
		{
			var value = reader.GetString();
			reader.Read();

			if (!_factories.TryGetValue(typeof(string), out var factory))
				ThrowHelper.ThrowJsonException("Unable to locate factory for multi-item union accepting a string value.");

			return factory.Invoke(value);
		}

		if (reader.TokenType == JsonTokenType.StartArray)
		{
			if (_arrayType is null)
				ThrowHelper.ThrowJsonException(exceptionMessage);

			if (!_factories.TryGetValue(_arrayType, out var factory))
				ThrowHelper.ThrowJsonException("Unable to locate factory for multi-item union accepting an array value.");

			var value = JsonSerializer.Deserialize(ref reader, _arrayType, options);

			return factory.Invoke(value);
		}

		ThrowHelper.ThrowJsonException($"Unable to deserialize JSON representing {typeof(TUnion)}.");

		return default; // We never reach here!
	}

	public override void Write(Utf8JsonWriter writer, TUnion value, JsonSerializerOptions options)
	{
		if (_types is null)
			ThrowHelper.ThrowJsonException("No types have been registered for serialization.");

		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		var serializeAsType = _types[value.ValueKind];

		JsonSerializer.Serialize(writer, value.Value, serializeAsType, options);
	}
}
