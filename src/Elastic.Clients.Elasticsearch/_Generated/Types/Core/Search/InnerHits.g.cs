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
namespace Elastic.Clients.Elasticsearch.Core.Search;
public sealed partial class InnerHits
{
	[JsonInclude]
	[JsonPropertyName("_source")]
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? Source { get; set; }

	[JsonInclude]
	[JsonPropertyName("collapse")]
	public Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? Collapse { get; set; }

	[JsonInclude]
	[JsonPropertyName("docvalue_fields")]
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? DocvalueFields { get; set; }

	[JsonInclude]
	[JsonPropertyName("explain")]
	public bool? Explain { get; set; }

	[JsonInclude]
	[JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Fields? Fields { get; set; }

	[JsonInclude]
	[JsonPropertyName("from")]
	public int? From { get; set; }

	[JsonInclude]
	[JsonPropertyName("highlight")]
	public Elastic.Clients.Elasticsearch.Core.Search.Highlight? Highlight { get; set; }

	[JsonInclude]
	[JsonPropertyName("ignore_unmapped")]
	public bool? IgnoreUnmapped { get; set; }

	[JsonInclude]
	[JsonPropertyName("name")]
	public Elastic.Clients.Elasticsearch.Name? Name { get; set; }

	[JsonInclude]
	[JsonPropertyName("script_fields")]
	public IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFields { get; set; }

	[JsonInclude]
	[JsonPropertyName("seq_no_primary_term")]
	public bool? SeqNoPrimaryTerm { get; set; }

	[JsonInclude]
	[JsonPropertyName("size")]
	public int? Size { get; set; }

	[JsonInclude]
	[JsonPropertyName("sort")]
	[JsonConverter(typeof(SortConverter))]
	public ICollection<Elastic.Clients.Elasticsearch.SortOptions>? Sort { get; set; }

	[JsonInclude]
	[JsonPropertyName("stored_field")]
	public Elastic.Clients.Elasticsearch.Fields? StoredField { get; set; }

	[JsonInclude]
	[JsonPropertyName("track_scores")]
	public bool? TrackScores { get; set; }

	[JsonInclude]
	[JsonPropertyName("version")]
	public bool? Version { get; set; }
}

public sealed partial class InnerHitsDescriptor<TDocument> : SerializableDescriptor<InnerHitsDescriptor<TDocument>>
{
	internal InnerHitsDescriptor(Action<InnerHitsDescriptor<TDocument>> configure) => configure.Invoke(this);
	public InnerHitsDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? CollapseValue { get; set; }

	private FieldCollapseDescriptor<TDocument> CollapseDescriptor { get; set; }

	private Action<FieldCollapseDescriptor<TDocument>> CollapseDescriptorAction { get; set; }

	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? DocvalueFieldsValue { get; set; }

	private QueryDsl.FieldAndFormatDescriptor<TDocument> DocvalueFieldsDescriptor { get; set; }

	private Action<QueryDsl.FieldAndFormatDescriptor<TDocument>> DocvalueFieldsDescriptorAction { get; set; }

	private Action<QueryDsl.FieldAndFormatDescriptor<TDocument>>[] DocvalueFieldsDescriptorActions { get; set; }

	private Elastic.Clients.Elasticsearch.Core.Search.Highlight? HighlightValue { get; set; }

	private HighlightDescriptor<TDocument> HighlightDescriptor { get; set; }

	private Action<HighlightDescriptor<TDocument>> HighlightDescriptorAction { get; set; }

	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }

	private SortOptionsDescriptor<TDocument> SortDescriptor { get; set; }

	private Action<SortOptionsDescriptor<TDocument>> SortDescriptorAction { get; set; }

	private Action<SortOptionsDescriptor<TDocument>>[] SortDescriptorActions { get; set; }

	private Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? SourceValue { get; set; }

	private bool? ExplainValue { get; set; }

	private Elastic.Clients.Elasticsearch.Fields? FieldsValue { get; set; }

	private int? FromValue { get; set; }

	private bool? IgnoreUnmappedValue { get; set; }

