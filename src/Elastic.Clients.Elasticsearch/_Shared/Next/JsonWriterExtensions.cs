// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

// NOTE:
// We make use of '[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]' for
// marker type parameters.
// The marker types are not actually constructed at any time, but this tricks the trimmer into checking the static
// constructor of all marker types. We use the static constructor to root the corresponding marker type converters,
// if the marker type has generic type arguments.

internal static class JsonWriterExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WritePropertyName<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, [DisallowNull] T name,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? markerType = null)
	{
		if ((markerType is null) && (typeof(T) == typeof(string)))
		{
			writer.WritePropertyName((string)(object)name);
			return;
		}

		if ((markerType is null) && (typeof(T) == typeof(JsonEncodedText)))
		{
			writer.WritePropertyName(Unsafe.As<T, JsonEncodedText>(ref name));
			return;
		}

		options.GetConverter<T>(markerType).WriteAsPropertyName(writer, name, options);
	}

	/// <summary>
	/// Serializes the given <paramref name="value"/> to JSON using the appropriate <see cref="JsonConverter"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="writer">The <see cref="Utf8JsonWriter"/> to use for writing the serialized value.</param>
	/// <param name="value">The value to serialize.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="markerType">An optional type hint, used to retrieve a matching converter from the given <paramref name="options"/>.</param>
	/// <remarks>
	/// The matching converter for <paramref name="markerType"/> must implement <see cref="IMarkerTypeConverter"/>.
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void WriteValue<T>(this Utf8JsonWriter writer, JsonSerializerOptions options, T value,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? markerType = null)
	{
		var converter = options.GetConverter<T>(markerType);

		if ((value is null) && !converter.HandleNull)
		{
			writer.WriteNullValue();
			return;
		}

		converter.Write(writer, value, options);
	}

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
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? nameMarkerType,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type? valueMarkerType,
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

		writer.WritePropertyName(options, name, nameMarkerType);
		writer.WriteValue(options, value, valueMarkerType);
	}
}
