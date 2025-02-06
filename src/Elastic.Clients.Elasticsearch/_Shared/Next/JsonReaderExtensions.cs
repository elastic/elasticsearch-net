// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

// NOTE:
// We make use of '[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]' for
// marker type parameters.
// The marker types are not actually constructed at any time, but this tricks the trimmer into checking the static
// constructor of all marker types. We use the static constructor to root the corresponding marker type converters,
// if the marker type has generic type arguments.

internal static class JsonReaderExtensions
{
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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ReadPropertyName<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? markerType = null)
	{
		Debug.Assert(reader.TokenType is JsonTokenType.PropertyName);

		if ((markerType is null) && (typeof(T) == typeof(string)))
		{
			return (T)(object)reader.GetString()!;
		}

		return options.GetConverter<T>(markerType).ReadAsPropertyName(ref reader, typeof(T), options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T? ReadValue<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? markerType = null)
	{
		var converter = options.GetConverter<T>(markerType);

		if ((reader.TokenType is JsonTokenType.Null) && !converter.HandleNull)
		{
			return default;
		}

		return converter.Read(ref reader, typeof(T), options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReadProperty<TName, TValue>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DisallowNull] out TName name, out TValue? value)
	{
		name = reader.ReadPropertyName<TName>(options);
		reader.Read();
		value = reader.ReadValue<TValue>(options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReadProperty<TName, TValue>(this ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DisallowNull] out TName name, out TValue? value,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? nameMarkerType,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? valueMarkerType)
	{
		name = reader.ReadPropertyName<TName>(options, nameMarkerType);
		reader.Read();
		value = reader.ReadValue<TValue>(options, valueMarkerType);
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
	/// <param name="markerType">An optional type hint, used to retrieve a matching converter from the given <paramref name="options"/>.</param>
	/// <returns><see langword="true"/> if the value has been read or <see langword="false"/>, if the property name did not match.</returns>
	/// <remarks>
	/// The matching converter for <paramref name="markerType"/> must implement <see cref="IMarkerTypeConverter"/>.
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryReadProperty<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options, JsonEncodedText name, ref T? value,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? markerType = null)
	{
		Debug.Assert(reader.TokenType is JsonTokenType.PropertyName);

		if (!reader.ValueTextEquals(name))
		{
			return false;
		}

		reader.Read();
		value = reader.ReadValue<T>(options, markerType);

		return true;
	}

	/// <summary>
	/// Compares the JSON encoded text to the JSON token value in the source and returns true if they match.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/>.</param>
	/// <param name="text">The JSON encoded text to compare against.</param>
	/// <returns><see langword="true"/> if the JSON token value in the source matches the JSON encoded look up text.</returns>
	/// <remarks>
	///     This is an alternative version of the built-in <see cref="Utf8JsonReader.ValueTextEquals(ReadOnlySpan{byte})"/> method
	///     that operates on pre-encoded JSON text.
	/// </remarks>
	public static bool ValueTextEquals(this ref Utf8JsonReader reader, JsonEncodedText text)
	{
		//Debug.Assert(reader.TokenType is JsonTokenType.PropertyName or JsonTokenType.String);

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
}
