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

internal sealed partial class DataStreamTimestampConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp>
{
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");

	public override Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Enabled = propEnabled.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampConverter))]
public sealed partial class DataStreamTimestamp
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamTimestamp(bool enabled)
	{
		Enabled = enabled;
	}
#if NET7_0_OR_GREATER
	public DataStreamTimestamp()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataStreamTimestamp()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataStreamTimestamp(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
}

public readonly partial struct DataStreamTimestampDescriptor
{
	internal Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamTimestampDescriptor(Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamTimestampDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampDescriptor(Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp instance) => new Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp(Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampDescriptor Enabled(bool value = true)
	{
		Instance.Enabled = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp Build(System.Action<Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestampDescriptor(new Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}