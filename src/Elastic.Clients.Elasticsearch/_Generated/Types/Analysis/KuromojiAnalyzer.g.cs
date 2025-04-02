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

internal sealed partial class KuromojiAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropMode = System.Text.Json.JsonEncodedText.Encode("mode");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropUserDictionary = System.Text.Json.JsonEncodedText.Encode("user_dictionary");

	public override Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizationMode> propMode = default;
		LocalJsonValue<string?> propUserDictionary = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMode.TryReadProperty(ref reader, options, PropMode, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propUserDictionary.TryReadProperty(ref reader, options, PropUserDictionary, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Mode = propMode.Value,
			UserDictionary = propUserDictionary.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMode, value.Mode, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropUserDictionary, value.UserDictionary, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerConverter))]
public sealed partial class KuromojiAnalyzer : Elastic.Clients.Elasticsearch.Analysis.IAnalyzer
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiAnalyzer(Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizationMode mode)
	{
		Mode = mode;
	}
#if NET7_0_OR_GREATER
	public KuromojiAnalyzer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KuromojiAnalyzer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KuromojiAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizationMode Mode { get; set; }

	public string Type => "kuromoji";

	public string? UserDictionary { get; set; }
}

public readonly partial struct KuromojiAnalyzerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KuromojiAnalyzerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer instance) => new Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer(Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor Mode(Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizationMode value)
	{
		Instance.Mode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor UserDictionary(string? value)
	{
		Instance.UserDictionary = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.KuromojiAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}