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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class IndexTemplateSummary
{
	/// <summary>
	/// <para>
	/// Aliases to add.
	/// If the index template includes a <c>data_stream</c> object, these are data stream aliases.
	/// Otherwise, these are index aliases.
	/// Data stream aliases ignore the <c>index_routing</c>, <c>routing</c>, and <c>search_routing</c> options.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("aliases")]
	[ReadOnlyIndexNameDictionaryConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.Alias))]
	public IReadOnlyDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; init; }
	[JsonInclude, JsonPropertyName("lifecycle")]
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover? Lifecycle { get; init; }

	/// <summary>
	/// <para>
	/// Mapping for fields in the index.
	/// If specified, this mapping can include field names, field data types, and mapping parameters.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("mappings")]
	public Elastic.Clients.Elasticsearch.Mapping.TypeMapping? Mappings { get; init; }

	/// <summary>
	/// <para>
	/// Configuration options for the index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? Settings { get; init; }
}