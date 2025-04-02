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

internal sealed partial class KuromojiReadingFormTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropUseRomaji = System.Text.Json.JsonEncodedText.Encode("use_romaji");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propUseRomaji = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propUseRomaji.TryReadProperty(ref reader, options, PropUseRomaji, null))
			{
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
		return new Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			UseRomaji = propUseRomaji.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropUseRomaji, value.UseRomaji, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterConverter))]
public sealed partial class KuromojiReadingFormTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiReadingFormTokenFilter(bool useRomaji)
	{
		UseRomaji = useRomaji;
	}
#if NET7_0_OR_GREATER
	public KuromojiReadingFormTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KuromojiReadingFormTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KuromojiReadingFormTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string Type => "kuromoji_readingform";

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool UseRomaji { get; set; }
	public string? Version { get; set; }
}

public readonly partial struct KuromojiReadingFormTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiReadingFormTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiReadingFormTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter(Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor UseRomaji(bool value = true)
	{
		Instance.UseRomaji = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.KuromojiReadingFormTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}