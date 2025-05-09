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

internal sealed partial class CleanupRepositoryResultsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.CleanupRepositoryResults>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeletedBlobs = System.Text.Json.JsonEncodedText.Encode("deleted_blobs");
	private static readonly System.Text.Json.JsonEncodedText PropDeletedBytes = System.Text.Json.JsonEncodedText.Encode("deleted_bytes");

	public override Elastic.Clients.Elasticsearch.Snapshot.CleanupRepositoryResults Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propDeletedBlobs = default;
		LocalJsonValue<long> propDeletedBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDeletedBlobs.TryReadProperty(ref reader, options, PropDeletedBlobs, null))
			{
				continue;
			}

			if (propDeletedBytes.TryReadProperty(ref reader, options, PropDeletedBytes, null))
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
		return new Elastic.Clients.Elasticsearch.Snapshot.CleanupRepositoryResults(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DeletedBlobs = propDeletedBlobs.Value,
			DeletedBytes = propDeletedBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.CleanupRepositoryResults value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDeletedBlobs, value.DeletedBlobs, null, null);
		writer.WriteProperty(options, PropDeletedBytes, value.DeletedBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.CleanupRepositoryResultsConverter))]
public sealed partial class CleanupRepositoryResults
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CleanupRepositoryResults(long deletedBlobs, long deletedBytes)
	{
		DeletedBlobs = deletedBlobs;
		DeletedBytes = deletedBytes;
	}
#if NET7_0_OR_GREATER
	public CleanupRepositoryResults()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CleanupRepositoryResults()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CleanupRepositoryResults(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of binary large objects (blobs) removed from the snapshot repository during cleanup operations.
	/// A non-zero value indicates that unreferenced blobs were found and subsequently cleaned up.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long DeletedBlobs { get; set; }

	/// <summary>
	/// <para>
	/// The number of bytes freed by cleanup operations.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long DeletedBytes { get; set; }
}