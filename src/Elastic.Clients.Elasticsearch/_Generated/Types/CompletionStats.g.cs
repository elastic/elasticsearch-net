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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class CompletionStats
{
	[JsonInclude, JsonPropertyName("fields")]
	[ReadOnlyFieldDictionaryConverter(typeof(Elastic.Clients.Elasticsearch.FieldSizeUsage))]
	public IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.FieldSizeUsage>? Fields { get; init; }

	/// <summary>
	/// <para>
	/// Total amount of memory used for completion across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public Elastic.Clients.Elasticsearch.ByteSize? Size { get; init; }

	/// <summary>
	/// <para>
	/// Total amount, in bytes, of memory used for completion across all shards assigned to selected nodes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size_in_bytes")]
	public long SizeInBytes { get; init; }
}