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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public class SplitIndexRequestParameters : RequestParameters<SplitIndexRequestParameters>
	{
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	}

	public partial class SplitIndexRequest : PlainRequestBase<SplitIndexRequestParameters>
	{
		public SplitIndexRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target) : base(r => r.Required("index", index).Required("target", target))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementSplit;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
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
		[JsonPropertyName("settings")]
		public Dictionary<string, object>? Settings { get; set; }
	}

	[JsonConverter(typeof(SplitIndexRequestDescriptorConverter))]
	public sealed partial class SplitIndexRequestDescriptor : RequestDescriptorBase<SplitIndexRequestDescriptor, SplitIndexRequestParameters>
	{
		public SplitIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target) : base(r => r.Required("index", index).Required("target", target))
		{
		}

		public SplitIndexRequestDescriptor()
		{
		}

		internal SplitIndexRequestDescriptor(Action<SplitIndexRequestDescriptor> configure) => configure.Invoke(this);
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementSplit;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		public SplitIndexRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public SplitIndexRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public SplitIndexRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
		internal Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? AliasesValue { get; private set; }

		internal Dictionary<string, object>? SettingsValue { get; private set; }

		public SplitIndexRequestDescriptor Aliases(Func<FluentDictionary<Elastic.Clients.Elasticsearch.IndexName?, Elastic.Clients.Elasticsearch.IndexManagement.Alias?>, FluentDictionary<Elastic.Clients.Elasticsearch.IndexName?, Elastic.Clients.Elasticsearch.IndexManagement.Alias?>> selector) => Assign(selector, (a, v) => a.AliasesValue = v?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.IndexName?, Elastic.Clients.Elasticsearch.IndexManagement.Alias?>()));
		public SplitIndexRequestDescriptor Settings(Func<FluentDictionary<string?, object?>, FluentDictionary<string?, object?>> selector) => Assign(selector, (a, v) => a.SettingsValue = v?.Invoke(new FluentDictionary<string?, object?>()));
	}

	internal sealed class SplitIndexRequestDescriptorConverter : JsonConverter<SplitIndexRequestDescriptor>
	{
		public override SplitIndexRequestDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, SplitIndexRequestDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.AliasesValue is not null)
			{
				writer.WritePropertyName("aliases");
				JsonSerializer.Serialize(writer, value.AliasesValue, options);
			}

			if (value.SettingsValue is not null)
			{
				writer.WritePropertyName("settings");
				JsonSerializer.Serialize(writer, value.SettingsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}