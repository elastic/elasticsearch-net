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

namespace Elastic.Clients.Elasticsearch.Xpack;

internal sealed partial class MonitoringConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.Monitoring>
{
	private static readonly System.Text.Json.JsonEncodedText PropAvailable = System.Text.Json.JsonEncodedText.Encode("available");
	private static readonly System.Text.Json.JsonEncodedText PropCollectionEnabled = System.Text.Json.JsonEncodedText.Encode("collection_enabled");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropEnabledExporters = System.Text.Json.JsonEncodedText.Encode("enabled_exporters");

	public override Elastic.Clients.Elasticsearch.Xpack.Monitoring Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAvailable = default;
		LocalJsonValue<bool> propCollectionEnabled = default;
		LocalJsonValue<bool> propEnabled = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, long>> propEnabledExporters = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAvailable.TryReadProperty(ref reader, options, PropAvailable, null))
			{
				continue;
			}

			if (propCollectionEnabled.TryReadProperty(ref reader, options, PropCollectionEnabled, null))
			{
				continue;
			}

			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, null))
			{
				continue;
			}

			if (propEnabledExporters.TryReadProperty(ref reader, options, PropEnabledExporters, static System.Collections.Generic.IReadOnlyDictionary<string, long> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, long>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.Xpack.Monitoring(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Available = propAvailable.Value,
			CollectionEnabled = propCollectionEnabled.Value,
			Enabled = propEnabled.Value,
			EnabledExporters = propEnabledExporters.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.Monitoring value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAvailable, value.Available, null, null);
		writer.WriteProperty(options, PropCollectionEnabled, value.CollectionEnabled, null, null);
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteProperty(options, PropEnabledExporters, value.EnabledExporters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, long> v) => w.WriteDictionaryValue<string, long>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.MonitoringConverter))]
public sealed partial class Monitoring
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Monitoring(bool available, bool collectionEnabled, bool enabled, System.Collections.Generic.IReadOnlyDictionary<string, long> enabledExporters)
	{
		Available = available;
		CollectionEnabled = collectionEnabled;
		Enabled = enabled;
		EnabledExporters = enabledExporters;
	}
#if NET7_0_OR_GREATER
	public Monitoring()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Monitoring()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Monitoring(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Available { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool CollectionEnabled { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, long> EnabledExporters { get; set; }
}