	private Elastic.Clients.Elasticsearch.Name? NameValue { get; set; }

	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; set; }

	private bool? SeqNoPrimaryTermValue { get; set; }

	private int? SizeValue { get; set; }

	private Elastic.Clients.Elasticsearch.Fields? StoredFieldValue { get; set; }

	private bool? TrackScoresValue { get; set; }

	private bool? VersionValue { get; set; }

	public InnerHitsDescriptor<TDocument> Collapse(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? collapse)
	{
		CollapseDescriptor = null;
		CollapseDescriptorAction = null;
		CollapseValue = collapse;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Collapse(FieldCollapseDescriptor<TDocument> descriptor)
	{
		CollapseValue = null;
		CollapseDescriptorAction = null;
		CollapseDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Collapse(Action<FieldCollapseDescriptor<TDocument>> configure)
	{
		CollapseValue = null;
		CollapseDescriptor = null;
		CollapseDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> DocvalueFields(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? docvalueFields)
	{
		DocvalueFieldsDescriptor = null;
		DocvalueFieldsDescriptorAction = null;
		DocvalueFieldsDescriptorActions = null;
		DocvalueFieldsValue = docvalueFields;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> DocvalueFields(QueryDsl.FieldAndFormatDescriptor<TDocument> descriptor)
	{
		DocvalueFieldsValue = null;
		DocvalueFieldsDescriptorAction = null;
		DocvalueFieldsDescriptorActions = null;
		DocvalueFieldsDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> DocvalueFields(Action<QueryDsl.FieldAndFormatDescriptor<TDocument>> configure)
	{
		DocvalueFieldsValue = null;
		DocvalueFieldsDescriptor = null;
		DocvalueFieldsDescriptorActions = null;
		DocvalueFieldsDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> DocvalueFields(params Action<QueryDsl.FieldAndFormatDescriptor<TDocument>>[] configure)
	{
		DocvalueFieldsValue = null;
		DocvalueFieldsDescriptor = null;
		DocvalueFieldsDescriptorAction = null;
		DocvalueFieldsDescriptorActions = configure;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Highlight(Elastic.Clients.Elasticsearch.Core.Search.Highlight? highlight)
	{
		HighlightDescriptor = null;
		HighlightDescriptorAction = null;
		HighlightValue = highlight;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Highlight(HighlightDescriptor<TDocument> descriptor)
	{
		HighlightValue = null;
		HighlightDescriptorAction = null;
		HighlightDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Highlight(Action<HighlightDescriptor<TDocument>> configure)
	{
		HighlightValue = null;
		HighlightDescriptor = null;
		HighlightDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Sort(SortOptionsDescriptor<TDocument> descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Sort(Action<SortOptionsDescriptor<TDocument>> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Sort(params Action<SortOptionsDescriptor<TDocument>>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? source)
	{
		SourceValue = source;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Explain(bool? explain = true)
	{
		ExplainValue = explain;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> From(int? from)
	{
		FromValue = from;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> IgnoreUnmapped(bool? ignoreUnmapped = true)
	{
		IgnoreUnmappedValue = ignoreUnmapped;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Name? name)
	{
		NameValue = name;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> ScriptFields(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>> selector)
	{
		ScriptFieldsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>());
		return Self;
	}

	public InnerHitsDescriptor<TDocument> SeqNoPrimaryTerm(bool? seqNoPrimaryTerm = true)
	{
		SeqNoPrimaryTermValue = seqNoPrimaryTerm;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> StoredField(Elastic.Clients.Elasticsearch.Fields? storedField)
	{
		StoredFieldValue = storedField;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> TrackScores(bool? trackScores = true)
	{
		TrackScoresValue = trackScores;
		return Self;
	}

	public InnerHitsDescriptor<TDocument> Version(bool? version = true)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CollapseDescriptor is not null)
		{
			writer.WritePropertyName("collapse");
			JsonSerializer.Serialize(writer, CollapseDescriptor, options);
		}
		else if (CollapseDescriptorAction is not null)
		{
			writer.WritePropertyName("collapse");
			JsonSerializer.Serialize(writer, new FieldCollapseDescriptor<TDocument>(CollapseDescriptorAction), options);
		}
		else if (CollapseValue is not null)
		{
			writer.WritePropertyName("collapse");
			JsonSerializer.Serialize(writer, CollapseValue, options);
		}

		if (DocvalueFieldsDescriptor is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DocvalueFieldsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DocvalueFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new QueryDsl.FieldAndFormatDescriptor<TDocument>(DocvalueFieldsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocvalueFieldsDescriptorActions is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			writer.WriteStartArray();
			foreach (var action in DocvalueFieldsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new QueryDsl.FieldAndFormatDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DocvalueFieldsValue is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			JsonSerializer.Serialize(writer, DocvalueFieldsValue, options);
		}

		if (HighlightDescriptor is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightDescriptor, options);
		}
		else if (HighlightDescriptorAction is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, new HighlightDescriptor<TDocument>(HighlightDescriptorAction), options);
		}
		else if (HighlightValue is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightValue, options);
		}

		if (SortDescriptor is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortDescriptor, options);
		}
		else if (SortDescriptorAction is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, new SortOptionsDescriptor<TDocument>(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length > 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new SortOptionsDescriptor<TDocument>(action), options);
			}

			if (SortDescriptorActions.Length > 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		if (SourceValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		if (ExplainValue.HasValue)
		{
			writer.WritePropertyName("explain");
			writer.WriteBooleanValue(ExplainValue.Value);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (FromValue.HasValue)
		{
			writer.WritePropertyName("from");
			writer.WriteNumberValue(FromValue.Value);
		}

		if (IgnoreUnmappedValue.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
		}

		if (NameValue is not null)
		{
			writer.WritePropertyName("name");
			JsonSerializer.Serialize(writer, NameValue, options);
		}

		if (ScriptFieldsValue is not null)
		{
			writer.WritePropertyName("script_fields");
			JsonSerializer.Serialize(writer, ScriptFieldsValue, options);
		}

		if (SeqNoPrimaryTermValue.HasValue)
		{
			writer.WritePropertyName("seq_no_primary_term");
			writer.WriteBooleanValue(SeqNoPrimaryTermValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (StoredFieldValue is not null)
		{
			writer.WritePropertyName("stored_field");
			JsonSerializer.Serialize(writer, StoredFieldValue, options);
		}

		if (TrackScoresValue.HasValue)
		{
			writer.WritePropertyName("track_scores");
			writer.WriteBooleanValue(TrackScoresValue.Value);
		}

		if (VersionValue.HasValue)
		{
			writer.WritePropertyName("version");
			writer.WriteBooleanValue(VersionValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class InnerHitsDescriptor : SerializableDescriptor<InnerHitsDescriptor>
{
	internal InnerHitsDescriptor(Action<InnerHitsDescriptor> configure) => configure.Invoke(this);
	public InnerHitsDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? CollapseValue { get; set; }

	private FieldCollapseDescriptor CollapseDescriptor { get; set; }

	private Action<FieldCollapseDescriptor> CollapseDescriptorAction { get; set; }

	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? DocvalueFieldsValue { get; set; }

	private QueryDsl.FieldAndFormatDescriptor DocvalueFieldsDescriptor { get; set; }

	private Action<QueryDsl.FieldAndFormatDescriptor> DocvalueFieldsDescriptorAction { get; set; }

	private Action<QueryDsl.FieldAndFormatDescriptor>[] DocvalueFieldsDescriptorActions { get; set; }

	private Elastic.Clients.Elasticsearch.Core.Search.Highlight? HighlightValue { get; set; }

	private HighlightDescriptor HighlightDescriptor { get; set; }

	private Action<HighlightDescriptor> HighlightDescriptorAction { get; set; }

	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }

	private SortOptionsDescriptor SortDescriptor { get; set; }

	private Action<SortOptionsDescriptor> SortDescriptorAction { get; set; }

	private Action<SortOptionsDescriptor>[] SortDescriptorActions { get; set; }

	private Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? SourceValue { get; set; }

	private bool? ExplainValue { get; set; }

	private Elastic.Clients.Elasticsearch.Fields? FieldsValue { get; set; }

	private int? FromValue { get; set; }

	private bool? IgnoreUnmappedValue { get; set; }

	private Elastic.Clients.Elasticsearch.Name? NameValue { get; set; }

	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; set; }

	private bool? SeqNoPrimaryTermValue { get; set; }

	private int? SizeValue { get; set; }

	private Elastic.Clients.Elasticsearch.Fields? StoredFieldValue { get; set; }

	private bool? TrackScoresValue { get; set; }

	private bool? VersionValue { get; set; }

	public InnerHitsDescriptor Collapse(Elastic.Clients.Elasticsearch.Core.Search.FieldCollapse? collapse)
	{
		CollapseDescriptor = null;
		CollapseDescriptorAction = null;
		CollapseValue = collapse;
		return Self;
	}

	public InnerHitsDescriptor Collapse(FieldCollapseDescriptor descriptor)
	{
		CollapseValue = null;
		CollapseDescriptorAction = null;
		CollapseDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor Collapse(Action<FieldCollapseDescriptor> configure)
	{
		CollapseValue = null;
		CollapseDescriptor = null;
		CollapseDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor DocvalueFields(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.FieldAndFormat>? docvalueFields)
	{
		DocvalueFieldsDescriptor = null;
		DocvalueFieldsDescriptorAction = null;
		DocvalueFieldsDescriptorActions = null;
		DocvalueFieldsValue = docvalueFields;
		return Self;
	}

	public InnerHitsDescriptor DocvalueFields(QueryDsl.FieldAndFormatDescriptor descriptor)
	{
		DocvalueFieldsValue = null;
		DocvalueFieldsDescriptorAction = null;
		DocvalueFieldsDescriptorActions = null;
		DocvalueFieldsDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor DocvalueFields(Action<QueryDsl.FieldAndFormatDescriptor> configure)
	{
		DocvalueFieldsValue = null;
		DocvalueFieldsDescriptor = null;
		DocvalueFieldsDescriptorActions = null;
		DocvalueFieldsDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor DocvalueFields(params Action<QueryDsl.FieldAndFormatDescriptor>[] configure)
	{
		DocvalueFieldsValue = null;
		DocvalueFieldsDescriptor = null;
		DocvalueFieldsDescriptorAction = null;
		DocvalueFieldsDescriptorActions = configure;
		return Self;
	}

	public InnerHitsDescriptor Highlight(Elastic.Clients.Elasticsearch.Core.Search.Highlight? highlight)
	{
		HighlightDescriptor = null;
		HighlightDescriptorAction = null;
		HighlightValue = highlight;
		return Self;
	}

	public InnerHitsDescriptor Highlight(HighlightDescriptor descriptor)
	{
		HighlightValue = null;
		HighlightDescriptorAction = null;
		HighlightDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor Highlight(Action<HighlightDescriptor> configure)
	{
		HighlightValue = null;
		HighlightDescriptor = null;
		HighlightDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public InnerHitsDescriptor Sort(SortOptionsDescriptor descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public InnerHitsDescriptor Sort(Action<SortOptionsDescriptor> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public InnerHitsDescriptor Sort(params Action<SortOptionsDescriptor>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	public InnerHitsDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? source)
	{
		SourceValue = source;
		return Self;
	}

	public InnerHitsDescriptor Explain(bool? explain = true)
	{
		ExplainValue = explain;
		return Self;
	}

	public InnerHitsDescriptor Fields(Elastic.Clients.Elasticsearch.Fields? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public InnerHitsDescriptor From(int? from)
	{
		FromValue = from;
		return Self;
	}

	public InnerHitsDescriptor IgnoreUnmapped(bool? ignoreUnmapped = true)
	{
		IgnoreUnmappedValue = ignoreUnmapped;
		return Self;
	}

	public InnerHitsDescriptor Name(Elastic.Clients.Elasticsearch.Name? name)
	{
		NameValue = name;
		return Self;
	}

	public InnerHitsDescriptor ScriptFields(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>> selector)
	{
		ScriptFieldsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.ScriptField>());
		return Self;
	}

	public InnerHitsDescriptor SeqNoPrimaryTerm(bool? seqNoPrimaryTerm = true)
	{
		SeqNoPrimaryTermValue = seqNoPrimaryTerm;
		return Self;
	}

	public InnerHitsDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	public InnerHitsDescriptor StoredField(Elastic.Clients.Elasticsearch.Fields? storedField)
	{
		StoredFieldValue = storedField;
		return Self;
	}

	public InnerHitsDescriptor TrackScores(bool? trackScores = true)
	{
		TrackScoresValue = trackScores;
		return Self;
	}

	public InnerHitsDescriptor Version(bool? version = true)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CollapseDescriptor is not null)
		{
			writer.WritePropertyName("collapse");
			JsonSerializer.Serialize(writer, CollapseDescriptor, options);
		}
		else if (CollapseDescriptorAction is not null)
		{
			writer.WritePropertyName("collapse");
			JsonSerializer.Serialize(writer, new FieldCollapseDescriptor(CollapseDescriptorAction), options);
		}
		else if (CollapseValue is not null)
		{
			writer.WritePropertyName("collapse");
			JsonSerializer.Serialize(writer, CollapseValue, options);
		}

		if (DocvalueFieldsDescriptor is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DocvalueFieldsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DocvalueFieldsDescriptorAction is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new QueryDsl.FieldAndFormatDescriptor(DocvalueFieldsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocvalueFieldsDescriptorActions is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			writer.WriteStartArray();
			foreach (var action in DocvalueFieldsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new QueryDsl.FieldAndFormatDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DocvalueFieldsValue is not null)
		{
			writer.WritePropertyName("docvalue_fields");
			JsonSerializer.Serialize(writer, DocvalueFieldsValue, options);
		}

		if (HighlightDescriptor is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightDescriptor, options);
		}
		else if (HighlightDescriptorAction is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, new HighlightDescriptor(HighlightDescriptorAction), options);
		}
		else if (HighlightValue is not null)
		{
			writer.WritePropertyName("highlight");
			JsonSerializer.Serialize(writer, HighlightValue, options);
		}

		if (SortDescriptor is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortDescriptor, options);
		}
		else if (SortDescriptorAction is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, new SortOptionsDescriptor(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length > 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new SortOptionsDescriptor(action), options);
			}

			if (SortDescriptorActions.Length > 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		if (SourceValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		if (ExplainValue.HasValue)
		{
			writer.WritePropertyName("explain");
			writer.WriteBooleanValue(ExplainValue.Value);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (FromValue.HasValue)
		{
			writer.WritePropertyName("from");
			writer.WriteNumberValue(FromValue.Value);
		}

		if (IgnoreUnmappedValue.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
		}

		if (NameValue is not null)
		{
			writer.WritePropertyName("name");
			JsonSerializer.Serialize(writer, NameValue, options);
		}

		if (ScriptFieldsValue is not null)
		{
			writer.WritePropertyName("script_fields");
			JsonSerializer.Serialize(writer, ScriptFieldsValue, options);
		}

		if (SeqNoPrimaryTermValue.HasValue)
		{
			writer.WritePropertyName("seq_no_primary_term");
			writer.WriteBooleanValue(SeqNoPrimaryTermValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (StoredFieldValue is not null)
		{
			writer.WritePropertyName("stored_field");
			JsonSerializer.Serialize(writer, StoredFieldValue, options);
		}

		if (TrackScoresValue.HasValue)
		{
			writer.WritePropertyName("track_scores");
			writer.WriteBooleanValue(TrackScoresValue.Value);
		}

		if (VersionValue.HasValue)
		{
			writer.WritePropertyName("version");
			writer.WriteBooleanValue(VersionValue.Value);
		}

		writer.WriteEndObject();
	}
}