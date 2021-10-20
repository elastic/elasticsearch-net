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
	public class IndexPutMappingRequestParameters : RequestParameters<IndexPutMappingRequestParameters>
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
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? WriteIndexOnly { get => Q<bool?>("write_index_only"); set => Q("write_index_only", value); }
	}

	[InterfaceConverterAttribute(typeof(IndexPutMappingRequestDescriptorConverter<IndexPutMappingRequest>))]
	public partial interface IIndexPutMappingRequest : IRequest<IndexPutMappingRequestParameters>
	{
		bool? DateDetection { get; set; }

		Union<bool?, Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?>? Dynamic { get; set; }

		IEnumerable<string>? DynamicDateFormats { get; set; }

		Union<Dictionary<string, Mapping.IDynamicTemplate>?, IEnumerable<Dictionary<string, Mapping.IDynamicTemplate>>?>? DynamicTemplates { get; set; }

		Mapping.IFieldNamesField? FieldNames { get; set; }

		Dictionary<string, object>? Meta { get; set; }

		bool? NumericDetection { get; set; }

		Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.Properties>? Properties { get; set; }

		Mapping.IRoutingField? Routing { get; set; }

		Mapping.ISourceField? Source { get; set; }

		Dictionary<string, Mapping.IRuntimeField>? Runtime { get; set; }
	}

	public partial class IndexPutMappingRequest : PlainRequestBase<IndexPutMappingRequestParameters>, IIndexPutMappingRequest
	{
		public IndexPutMappingRequest(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("indices", indices))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutMapping;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public bool? IncludeTypeName { get => Q<bool?>("include_type_name"); set => Q("include_type_name", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? WriteIndexOnly { get => Q<bool?>("write_index_only"); set => Q("write_index_only", value); }

		[JsonInclude]
		[JsonPropertyName("date_detection")]
		public bool? DateDetection { get; set; }

		[JsonInclude]
		[JsonPropertyName("dynamic")]
		public Union<bool?, Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?>? Dynamic { get; set; }

		[JsonInclude]
		[JsonPropertyName("dynamic_date_formats")]
		public IEnumerable<string>? DynamicDateFormats { get; set; }

		[JsonInclude]
		[JsonPropertyName("dynamic_templates")]
		public Union<Dictionary<string, Mapping.IDynamicTemplate>?, IEnumerable<Dictionary<string, Mapping.IDynamicTemplate>>?>? DynamicTemplates { get; set; }

		[JsonInclude]
		[JsonPropertyName("_field_names")]
		public Mapping.IFieldNamesField? FieldNames { get; set; }

		[JsonInclude]
		[JsonPropertyName("_meta")]
		public Dictionary<string, object>? Meta { get; set; }

		[JsonInclude]
		[JsonPropertyName("numeric_detection")]
		public bool? NumericDetection { get; set; }

		[JsonInclude]
		[JsonPropertyName("properties")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.Properties>? Properties { get; set; }

		[JsonInclude]
		[JsonPropertyName("_routing")]
		public Mapping.IRoutingField? Routing { get; set; }

		[JsonInclude]
		[JsonPropertyName("_source")]
		public Mapping.ISourceField? Source { get; set; }

		[JsonInclude]
		[JsonPropertyName("runtime")]
		public Dictionary<string, Mapping.IRuntimeField>? Runtime { get; set; }
	}

	public partial class IndexPutMappingRequestDescriptor : RequestDescriptorBase<IndexPutMappingRequestDescriptor, IndexPutMappingRequestParameters, IIndexPutMappingRequest>, IIndexPutMappingRequest
	{
		///<summary>/{index}/_mapping</summary>
        public IndexPutMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("indices", indices))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutMapping;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		bool? IIndexPutMappingRequest.DateDetection { get; set; }

		Union<bool?, Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?>? IIndexPutMappingRequest.Dynamic { get; set; }

		IEnumerable<string>? IIndexPutMappingRequest.DynamicDateFormats { get; set; }

		Union<Dictionary<string, Mapping.IDynamicTemplate>?, IEnumerable<Dictionary<string, Mapping.IDynamicTemplate>>?>? IIndexPutMappingRequest.DynamicTemplates { get; set; }

		Mapping.IFieldNamesField? IIndexPutMappingRequest.FieldNames { get; set; }

		Dictionary<string, object>? IIndexPutMappingRequest.Meta { get; set; }

		bool? IIndexPutMappingRequest.NumericDetection { get; set; }

		Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.Properties>? IIndexPutMappingRequest.Properties { get; set; }

		Mapping.IRoutingField? IIndexPutMappingRequest.Routing { get; set; }

		Mapping.ISourceField? IIndexPutMappingRequest.Source { get; set; }

		Dictionary<string, Mapping.IRuntimeField>? IIndexPutMappingRequest.Runtime { get; set; }

		public IndexPutMappingRequestDescriptor DateDetection(bool? dateDetection = true) => Assign(dateDetection, (a, v) => a.DateDetection = v);
		public IndexPutMappingRequestDescriptor Dynamic(Union<bool?, Elastic.Clients.Elasticsearch.Mapping.DynamicMapping?>? dynamic) => Assign(dynamic, (a, v) => a.Dynamic = v);
		public IndexPutMappingRequestDescriptor DynamicDateFormats(IEnumerable<string>? dynamicDateFormats) => Assign(dynamicDateFormats, (a, v) => a.DynamicDateFormats = v);
		public IndexPutMappingRequestDescriptor DynamicTemplates(Union<Dictionary<string, Mapping.IDynamicTemplate>?, IEnumerable<Dictionary<string, Mapping.IDynamicTemplate>>?>? dynamicTemplates) => Assign(dynamicTemplates, (a, v) => a.DynamicTemplates = v);
		public IndexPutMappingRequestDescriptor FieldNames(Mapping.IFieldNamesField? fieldNames) => Assign(fieldNames, (a, v) => a.FieldNames = v);
		public IndexPutMappingRequestDescriptor Meta(Func<FluentDictionary<string?, object?>, FluentDictionary<string?, object?>> selector) => Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string?, object?>()));
		public IndexPutMappingRequestDescriptor NumericDetection(bool? numericDetection = true) => Assign(numericDetection, (a, v) => a.NumericDetection = v);
		public IndexPutMappingRequestDescriptor Properties(Func<FluentDictionary<string?, Elastic.Clients.Elasticsearch.Mapping.Properties?>, FluentDictionary<string?, Elastic.Clients.Elasticsearch.Mapping.Properties?>> selector) => Assign(selector, (a, v) => a.Properties = v?.Invoke(new FluentDictionary<string?, Elastic.Clients.Elasticsearch.Mapping.Properties?>()));
		public IndexPutMappingRequestDescriptor Routing(Mapping.IRoutingField? routing) => Assign(routing, (a, v) => a.Routing = v);
		public IndexPutMappingRequestDescriptor Source(Mapping.ISourceField? source) => Assign(source, (a, v) => a.Source = v);
		public IndexPutMappingRequestDescriptor Runtime(Dictionary<string, Mapping.IRuntimeField>? runtime) => Assign(runtime, (a, v) => a.Runtime = v);
		public IndexPutMappingRequestDescriptor AllowNoIndices(bool? allowNoIndices) => Qs("allow_no_indices", allowNoIndices);
		public IndexPutMappingRequestDescriptor ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public IndexPutMappingRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable) => Qs("ignore_unavailable", ignoreUnavailable);
		public IndexPutMappingRequestDescriptor IncludeTypeName(bool? includeTypeName) => Qs("include_type_name", includeTypeName);
		public IndexPutMappingRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public IndexPutMappingRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public IndexPutMappingRequestDescriptor WriteIndexOnly(bool? writeIndexOnly) => Qs("write_index_only", writeIndexOnly);
	}

	internal sealed class IndexPutMappingRequestDescriptorConverter<TReadAs> : JsonConverter<IIndexPutMappingRequest> where TReadAs : class, IIndexPutMappingRequest
	{
		public override IIndexPutMappingRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => JsonSerializer.Deserialize<TReadAs>(ref reader, options);
		public override void Write(Utf8JsonWriter writer, IIndexPutMappingRequest value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.DateDetection.HasValue)
			{
				writer.WritePropertyName("date_detection");
				writer.WriteBooleanValue(value.DateDetection.Value);
			}

			if (value.Dynamic is not null)
			{
				writer.WritePropertyName("dynamic");
				JsonSerializer.Serialize(writer, value.Dynamic, options);
			}

			if (value.DynamicDateFormats is not null)
			{
				writer.WritePropertyName("dynamic_date_formats");
				JsonSerializer.Serialize(writer, value.DynamicDateFormats, options);
			}

			if (value.DynamicTemplates is not null)
			{
				writer.WritePropertyName("dynamic_templates");
				JsonSerializer.Serialize(writer, value.DynamicTemplates, options);
			}

			if (value.FieldNames is not null)
			{
				writer.WritePropertyName("_field_names");
				JsonSerializer.Serialize(writer, value.FieldNames, options);
			}

			if (value.Meta is not null)
			{
				writer.WritePropertyName("_meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (value.NumericDetection.HasValue)
			{
				writer.WritePropertyName("numeric_detection");
				writer.WriteBooleanValue(value.NumericDetection.Value);
			}

			if (value.Properties is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, value.Properties, options);
			}

			if (value.Routing is not null)
			{
				writer.WritePropertyName("_routing");
				JsonSerializer.Serialize(writer, value.Routing, options);
			}

			if (value.Source is not null)
			{
				writer.WritePropertyName("_source");
				JsonSerializer.Serialize(writer, value.Source, options);
			}

			if (value.Runtime is not null)
			{
				writer.WritePropertyName("runtime");
				JsonSerializer.Serialize(writer, value.Runtime, options);
			}

			writer.WriteEndObject();
		}
	}
}