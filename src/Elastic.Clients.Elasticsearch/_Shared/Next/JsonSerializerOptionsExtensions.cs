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
