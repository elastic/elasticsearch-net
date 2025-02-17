// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A delegate that writes a value to a given <see cref="Utf8JsonWriter"/> instance.
/// </summary>
/// <typeparam name="T">The type of the value to write.</typeparam>
/// <param name="writer">The <see cref="Utf8JsonWriter"/>.</param>
/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
/// <param name="value">The value to write.</param>
internal delegate void JsonWriteFunc<in T>(Utf8JsonWriter writer, JsonSerializerOptions options, T? value);

// NOTE:
// The marker type concept allows us to use specialized converters on a per-property basis in custom converters.
// This basically emulates using the '[JsonConverter({ConverterType})]' attribute on property level.

// NOTE:
// We make use of '[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]' for
// marker type parameters.
// The marker types are not actually constructed at any time, but this tricks the trimmer into checking the static
// constructor of all marker types. We use the static constructor to root the corresponding marker type converters,
// if the marker type has generic type arguments.

internal static class JsonWriterExtensions
{
	#region Default Generic Write Methods

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WriteValue<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, T? value)
	{
		var converter = options.GetConverter<T?>(null);

		if ((value is null) && !converter.HandleNull)
		{
			writer.WriteNullValue();
			return;
		}

		converter.Write(writer, value, options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WriteValueEx<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, T? value,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type markerType)
	{
		var converter = options.GetConverter<T?>(markerType);

		if ((value is null) && !converter.HandleNull)
		{
			writer.WriteNullValue();
			return;
		}

		converter.Write(writer, value, options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WritePropertyName<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, [DisallowNull] T name)
	{
		if (typeof(T) == typeof(string))
		{
			writer.WritePropertyName((string)(object)name);
			return;
		}

		if (typeof(T) == typeof(JsonEncodedText))
		{
			writer.WritePropertyName(Unsafe.As<T, JsonEncodedText>(ref name));
			return;
		}

		options.GetConverter<T>(null).WriteAsPropertyName(writer, name, options);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WritePropertyNameEx<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, [DisallowNull] T name,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type markerType)
	{
		options.GetConverter<T>(markerType).WriteAsPropertyName(writer, name, options);
	}

	#endregion Default Generic Write Methods

	#region Delegate Based Write Methods

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WriteValue<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, T? value,
		JsonWriteFunc<T>? writeValue)
	{
		if (writeValue is null)
		{
			writer.WriteValue(options, value);
			return;
		}

		writeValue(writer, options, value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WritePropertyName<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, [DisallowNull] T name,
		JsonWriteFunc<T>? writeValue)
	{
		if (writeValue is null)
		{
			writer.WritePropertyName(options, name);
			return;
		}

		writeValue(writer, options, name);
	}

	public static void WriteCollectionValue<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, IEnumerable<T>? collection,
		JsonWriteFunc<T>? writeElement)
	{
		if (collection is null)
		{
			writer.WriteNullValue();
			return;
		}

		writeElement ??= static (w, o, v) => WriteValue(w, o, v);

		writer.WriteStartArray();

		foreach (var element in collection)
		{
			writeElement(writer, options, element);
		}

		writer.WriteEndArray();
	}

	public static void WriteDictionaryValue<TKey, TValue>(this Utf8JsonWriter writer, JsonSerializerOptions options,
		IEnumerable<KeyValuePair<TKey, TValue>>? dictionary,
		JsonWriteFunc<TKey>? writeKey, JsonWriteFunc<TValue>? writeValue)
		where TKey : notnull
	{
		if (dictionary is null)
		{
			writer.WriteNullValue();
			return;
		}

		writeKey ??= static (w, o, v) => WritePropertyName(w, o, v!);
		writeValue ??= static (w, o, v) => WriteValue(w, o, v);

		writer.WriteStartObject();

		foreach (var pair in dictionary)
		{
			writeKey(writer, options, pair.Key);
			writeValue(writer, options, pair.Value);
		}

		writer.WriteEndObject();
	}

	public static void WriteUnionValue<T1, T2>(this Utf8JsonWriter writer, JsonSerializerOptions options,
		Union<T1, T2>? union,
		JsonWriteFunc<T1>? writeType1, JsonWriteFunc<T2>? writeType2)
	{
		if (union is null)
		{
			writer.WriteNullValue();
			return;
		}

		union.Match(
			value => (writeType1 ?? (static (w, o, v) => WriteValue(w, o, v))).Invoke(writer, options, value),
			value => (writeType2 ?? (static (w, o, v) => WriteValue(w, o, v))).Invoke(writer, options, value)
		);
	}

	#endregion Delegate Based Write Methods

	#region Specialized Write Methods

	public static void WriteSingleOrManyCollectionValue<T>(this Utf8JsonWriter writer, JsonSerializerOptions options,
		IEnumerable<T>? collection,
		JsonWriteFunc<T>? writeElement)
	{
		writeElement ??= static (w, o, v) => WriteValue(w, o, v);

		switch (collection)
		{
			case null:
				writer.WriteNullValue();
				return;

			case IList<T> and [{ } item]:
				writer.WriteValue(options, item);
				return;

			case IReadOnlyList<T> and [{ } item]:
				writer.WriteValue(options, item);
				return;

			case ICollection<T> { Count: 1 }:
				writer.WriteValue(options, collection.First());
				return;

			case IReadOnlyCollection<T> { Count: 1 }:
				writer.WriteValue(options, collection.First());
				return;
		}

		var value = collection.ToArray();
		if (value.Length is 1)
		{
			writer.WriteValue(options, value.First());
			return;
		}

		writer.WriteStartArray();

		foreach (var element in value)
		{
			writeElement(writer, options, element);
		}

		writer.WriteEndArray();
	}

	#endregion Specialized Write Methods

	#region Property Write Methods

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WriteProperty<TName, TValue>(this Utf8JsonWriter writer, JsonSerializerOptions options,
		[DisallowNull] TName name, TValue value,
		JsonIgnoreCondition? ignoreCondition = null)
	{
		var shouldIgnore = (ignoreCondition ?? options.DefaultIgnoreCondition) switch
		{
			JsonIgnoreCondition.Never => false,
			JsonIgnoreCondition.Always => true,
			JsonIgnoreCondition.WhenWritingDefault => EqualityComparer<TValue?>.Default.Equals(value, default),
			JsonIgnoreCondition.WhenWritingNull => (value is null),
			_ => throw new NotSupportedException("Unsupported JSON ignore condition.")
		};

		if (shouldIgnore)
		{
			return;
		}

		writer.WritePropertyName(options, name);
		writer.WriteValue(options, value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WriteProperty<TName, TValue>(this Utf8JsonWriter writer, JsonSerializerOptions options,
		[DisallowNull] TName name, TValue value,
		JsonWriteFunc<TName>? writeName, JsonWriteFunc<TValue>? writeValue,
		JsonIgnoreCondition? ignoreCondition = null)
	{
		var shouldIgnore = (ignoreCondition ?? options.DefaultIgnoreCondition) switch
		{
			JsonIgnoreCondition.Never => false,
			JsonIgnoreCondition.Always => true,
			JsonIgnoreCondition.WhenWritingDefault => EqualityComparer<TValue?>.Default.Equals(value, default),
			JsonIgnoreCondition.WhenWritingNull => (value is null),
			_ => throw new NotSupportedException("Unsupported JSON ignore condition.")
		};

		if (shouldIgnore)
		{
			return;
		}

		writeName ??= static (w, o, v) => WritePropertyName(w, o, v!);
		writeValue ??= static (w, o, v) => WriteValue(w, o, v);

		writeName(writer, options, name);
		writeValue(writer, options, value);
	}

	#endregion Property Write Methods
}
