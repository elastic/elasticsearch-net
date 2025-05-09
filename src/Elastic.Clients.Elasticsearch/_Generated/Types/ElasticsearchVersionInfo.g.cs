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

internal sealed partial class ElasticsearchVersionInfoConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo>
{
	private static readonly System.Text.Json.JsonEncodedText PropBuildDate = System.Text.Json.JsonEncodedText.Encode("build_date");
	private static readonly System.Text.Json.JsonEncodedText PropBuildFlavor = System.Text.Json.JsonEncodedText.Encode("build_flavor");
	private static readonly System.Text.Json.JsonEncodedText PropBuildHash = System.Text.Json.JsonEncodedText.Encode("build_hash");
	private static readonly System.Text.Json.JsonEncodedText PropBuildSnapshot = System.Text.Json.JsonEncodedText.Encode("build_snapshot");
	private static readonly System.Text.Json.JsonEncodedText PropBuildType = System.Text.Json.JsonEncodedText.Encode("build_type");
	private static readonly System.Text.Json.JsonEncodedText PropLuceneVersion = System.Text.Json.JsonEncodedText.Encode("lucene_version");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumIndexCompatibilityVersion = System.Text.Json.JsonEncodedText.Encode("minimum_index_compatibility_version");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumWireCompatibilityVersion = System.Text.Json.JsonEncodedText.Encode("minimum_wire_compatibility_version");
	private static readonly System.Text.Json.JsonEncodedText PropNumber = System.Text.Json.JsonEncodedText.Encode("number");

	public override Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.DateTimeOffset> propBuildDate = default;
		LocalJsonValue<string> propBuildFlavor = default;
		LocalJsonValue<string> propBuildHash = default;
		LocalJsonValue<bool> propBuildSnapshot = default;
		LocalJsonValue<string> propBuildType = default;
		LocalJsonValue<string> propLuceneVersion = default;
		LocalJsonValue<string> propMinimumIndexCompatibilityVersion = default;
		LocalJsonValue<string> propMinimumWireCompatibilityVersion = default;
		LocalJsonValue<string> propNumber = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBuildDate.TryReadProperty(ref reader, options, PropBuildDate, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propBuildFlavor.TryReadProperty(ref reader, options, PropBuildFlavor, null))
			{
				continue;
			}

			if (propBuildHash.TryReadProperty(ref reader, options, PropBuildHash, null))
			{
				continue;
			}

			if (propBuildSnapshot.TryReadProperty(ref reader, options, PropBuildSnapshot, null))
			{
				continue;
			}

			if (propBuildType.TryReadProperty(ref reader, options, PropBuildType, null))
			{
				continue;
			}

			if (propLuceneVersion.TryReadProperty(ref reader, options, PropLuceneVersion, null))
			{
				continue;
			}

			if (propMinimumIndexCompatibilityVersion.TryReadProperty(ref reader, options, PropMinimumIndexCompatibilityVersion, null))
			{
				continue;
			}

			if (propMinimumWireCompatibilityVersion.TryReadProperty(ref reader, options, PropMinimumWireCompatibilityVersion, null))
			{
				continue;
			}

			if (propNumber.TryReadProperty(ref reader, options, PropNumber, null))
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
		return new Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BuildDate = propBuildDate.Value,
			BuildFlavor = propBuildFlavor.Value,
			BuildHash = propBuildHash.Value,
			BuildSnapshot = propBuildSnapshot.Value,
			BuildType = propBuildType.Value,
			LuceneVersion = propLuceneVersion.Value,
			MinimumIndexCompatibilityVersion = propMinimumIndexCompatibilityVersion.Value,
			MinimumWireCompatibilityVersion = propMinimumWireCompatibilityVersion.Value,
			Number = propNumber.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBuildDate, value.BuildDate, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropBuildFlavor, value.BuildFlavor, null, null);
		writer.WriteProperty(options, PropBuildHash, value.BuildHash, null, null);
		writer.WriteProperty(options, PropBuildSnapshot, value.BuildSnapshot, null, null);
		writer.WriteProperty(options, PropBuildType, value.BuildType, null, null);
		writer.WriteProperty(options, PropLuceneVersion, value.LuceneVersion, null, null);
		writer.WriteProperty(options, PropMinimumIndexCompatibilityVersion, value.MinimumIndexCompatibilityVersion, null, null);
		writer.WriteProperty(options, PropMinimumWireCompatibilityVersion, value.MinimumWireCompatibilityVersion, null, null);
		writer.WriteProperty(options, PropNumber, value.Number, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ElasticsearchVersionInfoConverter))]
public sealed partial class ElasticsearchVersionInfo
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElasticsearchVersionInfo(System.DateTimeOffset buildDate, string buildFlavor, string buildHash, bool buildSnapshot, string buildType, string luceneVersion, string minimumIndexCompatibilityVersion, string minimumWireCompatibilityVersion, string number)
	{
		BuildDate = buildDate;
		BuildFlavor = buildFlavor;
		BuildHash = buildHash;
		BuildSnapshot = buildSnapshot;
		BuildType = buildType;
		LuceneVersion = luceneVersion;
		MinimumIndexCompatibilityVersion = minimumIndexCompatibilityVersion;
		MinimumWireCompatibilityVersion = minimumWireCompatibilityVersion;
		Number = number;
	}
#if NET7_0_OR_GREATER
	public ElasticsearchVersionInfo()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ElasticsearchVersionInfo()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ElasticsearchVersionInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch Git commit's date.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset BuildDate { get; set; }

	/// <summary>
	/// <para>
	/// The build flavor. For example, <c>default</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string BuildFlavor { get; set; }

	/// <summary>
	/// <para>
	/// The Elasticsearch Git commit's SHA hash.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string BuildHash { get; set; }

	/// <summary>
	/// <para>
	/// Indicates whether the Elasticsearch build was a snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool BuildSnapshot { get; set; }

	/// <summary>
	/// <para>
	/// The build type that corresponds to how Elasticsearch was installed.
	/// For example, <c>docker</c>, <c>rpm</c>, or <c>tar</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string BuildType { get; set; }

	/// <summary>
	/// <para>
	/// The version number of Elasticsearch's underlying Lucene software.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string LuceneVersion { get; set; }

	/// <summary>
	/// <para>
	/// The minimum index version with which the responding node can read from disk.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string MinimumIndexCompatibilityVersion { get; set; }

	/// <summary>
	/// <para>
	/// The minimum node version with which the responding node can communicate.
	/// Also the minimum version from which you can perform a rolling upgrade.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string MinimumWireCompatibilityVersion { get; set; }

	/// <summary>
	/// <para>
	/// The Elasticsearch version number.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Number { get; set; }
}