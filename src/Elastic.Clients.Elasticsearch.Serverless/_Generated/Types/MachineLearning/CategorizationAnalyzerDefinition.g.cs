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
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class CategorizationAnalyzerDefinition
{
	/// <summary>
	/// <para>
	/// One or more character filters. In addition to the built-in character filters, other plugins can provide more character filters. If this property is not specified, no character filters are applied prior to categorization. If you are customizing some other aspect of the analyzer and you need to achieve the equivalent of <c>categorization_filters</c> (which are not permitted when some other aspect of the analyzer is customized), add them here as pattern replace character filters.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("char_filter")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Analysis.ICharFilter>? CharFilter { get; set; }

	/// <summary>
	/// <para>
	/// One or more token filters. In addition to the built-in token filters, other plugins can provide more token filters. If this property is not specified, no token filters are applied prior to categorization.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("filter")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Analysis.ITokenFilter>? Filter { get; set; }

	/// <summary>
	/// <para>
	/// The name or definition of the tokenizer to use after character filters are applied. This property is compulsory if <c>categorization_analyzer</c> is specified as an object. Machine learning provides a tokenizer called <c>ml_standard</c> that tokenizes in a way that has been determined to produce good categorization results on a variety of log file formats for logs in English. If you want to use that tokenizer but change the character or token filters, specify "tokenizer": "ml_standard" in your <c>categorization_analyzer</c>. Additionally, the <c>ml_classic</c> tokenizer is available, which tokenizes in the same way as the non-customizable tokenizer in old versions of the product (before 6.2). <c>ml_classic</c> was the default categorization tokenizer in versions 6.2 to 7.13, so if you need categorization identical to the default for jobs created in these versions, specify "tokenizer": "ml_classic" in your <c>categorization_analyzer</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("tokenizer")]
	public Elastic.Clients.Elasticsearch.Serverless.Analysis.ITokenizer? Tokenizer { get; set; }
}

public sealed partial class CategorizationAnalyzerDefinitionDescriptor : SerializableDescriptor<CategorizationAnalyzerDefinitionDescriptor>
{
	internal CategorizationAnalyzerDefinitionDescriptor(Action<CategorizationAnalyzerDefinitionDescriptor> configure) => configure.Invoke(this);

	public CategorizationAnalyzerDefinitionDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Analysis.ICharFilter>? CharFilterValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Analysis.ITokenFilter>? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Analysis.ITokenizer? TokenizerValue { get; set; }

	/// <summary>
	/// <para>
	/// One or more character filters. In addition to the built-in character filters, other plugins can provide more character filters. If this property is not specified, no character filters are applied prior to categorization. If you are customizing some other aspect of the analyzer and you need to achieve the equivalent of <c>categorization_filters</c> (which are not permitted when some other aspect of the analyzer is customized), add them here as pattern replace character filters.
	/// </para>
	/// </summary>
	public CategorizationAnalyzerDefinitionDescriptor CharFilter(ICollection<Elastic.Clients.Elasticsearch.Serverless.Analysis.ICharFilter>? charFilter)
	{
		CharFilterValue = charFilter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// One or more token filters. In addition to the built-in token filters, other plugins can provide more token filters. If this property is not specified, no token filters are applied prior to categorization.
	/// </para>
	/// </summary>
	public CategorizationAnalyzerDefinitionDescriptor Filter(ICollection<Elastic.Clients.Elasticsearch.Serverless.Analysis.ITokenFilter>? filter)
	{
		FilterValue = filter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name or definition of the tokenizer to use after character filters are applied. This property is compulsory if <c>categorization_analyzer</c> is specified as an object. Machine learning provides a tokenizer called <c>ml_standard</c> that tokenizes in a way that has been determined to produce good categorization results on a variety of log file formats for logs in English. If you want to use that tokenizer but change the character or token filters, specify "tokenizer": "ml_standard" in your <c>categorization_analyzer</c>. Additionally, the <c>ml_classic</c> tokenizer is available, which tokenizes in the same way as the non-customizable tokenizer in old versions of the product (before 6.2). <c>ml_classic</c> was the default categorization tokenizer in versions 6.2 to 7.13, so if you need categorization identical to the default for jobs created in these versions, specify "tokenizer": "ml_classic" in your <c>categorization_analyzer</c>.
	/// </para>
	/// </summary>
	public CategorizationAnalyzerDefinitionDescriptor Tokenizer(Elastic.Clients.Elasticsearch.Serverless.Analysis.ITokenizer? tokenizer)
	{
		TokenizerValue = tokenizer;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CharFilterValue is not null)
		{
			writer.WritePropertyName("char_filter");
			JsonSerializer.Serialize(writer, CharFilterValue, options);
		}

		if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (TokenizerValue is not null)
		{
			writer.WritePropertyName("tokenizer");
			JsonSerializer.Serialize(writer, TokenizerValue, options);
		}

		writer.WriteEndObject();
	}
}