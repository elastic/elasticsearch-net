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
	public sealed partial class DateProcessor
	{
		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("formats")]
		public IEnumerable<string> Formats { get; set; }

		[JsonInclude]
		[JsonPropertyName("if")]
		public string? If { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_failure")]
		public bool? IgnoreFailure { get; set; }

		[JsonInclude]
		[JsonPropertyName("locale")]
		public string? Locale { get; set; }

		[JsonInclude]
		[JsonPropertyName("on_failure")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailure { get; set; }

		[JsonInclude]
		[JsonPropertyName("tag")]
		public string? Tag { get; set; }

		[JsonInclude]
		[JsonPropertyName("target_field")]
		public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }

		[JsonInclude]
		[JsonPropertyName("timezone")]
		public string? Timezone { get; set; }
	}

	public sealed partial class DateProcessorDescriptor<TDocument> : SerializableDescriptorBase<DateProcessorDescriptor<TDocument>>
	{
		internal DateProcessorDescriptor(Action<DateProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DateProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor<TDocument> OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private IEnumerable<string> FormatsValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private string? LocaleValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }

		private string? TimezoneValue { get; set; }

		public DateProcessorDescriptor<TDocument> OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> OnFailure(ProcessorContainerDescriptor<TDocument> descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> OnFailure(Action<ProcessorContainerDescriptor<TDocument>> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> OnFailure(params Action<ProcessorContainerDescriptor<TDocument>>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> Formats(IEnumerable<string> formats)
		{
			FormatsValue = formats;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> Locale(string? locale)
		{
			LocaleValue = locale;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public DateProcessorDescriptor<TDocument> Timezone(string? timezone)
		{
			TimezoneValue = timezone;
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

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			writer.WritePropertyName("formats");
			JsonSerializer.Serialize(writer, FormatsValue, options);
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

			if (!string.IsNullOrEmpty(LocaleValue))
			{
				writer.WritePropertyName("locale");
				writer.WriteStringValue(LocaleValue);
			}

			if (!string.IsNullOrEmpty(TagValue))
			{
				writer.WritePropertyName("tag");
				writer.WriteStringValue(TagValue);
			}

			if (TargetFieldValue is not null)
			{
				writer.WritePropertyName("target_field");
				JsonSerializer.Serialize(writer, TargetFieldValue, options);
			}

			if (!string.IsNullOrEmpty(TimezoneValue))
			{
				writer.WritePropertyName("timezone");
				writer.WriteStringValue(TimezoneValue);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DateProcessorDescriptor : SerializableDescriptorBase<DateProcessorDescriptor>
	{
		internal DateProcessorDescriptor(Action<DateProcessorDescriptor> configure) => configure.Invoke(this);
		public DateProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor>[] OnFailureDescriptorActions { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private IEnumerable<string> FormatsValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private string? LocaleValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }

		private string? TimezoneValue { get; set; }

		public DateProcessorDescriptor OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public DateProcessorDescriptor OnFailure(ProcessorContainerDescriptor descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public DateProcessorDescriptor OnFailure(Action<ProcessorContainerDescriptor> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public DateProcessorDescriptor OnFailure(params Action<ProcessorContainerDescriptor>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public DateProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public DateProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateProcessorDescriptor Formats(IEnumerable<string> formats)
		{
			FormatsValue = formats;
			return Self;
		}

		public DateProcessorDescriptor If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public DateProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public DateProcessorDescriptor Locale(string? locale)
		{
			LocaleValue = locale;
			return Self;
		}

		public DateProcessorDescriptor Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public DateProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public DateProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public DateProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public DateProcessorDescriptor Timezone(string? timezone)
		{
			TimezoneValue = timezone;
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

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			writer.WritePropertyName("formats");
			JsonSerializer.Serialize(writer, FormatsValue, options);
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

			if (!string.IsNullOrEmpty(LocaleValue))
			{
				writer.WritePropertyName("locale");
				writer.WriteStringValue(LocaleValue);
			}

			if (!string.IsNullOrEmpty(TagValue))
			{
				writer.WritePropertyName("tag");
				writer.WriteStringValue(TagValue);
			}

			if (TargetFieldValue is not null)
			{
				writer.WritePropertyName("target_field");
				JsonSerializer.Serialize(writer, TargetFieldValue, options);
			}

			if (!string.IsNullOrEmpty(TimezoneValue))
			{
				writer.WritePropertyName("timezone");
				writer.WriteStringValue(TimezoneValue);
			}

			writer.WriteEndObject();
		}
	}
}