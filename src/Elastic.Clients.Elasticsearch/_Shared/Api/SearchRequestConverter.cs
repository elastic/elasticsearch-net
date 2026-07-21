// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class SearchRequestOfTConverterFactory :
	JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(SearchRequest<>);

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();

#pragma warning disable IL3050
		return (JsonConverter)Activator.CreateInstance(typeof(SearchRequestOfTConverter<>).MakeGenericType(args[0]));
#pragma warning restore IL3050
	}
}

public sealed class SearchRequestOfTConverter<T> :
	JsonConverter<SearchRequest<T>>
{
	public override SearchRequest<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		throw new NotSupportedException();

	public override void Write(Utf8JsonWriter writer, SearchRequest<T> value, JsonSerializerOptions options) =>
		writer.WriteValue(options, (SearchRequest)value);
}
