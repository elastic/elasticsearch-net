// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Globalization;

using Elastic.Transport.Extensions;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Represents a value for a field which depends on the field mapping and is only known at runtime,
/// therefore cannot be specifically typed.
/// </summary>
[JsonConverter(typeof(FieldValueConverter))]
public readonly struct FieldValue : IEquatable<FieldValue>
{
	internal FieldValue(ValueKind kind, object? value)
	{
		Kind = kind;
		Value = value;
	}

	/// <summary>
	/// The kind of value contained within this <see cref="FieldValue"/>.
	/// </summary>
	public readonly ValueKind Kind { get; }

	/// <summary>
	/// The value contained within within this <see cref="FieldValue"/>.
	/// </summary>
	public readonly object? Value { get; }

	/// <summary>
	/// An enumeration of the possible value kinds that the <see cref="FieldValue"/> may contain.
	/// </summary>
	public enum ValueKind
	{
		Null,
		Double,
		Long,
		Boolean,
		String,
		LazyDocument,
		Composite
	}

	/// <summary>
	/// Represents a null value.
	/// </summary>
	public static FieldValue Null { get; } = new(ValueKind.Null, null);

	/// <summary>
	/// Represents a true boolean value.
	/// </summary>
	public static FieldValue True { get; } = new(ValueKind.Boolean, true);

	/// <summary>
	/// Represents a false boolean value.
	/// </summary>
	public static FieldValue False { get; } = new(ValueKind.Boolean, false);

	/// <summary>
	/// Factory method to create a <see cref="FieldValue"/> containing a long value.
	/// </summary>
	/// <param name="value">The long to store as the value.</param>
	/// <returns>The new <see cref="FieldValue"/>.</returns>
	public static FieldValue Long(long value) => new(ValueKind.Long, value);

	/// <summary>
	/// Factory method to create a <see cref="FieldValue"/> containing a boolean value.
	/// </summary>
	/// <param name="value">The boolean to store as the value.</param>
	/// <returns>The new <see cref="FieldValue"/>.</returns>
	public static FieldValue Boolean(bool value) => new(ValueKind.Boolean, value);

	/// <summary>
	/// Factory method to create a <see cref="FieldValue"/> containing a double value.
	/// </summary>
	/// <param name="value">The double to store as the value.</param>
	/// <returns>The new <see cref="FieldValue"/>.</returns>
	public static FieldValue Double(double value) => new(ValueKind.Double, value);

	/// <summary>
	/// Factory method to create a <see cref="FieldValue"/> containing a string value.
	/// </summary>
	/// <param name="value">The string to store as the value.</param>
	/// <returns>The new <see cref="FieldValue"/>.</returns>
	public static FieldValue String(string value) => new(ValueKind.String, value);

	// These are not expected to be required for consumer values but are used internally.
	internal static FieldValue Any(LazyJson value) => new(ValueKind.LazyDocument, value);

	internal static FieldValue Composite(object value) => new(ValueKind.Composite, value);

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.String"/>.
	/// </summary>
	public bool IsString => Kind == ValueKind.String;

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.Boolean"/>.
	/// </summary>
	public bool IsBool => Kind == ValueKind.Boolean;

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.Long"/>.
	/// </summary>
	public bool IsLong => Kind == ValueKind.Long;

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.Double"/>.
	/// </summary>
	public bool IsDouble => Kind == ValueKind.Double;

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.LazyDocument"/>.
	/// </summary>
	public bool IsLazyDocument => Kind == ValueKind.LazyDocument;

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.Null"/>.
	/// </summary>
	public bool IsNull => Kind == ValueKind.Null;

	/// <summary>
	/// Checks if the value of <see cref="ValueKind.Composite"/>.
	/// </summary>
	public bool IsComposite => Kind == ValueKind.Composite;

	/// <summary>
	/// Gets the value when the value kind is <see cref="ValueKind.String"/>.
	/// </summary>
	/// <param name="value">When this method returns, contains the value associated with this <see cref="FieldValue"/>,
	/// if the value kind is <see cref="ValueKind.String"/>; otherwise, the default value for the type of the value parameter.
	/// This parameter is passed uninitialized.</param>
	/// <returns>True if the value is a <see cref="string"/>.</returns>
	public bool TryGetString([NotNullWhen(returnValue: true)] out string? value)
	{
		value = null;

		if (!IsString)
			return false;

		value = (string)Value;
		return true;
	}

	/// <summary>
	/// Gets the value when the value kind is <see cref="ValueKind.String"/>.
	/// </summary>
	/// <param name="value">When this method returns, contains the value associated with this <see cref="FieldValue"/>,
	/// if the value kind is <see cref="ValueKind.String"/>; otherwise, the default value for the type of the value parameter.
	/// This parameter is passed uninitialized.</param>
	/// <returns>True if the value is a <see cref="string"/>.</returns>
	public bool TryGetBool([NotNullWhen(returnValue: true)] out bool? value)
	{
		value = null;

		if (!IsBool)
			return false;

		value = (bool)Value;
		return true;
	}

	/// <summary>
	/// Gets the value when the value kind is <see cref="ValueKind.Long"/>.
	/// </summary>
	/// <param name="value">When this method returns, contains the value associated with this <see cref="FieldValue"/>,
	/// if the value kind is <see cref="ValueKind.Long"/>; otherwise, the default value for the type of the value parameter.
	/// This parameter is passed uninitialized.</param>
	/// <returns>True if the value is a <see cref="long"/>.</returns>
	public bool TryGetLong([NotNullWhen(returnValue: true)] out long? value)
	{
		value = null;

		if (!IsLong)
			return false;

		value = (long)Value;
		return true;
	}

	/// <summary>
	/// Gets the value when the value kind is <see cref="ValueKind.Double"/>.
	/// </summary>
	/// <param name="value">When this method returns, contains the value associated with this <see cref="FieldValue"/>,
	/// if the value kind is <see cref="ValueKind.Double"/>; otherwise, the default value for the type of the value parameter.
	/// This parameter is passed uninitialized.</param>
	/// <returns>True if the value is a <see cref="double"/>.</returns>
	public bool TryGetDouble([NotNullWhen(returnValue: true)] out double? value)
	{
		value = null;

		if (!IsDouble)
			return false;

		value = (double)Value;
		return true;
	}

	/// <summary>
	/// Gets the value when the value kind is <see cref="ValueKind.LazyDocument"/>.
	/// </summary>
	/// <param name="value">When this method returns, contains the value associated with this <see cref="FieldValue"/>,
	/// if the value kind is <see cref="ValueKind.LazyDocument"/>; otherwise, the default value for the type of the value parameter.
	/// This parameter is passed uninitialized.</param>
	/// <returns>True if the value is a <see cref="LazyJson"/>.</returns>
	public bool TryGetLazyDocument([NotNullWhen(returnValue: true)] out LazyJson? value)
	{
		value = null;

		if (!IsLazyDocument)
			return false;

		value = (LazyJson)Value;
		return true;
	}

	/// <summary>
	/// Gets the value when the value kind is <see cref="ValueKind.Composite"/> and the value type is <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type expected for the value.</typeparam>
	///  <param name="value">When this method returns, contains the value associated with this <see cref="FieldValue"/>,
	/// if the value kind is <see cref="ValueKind.Composite"/> and the value type is <typeparamref name="T"/>; otherwise, the default
	/// value for the type of the value parameter. This parameter is passed uninitialized.</param>
	/// <returns>True if the value is of the specified <typeparamref name="T"/> type.</returns>
	public bool TryGet<T>([NotNullWhen(returnValue: true)] out T? value)
	{
		value = default;

		if (!IsComposite || Value is not T typedValue)
			return false;

		value = typedValue;
		return true;
	}

	internal bool TryGetComposite([NotNullWhen(returnValue: true)] out object? value)
	{
		value = default;

		if (!IsComposite)
			return false;

		value = Value;
		return true;
	}

	public override string ToString() =>
		Kind switch
		{
			ValueKind.Null => "null",
			ValueKind.Double => ((double)Value).ToString(CultureInfo.InvariantCulture),
			ValueKind.Long => ((long)Value).ToString(CultureInfo.InvariantCulture),
			ValueKind.Boolean => ((bool)Value).ToString(CultureInfo.InvariantCulture),
			ValueKind.String => Value as string,
			ValueKind.LazyDocument or ValueKind.Composite => throw new InvalidOperationException(
				"Composite field value cannot be formatted as a string."),
			_ => throw new InvalidOperationException($"Unknown kind '{Kind}'")
		};

	public override bool Equals(object? obj) => obj is FieldValue value && Equals(value);

	public bool Equals(FieldValue other) => Kind == other.Kind && EqualityComparer<object?>.Default.Equals(Value, other.Value);

	public override int GetHashCode()
	{
		unchecked
		{
			var hashCode = 1484969029;
			hashCode = hashCode * -1521134295 + Kind.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<object?>.Default.GetHashCode(Value);
			return hashCode;
		}
	}

	public static bool operator ==(FieldValue left, FieldValue right) => left.Equals(right);

	public static bool operator !=(FieldValue left, FieldValue right) => !(left == right);

	public static implicit operator FieldValue(string value) => String(value);

	public static implicit operator FieldValue(bool value) => Boolean(value);

	public static implicit operator FieldValue(int value) => Long(value);

	public static implicit operator FieldValue(long value) => Long(value);

	public static implicit operator FieldValue(double value) => Double(value);
}

