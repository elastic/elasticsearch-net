// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using System.Runtime.CompilerServices;

using System.Text.Json.Serialization;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static partial class JsonSerializerOptionsExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonConverter<T> GetConverter<T>(this JsonSerializerOptions options, Type? markerType)
	{
		// Mimics the internal behavior of `JsonSerializer.Serialize()` and as well seems to be required in order
		// to directly use converters like we do.
		// When getting a default generic converter from `JsonSerializerOptions` that are not read-only, a
		// `NotSupportedException` is thrown as soon as we call `converter.Read()` or `converter.Write()`.
#pragma warning disable IL2026, IL3050
		options.MakeReadOnly(true);
#pragma warning restore IL2026, IL3050

#pragma warning disable IL2026, IL3050
		var rawConverter = options.GetConverter(markerType ?? typeof(T));
#pragma warning restore IL2026, IL3050

		var converter = (JsonConverter<T>)(rawConverter is IMarkerTypeConverter markerTypeConverter
			? markerTypeConverter.WrappedConverter
			: rawConverter);

		return converter;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TContext GetContext<TContext>(this JsonSerializerOptions options)
	{
		return ContextProvider<TContext>.GetContext(options);
	}
}
