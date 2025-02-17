// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A delegate that reads a value from a given <see cref="Utf8JsonReader"/> instance.
/// </summary>
/// <typeparam name="T">The type of the value to read.</typeparam>
/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
/// <returns>The value read from the <paramref name="reader"/>.</returns>
internal delegate T? JsonReadFunc<out T>(ref Utf8JsonReader reader, JsonSerializerOptions options);

// NOTE:
// The marker type concept allows us to use specialized converters on a per-property basis in custom converters.
// This basically emulates using the '[JsonConverter({ConverterType})]' attribute on property level.

// NOTE:
// We make use of '[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]' for
// marker type parameters.
// The marker types are not actually constructed at any time, but this tricks the trimmer into checking the static
// constructor of all marker types. We use the static constructor to root the corresponding marker type converters,
// if the marker type has generic type arguments.

internal static class JsonReaderExtensions
{
	#region General Purpose

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidateToken(this ref Utf8JsonReader reader, JsonTokenType expected)
	{
		if (reader.TokenType != expected)
		{
			throw new JsonException($"Expected JSON '{expected}' token, but got '{reader.TokenType}'.");
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonException UnexpectedTokenException(this ref Utf8JsonReader reader, params ReadOnlySpan<JsonTokenType> expected)
	{
		string valid;
		if (expected.Length <= 1)
		{
			valid = $"'{expected[0]}'";
		}
		else
		{
			valid = string.Join(",", expected[..^2].ToArray().Select(x => $"'{x}'"));
			valid += $" or '{expected[^1]}'";
		}

		return new JsonException($"Expected JSON {valid} token, but got '{reader.TokenType}'.");
	}

	/// <summary>
	/// Compares the JSON encoded text to the JSON token value in the source and returns <see langword="true"/> if they match.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/>.</param>
	/// <param name="text">The JSON encoded text to compare against.</param>
	/// <returns><see langword="true"/> if the JSON token value in the source matches the JSON encoded look up text.</returns>
	/// <remarks>
	///     This is an alternative version of the built-in <see cref="Utf8JsonReader.ValueTextEquals(ReadOnlySpan{byte})"/> method
	///     with the only difference that this overload operates on pre-encoded JSON text.
	/// </remarks>
	public static bool ValueTextEquals(this ref Utf8JsonReader reader, JsonEncodedText text)
	{
		return reader.HasValueSequence
			? CompareToSequence(ref reader, text.EncodedUtf8Bytes)
			: reader.ValueSpan.SequenceEqual(text.EncodedUtf8Bytes);

		static bool CompareToSequence(ref Utf8JsonReader reader, ReadOnlySpan<byte> other)
		{
			var localSequence = reader.ValueSequence;
			if (localSequence.Length != other.Length)
			{
				return false;
			}

			var matchedSoFar = 0;

			foreach (var memory in localSequence)
			{
				var span = memory.Span;

				if (other[matchedSoFar..].StartsWith(span))
				{
					matchedSoFar += span.Length;
				}
				else
				{
					return false;
				}
			}

			return true;
		}
	}

	#endregion General Purpose

	#region Default Generic Read Methods

	/// <summary>
	/// Reads a value from a given <see cref="Utf8JsonReader"/> instance using the default <see cref="JsonConverter"/> for the
	/// type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <returns>The value read from the <paramref name="reader"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T? ReadValue<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		var converter = options.GetConverter<T>(null);

		if ((reader.TokenType is JsonTokenType.Null) && !converter.HandleNull)
		{
			return default;
		}

		return converter.Read(ref reader, typeof(T), options);
	}

	/// <summary>
	/// Reads a value from a given <see cref="Utf8JsonReader"/> instance using a specific <see cref="JsonConverter"/> that is
	/// retrieved from the <see cref="JsonSerializerOptions"/> based on <paramref name="markerType"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="markerType">
	/// The marker type that is used to retrieve a specific <see cref="IMarkerTypeConverter"/> from the given <paramref name="options"/>.
	/// </param>
	/// <returns>The value read from the <paramref name="reader"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T? ReadValueEx<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type markerType)
	{
		var converter = options.GetConverter<T>(markerType);

		if ((reader.TokenType is JsonTokenType.Null) && !converter.HandleNull)
		{
			return default;
		}

		return converter.Read(ref reader, typeof(T), options);
	}

	/// <summary>
	/// Reads a property name value from a given <see cref="Utf8JsonReader"/> instance using the default <see cref="JsonConverter"/>
	/// for the type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <returns>The property name value read from the <paramref name="reader"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ReadPropertyName<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		Debug.Assert(reader.TokenType is JsonTokenType.PropertyName);

		if (typeof(T) == typeof(string))
		{
			return (T)(object)reader.GetString()!;
		}

		return options.GetConverter<T>(null).ReadAsPropertyName(ref reader, typeof(T), options);
	}

