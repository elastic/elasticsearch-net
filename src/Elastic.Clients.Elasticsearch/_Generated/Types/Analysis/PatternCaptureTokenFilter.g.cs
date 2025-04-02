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

internal sealed partial class PatternCaptureTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropPatterns = System.Text.Json.JsonEncodedText.Encode("patterns");
	private static readonly System.Text.Json.JsonEncodedText PropPreserveOriginal = System.Text.Json.JsonEncodedText.Encode("preserve_original");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propPatterns = default;
		LocalJsonValue<bool?> propPreserveOriginal = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPatterns.TryReadProperty(ref reader, options, PropPatterns, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propPreserveOriginal.TryReadProperty(ref reader, options, PropPreserveOriginal, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Patterns = propPatterns.Value,
			PreserveOriginal = propPreserveOriginal.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPatterns, value.Patterns, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropPreserveOriginal, value.PreserveOriginal, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterConverter))]
public sealed partial class PatternCaptureTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PatternCaptureTokenFilter(System.Collections.Generic.ICollection<string> patterns)
	{
		Patterns = patterns;
	}
#if NET7_0_OR_GREATER
	public PatternCaptureTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PatternCaptureTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PatternCaptureTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> Patterns { get; set; }
	public bool? PreserveOriginal { get; set; }

	public string Type => "pattern_capture";

	public string? Version { get; set; }
}

public readonly partial struct PatternCaptureTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PatternCaptureTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PatternCaptureTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter(Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor Patterns(System.Collections.Generic.ICollection<string> value)
	{
		Instance.Patterns = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor Patterns()
	{
		Instance.Patterns = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor Patterns(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.Patterns = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor Patterns(params string[] values)
	{
		Instance.Patterns = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor PreserveOriginal(bool? value = true)
	{
		Instance.PreserveOriginal = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.PatternCaptureTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}