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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class PhraseSuggestCollateQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");

	public override Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propId = default;
		LocalJsonValue<string?> propSource = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Id = propId.Value,
			Source = propSource.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryConverter))]
public sealed partial class PhraseSuggestCollateQuery
{
#if NET7_0_OR_GREATER
	public PhraseSuggestCollateQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public PhraseSuggestCollateQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PhraseSuggestCollateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The search template ID.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	/// <summary>
	/// <para>
	/// The query source.
	/// </para>
	/// </summary>
	public string? Source { get; set; }
}

public readonly partial struct PhraseSuggestCollateQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggestCollateQueryDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PhraseSuggestCollateQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery instance) => new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The search template ID.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor Source(string? value)
	{
		Instance.Source = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQueryDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}