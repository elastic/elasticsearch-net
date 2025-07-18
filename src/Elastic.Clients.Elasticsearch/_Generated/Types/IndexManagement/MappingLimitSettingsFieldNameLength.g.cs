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

internal sealed partial class MappingLimitSettingsFieldNameLengthConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength>
{
	private static readonly System.Text.Json.JsonEncodedText PropLimit = System.Text.Json.JsonEncodedText.Encode("limit");

	public override Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propLimit = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLimit.TryReadProperty(ref reader, options, PropLimit, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Limit = propLimit.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLimit, value.Limit, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthConverter))]
public sealed partial class MappingLimitSettingsFieldNameLength
{
#if NET7_0_OR_GREATER
	public MappingLimitSettingsFieldNameLength()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public MappingLimitSettingsFieldNameLength()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MappingLimitSettingsFieldNameLength(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Setting for the maximum length of a field name. This setting isn’t really something that addresses mappings explosion but
	/// might still be useful if you want to limit the field length. It usually shouldn’t be necessary to set this setting. The
	/// default is okay unless a user starts to add a huge number of fields with really long names. Default is <c>Long.MAX_VALUE</c> (no limit).
	/// </para>
	/// </summary>
	public long? Limit { get; set; }
}

public readonly partial struct MappingLimitSettingsFieldNameLengthDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MappingLimitSettingsFieldNameLengthDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MappingLimitSettingsFieldNameLengthDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength instance) => new Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Setting for the maximum length of a field name. This setting isn’t really something that addresses mappings explosion but
	/// might still be useful if you want to limit the field length. It usually shouldn’t be necessary to set this setting. The
	/// default is okay unless a user starts to add a huge number of fields with really long names. Default is <c>Long.MAX_VALUE</c> (no limit).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthDescriptor Limit(long? value)
	{
		Instance.Limit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLengthDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}