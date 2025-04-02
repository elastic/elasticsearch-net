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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class PhraseSuggestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggest>
{
	private static readonly System.Text.Json.JsonEncodedText PropLength = System.Text.Json.JsonEncodedText.Encode("length");
	private static readonly System.Text.Json.JsonEncodedText PropOffset = System.Text.Json.JsonEncodedText.Encode("offset");
	private static readonly System.Text.Json.JsonEncodedText PropOptions = System.Text.Json.JsonEncodedText.Encode("options");
	private static readonly System.Text.Json.JsonEncodedText PropText = System.Text.Json.JsonEncodedText.Encode("text");

	public override Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propLength = default;
		LocalJsonValue<int> propOffset = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption>> propOptions = default;
		LocalJsonValue<string> propText = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLength.TryReadProperty(ref reader, options, PropLength, null))
			{
				continue;
			}

			if (propOffset.TryReadProperty(ref reader, options, PropOffset, null))
			{
				continue;
			}

			if (propOptions.TryReadProperty(ref reader, options, PropOptions, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption>(o, null)!))
			{
				continue;
			}

			if (propText.TryReadProperty(ref reader, options, PropText, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Length = propLength.Value,
			Offset = propOffset.Value,
			Options = propOptions.Value,
			Text = propText.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLength, value.Length, null, null);
		writer.WriteProperty(options, PropOffset, value.Offset, null, null);
		writer.WriteProperty(options, PropOptions, value.Options, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption> v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption>(o, v, null));
		writer.WriteProperty(options, PropText, value.Text, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestConverter))]
public sealed partial class PhraseSuggest : Elastic.Clients.Elasticsearch.Core.Search.ISuggest
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggest(int length, int offset, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption> options, string text)
	{
		Length = length;
		Offset = offset;
		Options = options;
		Text = text;
	}
#if NET7_0_OR_GREATER
	public PhraseSuggest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PhraseSuggest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PhraseSuggest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int Length { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Offset { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestOption> Options { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Text { get; set; }

	string Elastic.Clients.Elasticsearch.Core.Search.ISuggest.Type => "phrase";
}