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

internal sealed partial class UaxEmailUrlTokenizerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxTokenLength = System.Text.Json.JsonEncodedText.Encode("max_token_length");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propMaxTokenLength = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxTokenLength.TryReadProperty(ref reader, options, PropMaxTokenLength, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
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
		return new Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxTokenLength = propMaxTokenLength.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxTokenLength, value.MaxTokenLength, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerConverter))]
public sealed partial class UaxEmailUrlTokenizer : Elastic.Clients.Elasticsearch.Analysis.ITokenizer
{
#if NET7_0_OR_GREATER
	public UaxEmailUrlTokenizer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public UaxEmailUrlTokenizer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UaxEmailUrlTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public int? MaxTokenLength { get; set; }

	public string Type => "uax_url_email";

	public string? Version { get; set; }
}

public readonly partial struct UaxEmailUrlTokenizerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UaxEmailUrlTokenizerDescriptor(Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UaxEmailUrlTokenizerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor(Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer instance) => new Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer(Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor MaxTokenLength(int? value)
	{
		Instance.MaxTokenLength = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}