internal sealed class FieldValueConverter : JsonConverter<FieldValue>
{
	public override FieldValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return FieldValue.Null;

			case JsonTokenType.String:
				var stringValue = reader.GetString();
				return FieldValue.String(stringValue);

			case JsonTokenType.Number:
				if (reader.TryGetInt64(out var l))
				{
					return FieldValue.Long(l);
				}
				else if (reader.TryGetDouble(out var d))
				{
					return FieldValue.Double(d);
				}
				else
				{
					throw new JsonException("Unexpected number format which cannot be deserialised as a FieldValue.");
				}

			case JsonTokenType.True:
				return FieldValue.True;

			case JsonTokenType.False:
				return FieldValue.False;

			case JsonTokenType.StartObject:
			case JsonTokenType.StartArray:
				var value = JsonSerializer.Deserialize<LazyJson>(ref reader, options);
				return FieldValue.Any(value);
		}

		throw new JsonException($"Unexpected token type '{reader.TokenType}' read while deserializing a FieldValue.");
	}

	public override void Write(Utf8JsonWriter writer, FieldValue value, JsonSerializerOptions options)
	{
		if (value.TryGetString(out var stringValue))
		{
			writer.WriteStringValue(stringValue);
		}
		else if (value.TryGetBool(out var boolValue))
		{
			writer.WriteBooleanValue(boolValue.Value);
		}
		else if (value.TryGetLong(out var longValue))
		{
			writer.WriteNumberValue(longValue.Value);
		}
		else if (value.TryGetDouble(out var doubleValue))
		{
			writer.WriteNumberValue(doubleValue.Value);
		}
		else if (value.TryGetLazyDocument(out var lazyDocument))
		{
			writer.WriteRawValue(lazyDocument.Value.Bytes);
		}
		else if (value.TryGetComposite(out var objectValue))
		{
			if (!options.TryGetClientSettings(out var settings))
				ThrowHelper.ThrowJsonExceptionForMissingSettings();

			settings.SourceSerializer.Serialize(objectValue, objectValue.GetType(), writer, null);
		}
		else if (value.Kind == FieldValue.ValueKind.Null)
		{
			writer.WriteNullValue();
		}
		else
		{
			throw new JsonException($"Unsupported FieldValue kind. This is likely a bug and should be reported as an issue.");
		}
	}
}
