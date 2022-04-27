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
	public partial class AttachmentProcessor : ProcessorBase, IProcessorContainerVariant
	{
		[JsonIgnore]
		string IProcessorContainerVariant.ProcessorContainerVariantName => "attachment";
		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_missing")]
		public bool? IgnoreMissing { get; set; }

		[JsonInclude]
		[JsonPropertyName("indexed_chars")]
		public long? IndexedChars { get; set; }

		[JsonInclude]
		[JsonPropertyName("indexed_chars_field")]
		public Elastic.Clients.Elasticsearch.Field? IndexedCharsField { get; set; }

		[JsonInclude]
		[JsonPropertyName("properties")]
		public IEnumerable<string>? Properties { get; set; }

		[JsonInclude]
		[JsonPropertyName("resource_name")]
		public string? ResourceName { get; set; }

		[JsonInclude]
		[JsonPropertyName("target_field")]
		public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }
	}

	public sealed partial class AttachmentProcessorDescriptor<TDocument> : SerializableDescriptorBase<AttachmentProcessorDescriptor<TDocument>>
	{
		internal AttachmentProcessorDescriptor(Action<AttachmentProcessorDescriptor<TDocument>> configure) => configure.Invoke(this);
		public AttachmentProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private bool? IgnoreMissingValue { get; set; }

		private long? IndexedCharsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? IndexedCharsFieldValue { get; set; }

		private IEnumerable<string>? PropertiesValue { get; set; }

		private string? ResourceNameValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }

		public AttachmentProcessorDescriptor<TDocument> OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureValue = onFailure;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> IgnoreMissing(bool? ignoreMissing = true)
		{
			IgnoreMissingValue = ignoreMissing;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> IndexedChars(long? indexedChars)
		{
			IndexedCharsValue = indexedChars;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> IndexedCharsField(Elastic.Clients.Elasticsearch.Field? indexedCharsField)
		{
			IndexedCharsFieldValue = indexedCharsField;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> IndexedCharsField<TValue>(Expression<Func<TDocument, TValue>> indexedCharsField)
		{
			IndexedCharsFieldValue = indexedCharsField;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> Properties(IEnumerable<string>? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> ResourceName(string? resourceName)
		{
			ResourceNameValue = resourceName;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public AttachmentProcessorDescriptor<TDocument> TargetField<TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (OnFailureValue is not null)
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

			if (IndexedCharsValue.HasValue)
			{
				writer.WritePropertyName("indexed_chars");
				writer.WriteNumberValue(IndexedCharsValue.Value);
			}

			if (IndexedCharsFieldValue is not null)
			{
				writer.WritePropertyName("indexed_chars_field");
				JsonSerializer.Serialize(writer, IndexedCharsFieldValue, options);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (!string.IsNullOrEmpty(ResourceNameValue))
			{
				writer.WritePropertyName("resource_name");
				writer.WriteStringValue(ResourceNameValue);
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

			writer.WriteEndObject();
		}
	}

	public sealed partial class AttachmentProcessorDescriptor : SerializableDescriptorBase<AttachmentProcessorDescriptor>
	{
		internal AttachmentProcessorDescriptor(Action<AttachmentProcessorDescriptor> configure) => configure.Invoke(this);
		public AttachmentProcessorDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? OnFailureValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private string? IfValue { get; set; }

		private bool? IgnoreFailureValue { get; set; }

		private bool? IgnoreMissingValue { get; set; }

		private long? IndexedCharsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? IndexedCharsFieldValue { get; set; }

		private IEnumerable<string>? PropertiesValue { get; set; }

		private string? ResourceNameValue { get; set; }

		private string? TagValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? TargetFieldValue { get; set; }

		public AttachmentProcessorDescriptor OnFailure(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.ProcessorContainer>? onFailure)
		{
			OnFailureValue = onFailure;
			return Self;
		}

		public AttachmentProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public AttachmentProcessorDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public AttachmentProcessorDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public AttachmentProcessorDescriptor If(string? ifValue)
		{
			IfValue = ifValue;
			return Self;
		}

		public AttachmentProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true)
		{
			IgnoreFailureValue = ignoreFailure;
			return Self;
		}

		public AttachmentProcessorDescriptor IgnoreMissing(bool? ignoreMissing = true)
		{
			IgnoreMissingValue = ignoreMissing;
			return Self;
		}

		public AttachmentProcessorDescriptor IndexedChars(long? indexedChars)
		{
			IndexedCharsValue = indexedChars;
			return Self;
		}

		public AttachmentProcessorDescriptor IndexedCharsField(Elastic.Clients.Elasticsearch.Field? indexedCharsField)
		{
			IndexedCharsFieldValue = indexedCharsField;
			return Self;
		}

		public AttachmentProcessorDescriptor IndexedCharsField<TDocument, TValue>(Expression<Func<TDocument, TValue>> indexedCharsField)
		{
			IndexedCharsFieldValue = indexedCharsField;
			return Self;
		}

		public AttachmentProcessorDescriptor IndexedCharsField<TDocument>(Expression<Func<TDocument, object>> indexedCharsField)
		{
			IndexedCharsFieldValue = indexedCharsField;
			return Self;
		}

		public AttachmentProcessorDescriptor Properties(IEnumerable<string>? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public AttachmentProcessorDescriptor ResourceName(string? resourceName)
		{
			ResourceNameValue = resourceName;
			return Self;
		}

		public AttachmentProcessorDescriptor Tag(string? tag)
		{
			TagValue = tag;
			return Self;
		}

		public AttachmentProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public AttachmentProcessorDescriptor TargetField<TDocument, TValue>(Expression<Func<TDocument, TValue>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		public AttachmentProcessorDescriptor TargetField<TDocument>(Expression<Func<TDocument, object>> targetField)
		{
			TargetFieldValue = targetField;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (OnFailureValue is not null)
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

			if (IndexedCharsValue.HasValue)
			{
				writer.WritePropertyName("indexed_chars");
				writer.WriteNumberValue(IndexedCharsValue.Value);
			}

			if (IndexedCharsFieldValue is not null)
			{
				writer.WritePropertyName("indexed_chars_field");
				JsonSerializer.Serialize(writer, IndexedCharsFieldValue, options);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (!string.IsNullOrEmpty(ResourceNameValue))
			{
				writer.WritePropertyName("resource_name");
				writer.WriteStringValue(ResourceNameValue);
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

			writer.WriteEndObject();
		}
	}
}