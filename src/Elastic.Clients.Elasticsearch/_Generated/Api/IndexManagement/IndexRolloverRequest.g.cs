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

using Elastic.Clients.Elasticsearch.Experimental;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public class IndexRolloverRequestParameters : RequestParameters<IndexRolloverRequestParameters>
	{
		[JsonIgnore]
		public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

		[JsonIgnore]
		public bool? IncludeTypeName { get => Q<bool?>("include_type_name"); set => Q("include_type_name", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	}

	public partial class IndexRolloverRequest : PlainRequestBase<IndexRolloverRequestParameters>
	{
		public IndexRolloverRequest(Elastic.Clients.Elasticsearch.IndexAlias alias) : base(r => r.Required("alias", alias))
		{
		}

		public IndexRolloverRequest(Elastic.Clients.Elasticsearch.IndexAlias alias, Elastic.Clients.Elasticsearch.IndexName? new_index) : base(r => r.Required("alias", alias).Optional("new_index", new_index))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementRollover;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

		[JsonIgnore]
		public bool? IncludeTypeName { get => Q<bool?>("include_type_name"); set => Q("include_type_name", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

		[JsonInclude]
		[JsonPropertyName("aliases")]
		public Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; set; }

		[JsonInclude]
		[JsonPropertyName("conditions")]
		public Elastic.Clients.Elasticsearch.IndexManagement.Rollover.RolloverConditions? Conditions { get; set; }

		[JsonInclude]
		[JsonPropertyName("mappings")]
		public Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.TypeMapping>?, Elastic.Clients.Elasticsearch.Mapping.TypeMapping?>? Mappings { get; set; }

		[JsonInclude]
		[JsonPropertyName("settings")]
		public Dictionary<string, object>? Settings { get; set; }
	}

	[JsonConverter(typeof(IndexRolloverRequestDescriptorConverter))]
	public partial class IndexRolloverRequestDescriptor : RequestDescriptorBase<IndexRolloverRequestDescriptor, IndexRolloverRequestParameters>
	{
		public IndexRolloverRequestDescriptor(Elastic.Clients.Elasticsearch.IndexAlias alias) : base(r => r.Required("alias", alias))
		{
		}

		public IndexRolloverRequestDescriptor(Elastic.Clients.Elasticsearch.IndexAlias alias, Elastic.Clients.Elasticsearch.IndexName? new_index) : base(r => r.Required("alias", alias).Optional("new_index", new_index))
		{
		}

		internal Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? _aliases;
		internal Elastic.Clients.Elasticsearch.IndexManagement.Rollover.RolloverConditions? _conditions;
		internal Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.TypeMapping>?, Elastic.Clients.Elasticsearch.Mapping.TypeMapping?>? _mappings;
		internal Dictionary<string, object>? _settings;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementRollover;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public IndexRolloverRequestDescriptor DryRun(bool? dryRun) => Qs("dry_run", dryRun);
		public IndexRolloverRequestDescriptor IncludeTypeName(bool? includeTypeName) => Qs("include_type_name", includeTypeName);
		public IndexRolloverRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public IndexRolloverRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public IndexRolloverRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
		public IndexRolloverRequestDescriptor Aliases(Func<FluentDictionary<Elastic.Clients.Elasticsearch.IndexName?, Elastic.Clients.Elasticsearch.IndexManagement.Alias?>, FluentDictionary<Elastic.Clients.Elasticsearch.IndexName?, Elastic.Clients.Elasticsearch.IndexManagement.Alias?>> selector) => Assign(selector, (a, v) => a._aliases = v?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.IndexName?, Elastic.Clients.Elasticsearch.IndexManagement.Alias?>()));
		public IndexRolloverRequestDescriptor Conditions(Elastic.Clients.Elasticsearch.IndexManagement.Rollover.RolloverConditions? conditions) => Assign(conditions, (a, v) => a._conditions = v);
		public IndexRolloverRequestDescriptor Mappings(Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.TypeMapping>?, Elastic.Clients.Elasticsearch.Mapping.TypeMapping?>? mappings) => Assign(mappings, (a, v) => a._mappings = v);
		public IndexRolloverRequestDescriptor Settings(Func<FluentDictionary<string?, object?>, FluentDictionary<string?, object?>> selector) => Assign(selector, (a, v) => a._settings = v?.Invoke(new FluentDictionary<string?, object?>()));
	}

	internal sealed class IndexRolloverRequestDescriptorConverter : JsonConverter<IndexRolloverRequestDescriptor>
	{
		public override IndexRolloverRequestDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, IndexRolloverRequestDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value._aliases is not null)
			{
				writer.WritePropertyName("aliases");
				JsonSerializer.Serialize(writer, value._aliases, options);
			}

			if (value._conditions is not null)
			{
				writer.WritePropertyName("conditions");
				JsonSerializer.Serialize(writer, value._conditions, options);
			}

			if (value._mappings is not null)
			{
				writer.WritePropertyName("mappings");
				JsonSerializer.Serialize(writer, value._mappings, options);
			}

			if (value._settings is not null)
			{
				writer.WritePropertyName("settings");
				JsonSerializer.Serialize(writer, value._settings, options);
			}

			writer.WriteEndObject();
		}
	}
}