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

internal sealed partial class KeepTypesTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropMode = System.Text.Json.JsonEncodedText.Encode("mode");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropTypes = System.Text.Json.JsonEncodedText.Encode("types");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.KeepTypesMode?> propMode = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propTypes = default;
		LocalJsonValue<string?> propVersion = default;
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

			if (propTypes.TryReadProperty(ref reader, options, PropTypes, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Mode = propMode.Value,
			Types = propTypes.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMode, value.Mode, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropTypes, value.Types, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterConverter))]
public sealed partial class KeepTypesTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public KeepTypesTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public KeepTypesTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KeepTypesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KeepTypesMode? Mode { get; set; }

	public string Type => "keep_types";

	public System.Collections.Generic.ICollection<string>? Types { get; set; }
	public string? Version { get; set; }
}

public readonly partial struct KeepTypesTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeepTypesTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KeepTypesTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter(Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor Mode(Elastic.Clients.Elasticsearch.Analysis.KeepTypesMode? value)
	{
		Instance.Mode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor Types(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Types = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor Types(params string[] values)
	{
		Instance.Types = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.KeepTypesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}