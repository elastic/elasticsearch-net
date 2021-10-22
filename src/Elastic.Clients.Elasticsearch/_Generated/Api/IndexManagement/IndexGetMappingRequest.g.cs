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
	public class IndexGetMappingRequestParameters : RequestParameters<IndexGetMappingRequestParameters>
	{
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public bool? IncludeTypeName { get => Q<bool?>("include_type_name"); set => Q("include_type_name", value); }

		[JsonIgnore]
		public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }
	}

	public partial class IndexGetMappingRequest : PlainRequestBase<IndexGetMappingRequestParameters>
	{
		public IndexGetMappingRequest()
		{
		}

		public IndexGetMappingRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementGetMapping;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public bool? IncludeTypeName { get => Q<bool?>("include_type_name"); set => Q("include_type_name", value); }

		[JsonIgnore]
		public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }
	}

	[JsonConverter(typeof(IndexGetMappingRequestDescriptorConverter))]
	public partial class IndexGetMappingRequestDescriptor : RequestDescriptorBase<IndexGetMappingRequestDescriptor, IndexGetMappingRequestParameters>
	{
		public IndexGetMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementGetMapping;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		public IndexGetMappingRequestDescriptor AllowNoIndices(bool? allowNoIndices) => Qs("allow_no_indices", allowNoIndices);
		public IndexGetMappingRequestDescriptor ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public IndexGetMappingRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable) => Qs("ignore_unavailable", ignoreUnavailable);
		public IndexGetMappingRequestDescriptor IncludeTypeName(bool? includeTypeName) => Qs("include_type_name", includeTypeName);
		public IndexGetMappingRequestDescriptor Local(bool? local) => Qs("local", local);
		public IndexGetMappingRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
	}

	internal sealed class IndexGetMappingRequestDescriptorConverter : JsonConverter<IndexGetMappingRequestDescriptor>
	{
		public override IndexGetMappingRequestDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, IndexGetMappingRequestDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteEndObject();
		}
	}
}