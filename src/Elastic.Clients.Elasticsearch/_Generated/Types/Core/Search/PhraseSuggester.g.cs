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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public sealed partial class PhraseSuggester
{
	[JsonInclude, JsonPropertyName("analyzer")]
	public string? Analyzer { get; set; }
	[JsonInclude, JsonPropertyName("collate")]
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? Collate { get; set; }
	[JsonInclude, JsonPropertyName("confidence")]
	public double? Confidence { get; set; }
	[JsonInclude, JsonPropertyName("direct_generator")]
	public ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? DirectGenerator { get; set; }
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	[JsonInclude, JsonPropertyName("force_unigrams")]
	public bool? ForceUnigrams { get; set; }
	[JsonInclude, JsonPropertyName("gram_size")]
	public int? GramSize { get; set; }
	[JsonInclude, JsonPropertyName("highlight")]
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? Highlight { get; set; }
	[JsonInclude, JsonPropertyName("max_errors")]
	public double? MaxErrors { get; set; }
	[JsonInclude, JsonPropertyName("real_word_error_likelihood")]
	public double? RealWordErrorLikelihood { get; set; }
	[JsonInclude, JsonPropertyName("separator")]
	public string? Separator { get; set; }
	[JsonInclude, JsonPropertyName("shard_size")]
	public int? ShardSize { get; set; }
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }
	[JsonInclude, JsonPropertyName("smoothing")]
	public Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? Smoothing { get; set; }
	[JsonInclude, JsonPropertyName("text")]
	public string? Text { get; set; }
	[JsonInclude, JsonPropertyName("token_limit")]
	public int? TokenLimit { get; set; }

	public static implicit operator FieldSuggester(PhraseSuggester phraseSuggester) => Core.Search.FieldSuggester.Phrase(phraseSuggester);
}

