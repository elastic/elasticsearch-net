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

internal sealed partial class MultiplexerTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropFilters = System.Text.Json.JsonEncodedText.Encode("filters");
	private static readonly System.Text.Json.JsonEncodedText PropPreserveOriginal = System.Text.Json.JsonEncodedText.Encode("preserve_original");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propFilters = default;
		LocalJsonValue<bool?> propPreserveOriginal = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFilters.TryReadProperty(ref reader, options, PropFilters, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propPreserveOriginal.TryReadProperty(ref reader, options, PropPreserveOriginal, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
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
		return new Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Filters = propFilters.Value,
			PreserveOriginal = propPreserveOriginal.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFilters, value.Filters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropPreserveOriginal, value.PreserveOriginal, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterConverter))]
public sealed partial class MultiplexerTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiplexerTokenFilter(System.Collections.Generic.ICollection<string> filters)
	{
		Filters = filters;
	}
#if NET7_0_OR_GREATER
	public MultiplexerTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MultiplexerTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MultiplexerTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A list of token filters to apply to incoming tokens.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> Filters { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then emit the original token in addition to the filtered tokens.
	/// </para>
	/// </summary>
	public bool? PreserveOriginal { get; set; }

	public string Type => "multiplexer";

	public string? Version { get; set; }
}

public readonly partial struct MultiplexerTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiplexerTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiplexerTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter(Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A list of token filters to apply to incoming tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor Filters(System.Collections.Generic.ICollection<string> value)
	{
		Instance.Filters = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of token filters to apply to incoming tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor Filters(params string[] values)
	{
		Instance.Filters = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then emit the original token in addition to the filtered tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor PreserveOriginal(bool? value = true)
	{
		Instance.PreserveOriginal = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.MultiplexerTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}