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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class PluginStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.PluginStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropClassname = System.Text.Json.JsonEncodedText.Encode("classname");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropElasticsearchVersion = System.Text.Json.JsonEncodedText.Encode("elasticsearch_version");
	private static readonly System.Text.Json.JsonEncodedText PropExtendedPlugins = System.Text.Json.JsonEncodedText.Encode("extended_plugins");
	private static readonly System.Text.Json.JsonEncodedText PropHasNativeController = System.Text.Json.JsonEncodedText.Encode("has_native_controller");
	private static readonly System.Text.Json.JsonEncodedText PropJavaVersion = System.Text.Json.JsonEncodedText.Encode("java_version");
	private static readonly System.Text.Json.JsonEncodedText PropLicensed = System.Text.Json.JsonEncodedText.Encode("licensed");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.PluginStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClassname = default;
		LocalJsonValue<string> propDescription = default;
		LocalJsonValue<string> propElasticsearchVersion = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propExtendedPlugins = default;
		LocalJsonValue<bool> propHasNativeController = default;
		LocalJsonValue<string> propJavaVersion = default;
		LocalJsonValue<bool> propLicensed = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<string> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClassname.TryReadProperty(ref reader, options, PropClassname, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propElasticsearchVersion.TryReadProperty(ref reader, options, PropElasticsearchVersion, null))
			{
				continue;
			}

			if (propExtendedPlugins.TryReadProperty(ref reader, options, PropExtendedPlugins, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propHasNativeController.TryReadProperty(ref reader, options, PropHasNativeController, null))
			{
				continue;
			}

			if (propJavaVersion.TryReadProperty(ref reader, options, PropJavaVersion, null))
			{
				continue;
			}

			if (propLicensed.TryReadProperty(ref reader, options, PropLicensed, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
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
		return new Elastic.Clients.Elasticsearch.PluginStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Classname = propClassname.Value,
			Description = propDescription.Value,
			ElasticsearchVersion = propElasticsearchVersion.Value,
			ExtendedPlugins = propExtendedPlugins.Value,
			HasNativeController = propHasNativeController.Value,
			JavaVersion = propJavaVersion.Value,
			Licensed = propLicensed.Value,
			Name = propName.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.PluginStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClassname, value.Classname, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropElasticsearchVersion, value.ElasticsearchVersion, null, null);
		writer.WriteProperty(options, PropExtendedPlugins, value.ExtendedPlugins, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropHasNativeController, value.HasNativeController, null, null);
		writer.WriteProperty(options, PropJavaVersion, value.JavaVersion, null, null);
		writer.WriteProperty(options, PropLicensed, value.Licensed, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.PluginStatsConverter))]
public sealed partial class PluginStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PluginStats(string classname, string description, string elasticsearchVersion, System.Collections.Generic.IReadOnlyCollection<string> extendedPlugins, bool hasNativeController, string javaVersion, bool licensed, string name, string version)
	{
		Classname = classname;
		Description = description;
		ElasticsearchVersion = elasticsearchVersion;
		ExtendedPlugins = extendedPlugins;
		HasNativeController = hasNativeController;
		JavaVersion = javaVersion;
		Licensed = licensed;
		Name = name;
		Version = version;
	}
#if NET7_0_OR_GREATER
	public PluginStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PluginStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PluginStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Classname { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Description { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ElasticsearchVersion { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> ExtendedPlugins { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool HasNativeController { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JavaVersion { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Licensed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Version { get; set; }
}