public sealed partial class PhraseSuggesterDescriptor<TDocument> : SerializableDescriptor<PhraseSuggesterDescriptor<TDocument>>
{
	internal PhraseSuggesterDescriptor(Action<PhraseSuggesterDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PhraseSuggesterDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? DirectGeneratorValue { get; set; }
	private DirectGeneratorDescriptor<TDocument> DirectGeneratorDescriptor { get; set; }
	private Action<DirectGeneratorDescriptor<TDocument>> DirectGeneratorDescriptorAction { get; set; }
	private Action<DirectGeneratorDescriptor<TDocument>>[] DirectGeneratorDescriptorActions { get; set; }
	private string? AnalyzerValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? CollateValue { get; set; }
	private PhraseSuggestCollateDescriptor CollateDescriptor { get; set; }
	private Action<PhraseSuggestCollateDescriptor> CollateDescriptorAction { get; set; }
	private double? ConfidenceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private bool? ForceUnigramsValue { get; set; }
	private int? GramSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? HighlightValue { get; set; }
	private PhraseSuggestHighlightDescriptor HighlightDescriptor { get; set; }
	private Action<PhraseSuggestHighlightDescriptor> HighlightDescriptorAction { get; set; }
	private double? MaxErrorsValue { get; set; }
	private double? RealWordErrorLikelihoodValue { get; set; }
	private string? SeparatorValue { get; set; }
	private int? ShardSizeValue { get; set; }
	private int? SizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? SmoothingValue { get; set; }
	private SmoothingModelDescriptor SmoothingDescriptor { get; set; }
	private Action<SmoothingModelDescriptor> SmoothingDescriptorAction { get; set; }
	private string? TextValue { get; set; }
	private int? TokenLimitValue { get; set; }

	public PhraseSuggesterDescriptor<TDocument> DirectGenerator(ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? directGenerator)
	{
		DirectGeneratorDescriptor = null;
		DirectGeneratorDescriptorAction = null;
		DirectGeneratorDescriptorActions = null;
		DirectGeneratorValue = directGenerator;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> DirectGenerator(DirectGeneratorDescriptor<TDocument> descriptor)
	{
		DirectGeneratorValue = null;
		DirectGeneratorDescriptorAction = null;
		DirectGeneratorDescriptorActions = null;
		DirectGeneratorDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> DirectGenerator(Action<DirectGeneratorDescriptor<TDocument>> configure)
	{
		DirectGeneratorValue = null;
		DirectGeneratorDescriptor = null;
		DirectGeneratorDescriptorActions = null;
		DirectGeneratorDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> DirectGenerator(params Action<DirectGeneratorDescriptor<TDocument>>[] configure)
	{
		DirectGeneratorValue = null;
		DirectGeneratorDescriptor = null;
		DirectGeneratorDescriptorAction = null;
		DirectGeneratorDescriptorActions = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Collate(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? collate)
	{
		CollateDescriptor = null;
		CollateDescriptorAction = null;
		CollateValue = collate;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Collate(PhraseSuggestCollateDescriptor descriptor)
	{
		CollateValue = null;
		CollateDescriptorAction = null;
		CollateDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Collate(Action<PhraseSuggestCollateDescriptor> configure)
	{
		CollateValue = null;
		CollateDescriptor = null;
		CollateDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Confidence(double? confidence)
	{
		ConfidenceValue = confidence;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> ForceUnigrams(bool? forceUnigrams = true)
	{
		ForceUnigramsValue = forceUnigrams;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> GramSize(int? gramSize)
	{
		GramSizeValue = gramSize;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Highlight(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? highlight)
	{
		HighlightDescriptor = null;
		HighlightDescriptorAction = null;
		HighlightValue = highlight;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Highlight(PhraseSuggestHighlightDescriptor descriptor)
	{
		HighlightValue = null;
		HighlightDescriptorAction = null;
		HighlightDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Highlight(Action<PhraseSuggestHighlightDescriptor> configure)
	{
		HighlightValue = null;
		HighlightDescriptor = null;
		HighlightDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> MaxErrors(double? maxErrors)
	{
		MaxErrorsValue = maxErrors;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> RealWordErrorLikelihood(double? realWordErrorLikelihood)
	{
		RealWordErrorLikelihoodValue = realWordErrorLikelihood;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Separator(string? separator)
	{
		SeparatorValue = separator;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> ShardSize(int? shardSize)
	{
		ShardSizeValue = shardSize;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Smoothing(Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? smoothing)
	{
		SmoothingDescriptor = null;
		SmoothingDescriptorAction = null;
		SmoothingValue = smoothing;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Smoothing(SmoothingModelDescriptor descriptor)
	{
		SmoothingValue = null;
		SmoothingDescriptorAction = null;
		SmoothingDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Smoothing(Action<SmoothingModelDescriptor> configure)
	{
		SmoothingValue = null;
		SmoothingDescriptor = null;
		SmoothingDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> Text(string? text)
	{
		TextValue = text;
		return Self;
	}

	public PhraseSuggesterDescriptor<TDocument> TokenLimit(int? tokenLimit)
	{
		TokenLimitValue = tokenLimit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DirectGeneratorDescriptor is not null)
		{
			writer.WritePropertyName("direct_generator");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DirectGeneratorDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DirectGeneratorDescriptorAction is not null)
		{
			writer.WritePropertyName("direct_generator");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new DirectGeneratorDescriptor<TDocument>(DirectGeneratorDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DirectGeneratorDescriptorActions is not null)
		{
			writer.WritePropertyName("direct_generator");
			writer.WriteStartArray();
			foreach (var action in DirectGeneratorDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new DirectGeneratorDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DirectGeneratorValue is not null)
		{
			writer.WritePropertyName("direct_generator");
			JsonSerializer.Serialize(writer, DirectGeneratorValue, options);
		}

		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (CollateDescriptor is not null)
		{
			writer.WritePropertyName("collate");
			JsonSerializer.Serialize(writer, CollateDescriptor, options);
		}
		else if (CollateDescriptorAction is not null)
		{
			writer.WritePropertyName("collate");
			JsonSerializer.Serialize(writer, new PhraseSuggestCollateDescriptor(CollateDescriptorAction), options);
		}
		else if (CollateValue is not null)
		{
			writer.WritePropertyName("collate");
			JsonSerializer.Serialize(writer, CollateValue, options);
		}

		if (ConfidenceValue.HasValue)
		{
			writer.WritePropertyName("confidence");
			writer.WriteNumberValue(ConfidenceValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (ForceUnigramsValue.HasValue)
		{
			writer.WritePropertyName("force_unigrams");
			writer.WriteBooleanValue(ForceUnigramsValue.Value);
		}

		if (GramSizeValue.HasValue)
		{
			writer.WritePropertyName("gram_size");
			writer.WriteNumberValue(GramSizeValue.Value);
		}

		if (HighlightDescriptor is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightDescriptor, options);
		}
		else if (HighlightDescriptorAction is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, new PhraseSuggestHighlightDescriptor(HighlightDescriptorAction), options);
		}
		else if (HighlightValue is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightValue, options);
		}

		if (MaxErrorsValue.HasValue)
		{
			writer.WritePropertyName("max_errors");
			writer.WriteNumberValue(MaxErrorsValue.Value);
		}

		if (RealWordErrorLikelihoodValue.HasValue)
		{
			writer.WritePropertyName("real_word_error_likelihood");
			writer.WriteNumberValue(RealWordErrorLikelihoodValue.Value);
		}

		if (!string.IsNullOrEmpty(SeparatorValue))
		{
			writer.WritePropertyName("separator");
			writer.WriteStringValue(SeparatorValue);
		}

		if (ShardSizeValue.HasValue)
		{
			writer.WritePropertyName("shard_size");
			writer.WriteNumberValue(ShardSizeValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SmoothingDescriptor is not null)
		{
			writer.WritePropertyName("smoothing");
			JsonSerializer.Serialize(writer, SmoothingDescriptor, options);
		}
		else if (SmoothingDescriptorAction is not null)
		{
			writer.WritePropertyName("smoothing");
			JsonSerializer.Serialize(writer, new SmoothingModelDescriptor(SmoothingDescriptorAction), options);
		}
		else if (SmoothingValue is not null)
		{
			writer.WritePropertyName("smoothing");
			JsonSerializer.Serialize(writer, SmoothingValue, options);
		}

		if (!string.IsNullOrEmpty(TextValue))
		{
			writer.WritePropertyName("text");
			writer.WriteStringValue(TextValue);
		}

		if (TokenLimitValue.HasValue)
		{
			writer.WritePropertyName("token_limit");
			writer.WriteNumberValue(TokenLimitValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class PhraseSuggesterDescriptor : SerializableDescriptor<PhraseSuggesterDescriptor>
{
	internal PhraseSuggesterDescriptor(Action<PhraseSuggesterDescriptor> configure) => configure.Invoke(this);

	public PhraseSuggesterDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? DirectGeneratorValue { get; set; }
	private DirectGeneratorDescriptor DirectGeneratorDescriptor { get; set; }
	private Action<DirectGeneratorDescriptor> DirectGeneratorDescriptorAction { get; set; }
	private Action<DirectGeneratorDescriptor>[] DirectGeneratorDescriptorActions { get; set; }
	private string? AnalyzerValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? CollateValue { get; set; }
	private PhraseSuggestCollateDescriptor CollateDescriptor { get; set; }
	private Action<PhraseSuggestCollateDescriptor> CollateDescriptorAction { get; set; }
	private double? ConfidenceValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private bool? ForceUnigramsValue { get; set; }
	private int? GramSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? HighlightValue { get; set; }
	private PhraseSuggestHighlightDescriptor HighlightDescriptor { get; set; }
	private Action<PhraseSuggestHighlightDescriptor> HighlightDescriptorAction { get; set; }
	private double? MaxErrorsValue { get; set; }
	private double? RealWordErrorLikelihoodValue { get; set; }
	private string? SeparatorValue { get; set; }
	private int? ShardSizeValue { get; set; }
	private int? SizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? SmoothingValue { get; set; }
	private SmoothingModelDescriptor SmoothingDescriptor { get; set; }
	private Action<SmoothingModelDescriptor> SmoothingDescriptorAction { get; set; }
	private string? TextValue { get; set; }
	private int? TokenLimitValue { get; set; }

	public PhraseSuggesterDescriptor DirectGenerator(ICollection<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>? directGenerator)
	{
		DirectGeneratorDescriptor = null;
		DirectGeneratorDescriptorAction = null;
		DirectGeneratorDescriptorActions = null;
		DirectGeneratorValue = directGenerator;
		return Self;
	}

	public PhraseSuggesterDescriptor DirectGenerator(DirectGeneratorDescriptor descriptor)
	{
		DirectGeneratorValue = null;
		DirectGeneratorDescriptorAction = null;
		DirectGeneratorDescriptorActions = null;
		DirectGeneratorDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor DirectGenerator(Action<DirectGeneratorDescriptor> configure)
	{
		DirectGeneratorValue = null;
		DirectGeneratorDescriptor = null;
		DirectGeneratorDescriptorActions = null;
		DirectGeneratorDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor DirectGenerator(params Action<DirectGeneratorDescriptor>[] configure)
	{
		DirectGeneratorValue = null;
		DirectGeneratorDescriptor = null;
		DirectGeneratorDescriptorAction = null;
		DirectGeneratorDescriptorActions = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public PhraseSuggesterDescriptor Collate(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestCollate? collate)
	{
		CollateDescriptor = null;
		CollateDescriptorAction = null;
		CollateValue = collate;
		return Self;
	}

	public PhraseSuggesterDescriptor Collate(PhraseSuggestCollateDescriptor descriptor)
	{
		CollateValue = null;
		CollateDescriptorAction = null;
		CollateDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor Collate(Action<PhraseSuggestCollateDescriptor> configure)
	{
		CollateValue = null;
		CollateDescriptor = null;
		CollateDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor Confidence(double? confidence)
	{
		ConfidenceValue = confidence;
		return Self;
	}

	public PhraseSuggesterDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public PhraseSuggesterDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PhraseSuggesterDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PhraseSuggesterDescriptor ForceUnigrams(bool? forceUnigrams = true)
	{
		ForceUnigramsValue = forceUnigrams;
		return Self;
	}

	public PhraseSuggesterDescriptor GramSize(int? gramSize)
	{
		GramSizeValue = gramSize;
		return Self;
	}

	public PhraseSuggesterDescriptor Highlight(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggestHighlight? highlight)
	{
		HighlightDescriptor = null;
		HighlightDescriptorAction = null;
		HighlightValue = highlight;
		return Self;
	}

	public PhraseSuggesterDescriptor Highlight(PhraseSuggestHighlightDescriptor descriptor)
	{
		HighlightValue = null;
		HighlightDescriptorAction = null;
		HighlightDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor Highlight(Action<PhraseSuggestHighlightDescriptor> configure)
	{
		HighlightValue = null;
		HighlightDescriptor = null;
		HighlightDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor MaxErrors(double? maxErrors)
	{
		MaxErrorsValue = maxErrors;
		return Self;
	}

	public PhraseSuggesterDescriptor RealWordErrorLikelihood(double? realWordErrorLikelihood)
	{
		RealWordErrorLikelihoodValue = realWordErrorLikelihood;
		return Self;
	}

	public PhraseSuggesterDescriptor Separator(string? separator)
	{
		SeparatorValue = separator;
		return Self;
	}

	public PhraseSuggesterDescriptor ShardSize(int? shardSize)
	{
		ShardSizeValue = shardSize;
		return Self;
	}

	public PhraseSuggesterDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	public PhraseSuggesterDescriptor Smoothing(Elastic.Clients.Elasticsearch.Core.Search.SmoothingModel? smoothing)
	{
		SmoothingDescriptor = null;
		SmoothingDescriptorAction = null;
		SmoothingValue = smoothing;
		return Self;
	}

	public PhraseSuggesterDescriptor Smoothing(SmoothingModelDescriptor descriptor)
	{
		SmoothingValue = null;
		SmoothingDescriptorAction = null;
		SmoothingDescriptor = descriptor;
		return Self;
	}

	public PhraseSuggesterDescriptor Smoothing(Action<SmoothingModelDescriptor> configure)
	{
		SmoothingValue = null;
		SmoothingDescriptor = null;
		SmoothingDescriptorAction = configure;
		return Self;
	}

	public PhraseSuggesterDescriptor Text(string? text)
	{
		TextValue = text;
		return Self;
	}

	public PhraseSuggesterDescriptor TokenLimit(int? tokenLimit)
	{
		TokenLimitValue = tokenLimit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DirectGeneratorDescriptor is not null)
		{
			writer.WritePropertyName("direct_generator");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DirectGeneratorDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DirectGeneratorDescriptorAction is not null)
		{
			writer.WritePropertyName("direct_generator");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new DirectGeneratorDescriptor(DirectGeneratorDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DirectGeneratorDescriptorActions is not null)
		{
			writer.WritePropertyName("direct_generator");
			writer.WriteStartArray();
			foreach (var action in DirectGeneratorDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new DirectGeneratorDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DirectGeneratorValue is not null)
		{
			writer.WritePropertyName("direct_generator");
			JsonSerializer.Serialize(writer, DirectGeneratorValue, options);
		}

		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (CollateDescriptor is not null)
		{
			writer.WritePropertyName("collate");
			JsonSerializer.Serialize(writer, CollateDescriptor, options);
		}
		else if (CollateDescriptorAction is not null)
		{
			writer.WritePropertyName("collate");
			JsonSerializer.Serialize(writer, new PhraseSuggestCollateDescriptor(CollateDescriptorAction), options);
		}
		else if (CollateValue is not null)
		{
			writer.WritePropertyName("collate");
			JsonSerializer.Serialize(writer, CollateValue, options);
		}

		if (ConfidenceValue.HasValue)
		{
			writer.WritePropertyName("confidence");
			writer.WriteNumberValue(ConfidenceValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (ForceUnigramsValue.HasValue)
		{
			writer.WritePropertyName("force_unigrams");
			writer.WriteBooleanValue(ForceUnigramsValue.Value);
		}

		if (GramSizeValue.HasValue)
		{
			writer.WritePropertyName("gram_size");
			writer.WriteNumberValue(GramSizeValue.Value);
		}

		if (HighlightDescriptor is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightDescriptor, options);
		}
		else if (HighlightDescriptorAction is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, new PhraseSuggestHighlightDescriptor(HighlightDescriptorAction), options);
		}
		else if (HighlightValue is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightValue, options);
		}

		if (MaxErrorsValue.HasValue)
		{
			writer.WritePropertyName("max_errors");
			writer.WriteNumberValue(MaxErrorsValue.Value);
		}

		if (RealWordErrorLikelihoodValue.HasValue)
		{
			writer.WritePropertyName("real_word_error_likelihood");
			writer.WriteNumberValue(RealWordErrorLikelihoodValue.Value);
		}

		if (!string.IsNullOrEmpty(SeparatorValue))
		{
			writer.WritePropertyName("separator");
			writer.WriteStringValue(SeparatorValue);
		}

		if (ShardSizeValue.HasValue)
		{
			writer.WritePropertyName("shard_size");
			writer.WriteNumberValue(ShardSizeValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SmoothingDescriptor is not null)
		{
			writer.WritePropertyName("smoothing");
			JsonSerializer.Serialize(writer, SmoothingDescriptor, options);
		}
		else if (SmoothingDescriptorAction is not null)
		{
			writer.WritePropertyName("smoothing");
			JsonSerializer.Serialize(writer, new SmoothingModelDescriptor(SmoothingDescriptorAction), options);
		}
		else if (SmoothingValue is not null)
		{
			writer.WritePropertyName("smoothing");
			JsonSerializer.Serialize(writer, SmoothingValue, options);
		}

		if (!string.IsNullOrEmpty(TextValue))
		{
			writer.WritePropertyName("text");
			writer.WriteStringValue(TextValue);
		}

		if (TokenLimitValue.HasValue)
		{
			writer.WritePropertyName("token_limit");
			writer.WriteNumberValue(TokenLimitValue.Value);
		}

		writer.WriteEndObject();
	}
}