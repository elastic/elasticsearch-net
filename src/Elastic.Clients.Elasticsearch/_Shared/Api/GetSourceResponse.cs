// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

// TODO: Auto generate

[JsonConverter(typeof(GetSourceResponseConverterFactory))]
public partial class GetSourceResponse<TDocument>
{
	public TDocument Body { get; set; }
}

internal sealed partial class GetSourceResponseConverter<TDocument> : System.Text.Json.Serialization.JsonConverter<GetSourceResponse<TDocument>>
{
	public override GetSourceResponse<TDocument> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new GetSourceResponse<TDocument> { Body = reader.ReadValueEx<TDocument>(options, typeof(SourceMarker<TDocument>)) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetSourceResponse<TDocument> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValueEx(options, value.Body, typeof(SourceMarker<TDocument>));
	}
}

internal sealed partial class GetSourceResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(GetSourceResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(GetSourceResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}
