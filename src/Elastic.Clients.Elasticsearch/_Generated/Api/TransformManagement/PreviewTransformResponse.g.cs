// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.TransformManagement;

internal sealed partial class PreviewTransformResponseConverter<TTransform> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformResponse<TTransform>>
{
	private static readonly System.Text.Json.JsonEncodedText PropGeneratedDestIndex = System.Text.Json.JsonEncodedText.Encode("generated_dest_index");
	private static readonly System.Text.Json.JsonEncodedText PropPreview = System.Text.Json.JsonEncodedText.Encode("preview");

	public override Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformResponse<TTransform> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexState> propGeneratedDestIndex = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<TTransform>> propPreview = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propGeneratedDestIndex.TryReadProperty(ref reader, options, PropGeneratedDestIndex, null))
			{
				continue;
			}

			if (propPreview.TryReadProperty(ref reader, options, PropPreview, static System.Collections.Generic.IReadOnlyCollection<TTransform> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<TTransform>(o, static TTransform (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<TTransform>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SourceMarker<TTransform>))!)!))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformResponse<TTransform>(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			GeneratedDestIndex = propGeneratedDestIndex.Value,
			Preview = propPreview.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformResponse<TTransform> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropGeneratedDestIndex, value.GeneratedDestIndex, null, null);
		writer.WriteProperty(options, PropPreview, value.Preview, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<TTransform> v) => w.WriteCollectionValue<TTransform>(o, v, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, TTransform v) => w.WriteValueEx<TTransform>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SourceMarker<TTransform>))));
		writer.WriteEndObject();
	}
}

internal sealed partial class PreviewTransformResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(PreviewTransformResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(PreviewTransformResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformResponseConverterFactory))]
public sealed partial class PreviewTransformResponse<TTransform> : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PreviewTransformResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PreviewTransformResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
required
#endif
Elastic.Clients.Elasticsearch.IndexManagement.IndexState GeneratedDestIndex { get; set; }
	public
#if NET7_0_OR_GREATER
required
#endif
System.Collections.Generic.IReadOnlyCollection<TTransform> Preview { get; set; }
}