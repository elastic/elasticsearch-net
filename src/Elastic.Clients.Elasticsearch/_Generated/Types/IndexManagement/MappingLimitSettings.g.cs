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
namespace Elastic.Clients.Elasticsearch.IndexManagement;
public sealed partial class MappingLimitSettings
{
	[JsonInclude, JsonPropertyName("coerce")]
	public bool? Coerce { get; set; }

	[JsonInclude, JsonPropertyName("depth")]
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsDepth? Depth { get; set; }

	[JsonInclude, JsonPropertyName("dimension_fields")]
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsDimensionFields? DimensionFields { get; set; }

	[JsonInclude, JsonPropertyName("field_name_length")]
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength? FieldNameLength { get; set; }

	[JsonInclude, JsonPropertyName("ignore_malformed")]
	public bool? IgnoreMalformed { get; set; }

	[JsonInclude, JsonPropertyName("nested_fields")]
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsNestedFields? NestedFields { get; set; }

	[JsonInclude, JsonPropertyName("nested_objects")]
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsNestedObjects? NestedObjects { get; set; }

	[JsonInclude, JsonPropertyName("total_fields")]
	public Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsTotalFields? TotalFields { get; set; }
}

public sealed partial class MappingLimitSettingsDescriptor : SerializableDescriptor<MappingLimitSettingsDescriptor>
{
	internal MappingLimitSettingsDescriptor(Action<MappingLimitSettingsDescriptor> configure) => configure.Invoke(this);
	public MappingLimitSettingsDescriptor() : base()
	{
	}

