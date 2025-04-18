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

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class GeoIpDownloadStatisticsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.GeoIpDownloadStatistics>
{
	private static readonly System.Text.Json.JsonEncodedText PropDatabasesCount = System.Text.Json.JsonEncodedText.Encode("databases_count");
	private static readonly System.Text.Json.JsonEncodedText PropExpiredDatabases = System.Text.Json.JsonEncodedText.Encode("expired_databases");
	private static readonly System.Text.Json.JsonEncodedText PropFailedDownloads = System.Text.Json.JsonEncodedText.Encode("failed_downloads");
	private static readonly System.Text.Json.JsonEncodedText PropSkippedUpdates = System.Text.Json.JsonEncodedText.Encode("skipped_updates");
	private static readonly System.Text.Json.JsonEncodedText PropSuccessfulDownloads = System.Text.Json.JsonEncodedText.Encode("successful_downloads");
	private static readonly System.Text.Json.JsonEncodedText PropTotalDownloadTime = System.Text.Json.JsonEncodedText.Encode("total_download_time");

	public override Elastic.Clients.Elasticsearch.Ingest.GeoIpDownloadStatistics Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propDatabasesCount = default;
		LocalJsonValue<int> propExpiredDatabases = default;
		LocalJsonValue<int> propFailedDownloads = default;
		LocalJsonValue<int> propSkippedUpdates = default;
		LocalJsonValue<int> propSuccessfulDownloads = default;
		LocalJsonValue<System.TimeSpan> propTotalDownloadTime = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDatabasesCount.TryReadProperty(ref reader, options, PropDatabasesCount, null))
			{
				continue;
			}

			if (propExpiredDatabases.TryReadProperty(ref reader, options, PropExpiredDatabases, null))
			{
				continue;
			}

			if (propFailedDownloads.TryReadProperty(ref reader, options, PropFailedDownloads, null))
			{
				continue;
			}

			if (propSkippedUpdates.TryReadProperty(ref reader, options, PropSkippedUpdates, null))
			{
				continue;
			}

			if (propSuccessfulDownloads.TryReadProperty(ref reader, options, PropSuccessfulDownloads, null))
			{
				continue;
			}

			if (propTotalDownloadTime.TryReadProperty(ref reader, options, PropTotalDownloadTime, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.Ingest.GeoIpDownloadStatistics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DatabasesCount = propDatabasesCount.Value,
			ExpiredDatabases = propExpiredDatabases.Value,
			FailedDownloads = propFailedDownloads.Value,
			SkippedUpdates = propSkippedUpdates.Value,
			SuccessfulDownloads = propSuccessfulDownloads.Value,
			TotalDownloadTime = propTotalDownloadTime.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.GeoIpDownloadStatistics value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDatabasesCount, value.DatabasesCount, null, null);
		writer.WriteProperty(options, PropExpiredDatabases, value.ExpiredDatabases, null, null);
		writer.WriteProperty(options, PropFailedDownloads, value.FailedDownloads, null, null);
		writer.WriteProperty(options, PropSkippedUpdates, value.SkippedUpdates, null, null);
		writer.WriteProperty(options, PropSuccessfulDownloads, value.SuccessfulDownloads, null, null);
		writer.WriteProperty(options, PropTotalDownloadTime, value.TotalDownloadTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.GeoIpDownloadStatisticsConverter))]
public sealed partial class GeoIpDownloadStatistics
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoIpDownloadStatistics(int databasesCount, int expiredDatabases, int failedDownloads, int skippedUpdates, int successfulDownloads, System.TimeSpan totalDownloadTime)
	{
		DatabasesCount = databasesCount;
		ExpiredDatabases = expiredDatabases;
		FailedDownloads = failedDownloads;
		SkippedUpdates = skippedUpdates;
		SuccessfulDownloads = successfulDownloads;
		TotalDownloadTime = totalDownloadTime;
	}
#if NET7_0_OR_GREATER
	public GeoIpDownloadStatistics()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoIpDownloadStatistics()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoIpDownloadStatistics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Current number of databases available for use.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DatabasesCount { get; set; }

	/// <summary>
	/// <para>
	/// Total number of databases not updated after 30 days
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ExpiredDatabases { get; set; }

	/// <summary>
	/// <para>
	/// Total number of failed database downloads.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int FailedDownloads { get; set; }

	/// <summary>
	/// <para>
	/// Total number of database updates skipped.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int SkippedUpdates { get; set; }

	/// <summary>
	/// <para>
	/// Total number of successful database downloads.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int SuccessfulDownloads { get; set; }

	/// <summary>
	/// <para>
	/// Total milliseconds spent downloading databases.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TotalDownloadTime { get; set; }
}