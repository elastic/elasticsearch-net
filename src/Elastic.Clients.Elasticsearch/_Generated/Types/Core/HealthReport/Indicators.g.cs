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

namespace Elastic.Clients.Elasticsearch.Core.HealthReport;

public sealed partial class Indicators
{
	[JsonInclude, JsonPropertyName("data_stream_lifecycle")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.DataStreamLifecycleIndicator? DataStreamLifecycle { get; init; }
	[JsonInclude, JsonPropertyName("disk")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.DiskIndicator? Disk { get; init; }
	[JsonInclude, JsonPropertyName("file_settings")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.FileSettingsIndicator? FileSettings { get; init; }
	[JsonInclude, JsonPropertyName("ilm")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.IlmIndicator? Ilm { get; init; }
	[JsonInclude, JsonPropertyName("master_is_stable")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicator? MasterIsStable { get; init; }
	[JsonInclude, JsonPropertyName("repository_integrity")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.RepositoryIntegrityIndicator? RepositoryIntegrity { get; init; }
	[JsonInclude, JsonPropertyName("shards_availability")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsAvailabilityIndicator? ShardsAvailability { get; init; }
	[JsonInclude, JsonPropertyName("shards_capacity")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.ShardsCapacityIndicator? ShardsCapacity { get; init; }
	[JsonInclude, JsonPropertyName("slm")]
	public Elastic.Clients.Elasticsearch.Core.HealthReport.SlmIndicator? Slm { get; init; }
}