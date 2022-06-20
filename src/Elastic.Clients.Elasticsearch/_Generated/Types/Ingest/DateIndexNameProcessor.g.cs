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
	public partial class DateIndexNameProcessor : ProcessorBase, IProcessorVariant
	{
		[JsonIgnore]
		string IProcessorVariant.ProcessorVariantName => "date_index_name";
		[JsonInclude]
		[JsonPropertyName("date_formats")]
		public IEnumerable<string> DateFormats { get; set; }

		[JsonInclude]
		[JsonPropertyName("date_rounding")]
		public string DateRounding { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("index_name_format")]
		public string IndexNameFormat { get; set; }

		[JsonInclude]
		[JsonPropertyName("index_name_prefix")]
		public string IndexNamePrefix { get; set; }

		[JsonInclude]
		[JsonPropertyName("locale")]
		public string Locale { get; set; }

		[JsonInclude]
		[JsonPropertyName("timezone")]
		public string Timezone { get; set; }
	}

	public sealed partial class DateIndexNameProcessorDescriptor<TDocument> : SerializableDescriptorBase<DateIndexNameProcessorDescriptor<TDocument>>
	{
		internal DateIndexNameProcessorDescriptor(Action<DateIndexNameProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DateIndexNameProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor<TDocument> OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor<TDocument>>[] OnFailureDescriptorActions { get; set; }

		private IEnumerable<string> DateFormatsValue { get; set; }

		private string DateRoundingValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private string IndexNameFormatValue { get; set; }

		private string IndexNamePrefixValue { get; set; }

		private string LocaleValue { get; set; }

		private string? TagValue { get; set; }

		private string TimezoneValue { get; set; }

		public DateIndexNameProcessorDescriptor<TDocument> OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> OnFailure(ProcessorContainerDescriptor<TDocument> descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> OnFailure(Action<ProcessorContainerDescriptor<TDocument>> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> OnFailure(params Action<ProcessorContainerDescriptor<TDocument>>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> DateFormats(IEnumerable<string> dateFormats)
		{
			DateFormatsValue = dateFormats;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> DateRounding(string dateRounding)
		{
			DateRoundingValue = dateRounding;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> IndexNameFormat(string indexNameFormat)
		{
			IndexNameFormatValue = indexNameFormat;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> IndexNamePrefix(string indexNamePrefix)
		{
			IndexNamePrefixValue = indexNamePrefix;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> Locale(string locale)
		{
			LocaleValue = locale;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public DateIndexNameProcessorDescriptor<TDocument> Timezone(string timezone)
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

			writer.WritePropertyName("date_formats");
			JsonSerializer.Serialize(writer, DateFormatsValue, options);
			writer.WritePropertyName("date_rounding");
			writer.WriteStringValue(DateRoundingValue);
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

			writer.WritePropertyName("index_name_format");
			writer.WriteStringValue(IndexNameFormatValue);
			writer.WritePropertyName("index_name_prefix");
			writer.WriteStringValue(IndexNamePrefixValue);
			writer.WritePropertyName("locale");
			writer.WriteStringValue(LocaleValue);
			if (!string.IsNullOrEmpty(TagValue))
			{
				writer.WritePropertyName("tag");
				writer.WriteStringValue(TagValue);
			}

			writer.WritePropertyName("timezone");
			writer.WriteStringValue(TimezoneValue);
			writer.WriteEndObject();
		}
	}

	public sealed partial class DateIndexNameProcessorDescriptor : SerializableDescriptorBase<DateIndexNameProcessorDescriptor>
	{
		internal DateIndexNameProcessorDescriptor(Action<DateIndexNameProcessorDescriptor> configure) => configure.Invoke(this);
		public DateIndexNameProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private ProcessorContainerDescriptor OnFailureDescriptor { get; set; }

		private Action<ProcessorContainerDescriptor> OnFailureDescriptorAction { get; set; }

		private Action<ProcessorContainerDescriptor>[] OnFailureDescriptorActions { get; set; }

		private IEnumerable<string> DateFormatsValue { get; set; }

		private string DateRoundingValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private string IndexNameFormatValue { get; set; }

		private string IndexNamePrefixValue { get; set; }

		private string LocaleValue { get; set; }

		private string? TagValue { get; set; }

		private string TimezoneValue { get; set; }

		public DateIndexNameProcessorDescriptor OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureValue = onFailure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor OnFailure(ProcessorContainerDescriptor descriptor)
		{
			OnFailureValue = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptor = descriptor;
			return Self;
		}

		public DateIndexNameProcessorDescriptor OnFailure(Action<ProcessorContainerDescriptor> configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorActions = null;
			OnFailureDescriptorAction = configure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor OnFailure(params Action<ProcessorContainerDescriptor>[] configure)
		{
			OnFailureValue = null;
			OnFailureDescriptor = null;
			OnFailureDescriptorAction = null;
			OnFailureDescriptorActions = configure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor DateFormats(IEnumerable<string> dateFormats)
		{
			DateFormatsValue = dateFormats;
			return Self;
		}

		public DateIndexNameProcessorDescriptor DateRounding(string dateRounding)
		{
			DateRoundingValue = dateRounding;
			return Self;
		}

		public DateIndexNameProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public DateIndexNameProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateIndexNameProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public DateIndexNameProcessorDescriptor If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public DateIndexNameProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public DateIndexNameProcessorDescriptor IndexNameFormat(string indexNameFormat)
		{
			IndexNameFormatValue = indexNameFormat;
			return Self;
		}

		public DateIndexNameProcessorDescriptor IndexNamePrefix(string indexNamePrefix)
		{
			IndexNamePrefixValue = indexNamePrefix;
			return Self;
		}

		public DateIndexNameProcessorDescriptor Locale(string locale)
		{
			LocaleValue = locale;
			return Self;
		}

		public DateIndexNameProcessorDescriptor Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public DateIndexNameProcessorDescriptor Timezone(string timezone)
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

			writer.WritePropertyName("date_formats");
			JsonSerializer.Serialize(writer, DateFormatsValue, options);
			writer.WritePropertyName("date_rounding");
			writer.WriteStringValue(DateRoundingValue);
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

			writer.WritePropertyName("index_name_format");
			writer.WriteStringValue(IndexNameFormatValue);
			writer.WritePropertyName("index_name_prefix");
			writer.WriteStringValue(IndexNamePrefixValue);
			writer.WritePropertyName("locale");
			writer.WriteStringValue(LocaleValue);
			if (!string.IsNullOrEmpty(TagValue))
			{
				writer.WritePropertyName("tag");
				writer.WriteStringValue(TagValue);
			}

			writer.WritePropertyName("timezone");
			writer.WriteStringValue(TimezoneValue);
			writer.WriteEndObject();
		}
	}
}