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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Xpack;

internal sealed partial class FeaturesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.Features>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregateMetric = System.Text.Json.JsonEncodedText.Encode("aggregate_metric");
	private static readonly System.Text.Json.JsonEncodedText PropAnalytics = System.Text.Json.JsonEncodedText.Encode("analytics");
	private static readonly System.Text.Json.JsonEncodedText PropArchive = System.Text.Json.JsonEncodedText.Encode("archive");
	private static readonly System.Text.Json.JsonEncodedText PropCcr = System.Text.Json.JsonEncodedText.Encode("ccr");
	private static readonly System.Text.Json.JsonEncodedText PropDataStreams = System.Text.Json.JsonEncodedText.Encode("data_streams");
	private static readonly System.Text.Json.JsonEncodedText PropDataTiers = System.Text.Json.JsonEncodedText.Encode("data_tiers");
	private static readonly System.Text.Json.JsonEncodedText PropEnrich = System.Text.Json.JsonEncodedText.Encode("enrich");
	private static readonly System.Text.Json.JsonEncodedText PropEnterpriseSearch = System.Text.Json.JsonEncodedText.Encode("enterprise_search");
	private static readonly System.Text.Json.JsonEncodedText PropEql = System.Text.Json.JsonEncodedText.Encode("eql");
	private static readonly System.Text.Json.JsonEncodedText PropEsql = System.Text.Json.JsonEncodedText.Encode("esql");
	private static readonly System.Text.Json.JsonEncodedText PropGraph = System.Text.Json.JsonEncodedText.Encode("graph");
	private static readonly System.Text.Json.JsonEncodedText PropIlm = System.Text.Json.JsonEncodedText.Encode("ilm");
	private static readonly System.Text.Json.JsonEncodedText PropLogsdb = System.Text.Json.JsonEncodedText.Encode("logsdb");
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
	private static readonly System.Text.Json.JsonEncodedText PropUniversalProfiling = System.Text.Json.JsonEncodedText.Encode("universal_profiling");
	private static readonly System.Text.Json.JsonEncodedText PropVotingOnly = System.Text.Json.JsonEncodedText.Encode("voting_only");
	private static readonly System.Text.Json.JsonEncodedText PropWatcher = System.Text.Json.JsonEncodedText.Encode("watcher");

	public override Elastic.Clients.Elasticsearch.Xpack.Features Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propAggregateMetric = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propAnalytics = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propArchive = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propCcr = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propDataStreams = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propDataTiers = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propEnrich = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propEnterpriseSearch = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propEql = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propEsql = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propGraph = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propIlm = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propLogsdb = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propLogstash = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propMl = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propMonitoring = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propRollup = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature?> propRuntimeFields = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propSearchableSnapshots = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propSecurity = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propSlm = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propSpatial = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propSql = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propTransform = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propUniversalProfiling = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propVotingOnly = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.Feature> propWatcher = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregateMetric.TryReadProperty(ref reader, options, PropAggregateMetric, null))
			{
				continue;
			}

			if (propAnalytics.TryReadProperty(ref reader, options, PropAnalytics, null))
			{
				continue;
			}

			if (propArchive.TryReadProperty(ref reader, options, PropArchive, null))
			{
				continue;
			}

			if (propCcr.TryReadProperty(ref reader, options, PropCcr, null))
			{
				continue;
			}

			if (propDataStreams.TryReadProperty(ref reader, options, PropDataStreams, null))
			{
				continue;
			}

			if (propDataTiers.TryReadProperty(ref reader, options, PropDataTiers, null))
			{
				continue;
			}

			if (propEnrich.TryReadProperty(ref reader, options, PropEnrich, null))
			{
				continue;
			}

			if (propEnterpriseSearch.TryReadProperty(ref reader, options, PropEnterpriseSearch, null))
			{
				continue;
			}

			if (propEql.TryReadProperty(ref reader, options, PropEql, null))
			{
				continue;
			}

			if (propEsql.TryReadProperty(ref reader, options, PropEsql, null))
			{
				continue;
			}

			if (propGraph.TryReadProperty(ref reader, options, PropGraph, null))
			{
				continue;
			}

			if (propIlm.TryReadProperty(ref reader, options, PropIlm, null))
			{
				continue;
			}

			if (propLogsdb.TryReadProperty(ref reader, options, PropLogsdb, null))
			{
				continue;
			}

			if (propLogstash.TryReadProperty(ref reader, options, PropLogstash, null))
			{
				continue;
			}

			if (propMl.TryReadProperty(ref reader, options, PropMl, null))
			{
				continue;
			}

			if (propMonitoring.TryReadProperty(ref reader, options, PropMonitoring, null))
			{
				continue;
			}

			if (propRollup.TryReadProperty(ref reader, options, PropRollup, null))
			{
				continue;
			}

			if (propRuntimeFields.TryReadProperty(ref reader, options, PropRuntimeFields, null))
			{
				continue;
			}

			if (propSearchableSnapshots.TryReadProperty(ref reader, options, PropSearchableSnapshots, null))
			{
				continue;
			}

			if (propSecurity.TryReadProperty(ref reader, options, PropSecurity, null))
			{
				continue;
			}

			if (propSlm.TryReadProperty(ref reader, options, PropSlm, null))
			{
				continue;
			}

			if (propSpatial.TryReadProperty(ref reader, options, PropSpatial, null))
			{
				continue;
			}

			if (propSql.TryReadProperty(ref reader, options, PropSql, null))
			{
				continue;
			}

			if (propTransform.TryReadProperty(ref reader, options, PropTransform, null))
			{
				continue;
			}

			if (propUniversalProfiling.TryReadProperty(ref reader, options, PropUniversalProfiling, null))
			{
				continue;
			}

			if (propVotingOnly.TryReadProperty(ref reader, options, PropVotingOnly, null))
			{
				continue;
			}

			if (propWatcher.TryReadProperty(ref reader, options, PropWatcher, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Xpack.Features(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AggregateMetric = propAggregateMetric.Value,
			Analytics = propAnalytics.Value,
			Archive = propArchive.Value,
			Ccr = propCcr.Value,
			DataStreams = propDataStreams.Value,
			DataTiers = propDataTiers.Value,
			Enrich = propEnrich.Value,
			EnterpriseSearch = propEnterpriseSearch.Value,
			Eql = propEql.Value,
			Esql = propEsql.Value,
			Graph = propGraph.Value,
			Ilm = propIlm.Value,
			Logsdb = propLogsdb.Value,
			Logstash = propLogstash.Value,
			Ml = propMl.Value,
			Monitoring = propMonitoring.Value,
			Rollup = propRollup.Value,
			RuntimeFields = propRuntimeFields.Value,
			SearchableSnapshots = propSearchableSnapshots.Value,
			Security = propSecurity.Value,
			Slm = propSlm.Value,
			Spatial = propSpatial.Value,
			Sql = propSql.Value,
			Transform = propTransform.Value,
			UniversalProfiling = propUniversalProfiling.Value,
			VotingOnly = propVotingOnly.Value,
			Watcher = propWatcher.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.Features value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregateMetric, value.AggregateMetric, null, null);
		writer.WriteProperty(options, PropAnalytics, value.Analytics, null, null);
		writer.WriteProperty(options, PropArchive, value.Archive, null, null);
		writer.WriteProperty(options, PropCcr, value.Ccr, null, null);
		writer.WriteProperty(options, PropDataStreams, value.DataStreams, null, null);
		writer.WriteProperty(options, PropDataTiers, value.DataTiers, null, null);
		writer.WriteProperty(options, PropEnrich, value.Enrich, null, null);
		writer.WriteProperty(options, PropEnterpriseSearch, value.EnterpriseSearch, null, null);
		writer.WriteProperty(options, PropEql, value.Eql, null, null);
		writer.WriteProperty(options, PropEsql, value.Esql, null, null);
		writer.WriteProperty(options, PropGraph, value.Graph, null, null);
		writer.WriteProperty(options, PropIlm, value.Ilm, null, null);
		writer.WriteProperty(options, PropLogsdb, value.Logsdb, null, null);
		writer.WriteProperty(options, PropLogstash, value.Logstash, null, null);
		writer.WriteProperty(options, PropMl, value.Ml, null, null);
		writer.WriteProperty(options, PropMonitoring, value.Monitoring, null, null);
		writer.WriteProperty(options, PropRollup, value.Rollup, null, null);
		writer.WriteProperty(options, PropRuntimeFields, value.RuntimeFields, null, null);
		writer.WriteProperty(options, PropSearchableSnapshots, value.SearchableSnapshots, null, null);
		writer.WriteProperty(options, PropSecurity, value.Security, null, null);
		writer.WriteProperty(options, PropSlm, value.Slm, null, null);
		writer.WriteProperty(options, PropSpatial, value.Spatial, null, null);
		writer.WriteProperty(options, PropSql, value.Sql, null, null);
		writer.WriteProperty(options, PropTransform, value.Transform, null, null);
		writer.WriteProperty(options, PropUniversalProfiling, value.UniversalProfiling, null, null);
		writer.WriteProperty(options, PropVotingOnly, value.VotingOnly, null, null);
		writer.WriteProperty(options, PropWatcher, value.Watcher, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.FeaturesConverter))]
