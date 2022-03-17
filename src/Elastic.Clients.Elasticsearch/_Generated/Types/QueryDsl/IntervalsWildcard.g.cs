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
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public partial class IntervalsWildcard : IIntervalsContainerVariant, IIntervalsQueryVariant
	{
		[JsonIgnore]
		string QueryDsl.IIntervalsContainerVariant.IntervalsContainerVariantName => "wildcard";
		[JsonIgnore]
		string QueryDsl.IIntervalsQueryVariant.IntervalsQueryVariantName => "wildcard";
		[JsonInclude]
		[JsonPropertyName("analyzer")]
		public string? Analyzer { get; set; }

		[JsonInclude]
		[JsonPropertyName("pattern")]
		public string Pattern { get; set; }

		[JsonInclude]
		[JsonPropertyName("use_field")]
		public Elastic.Clients.Elasticsearch.Field? UseField { get; set; }
	}

	public sealed partial class IntervalsWildcardDescriptor<TDocument> : DescriptorBase<IntervalsWildcardDescriptor<TDocument>>
	{
		internal IntervalsWildcardDescriptor(Action<IntervalsWildcardDescriptor<TDocument>> configure) => configure.Invoke(this);
		public IntervalsWildcardDescriptor() : base()
		{
		}

		private string? AnalyzerValue { get; set; }

		private string PatternValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? UseFieldValue { get; set; }

		public IntervalsWildcardDescriptor<TDocument> Analyzer(string? analyzer)
		{
			AnalyzerValue = analyzer;
			return Self;
		}

		public IntervalsWildcardDescriptor<TDocument> Pattern(string pattern)
		{
			PatternValue = pattern;
			return Self;
		}

		public IntervalsWildcardDescriptor<TDocument> UseField(Elastic.Clients.Elasticsearch.Field? useField)
		{
			UseFieldValue = useField;
			return Self;
		}

		public IntervalsWildcardDescriptor<TDocument> UseField<TValue>(Expression<Func<TDocument, TValue>> useField)
		{
			UseFieldValue = useField;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(AnalyzerValue))
			{
				writer.WritePropertyName("analyzer");
				writer.WriteStringValue(AnalyzerValue);
			}

			writer.WritePropertyName("pattern");
			writer.WriteStringValue(PatternValue);
			if (UseFieldValue is not null)
			{
				writer.WritePropertyName("use_field");
				JsonSerializer.Serialize(writer, UseFieldValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class IntervalsWildcardDescriptor : DescriptorBase<IntervalsWildcardDescriptor>
	{
		internal IntervalsWildcardDescriptor(Action<IntervalsWildcardDescriptor> configure) => configure.Invoke(this);
		public IntervalsWildcardDescriptor() : base()
		{
		}

		private string? AnalyzerValue { get; set; }

		private string PatternValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? UseFieldValue { get; set; }

		public IntervalsWildcardDescriptor Analyzer(string? analyzer)
		{
			AnalyzerValue = analyzer;
			return Self;
		}

		public IntervalsWildcardDescriptor Pattern(string pattern)
		{
			PatternValue = pattern;
			return Self;
		}

		public IntervalsWildcardDescriptor UseField(Elastic.Clients.Elasticsearch.Field? useField)
		{
			UseFieldValue = useField;
			return Self;
		}

		public IntervalsWildcardDescriptor UseField<TDocument, TValue>(Expression<Func<TDocument, TValue>> useField)
		{
			UseFieldValue = useField;
			return Self;
		}

		public IntervalsWildcardDescriptor UseField<TDocument>(Expression<Func<TDocument, object>> useField)
		{
			UseFieldValue = useField;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(AnalyzerValue))
			{
				writer.WritePropertyName("analyzer");
				writer.WriteStringValue(AnalyzerValue);
			}

			writer.WritePropertyName("pattern");
			writer.WriteStringValue(PatternValue);
			if (UseFieldValue is not null)
			{
				writer.WritePropertyName("use_field");
				JsonSerializer.Serialize(writer, UseFieldValue, options);
			}

			writer.WriteEndObject();
		}
	}
}