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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class IndexVersioningConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning>
{
	private static readonly System.Text.Json.JsonEncodedText PropCreated = System.Text.Json.JsonEncodedText.Encode("created");
	private static readonly System.Text.Json.JsonEncodedText PropCreatedString = System.Text.Json.JsonEncodedText.Encode("created_string");

	public override Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propCreated = default;
		LocalJsonValue<string?> propCreatedString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCreated.TryReadProperty(ref reader, options, PropCreated, null))
			{
				continue;
			}

			if (propCreatedString.TryReadProperty(ref reader, options, PropCreatedString, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Created = propCreated.Value,
			CreatedString = propCreatedString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCreated, value.Created, null, null);
		writer.WriteProperty(options, PropCreatedString, value.CreatedString, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningConverter))]
public sealed partial class IndexVersioning
{
#if NET7_0_OR_GREATER
	public IndexVersioning()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public IndexVersioning()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IndexVersioning(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? Created { get; set; }
	public string? CreatedString { get; set; }
}

public readonly partial struct IndexVersioningDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexVersioningDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexVersioningDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning instance) => new Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning(Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor Created(string? value)
	{
		Instance.Created = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor CreatedString(string? value)
	{
		Instance.CreatedString = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioningDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.IndexVersioning(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}