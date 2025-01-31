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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Xpack;

internal sealed partial class XpackUsageResponseConverter : System.Text.Json.Serialization.JsonConverter<XpackUsageResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregateMetric = System.Text.Json.JsonEncodedText.Encode("aggregate_metric");
	private static readonly System.Text.Json.JsonEncodedText PropAnalytics = System.Text.Json.JsonEncodedText.Encode("analytics");
	private static readonly System.Text.Json.JsonEncodedText PropArchive = System.Text.Json.JsonEncodedText.Encode("archive");
	private static readonly System.Text.Json.JsonEncodedText PropCcr = System.Text.Json.JsonEncodedText.Encode("ccr");
	private static readonly System.Text.Json.JsonEncodedText PropDataFrame = System.Text.Json.JsonEncodedText.Encode("data_frame");
	private static readonly System.Text.Json.JsonEncodedText PropDataScience = System.Text.Json.JsonEncodedText.Encode("data_science");
	private static readonly System.Text.Json.JsonEncodedText PropDataStreams = System.Text.Json.JsonEncodedText.Encode("data_streams");
	private static readonly System.Text.Json.JsonEncodedText PropDataTiers = System.Text.Json.JsonEncodedText.Encode("data_tiers");
	private static readonly System.Text.Json.JsonEncodedText PropEnrich = System.Text.Json.JsonEncodedText.Encode("enrich");
	private static readonly System.Text.Json.JsonEncodedText PropEql = System.Text.Json.JsonEncodedText.Encode("eql");
	private static readonly System.Text.Json.JsonEncodedText PropFlattened = System.Text.Json.JsonEncodedText.Encode("flattened");
	private static readonly System.Text.Json.JsonEncodedText PropFrozenIndices = System.Text.Json.JsonEncodedText.Encode("frozen_indices");
	private static readonly System.Text.Json.JsonEncodedText PropGraph = System.Text.Json.JsonEncodedText.Encode("graph");
	private static readonly System.Text.Json.JsonEncodedText PropHealthApi = System.Text.Json.JsonEncodedText.Encode("health_api");
	private static readonly System.Text.Json.JsonEncodedText PropIlm = System.Text.Json.JsonEncodedText.Encode("ilm");
	private static readonly System.Text.Json.JsonEncodedText PropLogstash = System.Text.Json.JsonEncodedText.Encode("logstash");
	private static readonly System.Text.Json.JsonEncodedText PropMl = System.Text.Json.JsonEncodedText.Encode("ml");
	private static readonly System.Text.Json.JsonEncodedText PropMonitoring = System.Text.Json.JsonEncodedText.Encode("monitoring");
	private static readonly System.Text.Json.JsonEncodedText PropRollup = System.Text.Json.JsonEncodedText.Encode("rollup");
	private static readonly System.Text.Json.JsonEncodedText PropRuntimeFields = System.Text.Json.JsonEncodedText.Encode("runtime_fields");
	private static readonly System.Text.Json.JsonEncodedText PropSearchableSnapshots = System.Text.Json.JsonEncodedText.Encode("searchable_snapshots");
	private static readonly System.Text.Json.JsonEncodedText PropSecurity = System.Text.Json.JsonEncodedText.Encode("security");
	private static readonly System.Text.Json.JsonEncodedText PropSlm = System.Text.Json.JsonEncodedText.Encode("slm");
	private static readonly System.Text.Json.JsonEncodedText PropSpatial = System.Text.Json.JsonEncodedText.Encode("spatial");
	private static readonly System.Text.Json.JsonEncodedText PropSql = System.Text.Json.JsonEncodedText.Encode("sql");
	private static readonly System.Text.Json.JsonEncodedText PropTransform = System.Text.Json.JsonEncodedText.Encode("transform");
	private static readonly System.Text.Json.JsonEncodedText PropVectors = System.Text.Json.JsonEncodedText.Encode("vectors");
	private static readonly System.Text.Json.JsonEncodedText PropVotingOnly = System.Text.Json.JsonEncodedText.Encode("voting_only");
	private static readonly System.Text.Json.JsonEncodedText PropWatcher = System.Text.Json.JsonEncodedText.Encode("watcher");

	public override XpackUsageResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propAggregateMetric = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Analytics> propAnalytics = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Archive> propArchive = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Ccr> propCcr = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base?> propDataFrame = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base?> propDataScience = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.DataStreams?> propDataStreams = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.DataTiers> propDataTiers = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base?> propEnrich = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Eql> propEql = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Flattened?> propFlattened = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.FrozenIndices> propFrozenIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propGraph = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.HealthStatistics?> propHealthApi = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Ilm> propIlm = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propLogstash = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.MachineLearning> propMl = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Monitoring> propMonitoring = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propRollup = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.RuntimeFieldTypes?> propRuntimeFields = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.SearchableSnapshots> propSearchableSnapshots = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Security> propSecurity = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Slm> propSlm = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propSpatial = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Sql> propSql = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propTransform = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Vector?> propVectors = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Base> propVotingOnly = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Watcher> propWatcher = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregateMetric.TryRead(ref reader, options, PropAggregateMetric))
			{
				continue;
			}

			if (propAnalytics.TryRead(ref reader, options, PropAnalytics))
			{
				continue;
			}

			if (propArchive.TryRead(ref reader, options, PropArchive))
			{
				continue;
			}

			if (propCcr.TryRead(ref reader, options, PropCcr))
			{
				continue;
			}

			if (propDataFrame.TryRead(ref reader, options, PropDataFrame))
			{
				continue;
			}

			if (propDataScience.TryRead(ref reader, options, PropDataScience))
			{
				continue;
			}

			if (propDataStreams.TryRead(ref reader, options, PropDataStreams))
			{
				continue;
			}

			if (propDataTiers.TryRead(ref reader, options, PropDataTiers))
			{
				continue;
			}

			if (propEnrich.TryRead(ref reader, options, PropEnrich))
			{
				continue;
			}

			if (propEql.TryRead(ref reader, options, PropEql))
			{
				continue;
			}

			if (propFlattened.TryRead(ref reader, options, PropFlattened))
			{
				continue;
			}

			if (propFrozenIndices.TryRead(ref reader, options, PropFrozenIndices))
			{
				continue;
			}

			if (propGraph.TryRead(ref reader, options, PropGraph))
			{
				continue;
			}

			if (propHealthApi.TryRead(ref reader, options, PropHealthApi))
			{
				continue;
			}

			if (propIlm.TryRead(ref reader, options, PropIlm))
			{
				continue;
			}

			if (propLogstash.TryRead(ref reader, options, PropLogstash))
			{
				continue;
			}

			if (propMl.TryRead(ref reader, options, PropMl))
			{
				continue;
			}

			if (propMonitoring.TryRead(ref reader, options, PropMonitoring))
			{
				continue;
			}

			if (propRollup.TryRead(ref reader, options, PropRollup))
			{
				continue;
			}

			if (propRuntimeFields.TryRead(ref reader, options, PropRuntimeFields))
			{
				continue;
			}

			if (propSearchableSnapshots.TryRead(ref reader, options, PropSearchableSnapshots))
			{
				continue;
			}

			if (propSecurity.TryRead(ref reader, options, PropSecurity))
			{
				continue;
			}

			if (propSlm.TryRead(ref reader, options, PropSlm))
			{
				continue;
			}

			if (propSpatial.TryRead(ref reader, options, PropSpatial))
			{
				continue;
			}

			if (propSql.TryRead(ref reader, options, PropSql))
			{
				continue;
			}

			if (propTransform.TryRead(ref reader, options, PropTransform))
			{
				continue;
			}

			if (propVectors.TryRead(ref reader, options, PropVectors))
			{
				continue;
			}

			if (propVotingOnly.TryRead(ref reader, options, PropVotingOnly))
			{
				continue;
			}

			if (propWatcher.TryRead(ref reader, options, PropWatcher))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new XpackUsageResponse
		{
			AggregateMetric = propAggregateMetric.Value
,
			Analytics = propAnalytics.Value
,
			Archive = propArchive.Value
,
			Ccr = propCcr.Value
,
			DataFrame = propDataFrame.Value
,
			DataScience = propDataScience.Value
,
			DataStreams = propDataStreams.Value
,
			DataTiers = propDataTiers.Value
,
			Enrich = propEnrich.Value
,
			Eql = propEql.Value
,
			Flattened = propFlattened.Value
,
			FrozenIndices = propFrozenIndices.Value
,
			Graph = propGraph.Value
,
			HealthApi = propHealthApi.Value
,
			Ilm = propIlm.Value
,
			Logstash = propLogstash.Value
,
			Ml = propMl.Value
,
			Monitoring = propMonitoring.Value
,
			Rollup = propRollup.Value
,
			RuntimeFields = propRuntimeFields.Value
,
			SearchableSnapshots = propSearchableSnapshots.Value
,
			Security = propSecurity.Value
,
			Slm = propSlm.Value
,
			Spatial = propSpatial.Value
,
			Sql = propSql.Value
,
			Transform = propTransform.Value
,
			Vectors = propVectors.Value
,
			VotingOnly = propVotingOnly.Value
,
			Watcher = propWatcher.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, XpackUsageResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregateMetric, value.AggregateMetric);
		writer.WriteProperty(options, PropAnalytics, value.Analytics);
		writer.WriteProperty(options, PropArchive, value.Archive);
		writer.WriteProperty(options, PropCcr, value.Ccr);
		writer.WriteProperty(options, PropDataFrame, value.DataFrame);
		writer.WriteProperty(options, PropDataScience, value.DataScience);
		writer.WriteProperty(options, PropDataStreams, value.DataStreams);
		writer.WriteProperty(options, PropDataTiers, value.DataTiers);
		writer.WriteProperty(options, PropEnrich, value.Enrich);
		writer.WriteProperty(options, PropEql, value.Eql);
		writer.WriteProperty(options, PropFlattened, value.Flattened);
		writer.WriteProperty(options, PropFrozenIndices, value.FrozenIndices);
		writer.WriteProperty(options, PropGraph, value.Graph);
		writer.WriteProperty(options, PropHealthApi, value.HealthApi);
		writer.WriteProperty(options, PropIlm, value.Ilm);
		writer.WriteProperty(options, PropLogstash, value.Logstash);
		writer.WriteProperty(options, PropMl, value.Ml);
		writer.WriteProperty(options, PropMonitoring, value.Monitoring);
		writer.WriteProperty(options, PropRollup, value.Rollup);
		writer.WriteProperty(options, PropRuntimeFields, value.RuntimeFields);
		writer.WriteProperty(options, PropSearchableSnapshots, value.SearchableSnapshots);
		writer.WriteProperty(options, PropSecurity, value.Security);
		writer.WriteProperty(options, PropSlm, value.Slm);
		writer.WriteProperty(options, PropSpatial, value.Spatial);
		writer.WriteProperty(options, PropSql, value.Sql);
		writer.WriteProperty(options, PropTransform, value.Transform);
		writer.WriteProperty(options, PropVectors, value.Vectors);
		writer.WriteProperty(options, PropVotingOnly, value.VotingOnly);
		writer.WriteProperty(options, PropWatcher, value.Watcher);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(XpackUsageResponseConverter))]
public sealed partial class XpackUsageResponse : ElasticsearchResponse
{
	public Elastic.Clients.Elasticsearch.Xpack.Base AggregateMetric { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Analytics Analytics { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Archive Archive { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Ccr Ccr { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base? DataFrame { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base? DataScience { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.DataStreams? DataStreams { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.DataTiers DataTiers { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base? Enrich { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Eql Eql { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Flattened? Flattened { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.FrozenIndices FrozenIndices { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base Graph { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.HealthStatistics? HealthApi { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Ilm Ilm { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base Logstash { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.MachineLearning Ml { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Monitoring Monitoring { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base Rollup { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.RuntimeFieldTypes? RuntimeFields { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.SearchableSnapshots SearchableSnapshots { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Security Security { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Slm Slm { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base Spatial { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Sql Sql { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base Transform { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Vector? Vectors { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Base VotingOnly { get; init; }
	public Elastic.Clients.Elasticsearch.Xpack.Watcher Watcher { get; init; }
}