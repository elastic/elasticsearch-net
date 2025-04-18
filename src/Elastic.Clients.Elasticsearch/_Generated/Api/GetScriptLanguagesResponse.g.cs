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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class GetScriptLanguagesResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.GetScriptLanguagesResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropLanguageContexts = System.Text.Json.JsonEncodedText.Encode("language_contexts");
	private static readonly System.Text.Json.JsonEncodedText PropTypesAllowed = System.Text.Json.JsonEncodedText.Encode("types_allowed");

	public override Elastic.Clients.Elasticsearch.GetScriptLanguagesResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.GetScriptLanguages.LanguageContext>> propLanguageContexts = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propTypesAllowed = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLanguageContexts.TryReadProperty(ref reader, options, PropLanguageContexts, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.GetScriptLanguages.LanguageContext> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.GetScriptLanguages.LanguageContext>(o, null)!))
			{
				continue;
			}

			if (propTypesAllowed.TryReadProperty(ref reader, options, PropTypesAllowed, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.GetScriptLanguagesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			LanguageContexts = propLanguageContexts.Value,
			TypesAllowed = propTypesAllowed.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.GetScriptLanguagesResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLanguageContexts, value.LanguageContexts, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.GetScriptLanguages.LanguageContext> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.GetScriptLanguages.LanguageContext>(o, v, null));
		writer.WriteProperty(options, PropTypesAllowed, value.TypesAllowed, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.GetScriptLanguagesResponseConverter))]
public sealed partial class GetScriptLanguagesResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetScriptLanguagesResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetScriptLanguagesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.GetScriptLanguages.LanguageContext> LanguageContexts { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<string> TypesAllowed { get; set; }
}