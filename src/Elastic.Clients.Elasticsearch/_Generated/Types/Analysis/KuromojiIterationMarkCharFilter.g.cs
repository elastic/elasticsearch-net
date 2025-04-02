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

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class KuromojiIterationMarkCharFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropNormalizeKana = System.Text.Json.JsonEncodedText.Encode("normalize_kana");
	private static readonly System.Text.Json.JsonEncodedText PropNormalizeKanji = System.Text.Json.JsonEncodedText.Encode("normalize_kanji");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propNormalizeKana = default;
		LocalJsonValue<bool> propNormalizeKanji = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNormalizeKana.TryReadProperty(ref reader, options, PropNormalizeKana, null))
			{
				continue;
			}

			if (propNormalizeKanji.TryReadProperty(ref reader, options, PropNormalizeKanji, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			NormalizeKana = propNormalizeKana.Value,
			NormalizeKanji = propNormalizeKanji.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNormalizeKana, value.NormalizeKana, null, null);
		writer.WriteProperty(options, PropNormalizeKanji, value.NormalizeKanji, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterConverter))]
public sealed partial class KuromojiIterationMarkCharFilter : Elastic.Clients.Elasticsearch.Analysis.ICharFilter
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiIterationMarkCharFilter(bool normalizeKana, bool normalizeKanji)
	{
		NormalizeKana = normalizeKana;
		NormalizeKanji = normalizeKanji;
	}
#if NET7_0_OR_GREATER
	public KuromojiIterationMarkCharFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KuromojiIterationMarkCharFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KuromojiIterationMarkCharFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool NormalizeKana { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool NormalizeKanji { get; set; }

	public string Type => "kuromoji_iteration_mark";

	public string? Version { get; set; }
}

public readonly partial struct KuromojiIterationMarkCharFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiIterationMarkCharFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiIterationMarkCharFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter(Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor NormalizeKana(bool value = true)
	{
		Instance.NormalizeKana = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor NormalizeKanji(bool value = true)
	{
		Instance.NormalizeKanji = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.KuromojiIterationMarkCharFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}