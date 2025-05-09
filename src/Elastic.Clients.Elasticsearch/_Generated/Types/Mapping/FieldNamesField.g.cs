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

namespace Elastic.Clients.Elasticsearch.Mapping;

internal sealed partial class FieldNamesFieldConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.FieldNamesField>
{
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");

	public override Elastic.Clients.Elasticsearch.Mapping.FieldNamesField Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propEnabled = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, null))
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
		return new Elastic.Clients.Elasticsearch.Mapping.FieldNamesField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Enabled = propEnabled.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.FieldNamesField value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldConverter))]
public sealed partial class FieldNamesField
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldNamesField(bool enabled)
	{
		Enabled = enabled;
	}
#if NET7_0_OR_GREATER
	public FieldNamesField()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FieldNamesField()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldNamesField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
}

public readonly partial struct FieldNamesFieldDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.FieldNamesField Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldNamesFieldDescriptor(Elastic.Clients.Elasticsearch.Mapping.FieldNamesField instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldNamesFieldDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.FieldNamesField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldDescriptor(Elastic.Clients.Elasticsearch.Mapping.FieldNamesField instance) => new Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.FieldNamesField(Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldDescriptor Enabled(bool value = true)
	{
		Instance.Enabled = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.FieldNamesField Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Mapping.FieldNamesFieldDescriptor(new Elastic.Clients.Elasticsearch.Mapping.FieldNamesField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}