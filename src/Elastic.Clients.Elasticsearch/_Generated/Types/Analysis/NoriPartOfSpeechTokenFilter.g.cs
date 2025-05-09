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

internal sealed partial class NoriPartOfSpeechTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropStoptags = System.Text.Json.JsonEncodedText.Encode("stoptags");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propStoptags = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propStoptags.TryReadProperty(ref reader, options, PropStoptags, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Stoptags = propStoptags.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropStoptags, value.Stoptags, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterConverter))]
public sealed partial class NoriPartOfSpeechTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public NoriPartOfSpeechTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public NoriPartOfSpeechTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NoriPartOfSpeechTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// An array of part-of-speech tags that should be removed.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Stoptags { get; set; }

	public string Type => "nori_part_of_speech";

	public string? Version { get; set; }
}

public readonly partial struct NoriPartOfSpeechTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NoriPartOfSpeechTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NoriPartOfSpeechTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter(Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// An array of part-of-speech tags that should be removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor Stoptags(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Stoptags = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of part-of-speech tags that should be removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor Stoptags(params string[] values)
	{
		Instance.Stoptags = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.NoriPartOfSpeechTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}