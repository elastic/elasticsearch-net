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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Mapping;
public sealed partial class TypeMapping
{
	[JsonInclude]
	[JsonPropertyName("_data_stream_timestamp")]
	public Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp? DataStreamTimestamp { get; set; }

	[JsonInclude]
	[JsonPropertyName("_field_names")]
	public Elastic.Clients.Elasticsearch.Mapping.FieldNamesField? FieldNames { get; set; }

	[JsonInclude]
	[JsonPropertyName("_meta")]
	public Dictionary<string, object>? Meta { get; set; }

	[JsonInclude]
	[JsonPropertyName("_routing")]
	public Elastic.Clients.Elasticsearch.Mapping.RoutingField? Routing { get; set; }

	[JsonInclude]
	[JsonPropertyName("_size")]
	public Elastic.Clients.Elasticsearch.Mapping.SizeField? Size { get; set; }

	[JsonInclude]
	[JsonPropertyName("_source")]
	public Elastic.Clients.Elasticsearch.Mapping.SourceField? Source { get; set; }

	[JsonInclude]
	[JsonPropertyName("all_field")]
	public Elastic.Clients.Elasticsearch.Mapping.AllField? AllField { get; set; }

	[JsonInclude]
	[JsonPropertyName("date_detection")]
	public bool? DateDetection { get; set; }

	[JsonInclude]
	[JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }

	[JsonInclude]
	[JsonPropertyName("dynamic_date_formats")]
	public IEnumerable<string>? DynamicDateFormats { get; set; }

	[JsonInclude]
	[JsonPropertyName("dynamic_templates")]
	public Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>?, IEnumerable<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>>?>? DynamicTemplates { get; set; }

	[JsonInclude]
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; set; }

	[JsonInclude]
	[JsonPropertyName("index_field")]
	public Elastic.Clients.Elasticsearch.Mapping.IndexField? IndexField { get; set; }

	[JsonInclude]
	[JsonPropertyName("numeric_detection")]
	public bool? NumericDetection { get; set; }

	[JsonInclude]
	[JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }

	[JsonInclude]
	[JsonPropertyName("runtime")]
	public Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? Runtime { get; set; }
}

public sealed partial class TypeMappingDescriptor<TDocument> : SerializableDescriptor<TypeMappingDescriptor<TDocument>>
{
	internal TypeMappingDescriptor(Action<TypeMappingDescriptor<TDocument>> configure) => configure.Invoke(this);
	public TypeMappingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp? DataStreamTimestampValue { get; set; }

	private DataStreamTimestampDescriptor DataStreamTimestampDescriptor { get; set; }

	private Action<DataStreamTimestampDescriptor> DataStreamTimestampDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.FieldNamesField? FieldNamesValue { get; set; }

	private FieldNamesFieldDescriptor FieldNamesDescriptor { get; set; }

	private Action<FieldNamesFieldDescriptor> FieldNamesDescriptorAction { get; set; }

	private Dictionary<string, object>? MetaValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.RoutingField? RoutingValue { get; set; }

	private RoutingFieldDescriptor RoutingDescriptor { get; set; }

	private Action<RoutingFieldDescriptor> RoutingDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.SizeField? SizeValue { get; set; }

	private SizeFieldDescriptor SizeDescriptor { get; set; }

	private Action<SizeFieldDescriptor> SizeDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.SourceField? SourceValue { get; set; }

	private SourceFieldDescriptor SourceDescriptor { get; set; }

	private Action<SourceFieldDescriptor> SourceDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.AllField? AllFieldValue { get; set; }

	private AllFieldDescriptor AllFieldDescriptor { get; set; }

	private Action<AllFieldDescriptor> AllFieldDescriptorAction { get; set; }

	private bool? DateDetectionValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

	private IEnumerable<string>? DynamicDateFormatsValue { get; set; }

	private Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>?, IEnumerable<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>>?>? DynamicTemplatesValue { get; set; }

	private bool? EnabledValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.IndexField? IndexFieldValue { get; set; }

	private IndexFieldDescriptor IndexFieldDescriptor { get; set; }

	private Action<IndexFieldDescriptor> IndexFieldDescriptorAction { get; set; }

	private bool? NumericDetectionValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

	private Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeValue { get; set; }