	private bool? CoerceValue { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsDepth? DepthValue { get; set; }

	private MappingLimitSettingsDepthDescriptor DepthDescriptor { get; set; }

	private Action<MappingLimitSettingsDepthDescriptor> DepthDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsDimensionFields? DimensionFieldsValue { get; set; }

	private MappingLimitSettingsDimensionFieldsDescriptor DimensionFieldsDescriptor { get; set; }

	private Action<MappingLimitSettingsDimensionFieldsDescriptor> DimensionFieldsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength? FieldNameLengthValue { get; set; }

	private MappingLimitSettingsFieldNameLengthDescriptor FieldNameLengthDescriptor { get; set; }

	private Action<MappingLimitSettingsFieldNameLengthDescriptor> FieldNameLengthDescriptorAction { get; set; }

	private bool? IgnoreMalformedValue { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsNestedFields? NestedFieldsValue { get; set; }

	private MappingLimitSettingsNestedFieldsDescriptor NestedFieldsDescriptor { get; set; }

	private Action<MappingLimitSettingsNestedFieldsDescriptor> NestedFieldsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsNestedObjects? NestedObjectsValue { get; set; }

	private MappingLimitSettingsNestedObjectsDescriptor NestedObjectsDescriptor { get; set; }

	private Action<MappingLimitSettingsNestedObjectsDescriptor> NestedObjectsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsTotalFields? TotalFieldsValue { get; set; }

	private MappingLimitSettingsTotalFieldsDescriptor TotalFieldsDescriptor { get; set; }

	private Action<MappingLimitSettingsTotalFieldsDescriptor> TotalFieldsDescriptorAction { get; set; }

	public MappingLimitSettingsDescriptor Coerce(bool? coerce = true)
	{
		CoerceValue = coerce;
		return Self;
	}

	public MappingLimitSettingsDescriptor Depth(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsDepth? depth)
	{
		DepthDescriptor = null;
		DepthDescriptorAction = null;
		DepthValue = depth;
		return Self;
	}

	public MappingLimitSettingsDescriptor Depth(MappingLimitSettingsDepthDescriptor descriptor)
	{
		DepthValue = null;
		DepthDescriptorAction = null;
		DepthDescriptor = descriptor;
		return Self;
	}

	public MappingLimitSettingsDescriptor Depth(Action<MappingLimitSettingsDepthDescriptor> configure)
	{
		DepthValue = null;
		DepthDescriptor = null;
		DepthDescriptorAction = configure;
		return Self;
	}

	public MappingLimitSettingsDescriptor DimensionFields(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsDimensionFields? dimensionFields)
	{
		DimensionFieldsDescriptor = null;
		DimensionFieldsDescriptorAction = null;
		DimensionFieldsValue = dimensionFields;
		return Self;
	}

	public MappingLimitSettingsDescriptor DimensionFields(MappingLimitSettingsDimensionFieldsDescriptor descriptor)
	{
		DimensionFieldsValue = null;
		DimensionFieldsDescriptorAction = null;
		DimensionFieldsDescriptor = descriptor;
		return Self;
	}

	public MappingLimitSettingsDescriptor DimensionFields(Action<MappingLimitSettingsDimensionFieldsDescriptor> configure)
	{
		DimensionFieldsValue = null;
		DimensionFieldsDescriptor = null;
		DimensionFieldsDescriptorAction = configure;
		return Self;
	}

	public MappingLimitSettingsDescriptor FieldNameLength(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsFieldNameLength? fieldNameLength)
	{
		FieldNameLengthDescriptor = null;
		FieldNameLengthDescriptorAction = null;
		FieldNameLengthValue = fieldNameLength;
		return Self;
	}

	public MappingLimitSettingsDescriptor FieldNameLength(MappingLimitSettingsFieldNameLengthDescriptor descriptor)
	{
		FieldNameLengthValue = null;
		FieldNameLengthDescriptorAction = null;
		FieldNameLengthDescriptor = descriptor;
		return Self;
	}

	public MappingLimitSettingsDescriptor FieldNameLength(Action<MappingLimitSettingsFieldNameLengthDescriptor> configure)
	{
		FieldNameLengthValue = null;
		FieldNameLengthDescriptor = null;
		FieldNameLengthDescriptorAction = configure;
		return Self;
	}

	public MappingLimitSettingsDescriptor IgnoreMalformed(bool? ignoreMalformed = true)
	{
		IgnoreMalformedValue = ignoreMalformed;
		return Self;
	}

	public MappingLimitSettingsDescriptor NestedFields(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsNestedFields? nestedFields)
	{
		NestedFieldsDescriptor = null;
		NestedFieldsDescriptorAction = null;
		NestedFieldsValue = nestedFields;
		return Self;
	}

	public MappingLimitSettingsDescriptor NestedFields(MappingLimitSettingsNestedFieldsDescriptor descriptor)
	{
		NestedFieldsValue = null;
		NestedFieldsDescriptorAction = null;
		NestedFieldsDescriptor = descriptor;
		return Self;
	}

	public MappingLimitSettingsDescriptor NestedFields(Action<MappingLimitSettingsNestedFieldsDescriptor> configure)
	{
		NestedFieldsValue = null;
		NestedFieldsDescriptor = null;
		NestedFieldsDescriptorAction = configure;
		return Self;
	}

	public MappingLimitSettingsDescriptor NestedObjects(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsNestedObjects? nestedObjects)
	{
		NestedObjectsDescriptor = null;
		NestedObjectsDescriptorAction = null;
		NestedObjectsValue = nestedObjects;
		return Self;
	}

	public MappingLimitSettingsDescriptor NestedObjects(MappingLimitSettingsNestedObjectsDescriptor descriptor)
	{
		NestedObjectsValue = null;
		NestedObjectsDescriptorAction = null;
		NestedObjectsDescriptor = descriptor;
		return Self;
	}

	public MappingLimitSettingsDescriptor NestedObjects(Action<MappingLimitSettingsNestedObjectsDescriptor> configure)
	{
		NestedObjectsValue = null;
		NestedObjectsDescriptor = null;
		NestedObjectsDescriptorAction = configure;
		return Self;
	}

	public MappingLimitSettingsDescriptor TotalFields(Elastic.Clients.Elasticsearch.IndexManagement.MappingLimitSettingsTotalFields? totalFields)
	{
		TotalFieldsDescriptor = null;
		TotalFieldsDescriptorAction = null;
		TotalFieldsValue = totalFields;
		return Self;
	}

	public MappingLimitSettingsDescriptor TotalFields(MappingLimitSettingsTotalFieldsDescriptor descriptor)
	{
		TotalFieldsValue = null;
		TotalFieldsDescriptorAction = null;
		TotalFieldsDescriptor = descriptor;
		return Self;
	}

	public MappingLimitSettingsDescriptor TotalFields(Action<MappingLimitSettingsTotalFieldsDescriptor> configure)
	{
		TotalFieldsValue = null;
		TotalFieldsDescriptor = null;
		TotalFieldsDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CoerceValue.HasValue)
		{
			writer.WritePropertyName("coerce");
			writer.WriteBooleanValue(CoerceValue.Value);
		}

		if (DepthDescriptor is not null)
		{
			writer.WritePropertyName("depth");
			JsonSerializer.Serialize(writer, DepthDescriptor, options);
		}
		else if (DepthDescriptorAction is not null)
		{
			writer.WritePropertyName("depth");
			JsonSerializer.Serialize(writer, new MappingLimitSettingsDepthDescriptor(DepthDescriptorAction), options);
		}
		else if (DepthValue is not null)
		{
			writer.WritePropertyName("depth");
			JsonSerializer.Serialize(writer, DepthValue, options);
		}

		if (DimensionFieldsDescriptor is not null)
		{
			writer.WritePropertyName("dimension_fields");
			JsonSerializer.Serialize(writer, DimensionFieldsDescriptor, options);
		}
		else if (DimensionFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("dimension_fields");
			JsonSerializer.Serialize(writer, new MappingLimitSettingsDimensionFieldsDescriptor(DimensionFieldsDescriptorAction), options);
		}
		else if (DimensionFieldsValue is not null)
		{
			writer.WritePropertyName("dimension_fields");
			JsonSerializer.Serialize(writer, DimensionFieldsValue, options);
		}

		if (FieldNameLengthDescriptor is not null)
		{
			writer.WritePropertyName("field_name_length");
			JsonSerializer.Serialize(writer, FieldNameLengthDescriptor, options);
		}
		else if (FieldNameLengthDescriptorAction is not null)
		{
			writer.WritePropertyName("field_name_length");
			JsonSerializer.Serialize(writer, new MappingLimitSettingsFieldNameLengthDescriptor(FieldNameLengthDescriptorAction), options);
		}
		else if (FieldNameLengthValue is not null)
		{
			writer.WritePropertyName("field_name_length");
			JsonSerializer.Serialize(writer, FieldNameLengthValue, options);
		}

		if (IgnoreMalformedValue.HasValue)
		{
			writer.WritePropertyName("ignore_malformed");
			writer.WriteBooleanValue(IgnoreMalformedValue.Value);
		}

		if (NestedFieldsDescriptor is not null)
		{
			writer.WritePropertyName("nested_fields");
			JsonSerializer.Serialize(writer, NestedFieldsDescriptor, options);
		}
		else if (NestedFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("nested_fields");
			JsonSerializer.Serialize(writer, new MappingLimitSettingsNestedFieldsDescriptor(NestedFieldsDescriptorAction), options);
		}
		else if (NestedFieldsValue is not null)
		{
			writer.WritePropertyName("nested_fields");
			JsonSerializer.Serialize(writer, NestedFieldsValue, options);
		}

		if (NestedObjectsDescriptor is not null)
		{
			writer.WritePropertyName("nested_objects");
			JsonSerializer.Serialize(writer, NestedObjectsDescriptor, options);
		}
		else if (NestedObjectsDescriptorAction is not null)
		{
			writer.WritePropertyName("nested_objects");
			JsonSerializer.Serialize(writer, new MappingLimitSettingsNestedObjectsDescriptor(NestedObjectsDescriptorAction), options);
		}
		else if (NestedObjectsValue is not null)
		{
			writer.WritePropertyName("nested_objects");
			JsonSerializer.Serialize(writer, NestedObjectsValue, options);
		}

		if (TotalFieldsDescriptor is not null)
		{
			writer.WritePropertyName("total_fields");
			JsonSerializer.Serialize(writer, TotalFieldsDescriptor, options);
		}
		else if (TotalFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("total_fields");
			JsonSerializer.Serialize(writer, new MappingLimitSettingsTotalFieldsDescriptor(TotalFieldsDescriptorAction), options);
		}
		else if (TotalFieldsValue is not null)
		{
			writer.WritePropertyName("total_fields");
			JsonSerializer.Serialize(writer, TotalFieldsValue, options);
		}

		writer.WriteEndObject();
	}
}