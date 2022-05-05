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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ingest
{
	public partial class EnrichProcessor : ProcessorBase, IProcessorContainerVariant
	{
		[JsonIgnore]
		string IProcessorContainerVariant.ProcessorContainerVariantName => "enrich";
		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_missing")]
		public bool? IgnoreMissing { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_matches")]
		public int? MaxMatches { get; set; }

		[JsonInclude]
		[JsonPropertyName("override")]
		public bool? Override { get; set; }

		[JsonInclude]
		[JsonPropertyName("policy_name")]
		public string PolicyName { get; set; }

		[JsonInclude]
		[JsonPropertyName("shape_relation")]
		public Elastic.Clients.Elasticsearch.GeoShapeRelation? ShapeRelation { get; set; }

		[JsonInclude]
		[JsonPropertyName("target_field")]
		public Elastic.Clients.Elasticsearch.Field TargetField { get; set; }
	}

	public sealed partial class EnrichProcessorDescriptor<TDocument> : SerializableDescriptorBase<EnrichProcessorDescriptor<TDocument>>
	{
		internal EnrichProcessorDescriptor(Action<EnrichProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
		public EnrichProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor<TDocument> OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private bool? IgnoreMissingValue { get; set; }

		private int? MaxMatchesValue { get; set; }

		private bool? OverrideValue { get; set; }

		private string PolicyNameValue { get; set; }

		private Elastic.Clients.Elasticsearch.GeoShapeRelation? ShapeRelationValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field TargetFieldValue { get; set; }

		public EnrichProcessorDescriptor<TDocument> OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> OnFailure(ProcessorContainerDescriptor<TDocument> descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> OnFailure(Action<ProcessorContainerDescriptor<TDocument>> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> OnFailure(params Action<ProcessorContainerDescriptor<TDocument>>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> IgnoreMissing(bool? ignoreMissing = true)
		{
			IgnoreMissingValue = ignoreMissing;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> MaxMatches(int? maxMatches)
		{
			MaxMatchesValue = maxMatches;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> Override(bool? overrideValue = true)
		{
			OverrideValue = overrideValue;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> PolicyName(string policyName)
		{
			PolicyNameValue = policyName;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> ShapeRelation(Elastic.Clients.Elasticsearch.GeoShapeRelation? shapeRelation)
		{
			ShapeRelationValue = shapeRelation;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public EnrichProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (OnFailureDescriptor is not null)
			{
				writer.WritePropertyName("on_failure");
				JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
			}
			else if (OnFailureDescriptorAction is not null)
			{
				writer.WritePropertyName("on_failure");
				JsonSerializer.Serialize(writer, new ProcessorContainerDescriptor<TDocument>(OnFailureDescriptorAction), options);
			}
			else if (OnFailureDescriptorActions is not null)
			{
				writer.WritePropertyName("on_failure");
				writer.WriteStartArray();
				foreach (var action in OnFailureDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new ProcessorContainerDescriptor<TDocument>(action), options);
				}

				writer.WriteEndArray();
			}
			else if (OnFailureValue is not null)
			{
				writer.WritePropertyName("on_failure");
				JsonSerializer.Serialize(writer, OnFailureValue, options);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (!string.IsNullOrEmpty(IfValue))
			{
				writer.WritePropertyName("if");
				writer.WriteStringValue(IfValue);
			}

			if (IgnoreFailureValue.HasValue)
			{
				writer.WritePropertyName("ignore_failure");
				writer.WriteBooleanValue(IgnoreFailureValue.Value);
			}

			if (IgnoreMissingValue.HasValue)
			{
				writer.WritePropertyName("ignore_missing");
				writer.WriteBooleanValue(IgnoreMissingValue.Value);
			}

			if (MaxMatchesValue.HasValue)
			{
				writer.WritePropertyName("max_matches");
				writer.WriteNumberValue(MaxMatchesValue.Value);
			}

			if (OverrideValue.HasValue)
			{
				writer.WritePropertyName("override");
				writer.WriteBooleanValue(OverrideValue.Value);
			}

			writer.WritePropertyName("policy_name");
			writer.WriteStringValue(PolicyNameValue);
			if (ShapeRelationValue is not null)
			{
				writer.WritePropertyName("shape_relation");
				JsonSerializer.Serialize(writer, ShapeRelationValue, options);
			}

			if (!string.IsNullOrEmpty(TagValue))
			{
				writer.WritePropertyName("tag");
				writer.WriteStringValue(TagValue);
			}

			writer.WritePropertyName("target_field");
			JsonSerializer.Serialize(writer, TargetFieldValue, options);
			writer.WriteEndObject();
		}
	}

	public sealed partial class EnrichProcessorDescriptor : SerializableDescriptorBase<EnrichProcessorDescriptor>
	{
		internal EnrichProcessorDescriptor(Action<EnrichProcessorDescriptor> configure) => configure.Invoke(this);
		public EnrichProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor>[] OnFailureDescriptorActions { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private bool? IgnoreMissingValue { get; set; }

		private int? MaxMatchesValue { get; set; }

		private bool? OverrideValue { get; set; }

		private string PolicyNameValue { get; set; }

		private Elastic.Clients.Elasticsearch.GeoShapeRelation? ShapeRelationValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field TargetFieldValue { get; set; }

		public EnrichProcessorDescriptor OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public EnrichProcessorDescriptor OnFailure(ProcessorContainerDescriptor descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public EnrichProcessorDescriptor OnFailure(Action<ProcessorContainerDescriptor> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public EnrichProcessorDescriptor OnFailure(params Action<ProcessorContainerDescriptor>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public EnrichProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public EnrichProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public EnrichProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public EnrichProcessorDescriptor If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public EnrichProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public EnrichProcessorDescriptor IgnoreMissing(bool? ignoreMissing = true)
		{
			IgnoreMissingValue = ignoreMissing;
			return Self;
		}

		public EnrichProcessorDescriptor MaxMatches(int? maxMatches)
		{
			MaxMatchesValue = maxMatches;
			return Self;
		}

		public EnrichProcessorDescriptor Override(bool? overrideValue = true)
		{
			OverrideValue = overrideValue;
			return Self;
		}

		public EnrichProcessorDescriptor PolicyName(string policyName)
		{
			PolicyNameValue = policyName;
			return Self;
		}

		public EnrichProcessorDescriptor ShapeRelation(Elastic.Clients.Elasticsearch.GeoShapeRelation? shapeRelation)
		{
			ShapeRelationValue = shapeRelation;
			return Self;
		}

		public EnrichProcessorDescriptor Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public EnrichProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public EnrichProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public EnrichProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (OnFailureDescriptor is not null)
			{
				writer.WritePropertyName("on_failure");
				JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
			}
			else if (OnFailureDescriptorAction is not null)
			{
				writer.WritePropertyName("on_failure");
				JsonSerializer.Serialize(writer, new ProcessorContainerDescriptor(OnFailureDescriptorAction), options);
			}
			else if (OnFailureDescriptorActions is not null)
			{
				writer.WritePropertyName("on_failure");
				writer.WriteStartArray();
				foreach (var action in OnFailureDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new ProcessorContainerDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (OnFailureValue is not null)
			{
				writer.WritePropertyName("on_failure");
				JsonSerializer.Serialize(writer, OnFailureValue, options);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (!string.IsNullOrEmpty(IfValue))
			{
				writer.WritePropertyName("if");
				writer.WriteStringValue(IfValue);
			}

			if (IgnoreFailureValue.HasValue)
			{
				writer.WritePropertyName("ignore_failure");
				writer.WriteBooleanValue(IgnoreFailureValue.Value);
			}

			if (IgnoreMissingValue.HasValue)
			{
				writer.WritePropertyName("ignore_missing");
				writer.WriteBooleanValue(IgnoreMissingValue.Value);
			}

			if (MaxMatchesValue.HasValue)
			{
				writer.WritePropertyName("max_matches");
				writer.WriteNumberValue(MaxMatchesValue.Value);
			}

			if (OverrideValue.HasValue)
			{
				writer.WritePropertyName("override");
				writer.WriteBooleanValue(OverrideValue.Value);
			}

			writer.WritePropertyName("policy_name");
			writer.WriteStringValue(PolicyNameValue);
			if (ShapeRelationValue is not null)
			{
				writer.WritePropertyName("shape_relation");
				JsonSerializer.Serialize(writer, ShapeRelationValue, options);
			}

			if (!string.IsNullOrEmpty(TagValue))
			{
				writer.WritePropertyName("tag");
				writer.WriteStringValue(TagValue);
			}

			writer.WritePropertyName("target_field");
			JsonSerializer.Serialize(writer, TargetFieldValue, options);
			writer.WriteEndObject();
		}
	}
}