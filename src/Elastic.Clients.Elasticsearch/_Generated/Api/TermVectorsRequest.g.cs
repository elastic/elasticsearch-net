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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class TermVectorsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// Used as the default list unless a specific field list is provided in the <c>completion_fields</c> or <c>fielddata_fields</c> parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the document count, sum of document frequencies, and sum of total term frequencies.
	/// </para>
	/// </summary>
	public bool? FieldStatistics { get => Q<bool?>("field_statistics"); set => Q("field_statistics", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term offsets.
	/// </para>
	/// </summary>
	public bool? Offsets { get => Q<bool?>("offsets"); set => Q("offsets", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term payloads.
	/// </para>
	/// </summary>
	public bool? Payloads { get => Q<bool?>("payloads"); set => Q("payloads", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term positions.
	/// </para>
	/// </summary>
	public bool? Positions { get => Q<bool?>("positions"); set => Q("positions", value); }

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on.
	/// Random by default.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// If true, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term frequency and document frequency.
	/// </para>
	/// </summary>
	public bool? TermStatistics { get => Q<bool?>("term_statistics"); set => Q("term_statistics", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns the document version as part of a hit.
	/// </para>
	/// </summary>
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>
	/// Specific version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }
}

/// <summary>
/// <para>
/// Get term vector information.
/// </para>
/// <para>
/// Get information and statistics about terms in the fields of a particular document.
/// </para>
/// </summary>
public sealed partial class TermVectorsRequest<TDocument> : PlainRequest<TermVectorsRequestParameters>
{
	public TermVectorsRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Required("index", index).Optional("id", id))
	{
	}

	public TermVectorsRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceTermvectors;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "termvectors";

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// Used as the default list unless a specific field list is provided in the <c>completion_fields</c> or <c>fielddata_fields</c> parameters.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the document count, sum of document frequencies, and sum of total term frequencies.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? FieldStatistics { get => Q<bool?>("field_statistics"); set => Q("field_statistics", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term offsets.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Offsets { get => Q<bool?>("offsets"); set => Q("offsets", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term payloads.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Payloads { get => Q<bool?>("payloads"); set => Q("payloads", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term positions.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Positions { get => Q<bool?>("positions"); set => Q("positions", value); }

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on.
	/// Random by default.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// If true, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes term frequency and document frequency.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? TermStatistics { get => Q<bool?>("term_statistics"); set => Q("term_statistics", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns the document version as part of a hit.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>
	/// Specific version type.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }

	/// <summary>
	/// <para>
	/// An artificial document (a document not present in the index) for which you want to retrieve term vectors.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("doc")]
	[SourceConverter]
	public TDocument? Doc { get; set; }

	/// <summary>
	/// <para>
	/// Filter terms based on their tf-idf scores.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("filter")]
	public Elastic.Clients.Elasticsearch.Core.TermVectors.Filter? Filter { get; set; }

	/// <summary>
	/// <para>
	/// Overrides the default per-field analyzer.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("per_field_analyzer")]
	public IDictionary<Elastic.Clients.Elasticsearch.Field, string>? PerFieldAnalyzer { get; set; }
}

/// <summary>
/// <para>
/// Get term vector information.
/// </para>
/// <para>
/// Get information and statistics about terms in the fields of a particular document.
/// </para>
/// </summary>
public sealed partial class TermVectorsRequestDescriptor<TDocument> : RequestDescriptor<TermVectorsRequestDescriptor<TDocument>, TermVectorsRequestParameters>
{
	internal TermVectorsRequestDescriptor(Action<TermVectorsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TermVectorsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Required("index", index).Optional("id", id))
	{
	}

	public TermVectorsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	public TermVectorsRequestDescriptor(TDocument document) : this(typeof(TDocument), Elastic.Clients.Elasticsearch.Id.From(document))
	{
	}

	public TermVectorsRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.IndexName index) : this(index, Elastic.Clients.Elasticsearch.Id.From(document))
	{
	}

	public TermVectorsRequestDescriptor(TDocument document, Elastic.Clients.Elasticsearch.Id? id) : this(typeof(TDocument), id)
	{
	}

	public TermVectorsRequestDescriptor(Elastic.Clients.Elasticsearch.Id? id) : this(typeof(TDocument), id)
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceTermvectors;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "termvectors";

	public TermVectorsRequestDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields? fields) => Qs("fields", fields);
	public TermVectorsRequestDescriptor<TDocument> FieldStatistics(bool? fieldStatistics = true) => Qs("field_statistics", fieldStatistics);
	public TermVectorsRequestDescriptor<TDocument> Offsets(bool? offsets = true) => Qs("offsets", offsets);
	public TermVectorsRequestDescriptor<TDocument> Payloads(bool? payloads = true) => Qs("payloads", payloads);
	public TermVectorsRequestDescriptor<TDocument> Positions(bool? positions = true) => Qs("positions", positions);
	public TermVectorsRequestDescriptor<TDocument> Preference(string? preference) => Qs("preference", preference);
	public TermVectorsRequestDescriptor<TDocument> Realtime(bool? realtime = true) => Qs("realtime", realtime);
	public TermVectorsRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public TermVectorsRequestDescriptor<TDocument> TermStatistics(bool? termStatistics = true) => Qs("term_statistics", termStatistics);
	public TermVectorsRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
	public TermVectorsRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType) => Qs("version_type", versionType);

	public TermVectorsRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	public TermVectorsRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private TDocument? DocValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.TermVectors.Filter? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor> FilterDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, string>? PerFieldAnalyzerValue { get; set; }

	/// <summary>
	/// <para>
	/// An artificial document (a document not present in the index) for which you want to retrieve term vectors.
	/// </para>
	/// </summary>
	public TermVectorsRequestDescriptor<TDocument> Doc(TDocument? doc)
	{
		DocValue = doc;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Filter terms based on their tf-idf scores.
	/// </para>
	/// </summary>
	public TermVectorsRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.Core.TermVectors.Filter? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public TermVectorsRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public TermVectorsRequestDescriptor<TDocument> Filter(Action<Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Overrides the default per-field analyzer.
	/// </para>
	/// </summary>
	public TermVectorsRequestDescriptor<TDocument> PerFieldAnalyzer(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>> selector)
	{
		PerFieldAnalyzerValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, string>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DocValue is not null)
		{
			writer.WritePropertyName("doc");
			settings.SourceSerializer.Serialize(DocValue, writer);
		}

		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (PerFieldAnalyzerValue is not null)
		{
			writer.WritePropertyName("per_field_analyzer");
			JsonSerializer.Serialize(writer, PerFieldAnalyzerValue, options);
		}

		writer.WriteEndObject();
	}
}