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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Snapshot
{
	public sealed class SnapshotRestoreRequestParameters : RequestParameters<SnapshotRestoreRequestParameters>
	{
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
	}

	public sealed partial class SnapshotRestoreRequest : PlainRequestBase<SnapshotRestoreRequestParameters>
	{
		public SnapshotRestoreRequest(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotRestore;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }

		[JsonInclude]
		[JsonPropertyName("ignore_index_settings")]
		public IEnumerable<string>? IgnoreIndexSettings { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_unavailable")]
		public bool? IgnoreUnavailable { get; set; }

		[JsonInclude]
		[JsonPropertyName("include_aliases")]
		public bool? IncludeAliases { get; set; }

		[JsonInclude]
		[JsonPropertyName("include_global_state")]
		public bool? IncludeGlobalState { get; set; }

		[JsonInclude]
		[JsonPropertyName("index_settings")]
		public Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? IndexSettings { get; set; }

		[JsonInclude]
		[JsonPropertyName("indices")]
		public Elastic.Clients.Elasticsearch.Indices? Indices { get; set; }

		[JsonInclude]
		[JsonPropertyName("partial")]
		public bool? Partial { get; set; }

		[JsonInclude]
		[JsonPropertyName("rename_pattern")]
		public string? RenamePattern { get; set; }

		[JsonInclude]
		[JsonPropertyName("rename_replacement")]
		public string? RenameReplacement { get; set; }
	}

	public sealed partial class SnapshotRestoreRequestDescriptor<TDocument> : RequestDescriptorBase<SnapshotRestoreRequestDescriptor<TDocument>, SnapshotRestoreRequestParameters>
	{
		internal SnapshotRestoreRequestDescriptor(Action<SnapshotRestoreRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public SnapshotRestoreRequestDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		internal SnapshotRestoreRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotRestore;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public SnapshotRestoreRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
		public SnapshotRestoreRequestDescriptor<TDocument> WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);
		public SnapshotRestoreRequestDescriptor<TDocument> Repository(Elastic.Clients.Elasticsearch.Name repository)
		{
			RouteValues.Required("repository", repository);
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> Snapshot(Elastic.Clients.Elasticsearch.Name snapshot)
		{
			RouteValues.Required("snapshot", snapshot);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? IndexSettingsValue { get; set; }

		private IndexManagement.IndexSettingsDescriptor<TDocument> IndexSettingsDescriptor { get; set; }

		private Action<IndexManagement.IndexSettingsDescriptor<TDocument>> IndexSettingsDescriptorAction { get; set; }

		private IEnumerable<string>? IgnoreIndexSettingsValue { get; set; }

		private bool? IgnoreUnavailableValue { get; set; }

		private bool? IncludeAliasesValue { get; set; }

		private bool? IncludeGlobalStateValue { get; set; }

		private Elastic.Clients.Elasticsearch.Indices? IndicesValue { get; set; }

		private bool? PartialValue { get; set; }

		private string? RenamePatternValue { get; set; }

		private string? RenameReplacementValue { get; set; }

		public SnapshotRestoreRequestDescriptor<TDocument> IndexSettings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? indexSettings)
		{
			IndexSettingsDescriptor = null;
			IndexSettingsDescriptorAction = null;
			IndexSettingsValue = indexSettings;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> IndexSettings(IndexManagement.IndexSettingsDescriptor<TDocument> descriptor)
		{
			IndexSettingsValue = null;
			IndexSettingsDescriptorAction = null;
			IndexSettingsDescriptor = descriptor;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> IndexSettings(Action<IndexManagement.IndexSettingsDescriptor<TDocument>> configure)
		{
			IndexSettingsValue = null;
			IndexSettingsDescriptor = null;
			IndexSettingsDescriptorAction = configure;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> IgnoreIndexSettings(IEnumerable<string>? ignoreIndexSettings)
		{
			IgnoreIndexSettingsValue = ignoreIndexSettings;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true)
		{
			IgnoreUnavailableValue = ignoreUnavailable;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> IncludeAliases(bool? includeAliases = true)
		{
			IncludeAliasesValue = includeAliases;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> IncludeGlobalState(bool? includeGlobalState = true)
		{
			IncludeGlobalStateValue = includeGlobalState;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
		{
			IndicesValue = indices;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> Partial(bool? partial = true)
		{
			PartialValue = partial;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> RenamePattern(string? renamePattern)
		{
			RenamePatternValue = renamePattern;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor<TDocument> RenameReplacement(string? renameReplacement)
		{
			RenameReplacementValue = renameReplacement;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (IndexSettingsDescriptor is not null)
			{
				writer.WritePropertyName("index_settings");
				JsonSerializer.Serialize(writer, IndexSettingsDescriptor, options);
			}
			else if (IndexSettingsDescriptorAction is not null)
			{
				writer.WritePropertyName("index_settings");
				JsonSerializer.Serialize(writer, new IndexManagement.IndexSettingsDescriptor<TDocument>(IndexSettingsDescriptorAction), options);
			}
			else if (IndexSettingsValue is not null)
			{
				writer.WritePropertyName("index_settings");
				JsonSerializer.Serialize(writer, IndexSettingsValue, options);
			}

			if (IgnoreIndexSettingsValue is not null)
			{
				writer.WritePropertyName("ignore_index_settings");
				JsonSerializer.Serialize(writer, IgnoreIndexSettingsValue, options);
			}

			if (IgnoreUnavailableValue.HasValue)
			{
				writer.WritePropertyName("ignore_unavailable");
				writer.WriteBooleanValue(IgnoreUnavailableValue.Value);
			}

			if (IncludeAliasesValue.HasValue)
			{
				writer.WritePropertyName("include_aliases");
				writer.WriteBooleanValue(IncludeAliasesValue.Value);
			}

			if (IncludeGlobalStateValue.HasValue)
			{
				writer.WritePropertyName("include_global_state");
				writer.WriteBooleanValue(IncludeGlobalStateValue.Value);
			}

			if (IndicesValue is not null)
			{
				writer.WritePropertyName("indices");
				JsonSerializer.Serialize(writer, IndicesValue, options);
			}

			if (PartialValue.HasValue)
			{
				writer.WritePropertyName("partial");
				writer.WriteBooleanValue(PartialValue.Value);
			}

			if (!string.IsNullOrEmpty(RenamePatternValue))
			{
				writer.WritePropertyName("rename_pattern");
				writer.WriteStringValue(RenamePatternValue);
			}

			if (!string.IsNullOrEmpty(RenameReplacementValue))
			{
				writer.WritePropertyName("rename_replacement");
				writer.WriteStringValue(RenameReplacementValue);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class SnapshotRestoreRequestDescriptor : RequestDescriptorBase<SnapshotRestoreRequestDescriptor, SnapshotRestoreRequestParameters>
	{
		internal SnapshotRestoreRequestDescriptor(Action<SnapshotRestoreRequestDescriptor> configure) => configure.Invoke(this);
		public SnapshotRestoreRequestDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
		{
		}

		internal SnapshotRestoreRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SnapshotRestore;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public SnapshotRestoreRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
		public SnapshotRestoreRequestDescriptor WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);
		public SnapshotRestoreRequestDescriptor Repository(Elastic.Clients.Elasticsearch.Name repository)
		{
			RouteValues.Required("repository", repository);
			return Self;
		}

		public SnapshotRestoreRequestDescriptor Snapshot(Elastic.Clients.Elasticsearch.Name snapshot)
		{
			RouteValues.Required("snapshot", snapshot);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? IndexSettingsValue { get; set; }

		private IndexManagement.IndexSettingsDescriptor IndexSettingsDescriptor { get; set; }

		private Action<IndexManagement.IndexSettingsDescriptor> IndexSettingsDescriptorAction { get; set; }

		private IEnumerable<string>? IgnoreIndexSettingsValue { get; set; }

		private bool? IgnoreUnavailableValue { get; set; }

		private bool? IncludeAliasesValue { get; set; }

		private bool? IncludeGlobalStateValue { get; set; }

		private Elastic.Clients.Elasticsearch.Indices? IndicesValue { get; set; }

		private bool? PartialValue { get; set; }

		private string? RenamePatternValue { get; set; }

		private string? RenameReplacementValue { get; set; }

		public SnapshotRestoreRequestDescriptor IndexSettings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? indexSettings)
		{
			IndexSettingsDescriptor = null;
			IndexSettingsDescriptorAction = null;
			IndexSettingsValue = indexSettings;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor IndexSettings(IndexManagement.IndexSettingsDescriptor descriptor)
		{
			IndexSettingsValue = null;
			IndexSettingsDescriptorAction = null;
			IndexSettingsDescriptor = descriptor;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor IndexSettings(Action<IndexManagement.IndexSettingsDescriptor> configure)
		{
			IndexSettingsValue = null;
			IndexSettingsDescriptor = null;
			IndexSettingsDescriptorAction = configure;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor IgnoreIndexSettings(IEnumerable<string>? ignoreIndexSettings)
		{
			IgnoreIndexSettingsValue = ignoreIndexSettings;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true)
		{
			IgnoreUnavailableValue = ignoreUnavailable;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor IncludeAliases(bool? includeAliases = true)
		{
			IncludeAliasesValue = includeAliases;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor IncludeGlobalState(bool? includeGlobalState = true)
		{
			IncludeGlobalStateValue = includeGlobalState;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
		{
			IndicesValue = indices;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor Partial(bool? partial = true)
		{
			PartialValue = partial;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor RenamePattern(string? renamePattern)
		{
			RenamePatternValue = renamePattern;
			return Self;
		}

		public SnapshotRestoreRequestDescriptor RenameReplacement(string? renameReplacement)
		{
			RenameReplacementValue = renameReplacement;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (IndexSettingsDescriptor is not null)
			{
				writer.WritePropertyName("index_settings");
				JsonSerializer.Serialize(writer, IndexSettingsDescriptor, options);
			}
			else if (IndexSettingsDescriptorAction is not null)
			{
				writer.WritePropertyName("index_settings");
				JsonSerializer.Serialize(writer, new IndexManagement.IndexSettingsDescriptor(IndexSettingsDescriptorAction), options);
			}
			else if (IndexSettingsValue is not null)
			{
				writer.WritePropertyName("index_settings");
				JsonSerializer.Serialize(writer, IndexSettingsValue, options);
			}

			if (IgnoreIndexSettingsValue is not null)
			{
				writer.WritePropertyName("ignore_index_settings");
				JsonSerializer.Serialize(writer, IgnoreIndexSettingsValue, options);
			}

			if (IgnoreUnavailableValue.HasValue)
			{
				writer.WritePropertyName("ignore_unavailable");
				writer.WriteBooleanValue(IgnoreUnavailableValue.Value);
			}

			if (IncludeAliasesValue.HasValue)
			{
				writer.WritePropertyName("include_aliases");
				writer.WriteBooleanValue(IncludeAliasesValue.Value);
			}

			if (IncludeGlobalStateValue.HasValue)
			{
				writer.WritePropertyName("include_global_state");
				writer.WriteBooleanValue(IncludeGlobalStateValue.Value);
			}

			if (IndicesValue is not null)
			{
				writer.WritePropertyName("indices");
				JsonSerializer.Serialize(writer, IndicesValue, options);
			}

			if (PartialValue.HasValue)
			{
				writer.WritePropertyName("partial");
				writer.WriteBooleanValue(PartialValue.Value);
			}

			if (!string.IsNullOrEmpty(RenamePatternValue))
			{
				writer.WritePropertyName("rename_pattern");
				writer.WriteStringValue(RenamePatternValue);
			}

			if (!string.IsNullOrEmpty(RenameReplacementValue))
			{
				writer.WritePropertyName("rename_replacement");
				writer.WriteStringValue(RenameReplacementValue);
			}

			writer.WriteEndObject();
		}
	}
}