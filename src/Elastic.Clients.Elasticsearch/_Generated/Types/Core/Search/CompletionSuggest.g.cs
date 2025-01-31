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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class CompletionSuggestConverter<TDocument> : System.Text.Json.Serialization.JsonConverter<CompletionSuggest<TDocument>>
{
	private static readonly System.Text.Json.JsonEncodedText PropLength = System.Text.Json.JsonEncodedText.Encode("length");
	private static readonly System.Text.Json.JsonEncodedText PropOffset = System.Text.Json.JsonEncodedText.Encode("offset");
	private static readonly System.Text.Json.JsonEncodedText PropOptions = System.Text.Json.JsonEncodedText.Encode("options");
	private static readonly System.Text.Json.JsonEncodedText PropText = System.Text.Json.JsonEncodedText.Encode("text");

	public override CompletionSuggest<TDocument> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propLength = default;
		LocalJsonValue<int> propOffset = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggestOption<TDocument>>> propOptions = default;
		LocalJsonValue<string> propText = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLength.TryRead(ref reader, options, PropLength))
			{
				continue;
			}

			if (propOffset.TryRead(ref reader, options, PropOffset))
			{
				continue;
			}

			if (propOptions.TryRead(ref reader, options, PropOptions, typeof(SingleOrManyMarker<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggestOption<TDocument>>, Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggestOption<TDocument>>)))
			{
				continue;
			}

			if (propText.TryRead(ref reader, options, PropText))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new CompletionSuggest<TDocument>
		{
			Length = propLength.Value
,
			Offset = propOffset.Value
,
			Options = propOptions.Value
,
			Text = propText.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, CompletionSuggest<TDocument> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLength, value.Length);
		writer.WriteProperty(options, PropOffset, value.Offset);
		writer.WriteProperty(options, PropOptions, value.Options, null, typeof(SingleOrManyMarker<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggestOption<TDocument>>, Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggestOption<TDocument>>));
		writer.WriteProperty(options, PropText, value.Text);
		writer.WriteEndObject();
	}
}

internal sealed partial class CompletionSuggestConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(CompletionSuggest<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(CompletionSuggestConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[JsonConverter(typeof(CompletionSuggestConverterFactory))]
public sealed partial class CompletionSuggest<TDocument> : ISuggest
{
	public int Length { get; init; }
	public int Offset { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggestOption<TDocument>> Options { get; init; }
	public string Text { get; init; }

	string ISuggest.Type => "completion";
}