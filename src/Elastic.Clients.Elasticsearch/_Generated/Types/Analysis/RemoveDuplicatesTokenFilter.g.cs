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

internal sealed partial class RemoveDuplicatesTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
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
		return new Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterConverter))]
public sealed partial class RemoveDuplicatesTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public RemoveDuplicatesTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RemoveDuplicatesTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RemoveDuplicatesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string Type => "remove_duplicates";

	public string? Version { get; set; }
}

public readonly partial struct RemoveDuplicatesTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveDuplicatesTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveDuplicatesTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter(Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.RemoveDuplicatesTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}