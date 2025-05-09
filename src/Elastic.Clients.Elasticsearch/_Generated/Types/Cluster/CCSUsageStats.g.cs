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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class CCSUsageStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.CCSUsageStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropClients = System.Text.Json.JsonEncodedText.Encode("clients");
	private static readonly System.Text.Json.JsonEncodedText PropClusters = System.Text.Json.JsonEncodedText.Encode("clusters");
	private static readonly System.Text.Json.JsonEncodedText PropFailureReasons = System.Text.Json.JsonEncodedText.Encode("failure_reasons");
	private static readonly System.Text.Json.JsonEncodedText PropFeatures = System.Text.Json.JsonEncodedText.Encode("features");
	private static readonly System.Text.Json.JsonEncodedText PropRemotesPerSearchAvg = System.Text.Json.JsonEncodedText.Encode("remotes_per_search_avg");
	private static readonly System.Text.Json.JsonEncodedText PropRemotesPerSearchMax = System.Text.Json.JsonEncodedText.Encode("remotes_per_search_max");
	private static readonly System.Text.Json.JsonEncodedText PropSkipped = System.Text.Json.JsonEncodedText.Encode("skipped");
	private static readonly System.Text.Json.JsonEncodedText PropSuccess = System.Text.Json.JsonEncodedText.Encode("success");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");
	private static readonly System.Text.Json.JsonEncodedText PropTookMrtFalse = System.Text.Json.JsonEncodedText.Encode("took_mrt_false");
	private static readonly System.Text.Json.JsonEncodedText PropTookMrtTrue = System.Text.Json.JsonEncodedText.Encode("took_mrt_true");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override Elastic.Clients.Elasticsearch.Cluster.CCSUsageStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, int>> propClients = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats>> propClusters = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, int>> propFailureReasons = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, int>> propFeatures = default;
		LocalJsonValue<double> propRemotesPerSearchAvg = default;
		LocalJsonValue<int> propRemotesPerSearchMax = default;
		LocalJsonValue<int> propSkipped = default;
		LocalJsonValue<int> propSuccess = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue> propTook = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue?> propTookMrtFalse = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue?> propTookMrtTrue = default;
		LocalJsonValue<int> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClients.TryReadProperty(ref reader, options, PropClients, static System.Collections.Generic.IReadOnlyDictionary<string, int> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, int>(o, null, null)!))
			{
				continue;
			}

			if (propClusters.TryReadProperty(ref reader, options, PropClusters, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats>(o, null, null)!))
			{
				continue;
			}

			if (propFailureReasons.TryReadProperty(ref reader, options, PropFailureReasons, static System.Collections.Generic.IReadOnlyDictionary<string, int> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, int>(o, null, null)!))
			{
				continue;
			}

			if (propFeatures.TryReadProperty(ref reader, options, PropFeatures, static System.Collections.Generic.IReadOnlyDictionary<string, int> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, int>(o, null, null)!))
			{
				continue;
			}

			if (propRemotesPerSearchAvg.TryReadProperty(ref reader, options, PropRemotesPerSearchAvg, null))
			{
				continue;
			}

			if (propRemotesPerSearchMax.TryReadProperty(ref reader, options, PropRemotesPerSearchMax, null))
			{
				continue;
			}

			if (propSkipped.TryReadProperty(ref reader, options, PropSkipped, null))
			{
				continue;
			}

			if (propSuccess.TryReadProperty(ref reader, options, PropSuccess, null))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, null))
			{
				continue;
			}

			if (propTookMrtFalse.TryReadProperty(ref reader, options, PropTookMrtFalse, null))
			{
				continue;
			}

			if (propTookMrtTrue.TryReadProperty(ref reader, options, PropTookMrtTrue, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.CCSUsageStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Clients = propClients.Value,
			Clusters = propClusters.Value,
			FailureReasons = propFailureReasons.Value,
			Features = propFeatures.Value,
			RemotesPerSearchAvg = propRemotesPerSearchAvg.Value,
			RemotesPerSearchMax = propRemotesPerSearchMax.Value,
			Skipped = propSkipped.Value,
			Success = propSuccess.Value,
			Took = propTook.Value,
			TookMrtFalse = propTookMrtFalse.Value,
			TookMrtTrue = propTookMrtTrue.Value,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.CCSUsageStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClients, value.Clients, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, int> v) => w.WriteDictionaryValue<string, int>(o, v, null, null));
		writer.WriteProperty(options, PropClusters, value.Clusters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats>(o, v, null, null));
		writer.WriteProperty(options, PropFailureReasons, value.FailureReasons, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, int> v) => w.WriteDictionaryValue<string, int>(o, v, null, null));
		writer.WriteProperty(options, PropFeatures, value.Features, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, int> v) => w.WriteDictionaryValue<string, int>(o, v, null, null));
		writer.WriteProperty(options, PropRemotesPerSearchAvg, value.RemotesPerSearchAvg, null, null);
		writer.WriteProperty(options, PropRemotesPerSearchMax, value.RemotesPerSearchMax, null, null);
		writer.WriteProperty(options, PropSkipped, value.Skipped, null, null);
		writer.WriteProperty(options, PropSuccess, value.Success, null, null);
		writer.WriteProperty(options, PropTook, value.Took, null, null);
		writer.WriteProperty(options, PropTookMrtFalse, value.TookMrtFalse, null, null);
		writer.WriteProperty(options, PropTookMrtTrue, value.TookMrtTrue, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.CCSUsageStatsConverter))]
