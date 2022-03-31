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
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public class UpdateAliasesRequestParameters : RequestParameters<UpdateAliasesRequestParameters>
	{
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }
	}

	public partial class UpdateAliasesRequest : PlainRequestBase<UpdateAliasesRequestParameters>
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementUpdateAliases;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonInclude]
		[JsonPropertyName("actions")]
		public IEnumerable<Elastic.Clients.Elasticsearch.IndexManagement.Action>? Actions { get; set; }
	}

	public sealed partial class UpdateAliasesRequestDescriptor : RequestDescriptorBase<UpdateAliasesRequestDescriptor, UpdateAliasesRequestParameters>
	{
		internal UpdateAliasesRequestDescriptor(Action<UpdateAliasesRequestDescriptor> configure) => configure.Invoke(this);
		public UpdateAliasesRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementUpdateAliases;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public UpdateAliasesRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public UpdateAliasesRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		private IEnumerable<Elastic.Clients.Elasticsearch.IndexManagement.Action>? ActionsValue { get; set; }

		public UpdateAliasesRequestDescriptor Actions(IEnumerable<Elastic.Clients.Elasticsearch.IndexManagement.Action>? actions)
		{
			ActionsValue = actions;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ActionsValue is not null)
			{
				writer.WritePropertyName("actions");
				JsonSerializer.Serialize(writer, ActionsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}