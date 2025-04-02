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

namespace Elastic.Clients.Elasticsearch.Snapshot;

internal sealed partial class SnapshotResponseItemConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.SnapshotResponseItem>
{
	private static readonly System.Text.Json.JsonEncodedText PropError = System.Text.Json.JsonEncodedText.Encode("error");
	private static readonly System.Text.Json.JsonEncodedText PropRepository = System.Text.Json.JsonEncodedText.Encode("repository");
	private static readonly System.Text.Json.JsonEncodedText PropSnapshots = System.Text.Json.JsonEncodedText.Encode("snapshots");

	public override Elastic.Clients.Elasticsearch.Snapshot.SnapshotResponseItem Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.ErrorCause?> propError = default;
		LocalJsonValue<string> propRepository = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Snapshot.SnapshotInfo>?> propSnapshots = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propError.TryReadProperty(ref reader, options, PropError, null))
			{
				continue;
			}

			if (propRepository.TryReadProperty(ref reader, options, PropRepository, null))
			{
				continue;
			}

			if (propSnapshots.TryReadProperty(ref reader, options, PropSnapshots, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Snapshot.SnapshotInfo>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Snapshot.SnapshotInfo>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Snapshot.SnapshotResponseItem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Error = propError.Value,
			Repository = propRepository.Value,
			Snapshots = propSnapshots.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.SnapshotResponseItem value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropError, value.Error, null, null);
		writer.WriteProperty(options, PropRepository, value.Repository, null, null);
		writer.WriteProperty(options, PropSnapshots, value.Snapshots, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Snapshot.SnapshotInfo>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Snapshot.SnapshotInfo>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.SnapshotResponseItemConverter))]
public sealed partial class SnapshotResponseItem
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SnapshotResponseItem(string repository)
	{
		Repository = repository;
	}
#if NET7_0_OR_GREATER
	public SnapshotResponseItem()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SnapshotResponseItem()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SnapshotResponseItem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.ErrorCause? Error { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Repository { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Snapshot.SnapshotInfo>? Snapshots { get; set; }
}