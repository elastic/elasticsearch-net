// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class IntermediateSourceConverter<T> : JsonConverter<T>
{
	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var converter = options.GetConverter(typeof(SourceMarker<>).MakeGenericType(typeToConvert));

		if (converter is SourceConverter<T> sourceConverter)
		{
			var source = sourceConverter.Read(ref reader, typeToConvert, options);
			return source.Source;
		}

		return default;
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		var converter = options.GetConverter(typeof(SourceMarker<>).MakeGenericType(typeof(T)));

		if (converter is SourceConverter<T> sourceConverter)
		{
			sourceConverter.Write(writer, new SourceMarker<T> { Source = value }, options);
		}
	}
}
