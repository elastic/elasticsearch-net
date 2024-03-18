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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed class PutMappingRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>If `false`, the request returns an error if any wildcard expression, index alias, or `_all` value targets only missing or closed indices.<br/>This behavior applies even if the request targets other open indices.</para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match.<br/>If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.<br/>Supports comma-separated values, such as `open,hidden`.<br/>Valid values are: `all`, `open`, `closed`, `hidden`, `none`.</para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>If `false`, the request returns an error if it targets a missing or closed index.</para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>Period to wait for a connection to the master node.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>Period to wait for a response.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>If `true`, the mappings are applied only to the current write index for the target.</para>
	/// </summary>
	public bool? WriteIndexOnly { get => Q<bool?>("write_index_only"); set => Q("write_index_only", value); }
}

/// <summary>
/// <para>Adds new fields to an existing data stream or index.<br/>You can also use this API to change the search settings of existing fields.<br/>For data streams, these changes are applied to all backing indices by default.</para>
/// </summary>
public sealed partial class PutMappingRequest : PlainRequest<PutMappingRequestParameters>
{
	public PutMappingRequest(Elastic.Clients.Elasticsearch.Serverless.Indices indices) : base(r => r.Required("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPutMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.put_mapping";

	/// <summary>
	/// <para>If `false`, the request returns an error if any wildcard expression, index alias, or `_all` value targets only missing or closed indices.<br/>This behavior applies even if the request targets other open indices.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match.<br/>If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.<br/>Supports comma-separated values, such as `open,hidden`.<br/>Valid values are: `all`, `open`, `closed`, `hidden`, `none`.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>If `false`, the request returns an error if it targets a missing or closed index.</para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>Period to wait for a connection to the master node.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>Period to wait for a response.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>If `true`, the mappings are applied only to the current write index for the target.</para>
	/// </summary>
	[JsonIgnore]
	public bool? WriteIndexOnly { get => Q<bool?>("write_index_only"); set => Q("write_index_only", value); }

	/// <summary>
	/// <para>Controls whether dynamic date detection is enabled.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("date_detection")]
	public bool? DateDetection { get; set; }

	/// <summary>
	/// <para>Controls whether new fields are added dynamically.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? Dynamic { get; set; }

	/// <summary>
	/// <para>If date detection is enabled then new string fields are checked<br/>against 'dynamic_date_formats' and if the value matches then<br/>a new date field is added instead of string.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("dynamic_date_formats")]
	public ICollection<string>? DynamicDateFormats { get; set; }

	/// <summary>
	/// <para>Specify dynamic templates for the mapping.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("dynamic_templates")]
	[SingleOrManyCollectionConverter(typeof(IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>))]
	public ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>? DynamicTemplates { get; set; }

	/// <summary>
	/// <para>Control whether field names are enabled for the index.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_field_names")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.FieldNamesField? FieldNames { get; set; }

	/// <summary>
	/// <para>A mapping type can have custom meta data associated with it. These are<br/>not used at all by Elasticsearch, but can be used to store<br/>application-specific metadata.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_meta")]
	public IDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>Automatically map strings into numeric data types for all fields.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("numeric_detection")]
	public bool? NumericDetection { get; set; }

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? Properties { get; set; }

	/// <summary>
	/// <para>Enable making a routing value required on indexed documents.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_routing")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.RoutingField? Routing { get; set; }

	/// <summary>
	/// <para>Mapping of runtime fields for the index.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("runtime")]
	public IDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>? Runtime { get; set; }

	/// <summary>
	/// <para>Control whether the _source field is enabled on the index.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_source")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.SourceField? Source { get; set; }
}

/// <summary>
/// <para>Adds new fields to an existing data stream or index.<br/>You can also use this API to change the search settings of existing fields.<br/>For data streams, these changes are applied to all backing indices by default.</para>
/// </summary>
public sealed partial class PutMappingRequestDescriptor<TDocument> : RequestDescriptor<PutMappingRequestDescriptor<TDocument>, PutMappingRequestParameters>
{
	internal PutMappingRequestDescriptor(Action<PutMappingRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PutMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Indices indices) : base(r => r.Required("index", indices))
	{
	}

	internal PutMappingRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPutMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.put_mapping";

	public PutMappingRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public PutMappingRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public PutMappingRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public PutMappingRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutMappingRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);
	public PutMappingRequestDescriptor<TDocument> WriteIndexOnly(bool? writeIndexOnly = true) => Qs("write_index_only", writeIndexOnly);

