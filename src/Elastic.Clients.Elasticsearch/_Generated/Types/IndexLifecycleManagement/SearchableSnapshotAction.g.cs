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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

public sealed partial class SearchableSnapshotAction
{
	[JsonInclude, JsonPropertyName("force_merge_index")]
	public bool? ForceMergeIndex { get; set; }
	[JsonInclude, JsonPropertyName("snapshot_repository")]
	public string SnapshotRepository { get; set; }
}

public sealed partial class SearchableSnapshotActionDescriptor : SerializableDescriptor<SearchableSnapshotActionDescriptor>
{
	internal SearchableSnapshotActionDescriptor(Action<SearchableSnapshotActionDescriptor> configure) => configure.Invoke(this);

	public SearchableSnapshotActionDescriptor() : base()
	{
	}

	private bool? ForceMergeIndexValue { get; set; }
	private string SnapshotRepositoryValue { get; set; }

	public SearchableSnapshotActionDescriptor ForceMergeIndex(bool? forceMergeIndex = true)
	{
		ForceMergeIndexValue = forceMergeIndex;
		return Self;
	}

	public SearchableSnapshotActionDescriptor SnapshotRepository(string snapshotRepository)
	{
		SnapshotRepositoryValue = snapshotRepository;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ForceMergeIndexValue.HasValue)
		{
			writer.WritePropertyName("force_merge_index");
			writer.WriteBooleanValue(ForceMergeIndexValue.Value);
		}

		writer.WritePropertyName("snapshot_repository");
		writer.WriteStringValue(SnapshotRepositoryValue);
		writer.WriteEndObject();
	}
}