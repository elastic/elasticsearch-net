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
namespace Elastic.Clients.Elasticsearch.Mapping
{
	public partial class CompletionProperty : DocValuesPropertyBase, IProperty
	{
		[JsonInclude]
		[JsonPropertyName("analyzer")]
		public string? Analyzer { get; set; }

		[JsonInclude]
		[JsonPropertyName("contexts")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Mapping.SuggestContext>? Contexts { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_input_length")]
		public int? MaxInputLength { get; set; }

		[JsonInclude]
		[JsonPropertyName("preserve_position_increments")]
		public bool? PreservePositionIncrements { get; set; }

		[JsonInclude]
		[JsonPropertyName("preserve_separators")]
		public bool? PreserveSeparators { get; set; }

		[JsonInclude]
		[JsonPropertyName("search_analyzer")]
		public string? SearchAnalyzer { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "completion";
	}

	public sealed partial class CompletionPropertyDescriptor<TDocument> : SerializableDescriptorBase<CompletionPropertyDescriptor<TDocument>>, IBuildableDescriptor<CompletionProperty>
	{
		internal CompletionPropertyDescriptor(Action<CompletionPropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public CompletionPropertyDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Mapping.SuggestContext>? ContextsValue { get; set; }

		private SuggestContextDescriptor<TDocument> ContextsDescriptor { get; set; }

		private Action<SuggestContextDescriptor<TDocument>> ContextsDescriptorAction { get; set; }

		private Action<SuggestContextDescriptor<TDocument>>[] ContextsDescriptorActions { get; set; }

		private string? AnalyzerValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private PropertiesDescriptor<TDocument> FieldsDescriptor { get; set; }

		private Action<PropertiesDescriptor<TDocument>> FieldsDescriptorAction { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private int? MaxInputLengthValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private bool? PreservePositionIncrementsValue { get; set; }

		private bool? PreserveSeparatorsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private PropertiesDescriptor<TDocument> PropertiesDescriptor { get; set; }

		private Action<PropertiesDescriptor<TDocument>> PropertiesDescriptorAction { get; set; }

		private string? SearchAnalyzerValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		public CompletionPropertyDescriptor<TDocument> Contexts(IEnumerable<Elastic.Clients.Elasticsearch.Mapping.SuggestContext>? contexts)
		{
			ContextsDescriptor = null;
			ContextsDescriptorAction = null;
			ContextsDescriptorActions = null;
			ContextsValue = contexts;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Contexts(SuggestContextDescriptor<TDocument> descriptor)
		{
			ContextsValue = null;
			ContextsDescriptorAction = null;
			ContextsDescriptorActions = null;
			ContextsDescriptor = descriptor;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Contexts(Action<SuggestContextDescriptor<TDocument>> configure)
		{
			ContextsValue = null;
			ContextsDescriptor = null;
			ContextsDescriptorActions = null;
			ContextsDescriptorAction = configure;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Contexts(params Action<SuggestContextDescriptor<TDocument>>[] configure)
		{
			ContextsValue = null;
			ContextsDescriptor = null;
			ContextsDescriptorAction = null;
			ContextsDescriptorActions = configure;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Analyzer(string? analyzer)
		{
			AnalyzerValue = analyzer;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> CopyTo<TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> MaxInputLength(int? maxInputLength)
		{
			MaxInputLengthValue = maxInputLength;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> PreservePositionIncrements(bool? preservePositionIncrements = true)
		{
			PreservePositionIncrementsValue = preservePositionIncrements;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> PreserveSeparators(bool? preserveSeparators = true)
		{
			PreserveSeparatorsValue = preserveSeparators;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> SearchAnalyzer(string? searchAnalyzer)
		{
			SearchAnalyzerValue = searchAnalyzer;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public CompletionPropertyDescriptor<TDocument> Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ContextsDescriptor is not null)
			{
				writer.WritePropertyName("contexts");
				JsonSerializer.Serialize(writer, ContextsDescriptor, options);
			}
			else if (ContextsDescriptorAction is not null)
			{
				writer.WritePropertyName("contexts");
				JsonSerializer.Serialize(writer, new SuggestContextDescriptor<TDocument>(ContextsDescriptorAction), options);
			}
			else if (ContextsDescriptorActions is not null)
			{
				writer.WritePropertyName("contexts");
				writer.WriteStartArray();
				foreach (var action in ContextsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new SuggestContextDescriptor<TDocument>(action), options);
				}

				writer.WriteEndArray();
			}
			else if (ContextsValue is not null)
			{
				writer.WritePropertyName("contexts");
				JsonSerializer.Serialize(writer, ContextsValue, options);
			}

			if (!string.IsNullOrEmpty(AnalyzerValue))
			{
				writer.WritePropertyName("analyzer");
				writer.WriteStringValue(AnalyzerValue);
			}

			if (CopyToValue is not null)
			{
				writer.WritePropertyName("copy_to");
				JsonSerializer.Serialize(writer, CopyToValue, options);
			}

			if (DocValuesValue.HasValue)
			{
				writer.WritePropertyName("doc_values");
				writer.WriteBooleanValue(DocValuesValue.Value);
			}

			if (DynamicValue is not null)
			{
				writer.WritePropertyName("dynamic");
				JsonSerializer.Serialize(writer, DynamicValue, options);
			}

			if (FieldsValue is not null)
			{
				writer.WritePropertyName("fields");
				JsonSerializer.Serialize(writer, FieldsValue, options);
			}

			if (IgnoreAboveValue.HasValue)
			{
				writer.WritePropertyName("ignore_above");
				writer.WriteNumberValue(IgnoreAboveValue.Value);
			}

			if (LocalMetadataValue is not null)
			{
				writer.WritePropertyName("local_metadata");
				JsonSerializer.Serialize(writer, LocalMetadataValue, options);
			}

			if (MaxInputLengthValue.HasValue)
			{
				writer.WritePropertyName("max_input_length");
				writer.WriteNumberValue(MaxInputLengthValue.Value);
			}

			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			if (PreservePositionIncrementsValue.HasValue)
			{
				writer.WritePropertyName("preserve_position_increments");
				writer.WriteBooleanValue(PreservePositionIncrementsValue.Value);
			}

			if (PreserveSeparatorsValue.HasValue)
			{
				writer.WritePropertyName("preserve_separators");
				writer.WriteBooleanValue(PreserveSeparatorsValue.Value);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (!string.IsNullOrEmpty(SearchAnalyzerValue))
			{
				writer.WritePropertyName("search_analyzer");
				writer.WriteStringValue(SearchAnalyzerValue);
			}

			if (!string.IsNullOrEmpty(SimilarityValue))
			{
				writer.WritePropertyName("similarity");
				writer.WriteStringValue(SimilarityValue);
			}

			if (StoreValue.HasValue)
			{
				writer.WritePropertyName("store");
				writer.WriteBooleanValue(StoreValue.Value);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("completion");
			writer.WriteEndObject();
		}

		CompletionProperty IBuildableDescriptor<CompletionProperty>.Build() => new()
		{ Contexts = ContextsValue, Analyzer = AnalyzerValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, LocalMetadata = LocalMetadataValue, MaxInputLength = MaxInputLengthValue, Meta = MetaValue, PreservePositionIncrements = PreservePositionIncrementsValue, PreserveSeparators = PreserveSeparatorsValue, Properties = PropertiesValue, SearchAnalyzer = SearchAnalyzerValue, Similarity = SimilarityValue, Store = StoreValue };
	}

	public sealed partial class CompletionPropertyDescriptor : SerializableDescriptorBase<CompletionPropertyDescriptor>, IBuildableDescriptor<CompletionProperty>
	{
		internal CompletionPropertyDescriptor(Action<CompletionPropertyDescriptor> configure) => configure.Invoke(this);
		public CompletionPropertyDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Mapping.SuggestContext>? ContextsValue { get; set; }

		private SuggestContextDescriptor ContextsDescriptor { get; set; }

		private Action<SuggestContextDescriptor> ContextsDescriptorAction { get; set; }

		private Action<SuggestContextDescriptor>[] ContextsDescriptorActions { get; set; }

		private string? AnalyzerValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fields? CopyToValue { get; set; }

		private bool? DocValuesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private int? MaxInputLengthValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private bool? PreservePositionIncrementsValue { get; set; }

		private bool? PreserveSeparatorsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		private string? SearchAnalyzerValue { get; set; }

		private string? SimilarityValue { get; set; }

		private bool? StoreValue { get; set; }

		public CompletionPropertyDescriptor Contexts(IEnumerable<Elastic.Clients.Elasticsearch.Mapping.SuggestContext>? contexts)
		{
			ContextsDescriptor = null;
			ContextsDescriptorAction = null;
			ContextsDescriptorActions = null;
			ContextsValue = contexts;
			return Self;
		}

		public CompletionPropertyDescriptor Contexts(SuggestContextDescriptor descriptor)
		{
			ContextsValue = null;
			ContextsDescriptorAction = null;
			ContextsDescriptorActions = null;
			ContextsDescriptor = descriptor;
			return Self;
		}

		public CompletionPropertyDescriptor Contexts(Action<SuggestContextDescriptor> configure)
		{
			ContextsValue = null;
			ContextsDescriptor = null;
			ContextsDescriptorActions = null;
			ContextsDescriptorAction = configure;
			return Self;
		}

		public CompletionPropertyDescriptor Contexts(params Action<SuggestContextDescriptor>[] configure)
		{
			ContextsValue = null;
			ContextsDescriptor = null;
			ContextsDescriptorAction = null;
			ContextsDescriptorActions = configure;
			return Self;
		}

		public CompletionPropertyDescriptor Analyzer(string? analyzer)
		{
			AnalyzerValue = analyzer;
			return Self;
		}

		public CompletionPropertyDescriptor CopyTo(Elastic.Clients.Elasticsearch.Fields? copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public CompletionPropertyDescriptor CopyTo<TDocument, TValue>(Expression<Func<TDocument, TValue>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public CompletionPropertyDescriptor CopyTo<TDocument>(Expression<Func<TDocument, object>> copyTo)
		{
			CopyToValue = copyTo;
			return Self;
		}

		public CompletionPropertyDescriptor DocValues(bool? docValues = true)
		{
			DocValuesValue = docValues;
			return Self;
		}

		public CompletionPropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public CompletionPropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public CompletionPropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public CompletionPropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public CompletionPropertyDescriptor MaxInputLength(int? maxInputLength)
		{
			MaxInputLengthValue = maxInputLength;
			return Self;
		}

		public CompletionPropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public CompletionPropertyDescriptor PreservePositionIncrements(bool? preservePositionIncrements = true)
		{
			PreservePositionIncrementsValue = preservePositionIncrements;
			return Self;
		}

		public CompletionPropertyDescriptor PreserveSeparators(bool? preserveSeparators = true)
		{
			PreserveSeparatorsValue = preserveSeparators;
			return Self;
		}

		public CompletionPropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public CompletionPropertyDescriptor SearchAnalyzer(string? searchAnalyzer)
		{
			SearchAnalyzerValue = searchAnalyzer;
			return Self;
		}

		public CompletionPropertyDescriptor Similarity(string? similarity)
		{
			SimilarityValue = similarity;
			return Self;
		}

		public CompletionPropertyDescriptor Store(bool? store = true)
		{
			StoreValue = store;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ContextsDescriptor is not null)
			{
				writer.WritePropertyName("contexts");
				JsonSerializer.Serialize(writer, ContextsDescriptor, options);
			}
			else if (ContextsDescriptorAction is not null)
			{
				writer.WritePropertyName("contexts");
				JsonSerializer.Serialize(writer, new SuggestContextDescriptor(ContextsDescriptorAction), options);
			}
			else if (ContextsDescriptorActions is not null)
			{
				writer.WritePropertyName("contexts");
				writer.WriteStartArray();
				foreach (var action in ContextsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new SuggestContextDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (ContextsValue is not null)
			{
				writer.WritePropertyName("contexts");
				JsonSerializer.Serialize(writer, ContextsValue, options);
			}

			if (!string.IsNullOrEmpty(AnalyzerValue))
			{
				writer.WritePropertyName("analyzer");
				writer.WriteStringValue(AnalyzerValue);
			}

			if (CopyToValue is not null)
			{
				writer.WritePropertyName("copy_to");
				JsonSerializer.Serialize(writer, CopyToValue, options);
			}

			if (DocValuesValue.HasValue)
			{
				writer.WritePropertyName("doc_values");
				writer.WriteBooleanValue(DocValuesValue.Value);
			}

			if (DynamicValue is not null)
			{
				writer.WritePropertyName("dynamic");
				JsonSerializer.Serialize(writer, DynamicValue, options);
			}

			if (FieldsValue is not null)
			{
				writer.WritePropertyName("fields");
				JsonSerializer.Serialize(writer, FieldsValue, options);
			}

			if (IgnoreAboveValue.HasValue)
			{
				writer.WritePropertyName("ignore_above");
				writer.WriteNumberValue(IgnoreAboveValue.Value);
			}

			if (LocalMetadataValue is not null)
			{
				writer.WritePropertyName("local_metadata");
				JsonSerializer.Serialize(writer, LocalMetadataValue, options);
			}

			if (MaxInputLengthValue.HasValue)
			{
				writer.WritePropertyName("max_input_length");
				writer.WriteNumberValue(MaxInputLengthValue.Value);
			}

			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			if (PreservePositionIncrementsValue.HasValue)
			{
				writer.WritePropertyName("preserve_position_increments");
				writer.WriteBooleanValue(PreservePositionIncrementsValue.Value);
			}

			if (PreserveSeparatorsValue.HasValue)
			{
				writer.WritePropertyName("preserve_separators");
				writer.WriteBooleanValue(PreserveSeparatorsValue.Value);
			}

			if (PropertiesValue is not null)
			{
				writer.WritePropertyName("properties");
				JsonSerializer.Serialize(writer, PropertiesValue, options);
			}

			if (!string.IsNullOrEmpty(SearchAnalyzerValue))
			{
				writer.WritePropertyName("search_analyzer");
				writer.WriteStringValue(SearchAnalyzerValue);
			}

			if (!string.IsNullOrEmpty(SimilarityValue))
			{
				writer.WritePropertyName("similarity");
				writer.WriteStringValue(SimilarityValue);
			}

			if (StoreValue.HasValue)
			{
				writer.WritePropertyName("store");
				writer.WriteBooleanValue(StoreValue.Value);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("completion");
			writer.WriteEndObject();
		}

		CompletionProperty IBuildableDescriptor<CompletionProperty>.Build() => new()
		{ Contexts = ContextsValue, Analyzer = AnalyzerValue, CopyTo = CopyToValue, DocValues = DocValuesValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, LocalMetadata = LocalMetadataValue, MaxInputLength = MaxInputLengthValue, Meta = MetaValue, PreservePositionIncrements = PreservePositionIncrementsValue, PreserveSeparators = PreserveSeparatorsValue, Properties = PropertiesValue, SearchAnalyzer = SearchAnalyzerValue, Similarity = SimilarityValue, Store = StoreValue };
	}
}