	public PutMappingRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Serverless.Indices indices)
	{
		RouteValues.Required("index", indices);
		return Self;
	}

	private bool? DateDetectionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? DynamicValue { get; set; }
	private ICollection<string>? DynamicDateFormatsValue { get; set; }
	private ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>? DynamicTemplatesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.FieldNamesField? FieldNamesValue { get; set; }
	private Mapping.FieldNamesFieldDescriptor FieldNamesDescriptor { get; set; }
	private Action<Mapping.FieldNamesFieldDescriptor> FieldNamesDescriptorAction { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private bool? NumericDetectionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.RoutingField? RoutingValue { get; set; }
	private Mapping.RoutingFieldDescriptor RoutingDescriptor { get; set; }
	private Action<Mapping.RoutingFieldDescriptor> RoutingDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>? RuntimeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.SourceField? SourceValue { get; set; }
	private Mapping.SourceFieldDescriptor SourceDescriptor { get; set; }
	private Action<Mapping.SourceFieldDescriptor> SourceDescriptorAction { get; set; }

	/// <summary>
	/// <para>Controls whether dynamic date detection is enabled.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> DateDetection(bool? dateDetection = true)
	{
		DateDetectionValue = dateDetection;
		return Self;
	}

	/// <summary>
	/// <para>Controls whether new fields are added dynamically.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	/// <summary>
	/// <para>If date detection is enabled then new string fields are checked<br/>against 'dynamic_date_formats' and if the value matches then<br/>a new date field is added instead of string.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> DynamicDateFormats(ICollection<string>? dynamicDateFormats)
	{
		DynamicDateFormatsValue = dynamicDateFormats;
		return Self;
	}

	/// <summary>
	/// <para>Specify dynamic templates for the mapping.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> DynamicTemplates(ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>? dynamicTemplates)
	{
		DynamicTemplatesValue = dynamicTemplates;
		return Self;
	}

	/// <summary>
	/// <para>Control whether field names are enabled for the index.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> FieldNames(Elastic.Clients.Elasticsearch.Serverless.Mapping.FieldNamesField? fieldNames)
	{
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = null;
		FieldNamesValue = fieldNames;
		return Self;
	}

	public PutMappingRequestDescriptor<TDocument> FieldNames(Mapping.FieldNamesFieldDescriptor descriptor)
	{
		FieldNamesValue = null;
		FieldNamesDescriptorAction = null;
		FieldNamesDescriptor = descriptor;
		return Self;
	}

	public PutMappingRequestDescriptor<TDocument> FieldNames(Action<Mapping.FieldNamesFieldDescriptor> configure)
	{
		FieldNamesValue = null;
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>A mapping type can have custom meta data associated with it. These are<br/>not used at all by Elasticsearch, but can be used to store<br/>application-specific metadata.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>Automatically map strings into numeric data types for all fields.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> NumericDetection(bool? numericDetection = true)
	{
		NumericDetectionValue = numericDetection;
		return Self;
	}

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Properties(Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Properties(Action<Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	/// <summary>
	/// <para>Enable making a routing value required on indexed documents.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Serverless.Mapping.RoutingField? routing)
	{
		RoutingDescriptor = null;
		RoutingDescriptorAction = null;
		RoutingValue = routing;
		return Self;
	}

	public PutMappingRequestDescriptor<TDocument> Routing(Mapping.RoutingFieldDescriptor descriptor)
	{
		RoutingValue = null;
		RoutingDescriptorAction = null;
		RoutingDescriptor = descriptor;
		return Self;
	}

	public PutMappingRequestDescriptor<TDocument> Routing(Action<Mapping.RoutingFieldDescriptor> configure)
	{
		RoutingValue = null;
		RoutingDescriptor = null;
		RoutingDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Mapping of runtime fields for the index.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Runtime(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>, FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>> selector)
	{
		RuntimeValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>());
		return Self;
	}

	/// <summary>
	/// <para>Control whether the _source field is enabled on the index.</para>
	/// </summary>
	public PutMappingRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Serverless.Mapping.SourceField? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public PutMappingRequestDescriptor<TDocument> Source(Mapping.SourceFieldDescriptor descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public PutMappingRequestDescriptor<TDocument> Source(Action<Mapping.SourceFieldDescriptor> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DateDetectionValue.HasValue)
		{
			writer.WritePropertyName("date_detection");
			writer.WriteBooleanValue(DateDetectionValue.Value);
		}

		if (DynamicValue is not null)
		{
			writer.WritePropertyName("dynamic");
			JsonSerializer.Serialize(writer, DynamicValue, options);
		}

		if (DynamicDateFormatsValue is not null)
		{
			writer.WritePropertyName("dynamic_date_formats");
			JsonSerializer.Serialize(writer, DynamicDateFormatsValue, options);
		}

		if (DynamicTemplatesValue is not null)
		{
			writer.WritePropertyName("dynamic_templates");
			SingleOrManySerializationHelper.Serialize<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>(DynamicTemplatesValue, writer, options);
		}

		if (FieldNamesDescriptor is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, FieldNamesDescriptor, options);
		}
		else if (FieldNamesDescriptorAction is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, new Mapping.FieldNamesFieldDescriptor(FieldNamesDescriptorAction), options);
		}
		else if (FieldNamesValue is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, FieldNamesValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("_meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (NumericDetectionValue.HasValue)
		{
			writer.WritePropertyName("numeric_detection");
			writer.WriteBooleanValue(NumericDetectionValue.Value);
		}

		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (RoutingDescriptor is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingDescriptor, options);
		}
		else if (RoutingDescriptorAction is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, new Mapping.RoutingFieldDescriptor(RoutingDescriptorAction), options);
		}
		else if (RoutingValue is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (RuntimeValue is not null)
		{
			writer.WritePropertyName("runtime");
			JsonSerializer.Serialize(writer, RuntimeValue, options);
		}

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, new Mapping.SourceFieldDescriptor(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>Adds new fields to an existing data stream or index.<br/>You can also use this API to change the search settings of existing fields.<br/>For data streams, these changes are applied to all backing indices by default.</para>
/// </summary>
public sealed partial class PutMappingRequestDescriptor : RequestDescriptor<PutMappingRequestDescriptor, PutMappingRequestParameters>
{
	internal PutMappingRequestDescriptor(Action<PutMappingRequestDescriptor> configure) => configure.Invoke(this);

	public PutMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Indices indices) : base(r => r.Required("index", indices))
	{
	}

	internal PutMappingRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPutMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.put_mapping";

	public PutMappingRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public PutMappingRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public PutMappingRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public PutMappingRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutMappingRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);
	public PutMappingRequestDescriptor WriteIndexOnly(bool? writeIndexOnly = true) => Qs("write_index_only", writeIndexOnly);

	public PutMappingRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Serverless.Indices indices)
	{
		RouteValues.Required("index", indices);
		return Self;
	}

	private bool? DateDetectionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? DynamicValue { get; set; }
	private ICollection<string>? DynamicDateFormatsValue { get; set; }
	private ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>? DynamicTemplatesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.FieldNamesField? FieldNamesValue { get; set; }
	private Mapping.FieldNamesFieldDescriptor FieldNamesDescriptor { get; set; }
	private Action<Mapping.FieldNamesFieldDescriptor> FieldNamesDescriptorAction { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private bool? NumericDetectionValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.RoutingField? RoutingValue { get; set; }
	private Mapping.RoutingFieldDescriptor RoutingDescriptor { get; set; }
	private Action<Mapping.RoutingFieldDescriptor> RoutingDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>? RuntimeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.SourceField? SourceValue { get; set; }
	private Mapping.SourceFieldDescriptor SourceDescriptor { get; set; }
	private Action<Mapping.SourceFieldDescriptor> SourceDescriptorAction { get; set; }

	/// <summary>
	/// <para>Controls whether dynamic date detection is enabled.</para>
	/// </summary>
	public PutMappingRequestDescriptor DateDetection(bool? dateDetection = true)
	{
		DateDetectionValue = dateDetection;
		return Self;
	}

	/// <summary>
	/// <para>Controls whether new fields are added dynamically.</para>
	/// </summary>
	public PutMappingRequestDescriptor Dynamic(Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	/// <summary>
	/// <para>If date detection is enabled then new string fields are checked<br/>against 'dynamic_date_formats' and if the value matches then<br/>a new date field is added instead of string.</para>
	/// </summary>
	public PutMappingRequestDescriptor DynamicDateFormats(ICollection<string>? dynamicDateFormats)
	{
		DynamicDateFormatsValue = dynamicDateFormats;
		return Self;
	}

	/// <summary>
	/// <para>Specify dynamic templates for the mapping.</para>
	/// </summary>
	public PutMappingRequestDescriptor DynamicTemplates(ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>? dynamicTemplates)
	{
		DynamicTemplatesValue = dynamicTemplates;
		return Self;
	}

	/// <summary>
	/// <para>Control whether field names are enabled for the index.</para>
	/// </summary>
	public PutMappingRequestDescriptor FieldNames(Elastic.Clients.Elasticsearch.Serverless.Mapping.FieldNamesField? fieldNames)
	{
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = null;
		FieldNamesValue = fieldNames;
		return Self;
	}

	public PutMappingRequestDescriptor FieldNames(Mapping.FieldNamesFieldDescriptor descriptor)
	{
		FieldNamesValue = null;
		FieldNamesDescriptorAction = null;
		FieldNamesDescriptor = descriptor;
		return Self;
	}

	public PutMappingRequestDescriptor FieldNames(Action<Mapping.FieldNamesFieldDescriptor> configure)
	{
		FieldNamesValue = null;
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>A mapping type can have custom meta data associated with it. These are<br/>not used at all by Elasticsearch, but can be used to store<br/>application-specific metadata.</para>
	/// </summary>
	public PutMappingRequestDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>Automatically map strings into numeric data types for all fields.</para>
	/// </summary>
	public PutMappingRequestDescriptor NumericDetection(bool? numericDetection = true)
	{
		NumericDetectionValue = numericDetection;
		return Self;
	}

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	public PutMappingRequestDescriptor Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	public PutMappingRequestDescriptor Properties<TDocument>(Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	/// <summary>
	/// <para>Mapping for a field. For new fields, this mapping can include:</para>
	/// <para>- Field name<br/>- Field data type<br/>- Mapping parameters</para>
	/// </summary>
	public PutMappingRequestDescriptor Properties<TDocument>(Action<Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	/// <summary>
	/// <para>Enable making a routing value required on indexed documents.</para>
	/// </summary>
	public PutMappingRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Serverless.Mapping.RoutingField? routing)
	{
		RoutingDescriptor = null;
		RoutingDescriptorAction = null;
		RoutingValue = routing;
		return Self;
	}

	public PutMappingRequestDescriptor Routing(Mapping.RoutingFieldDescriptor descriptor)
	{
		RoutingValue = null;
		RoutingDescriptorAction = null;
		RoutingDescriptor = descriptor;
		return Self;
	}

	public PutMappingRequestDescriptor Routing(Action<Mapping.RoutingFieldDescriptor> configure)
	{
		RoutingValue = null;
		RoutingDescriptor = null;
		RoutingDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Mapping of runtime fields for the index.</para>
	/// </summary>
	public PutMappingRequestDescriptor Runtime(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>, FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>> selector)
	{
		RuntimeValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>());
		return Self;
	}

	/// <summary>
	/// <para>Control whether the _source field is enabled on the index.</para>
	/// </summary>
	public PutMappingRequestDescriptor Source(Elastic.Clients.Elasticsearch.Serverless.Mapping.SourceField? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public PutMappingRequestDescriptor Source(Mapping.SourceFieldDescriptor descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public PutMappingRequestDescriptor Source(Action<Mapping.SourceFieldDescriptor> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DateDetectionValue.HasValue)
		{
			writer.WritePropertyName("date_detection");
			writer.WriteBooleanValue(DateDetectionValue.Value);
		}

		if (DynamicValue is not null)
		{
			writer.WritePropertyName("dynamic");
			JsonSerializer.Serialize(writer, DynamicValue, options);
		}

		if (DynamicDateFormatsValue is not null)
		{
			writer.WritePropertyName("dynamic_date_formats");
			JsonSerializer.Serialize(writer, DynamicDateFormatsValue, options);
		}

		if (DynamicTemplatesValue is not null)
		{
			writer.WritePropertyName("dynamic_templates");
			SingleOrManySerializationHelper.Serialize<IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicTemplate>>(DynamicTemplatesValue, writer, options);
		}

		if (FieldNamesDescriptor is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, FieldNamesDescriptor, options);
		}
		else if (FieldNamesDescriptorAction is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, new Mapping.FieldNamesFieldDescriptor(FieldNamesDescriptorAction), options);
		}
		else if (FieldNamesValue is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, FieldNamesValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("_meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (NumericDetectionValue.HasValue)
		{
			writer.WritePropertyName("numeric_detection");
			writer.WriteBooleanValue(NumericDetectionValue.Value);
		}

		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (RoutingDescriptor is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingDescriptor, options);
		}
		else if (RoutingDescriptorAction is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, new Mapping.RoutingFieldDescriptor(RoutingDescriptorAction), options);
		}
		else if (RoutingValue is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (RuntimeValue is not null)
		{
			writer.WritePropertyName("runtime");
			JsonSerializer.Serialize(writer, RuntimeValue, options);
		}

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, new Mapping.SourceFieldDescriptor(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		writer.WriteEndObject();
	}
}