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

internal sealed partial class SimplePatternSplitTokenizerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer>
{
	private static readonly System.Text.Json.JsonEncodedText PropPattern = System.Text.Json.JsonEncodedText.Encode("pattern");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propPattern = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPattern.TryReadProperty(ref reader, options, PropPattern, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Pattern = propPattern.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPattern, value.Pattern, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerConverter))]
public sealed partial class SimplePatternSplitTokenizer : Elastic.Clients.Elasticsearch.Analysis.ITokenizer
{
#if NET7_0_OR_GREATER
	public SimplePatternSplitTokenizer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SimplePatternSplitTokenizer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SimplePatternSplitTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? Pattern { get; set; }

	public string Type => "simple_pattern_split";

	public string? Version { get; set; }
}

public readonly partial struct SimplePatternSplitTokenizerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SimplePatternSplitTokenizerDescriptor(Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SimplePatternSplitTokenizerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor(Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer instance) => new Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer(Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor Pattern(string? value)
	{
		Instance.Pattern = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}