	/// <summary>
	/// Reads a property name value from a given <see cref="Utf8JsonReader"/> instance using a specific <see cref="JsonConverter"/>
	/// that is retrieved from the <see cref="JsonSerializerOptions"/> based on <paramref name="markerType"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="markerType">
	/// The marker type that is used to retrieve a specific <see cref="IMarkerTypeConverter"/> from the given <paramref name="options"/>.
	/// </param>
	/// <returns>The property name value read from the <paramref name="reader"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ReadPropertyNameEx<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type markerType)
	{
		Debug.Assert(reader.TokenType is JsonTokenType.PropertyName);

		return options.GetConverter<T>(markerType).ReadAsPropertyName(ref reader, typeof(T), options);
	}

	#endregion Default Generic Read Methods

	#region Delegate Based Read Methods

	/// <summary>
	/// Reads a value from a given <see cref="Utf8JsonReader"/> instance using a custom <see cref="JsonReadFunc{T}"/> delegate.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="readValue">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the value, or <see langword="null"/> to use
	/// the default converter for the type <typeparamref name="T"/>.
	/// </param>
	/// <returns>The value read from the <paramref name="reader"/> instance.</returns>
	/// <remarks>
	/// This overload provides a streamlined entry-point for reading arbitrary values when using custom read delegates.
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T? ReadValue<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<T>? readValue)
	{
		if (readValue is null)
		{
			return reader.ReadValue<T>(options);
		}

		return readValue(ref reader, options);
	}

	/// <summary>
	/// Reads a property name value from a given <see cref="Utf8JsonReader"/> instance using a custom <see cref="JsonReadFunc{T}"/>
	/// delegate.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="readValue">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the property name value, or <see langword="null"/>
	/// to use the default converter for the type <typeparamref name="T"/>.
	/// </param>
	/// <returns>The property name value read from the <paramref name="reader"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T? ReadPropertyName<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<T>? readValue)
	{
		if (readValue is null)
		{
			return reader.ReadPropertyName<T>(options);
		}

		return readValue(ref reader, options);
	}

	/// <summary>
	/// Reads a collection value from a given <see cref="Utf8JsonReader"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of the items in the collection.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="readElement">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the collection items, or <see langword="null"/> to use
	/// the default converter for the item type <typeparamref name="T"/>.
	/// </param>
	/// <returns>An instance of <see cref="List{T}"/>, or <see langword="null"/>.</returns>
	public static List<T?>? ReadCollectionValue<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<T>? readElement)
	{
		if (reader.TokenType is JsonTokenType.Null)
		{
			return null;
		}

		readElement ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<T>(ref r, o);

		reader.ValidateToken(JsonTokenType.StartArray);

		var result = new List<T?>();

		while (reader.Read() && (reader.TokenType is not JsonTokenType.EndArray))
		{
			result.Add(readElement(ref reader, options));
		}

		return result;
	}

	/// <summary>
	/// Reads a dictionary value from a given <see cref="Utf8JsonReader"/> instance.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="readKey">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the dictionary key, or <see langword="null"/> to use
	/// the default converter for the type <typeparamref name="TKey"/>.
	/// </param>
	/// /// <param name="readValue">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the dictionary values, or <see langword="null"/> to use
	/// the default converter for the type <typeparamref name="TValue"/>.
	/// </param>
	/// <returns>An instance of <see cref="Dictionary{TKey, TValue}"/>, or <see langword="null"/>.</returns>
	/// <exception cref="JsonException">If any dictionary key value is <see langword="null"/>.</exception>
	public static Dictionary<TKey, TValue?>? ReadDictionaryValue<TKey, TValue>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<TKey>? readKey, JsonReadFunc<TValue>? readValue)
		where TKey : notnull
	{
		if (reader.TokenType is JsonTokenType.Null)
		{
			return null;
		}

		readKey ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadPropertyName<TKey>(ref r, o);
		readValue ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<TValue>(ref r, o);

		reader.ValidateToken(JsonTokenType.StartObject);

		var result = new Dictionary<TKey, TValue?>();

		while (reader.Read() && (reader.TokenType is not JsonTokenType.EndObject))
		{
			var key = readKey(ref reader, options) ?? throw new JsonException("JSON dictionary key must not be 'null'.");
			reader.Read();
			var value = readValue(ref reader, options);

			result[key] = value;
		}

		return result;
	}

	/// <summary>
	/// Reads a <see cref="KeyValuePair{TKey,TValue}"/> value from a given <see cref="Utf8JsonReader"/> instance.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="readKey">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the key, or <see langword="null"/> to use the default
	/// converter for the type <typeparamref name="TKey"/>.
	/// </param>
	/// /// <param name="readValue">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the value, or <see langword="null"/> to use the default
	/// converter for the type <typeparamref name="TValue"/>.
	/// </param>
	/// <returns>An instance of <see cref="KeyValuePair{TKey, TValue}"/>.</returns>
	public static KeyValuePair<TKey, TValue?> ReadKeyValuePairValue<TKey, TValue>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<TKey>? readKey, JsonReadFunc<TValue>? readValue)
		where TKey : notnull
	{
		reader.ValidateToken(JsonTokenType.PropertyName);

		readKey ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadPropertyName<TKey>(ref r, o);
		readValue ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<TValue>(ref r, o);

		var key = readKey(ref reader, options) ?? throw new JsonException("JSON key-value pair key must not be 'null'.");
		reader.Read();
		var value = readValue(ref reader, options);

		return new KeyValuePair<TKey, TValue?>(key, value);
	}

	/// <summary>
	/// Reads an inline union value from a given <see cref="Utf8JsonReader"/> instance.
	/// </summary>
	/// <typeparam name="T1">The first union type.</typeparam>
	/// <typeparam name="T2">The second union type.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/>.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="selector">A function that selects the union variant (e.g. based on the current JSON token type).</param>
	/// <param name="readType1">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the first union variant type, or <see langword="null"/>
	/// to use the default converter for the type <typeparamref name="T1"/>.
	/// </param>
	/// <param name="readType2">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the second union variant type, or <see langword="null"/>
	/// to use the default converter for the type <typeparamref name="T2"/>.
	/// </param>
	/// <returns>An instance of <see cref="Union{T1,T2}"/>, or <see langword="null"/>.</returns>
	/// <exception cref="InvalidOperationException">If no matching union variant could be selected.</exception>
	public static Union<T1, T2>? ReadUnionValue<T1, T2>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonUnionSelectorFunc selector, JsonReadFunc<T1>? readType1, JsonReadFunc<T2>? readType2)
	{
		if (reader.TokenType is JsonTokenType.Null)
		{
			// TODO: We might want the selector to handle 'null'.
			return null;
		}

		return selector(ref reader, options) switch
		{
			UnionTag.T1 => (readType1 ?? (static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<T1>(ref r, o))).Invoke(ref reader, options),
			UnionTag.T2 => (readType2 ?? (static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<T2>(ref r, o))).Invoke(ref reader, options),
			_ => throw new InvalidOperationException($"Failed to select an union variant for union of type '{typeof(T1).Name}' or '{typeof(T2).Name}'.")
		};
	}

	#region Specialized Read Methods

	/// <summary>
	/// Reads a single item or collection value from a given <see cref="Utf8JsonReader"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of the items in the collection.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="readElement">
	/// The <see cref="JsonReadFunc{T}"/> delegate that should be called to read the collection items, or <see langword="null"/> to use
	/// the default converter for the item type <typeparamref name="T"/>.
	/// </param>
	/// <returns>An instance of <see cref="List{T}"/>, or <see langword="null"/>.</returns>
	public static List<T?>? ReadSingleOrManyCollectionValue<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<T>? readElement)
	{
		if (reader.TokenType is JsonTokenType.Null)
		{
			return null;
		}

		readElement ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<T>(ref r, o);

		if (reader.TokenType is not JsonTokenType.StartArray)
		{
			return [readElement(ref reader, options)];
		}

		var result = new List<T?>();

		while (reader.Read() && (reader.TokenType is not JsonTokenType.EndArray))
		{
			result.Add(readElement(ref reader, options));
		}

		return result;
	}

	// TODO: ICollection<KeyValuePair<K, V>> => OrderedDictionary<K, V>
	//       => ReadSingleKeyDictionaryCollectionValue()

	// TODO: For 'object' or 'object | object[]' (single or many) shortcut properties:
	//       => we assume the canonical representation to be used most of the time

	#endregion Specialized Read Methods

	#endregion Delegate Based Read Methods

	#region Property Read Methods

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReadProperty<TName, TValue>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DisallowNull] out TName name, out TValue? value)
	{
		name = reader.ReadPropertyName<TName>(options);
		reader.Read();
		value = reader.ReadValue<TValue?>(options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReadProperty<TName, TValue>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DisallowNull] out TName name, out TValue? value,
		JsonReadFunc<TName>? readName, JsonReadFunc<TValue>? readValue)
	{
		readName ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadPropertyName<TName>(ref r, o);
		readValue ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<TValue>(ref r, o);

		name = readName(ref reader, options) ?? throw new InvalidOperationException("JSON property name must not be 'null'.");
		reader.Read();
		value = readValue(ref reader, options);
	}

	/// <summary>
	/// Checks, if the current property name token is equal to the given <paramref name="name"/> and proceeds to read
	/// the corresponding value if the condition was met.
	/// </summary>
	/// <typeparam name="T">The type of the value to read.</typeparam>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/>.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="name">The property name to match.</param>
	/// <param name="value">Receives the deserialized value.</param>
	/// <param name="readValue">The <see cref="JsonReadFunc{T}"/> delegate used to read the value.</param>
	/// <returns><see langword="true"/> if the value has been read or <see langword="false"/>, if the property name did not match.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryReadProperty<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options, JsonEncodedText name,
		ref T? value, JsonReadFunc<T>? readValue)
	{
		Debug.Assert(reader.TokenType is JsonTokenType.PropertyName);

		if (!reader.ValueTextEquals(name))
		{
			return false;
		}

		readValue ??= static (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadValue<T?>(ref r, o);

		reader.Read();
		value = readValue.Invoke(ref reader, options);

		return true;
	}

	#endregion Property Read Methods
}
