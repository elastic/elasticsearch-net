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

internal sealed partial class CharGroupTokenizerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxTokenLength = System.Text.Json.JsonEncodedText.Encode("max_token_length");
	private static readonly System.Text.Json.JsonEncodedText PropTokenizeOnChars = System.Text.Json.JsonEncodedText.Encode("tokenize_on_chars");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propMaxTokenLength = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propTokenizeOnChars = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxTokenLength.TryReadProperty(ref reader, options, PropMaxTokenLength, null))
			{
				continue;
			}

			if (propTokenizeOnChars.TryReadProperty(ref reader, options, PropTokenizeOnChars, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxTokenLength = propMaxTokenLength.Value,
			TokenizeOnChars = propTokenizeOnChars.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxTokenLength, value.MaxTokenLength, null, null);
		writer.WriteProperty(options, PropTokenizeOnChars, value.TokenizeOnChars, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerConverter))]
public sealed partial class CharGroupTokenizer : Elastic.Clients.Elasticsearch.Analysis.ITokenizer
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CharGroupTokenizer(System.Collections.Generic.ICollection<string> tokenizeOnChars)
	{
		TokenizeOnChars = tokenizeOnChars;
	}
#if NET7_0_OR_GREATER
	public CharGroupTokenizer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CharGroupTokenizer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CharGroupTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public int? MaxTokenLength { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> TokenizeOnChars { get; set; }

	public string Type => "char_group";

	public string? Version { get; set; }
}

public readonly partial struct CharGroupTokenizerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CharGroupTokenizerDescriptor(Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CharGroupTokenizerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor(Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer instance) => new Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer(Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor MaxTokenLength(int? value)
	{
		Instance.MaxTokenLength = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor TokenizeOnChars(System.Collections.Generic.ICollection<string> value)
	{
		Instance.TokenizeOnChars = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor TokenizeOnChars()
	{
		Instance.TokenizeOnChars = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor TokenizeOnChars(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.TokenizeOnChars = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor TokenizeOnChars(params string[] values)
	{
		Instance.TokenizeOnChars = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}