	public TypeMappingDescriptor<TDocument> DataStreamTimestamp(Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp? dataStreamTimestamp)
	{
		DataStreamTimestampDescriptor = null;
		DataStreamTimestampDescriptorAction = null;
		DataStreamTimestampValue = dataStreamTimestamp;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> DataStreamTimestamp(DataStreamTimestampDescriptor descriptor)
	{
		DataStreamTimestampValue = null;
		DataStreamTimestampDescriptorAction = null;
		DataStreamTimestampDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> DataStreamTimestamp(Action<DataStreamTimestampDescriptor> configure)
	{
		DataStreamTimestampValue = null;
		DataStreamTimestampDescriptor = null;
		DataStreamTimestampDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> FieldNames(Elastic.Clients.Elasticsearch.Mapping.FieldNamesField? fieldNames)
	{
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = null;
		FieldNamesValue = fieldNames;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> FieldNames(FieldNamesFieldDescriptor descriptor)
	{
		FieldNamesValue = null;
		FieldNamesDescriptorAction = null;
		FieldNamesDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> FieldNames(Action<FieldNamesFieldDescriptor> configure)
	{
		FieldNamesValue = null;
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Mapping.RoutingField? routing)
	{
		RoutingDescriptor = null;
		RoutingDescriptorAction = null;
		RoutingValue = routing;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Routing(RoutingFieldDescriptor descriptor)
	{
		RoutingValue = null;
		RoutingDescriptorAction = null;
		RoutingDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Routing(Action<RoutingFieldDescriptor> configure)
	{
		RoutingValue = null;
		RoutingDescriptor = null;
		RoutingDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Size(Elastic.Clients.Elasticsearch.Mapping.SizeField? size)
	{
		SizeDescriptor = null;
		SizeDescriptorAction = null;
		SizeValue = size;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Size(SizeFieldDescriptor descriptor)
	{
		SizeValue = null;
		SizeDescriptorAction = null;
		SizeDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Size(Action<SizeFieldDescriptor> configure)
	{
		SizeValue = null;
		SizeDescriptor = null;
		SizeDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Mapping.SourceField? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Source(SourceFieldDescriptor descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Source(Action<SourceFieldDescriptor> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> AllField(Elastic.Clients.Elasticsearch.Mapping.AllField? allField)
	{
		AllFieldDescriptor = null;
		AllFieldDescriptorAction = null;
		AllFieldValue = allField;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> AllField(AllFieldDescriptor descriptor)
	{
		AllFieldValue = null;
		AllFieldDescriptorAction = null;
		AllFieldDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> AllField(Action<AllFieldDescriptor> configure)
	{
		AllFieldValue = null;
		AllFieldDescriptor = null;
		AllFieldDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> DateDetection(bool? dateDetection = true)
	{
		DateDetectionValue = dateDetection;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> DynamicDateFormats(IEnumerable<string>? dynamicDateFormats)
	{
		DynamicDateFormatsValue = dynamicDateFormats;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> DynamicTemplates(Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>?, IEnumerable<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>>?>? dynamicTemplates)
	{
		DynamicTemplatesValue = dynamicTemplates;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> IndexField(Elastic.Clients.Elasticsearch.Mapping.IndexField? indexField)
	{
		IndexFieldDescriptor = null;
		IndexFieldDescriptorAction = null;
		IndexFieldValue = indexField;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> IndexField(IndexFieldDescriptor descriptor)
	{
		IndexFieldValue = null;
		IndexFieldDescriptorAction = null;
		IndexFieldDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> IndexField(Action<IndexFieldDescriptor> configure)
	{
		IndexFieldValue = null;
		IndexFieldDescriptor = null;
		IndexFieldDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> NumericDetection(bool? numericDetection = true)
	{
		NumericDetectionValue = numericDetection;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TypeMappingDescriptor<TDocument> Runtime(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>, FluentDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>> selector)
	{
		RuntimeValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DataStreamTimestampDescriptor is not null)
		{
			writer.WritePropertyName("_data_stream_timestamp");
			JsonSerializer.Serialize(writer, DataStreamTimestampDescriptor, options);
		}
		else if (DataStreamTimestampDescriptorAction is not null)
		{
			writer.WritePropertyName("_data_stream_timestamp");
			JsonSerializer.Serialize(writer, new DataStreamTimestampDescriptor(DataStreamTimestampDescriptorAction), options);
		}
		else if (DataStreamTimestampValue is not null)
		{
			writer.WritePropertyName("_data_stream_timestamp");
			JsonSerializer.Serialize(writer, DataStreamTimestampValue, options);
		}

		if (FieldNamesDescriptor is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, FieldNamesDescriptor, options);
		}
		else if (FieldNamesDescriptorAction is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, new FieldNamesFieldDescriptor(FieldNamesDescriptorAction), options);
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

		if (RoutingDescriptor is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingDescriptor, options);
		}
		else if (RoutingDescriptorAction is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, new RoutingFieldDescriptor(RoutingDescriptorAction), options);
		}
		else if (RoutingValue is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (SizeDescriptor is not null)
		{
			writer.WritePropertyName("_size");
			JsonSerializer.Serialize(writer, SizeDescriptor, options);
		}
		else if (SizeDescriptorAction is not null)
		{
			writer.WritePropertyName("_size");
			JsonSerializer.Serialize(writer, new SizeFieldDescriptor(SizeDescriptorAction), options);
		}
		else if (SizeValue is not null)
		{
			writer.WritePropertyName("_size");
			JsonSerializer.Serialize(writer, SizeValue, options);
		}

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, new SourceFieldDescriptor(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		if (AllFieldDescriptor is not null)
		{
			writer.WritePropertyName("all_field");
			JsonSerializer.Serialize(writer, AllFieldDescriptor, options);
		}
		else if (AllFieldDescriptorAction is not null)
		{
			writer.WritePropertyName("all_field");
			JsonSerializer.Serialize(writer, new AllFieldDescriptor(AllFieldDescriptorAction), options);
		}
		else if (AllFieldValue is not null)
		{
			writer.WritePropertyName("all_field");
			JsonSerializer.Serialize(writer, AllFieldValue, options);
		}

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
			JsonSerializer.Serialize(writer, DynamicTemplatesValue, options);
		}

		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (IndexFieldDescriptor is not null)
		{
			writer.WritePropertyName("index_field");
			JsonSerializer.Serialize(writer, IndexFieldDescriptor, options);
		}
		else if (IndexFieldDescriptorAction is not null)
		{
			writer.WritePropertyName("index_field");
			JsonSerializer.Serialize(writer, new IndexFieldDescriptor(IndexFieldDescriptorAction), options);
		}
		else if (IndexFieldValue is not null)
		{
			writer.WritePropertyName("index_field");
			JsonSerializer.Serialize(writer, IndexFieldValue, options);
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

		if (RuntimeValue is not null)
		{
			writer.WritePropertyName("runtime");
			JsonSerializer.Serialize(writer, RuntimeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class TypeMappingDescriptor : SerializableDescriptor<TypeMappingDescriptor>
{
	internal TypeMappingDescriptor(Action<TypeMappingDescriptor> configure) => configure.Invoke(this);
	public TypeMappingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp? DataStreamTimestampValue { get; set; }

	private DataStreamTimestampDescriptor DataStreamTimestampDescriptor { get; set; }

	private Action<DataStreamTimestampDescriptor> DataStreamTimestampDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.FieldNamesField? FieldNamesValue { get; set; }

	private FieldNamesFieldDescriptor FieldNamesDescriptor { get; set; }

	private Action<FieldNamesFieldDescriptor> FieldNamesDescriptorAction { get; set; }

	private Dictionary<string, object>? MetaValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.RoutingField? RoutingValue { get; set; }

	private RoutingFieldDescriptor RoutingDescriptor { get; set; }

	private Action<RoutingFieldDescriptor> RoutingDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.SizeField? SizeValue { get; set; }

	private SizeFieldDescriptor SizeDescriptor { get; set; }

	private Action<SizeFieldDescriptor> SizeDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.SourceField? SourceValue { get; set; }

	private SourceFieldDescriptor SourceDescriptor { get; set; }

	private Action<SourceFieldDescriptor> SourceDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.AllField? AllFieldValue { get; set; }

	private AllFieldDescriptor AllFieldDescriptor { get; set; }

	private Action<AllFieldDescriptor> AllFieldDescriptorAction { get; set; }

	private bool? DateDetectionValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

	private IEnumerable<string>? DynamicDateFormatsValue { get; set; }

	private Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>?, IEnumerable<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>>?>? DynamicTemplatesValue { get; set; }

	private bool? EnabledValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.IndexField? IndexFieldValue { get; set; }

	private IndexFieldDescriptor IndexFieldDescriptor { get; set; }

	private Action<IndexFieldDescriptor> IndexFieldDescriptorAction { get; set; }

	private bool? NumericDetectionValue { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

	private Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeValue { get; set; }

	public TypeMappingDescriptor DataStreamTimestamp(Elastic.Clients.Elasticsearch.Mapping.DataStreamTimestamp? dataStreamTimestamp)
	{
		DataStreamTimestampDescriptor = null;
		DataStreamTimestampDescriptorAction = null;
		DataStreamTimestampValue = dataStreamTimestamp;
		return Self;
	}

	public TypeMappingDescriptor DataStreamTimestamp(DataStreamTimestampDescriptor descriptor)
	{
		DataStreamTimestampValue = null;
		DataStreamTimestampDescriptorAction = null;
		DataStreamTimestampDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor DataStreamTimestamp(Action<DataStreamTimestampDescriptor> configure)
	{
		DataStreamTimestampValue = null;
		DataStreamTimestampDescriptor = null;
		DataStreamTimestampDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor FieldNames(Elastic.Clients.Elasticsearch.Mapping.FieldNamesField? fieldNames)
	{
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = null;
		FieldNamesValue = fieldNames;
		return Self;
	}

	public TypeMappingDescriptor FieldNames(FieldNamesFieldDescriptor descriptor)
	{
		FieldNamesValue = null;
		FieldNamesDescriptorAction = null;
		FieldNamesDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor FieldNames(Action<FieldNamesFieldDescriptor> configure)
	{
		FieldNamesValue = null;
		FieldNamesDescriptor = null;
		FieldNamesDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public TypeMappingDescriptor Routing(Elastic.Clients.Elasticsearch.Mapping.RoutingField? routing)
	{
		RoutingDescriptor = null;
		RoutingDescriptorAction = null;
		RoutingValue = routing;
		return Self;
	}

	public TypeMappingDescriptor Routing(RoutingFieldDescriptor descriptor)
	{
		RoutingValue = null;
		RoutingDescriptorAction = null;
		RoutingDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor Routing(Action<RoutingFieldDescriptor> configure)
	{
		RoutingValue = null;
		RoutingDescriptor = null;
		RoutingDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor Size(Elastic.Clients.Elasticsearch.Mapping.SizeField? size)
	{
		SizeDescriptor = null;
		SizeDescriptorAction = null;
		SizeValue = size;
		return Self;
	}

	public TypeMappingDescriptor Size(SizeFieldDescriptor descriptor)
	{
		SizeValue = null;
		SizeDescriptorAction = null;
		SizeDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor Size(Action<SizeFieldDescriptor> configure)
	{
		SizeValue = null;
		SizeDescriptor = null;
		SizeDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor Source(Elastic.Clients.Elasticsearch.Mapping.SourceField? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public TypeMappingDescriptor Source(SourceFieldDescriptor descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor Source(Action<SourceFieldDescriptor> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor AllField(Elastic.Clients.Elasticsearch.Mapping.AllField? allField)
	{
		AllFieldDescriptor = null;
		AllFieldDescriptorAction = null;
		AllFieldValue = allField;
		return Self;
	}

	public TypeMappingDescriptor AllField(AllFieldDescriptor descriptor)
	{
		AllFieldValue = null;
		AllFieldDescriptorAction = null;
		AllFieldDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor AllField(Action<AllFieldDescriptor> configure)
	{
		AllFieldValue = null;
		AllFieldDescriptor = null;
		AllFieldDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor DateDetection(bool? dateDetection = true)
	{
		DateDetectionValue = dateDetection;
		return Self;
	}

	public TypeMappingDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public TypeMappingDescriptor DynamicDateFormats(IEnumerable<string>? dynamicDateFormats)
	{
		DynamicDateFormatsValue = dynamicDateFormats;
		return Self;
	}

	public TypeMappingDescriptor DynamicTemplates(Union<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>?, IEnumerable<Dictionary<string, Elastic.Clients.Elasticsearch.Mapping.DynamicTemplate>>?>? dynamicTemplates)
	{
		DynamicTemplatesValue = dynamicTemplates;
		return Self;
	}

	public TypeMappingDescriptor Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	public TypeMappingDescriptor IndexField(Elastic.Clients.Elasticsearch.Mapping.IndexField? indexField)
	{
		IndexFieldDescriptor = null;
		IndexFieldDescriptorAction = null;
		IndexFieldValue = indexField;
		return Self;
	}

	public TypeMappingDescriptor IndexField(IndexFieldDescriptor descriptor)
	{
		IndexFieldValue = null;
		IndexFieldDescriptorAction = null;
		IndexFieldDescriptor = descriptor;
		return Self;
	}

	public TypeMappingDescriptor IndexField(Action<IndexFieldDescriptor> configure)
	{
		IndexFieldValue = null;
		IndexFieldDescriptor = null;
		IndexFieldDescriptorAction = configure;
		return Self;
	}

	public TypeMappingDescriptor NumericDetection(bool? numericDetection = true)
	{
		NumericDetectionValue = numericDetection;
		return Self;
	}

	public TypeMappingDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public TypeMappingDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TypeMappingDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public TypeMappingDescriptor Runtime(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>, FluentDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>> selector)
	{
		RuntimeValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DataStreamTimestampDescriptor is not null)
		{
			writer.WritePropertyName("_data_stream_timestamp");
			JsonSerializer.Serialize(writer, DataStreamTimestampDescriptor, options);
		}
		else if (DataStreamTimestampDescriptorAction is not null)
		{
			writer.WritePropertyName("_data_stream_timestamp");
			JsonSerializer.Serialize(writer, new DataStreamTimestampDescriptor(DataStreamTimestampDescriptorAction), options);
		}
		else if (DataStreamTimestampValue is not null)
		{
			writer.WritePropertyName("_data_stream_timestamp");
			JsonSerializer.Serialize(writer, DataStreamTimestampValue, options);
		}

		if (FieldNamesDescriptor is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, FieldNamesDescriptor, options);
		}
		else if (FieldNamesDescriptorAction is not null)
		{
			writer.WritePropertyName("_field_names");
			JsonSerializer.Serialize(writer, new FieldNamesFieldDescriptor(FieldNamesDescriptorAction), options);
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

		if (RoutingDescriptor is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingDescriptor, options);
		}
		else if (RoutingDescriptorAction is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, new RoutingFieldDescriptor(RoutingDescriptorAction), options);
		}
		else if (RoutingValue is not null)
		{
			writer.WritePropertyName("_routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (SizeDescriptor is not null)
		{
			writer.WritePropertyName("_size");
			JsonSerializer.Serialize(writer, SizeDescriptor, options);
		}
		else if (SizeDescriptorAction is not null)
		{
			writer.WritePropertyName("_size");
			JsonSerializer.Serialize(writer, new SizeFieldDescriptor(SizeDescriptorAction), options);
		}
		else if (SizeValue is not null)
		{
			writer.WritePropertyName("_size");
			JsonSerializer.Serialize(writer, SizeValue, options);
		}

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, new SourceFieldDescriptor(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		if (AllFieldDescriptor is not null)
		{
			writer.WritePropertyName("all_field");
			JsonSerializer.Serialize(writer, AllFieldDescriptor, options);
		}
		else if (AllFieldDescriptorAction is not null)
		{
			writer.WritePropertyName("all_field");
			JsonSerializer.Serialize(writer, new AllFieldDescriptor(AllFieldDescriptorAction), options);
		}
		else if (AllFieldValue is not null)
		{
			writer.WritePropertyName("all_field");
			JsonSerializer.Serialize(writer, AllFieldValue, options);
		}

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
			JsonSerializer.Serialize(writer, DynamicTemplatesValue, options);
		}

		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (IndexFieldDescriptor is not null)
		{
			writer.WritePropertyName("index_field");
			JsonSerializer.Serialize(writer, IndexFieldDescriptor, options);
		}
		else if (IndexFieldDescriptorAction is not null)
		{
			writer.WritePropertyName("index_field");
			JsonSerializer.Serialize(writer, new IndexFieldDescriptor(IndexFieldDescriptorAction), options);
		}
		else if (IndexFieldValue is not null)
		{
			writer.WritePropertyName("index_field");
			JsonSerializer.Serialize(writer, IndexFieldValue, options);
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

		if (RuntimeValue is not null)
		{
			writer.WritePropertyName("runtime");
			JsonSerializer.Serialize(writer, RuntimeValue, options);
		}

		writer.WriteEndObject();
	}
}