public sealed partial class Features
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Features(Elastic.Clients.Elasticsearch.Xpack.Feature aggregateMetric, Elastic.Clients.Elasticsearch.Xpack.Feature analytics, Elastic.Clients.Elasticsearch.Xpack.Feature archive, Elastic.Clients.Elasticsearch.Xpack.Feature ccr, Elastic.Clients.Elasticsearch.Xpack.Feature dataStreams, Elastic.Clients.Elasticsearch.Xpack.Feature dataTiers, Elastic.Clients.Elasticsearch.Xpack.Feature enrich, Elastic.Clients.Elasticsearch.Xpack.Feature enterpriseSearch, Elastic.Clients.Elasticsearch.Xpack.Feature eql, Elastic.Clients.Elasticsearch.Xpack.Feature esql, Elastic.Clients.Elasticsearch.Xpack.Feature graph, Elastic.Clients.Elasticsearch.Xpack.Feature ilm, Elastic.Clients.Elasticsearch.Xpack.Feature logsdb, Elastic.Clients.Elasticsearch.Xpack.Feature logstash, Elastic.Clients.Elasticsearch.Xpack.Feature ml, Elastic.Clients.Elasticsearch.Xpack.Feature monitoring, Elastic.Clients.Elasticsearch.Xpack.Feature rollup, Elastic.Clients.Elasticsearch.Xpack.Feature searchableSnapshots, Elastic.Clients.Elasticsearch.Xpack.Feature security, Elastic.Clients.Elasticsearch.Xpack.Feature slm, Elastic.Clients.Elasticsearch.Xpack.Feature spatial, Elastic.Clients.Elasticsearch.Xpack.Feature sql, Elastic.Clients.Elasticsearch.Xpack.Feature transform, Elastic.Clients.Elasticsearch.Xpack.Feature universalProfiling, Elastic.Clients.Elasticsearch.Xpack.Feature votingOnly, Elastic.Clients.Elasticsearch.Xpack.Feature watcher)
	{
		AggregateMetric = aggregateMetric;
		Analytics = analytics;
		Archive = archive;
		Ccr = ccr;
		DataStreams = dataStreams;
		DataTiers = dataTiers;
		Enrich = enrich;
		EnterpriseSearch = enterpriseSearch;
		Eql = eql;
		Esql = esql;
		Graph = graph;
		Ilm = ilm;
		Logsdb = logsdb;
		Logstash = logstash;
		Ml = ml;
		Monitoring = monitoring;
		Rollup = rollup;
		SearchableSnapshots = searchableSnapshots;
		Security = security;
		Slm = slm;
		Spatial = spatial;
		Sql = sql;
		Transform = transform;
		UniversalProfiling = universalProfiling;
		VotingOnly = votingOnly;
		Watcher = watcher;
	}
#if NET7_0_OR_GREATER
	public Features()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Features()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Features(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature AggregateMetric { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Analytics { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Archive { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Ccr { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature DataStreams { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature DataTiers { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Enrich { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature EnterpriseSearch { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Eql { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Esql { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Graph { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Ilm { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Logsdb { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Logstash { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Ml { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Monitoring { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Rollup { get; set; }
	public Elastic.Clients.Elasticsearch.Xpack.Feature? RuntimeFields { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature SearchableSnapshots { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Security { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Slm { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Spatial { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Sql { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Transform { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature UniversalProfiling { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature VotingOnly { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.Feature Watcher { get; set; }
}