public sealed partial class CCSUsageStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CCSUsageStats(System.Collections.Generic.IReadOnlyDictionary<string, int> clients, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats> clusters, System.Collections.Generic.IReadOnlyDictionary<string, int> failureReasons, System.Collections.Generic.IReadOnlyDictionary<string, int> features, double remotesPerSearchAvg, int remotesPerSearchMax, int skipped, int success, Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue took, int total)
	{
		Clients = clients;
		Clusters = clusters;
		FailureReasons = failureReasons;
		Features = features;
		RemotesPerSearchAvg = remotesPerSearchAvg;
		RemotesPerSearchMax = remotesPerSearchMax;
		Skipped = skipped;
		Success = success;
		Took = took;
		Total = total;
	}
#if NET7_0_OR_GREATER
	public CCSUsageStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CCSUsageStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CCSUsageStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Statistics about the clients that executed cross-cluster search requests. The keys are the names of the clients, and the values are the number of requests that were executed by that client. Only known clients (such as <c>kibana</c> or <c>elasticsearch</c>) are counted.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, int> Clients { get; set; }

	/// <summary>
	/// <para>
	/// Statistics about the clusters that were queried in cross-cluster search requests. The keys are cluster names, and the values are per-cluster telemetry data. This also includes the local cluster itself, which uses the name <c>(local)</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Cluster.CCSUsageClusterStats> Clusters { get; set; }

	/// <summary>
	/// <para>
	/// Statistics about the reasons for cross-cluster search request failures. The keys are the failure reason names and the values are the number of requests that failed for that reason.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, int> FailureReasons { get; set; }

	/// <summary>
	/// <para>
	/// The keys are the names of the search feature, and the values are the number of requests that used that feature. Single request can use more than one feature (e.g. both <c>async</c> and <c>wildcard</c>).
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, int> Features { get; set; }

	/// <summary>
	/// <para>
	/// The average number of remote clusters that were queried in a single cross-cluster search request.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double RemotesPerSearchAvg { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of remote clusters that were queried in a single cross-cluster search request.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int RemotesPerSearchMax { get; set; }

	/// <summary>
	/// <para>
	/// The total number of cross-cluster search requests (successful or failed) that had at least one remote cluster skipped.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Skipped { get; set; }

	/// <summary>
	/// <para>
	/// The total number of cross-cluster search requests that have been successfully executed by the cluster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Success { get; set; }

	/// <summary>
	/// <para>
	/// Statistics about the time taken to execute cross-cluster search requests.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue Took { get; set; }

	/// <summary>
	/// <para>
	/// Statistics about the time taken to execute cross-cluster search requests for which the <c>ccs_minimize_roundtrips</c> setting was set to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue? TookMrtFalse { get; set; }

	/// <summary>
	/// <para>
	/// Statistics about the time taken to execute cross-cluster search requests for which the <c>ccs_minimize_roundtrips</c> setting was set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.CCSUsageTimeValue? TookMrtTrue { get; set; }

	/// <summary>
	/// <para>
	/// The total number of cross-cluster search requests that have been executed by the cluster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Total { get; set; }
}