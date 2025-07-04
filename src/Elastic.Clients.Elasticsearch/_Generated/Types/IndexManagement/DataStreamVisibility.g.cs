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

internal sealed partial class DataStreamVisibilityConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowCustomRouting = System.Text.Json.JsonEncodedText.Encode("allow_custom_routing");
	private static readonly System.Text.Json.JsonEncodedText PropHidden = System.Text.Json.JsonEncodedText.Encode("hidden");

	public override Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowCustomRouting = default;
		LocalJsonValue<bool?> propHidden = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowCustomRouting.TryReadProperty(ref reader, options, PropAllowCustomRouting, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propHidden.TryReadProperty(ref reader, options, PropHidden, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowCustomRouting = propAllowCustomRouting.Value,
			Hidden = propHidden.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowCustomRouting, value.AllowCustomRouting, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropHidden, value.Hidden, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityConverter))]
public sealed partial class DataStreamVisibility
{
#if NET7_0_OR_GREATER
	public DataStreamVisibility()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataStreamVisibility()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataStreamVisibility(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public bool? AllowCustomRouting { get; set; }
	public bool? Hidden { get; set; }
}

public readonly partial struct DataStreamVisibilityDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamVisibilityDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamVisibilityDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility instance) => new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor AllowCustomRouting(bool? value = true)
	{
		Instance.AllowCustomRouting = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor Hidden(bool? value = true)
	{
		Instance.Hidden = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibilityDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamVisibility(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}