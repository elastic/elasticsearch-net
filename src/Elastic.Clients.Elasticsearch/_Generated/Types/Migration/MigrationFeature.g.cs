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
namespace Elastic.Clients.Elasticsearch.Migration
{
	public partial class MigrationFeature
	{
		[JsonInclude]
		[JsonPropertyName("feature_name")]
		public string FeatureName { get; init; }

		[JsonInclude]
		[JsonPropertyName("indices")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Migration.MigrationFeatureIndexInfo> Indices { get; init; }

		[JsonInclude]
		[JsonPropertyName("migration_status")]
		public Elastic.Clients.Elasticsearch.Migration.MigrationStatus MigrationStatus { get; init; }

		[JsonInclude]
		[JsonPropertyName("minimum_index_version")]
		public string MinimumIndexVersion { get; init; }
	}
}