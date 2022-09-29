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
	public sealed partial class CircleProcessor
	{
		[JsonInclude]
		[JsonPropertyName("error_distance")]
		public double ErrorDistance { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("if")]
		public string? If { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_failure")]
		public bool? IgnoreFailure { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_missing")]
		public bool IgnoreMissing { get; set; }

		[JsonInclude]
		[JsonPropertyName("on_failure")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailure { get; set; }

		[JsonInclude]
		[JsonPropertyName("shape_type")]
		public Elastic.Clients.Elasticsearch.Ingest.ShapeType ShapeType { get; set; }

		[JsonInclude]
		[JsonPropertyName("tag")]
		public string? Tag { get; set; }

		[JsonInclude]
		[JsonPropertyName("target_field")]
		public Elastic.Clients.Elasticsearch.Field TargetField { get; set; }
	}

	public sealed partial class CircleProcessorDescriptor<TDocument> : SerializableDescriptorBase<CircleProcessorDescriptor<TDocument>>
	{
		internal CircleProcessorDescriptor(Action<CircleProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
		public CircleProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor<TDocument> OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }

		private double ErrorDistanceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private bool IgnoreMissingValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ingest.ShapeType ShapeTypeValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field TargetFieldValue { get; set; }

		public CircleProcessorDescriptor<TDocument> OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> OnFailure(ProcessorContainerDescriptor<TDocument> descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> OnFailure(Action<ProcessorContainerDescriptor<TDocument>> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> OnFailure(params Action<ProcessorContainerDescriptor<TDocument>>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> ErrorDistance(double errorDistance)
		{
			ErrorDistanceValue = errorDistance;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> IgnoreMissing(bool ignoreMissing = true)
		{
			IgnoreMissingValue = ignoreMissing;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> ShapeType(Elastic.Clients.Elasticsearch.Ingest.ShapeType shapeType)
		{
			ShapeTypeValue = shapeType;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public CircleProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
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
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
				writer.WriteEndArray();
			}
			else if (OnFailureDescriptorAction is not null)
			{
				writer.WritePropertyName("on_failure");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new ProcessorContainerDescriptor<TDocument>(OnFailureDescriptorAction), options);
				writer.WriteEndArray();
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

			writer.WritePropertyName("error_distance");
			writer.WriteNumberValue(ErrorDistanceValue);
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

			writer.WritePropertyName("ignore_missing");
			writer.WriteBooleanValue(IgnoreMissingValue);
			writer.WritePropertyName("shape_type");
			JsonSerializer.Serialize(writer, ShapeTypeValue, options);
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

	public sealed partial class CircleProcessorDescriptor : SerializableDescriptorBase<CircleProcessorDescriptor>
	{
		internal CircleProcessorDescriptor(Action<CircleProcessorDescriptor> configure) => configure.Invoke(this);
		public CircleProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor>[] OnFailureDescriptorActions { get; set; }

		private double ErrorDistanceValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private bool IgnoreMissingValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ingest.ShapeType ShapeTypeValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field TargetFieldValue { get; set; }

		public CircleProcessorDescriptor OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public CircleProcessorDescriptor OnFailure(ProcessorContainerDescriptor descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public CircleProcessorDescriptor OnFailure(Action<ProcessorContainerDescriptor> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public CircleProcessorDescriptor OnFailure(params Action<ProcessorContainerDescriptor>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public CircleProcessorDescriptor ErrorDistance(double errorDistance)
		{
			ErrorDistanceValue = errorDistance;
			return Self;
		}

		public CircleProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public CircleProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public CircleProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public CircleProcessorDescriptor If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public CircleProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public CircleProcessorDescriptor IgnoreMissing(bool ignoreMissing = true)
		{
			IgnoreMissingValue = ignoreMissing;
			return Self;
		}

		public CircleProcessorDescriptor ShapeType(Elastic.Clients.Elasticsearch.Ingest.ShapeType shapeType)
		{
			ShapeTypeValue = shapeType;
			return Self;
		}

		public CircleProcessorDescriptor Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public CircleProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public CircleProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public CircleProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
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
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, OnFailureDescriptor, options);
				writer.WriteEndArray();
			}
			else if (OnFailureDescriptorAction is not null)
			{
				writer.WritePropertyName("on_failure");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new ProcessorContainerDescriptor(OnFailureDescriptorAction), options);
				writer.WriteEndArray();
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

			writer.WritePropertyName("error_distance");
			writer.WriteNumberValue(ErrorDistanceValue);
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

			writer.WritePropertyName("ignore_missing");
			writer.WriteBooleanValue(IgnoreMissingValue);
			writer.WritePropertyName("shape_type");
			JsonSerializer.Serialize(writer, ShapeTypeValue, options);
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