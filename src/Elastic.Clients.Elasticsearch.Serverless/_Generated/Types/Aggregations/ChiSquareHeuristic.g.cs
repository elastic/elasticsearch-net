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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

public sealed partial class ChiSquareHeuristic
{
	/// <summary>
	/// <para>
	/// Set to <c>false</c> if you defined a custom background filter that represents a different set of documents that you want to compare to.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("background_is_superset")]
	public bool BackgroundIsSuperset { get; set; }

	/// <summary>
	/// <para>
	/// Set to <c>false</c> to filter out the terms that appear less often in the subset than in documents outside the subset.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("include_negatives")]
	public bool IncludeNegatives { get; set; }
}

public sealed partial class ChiSquareHeuristicDescriptor : SerializableDescriptor<ChiSquareHeuristicDescriptor>
{
	internal ChiSquareHeuristicDescriptor(Action<ChiSquareHeuristicDescriptor> configure) => configure.Invoke(this);

	public ChiSquareHeuristicDescriptor() : base()
	{
	}

	private bool BackgroundIsSupersetValue { get; set; }
	private bool IncludeNegativesValue { get; set; }

	/// <summary>
	/// <para>
	/// Set to <c>false</c> if you defined a custom background filter that represents a different set of documents that you want to compare to.
	/// </para>
	/// </summary>
	public ChiSquareHeuristicDescriptor BackgroundIsSuperset(bool backgroundIsSuperset = true)
	{
		BackgroundIsSupersetValue = backgroundIsSuperset;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Set to <c>false</c> to filter out the terms that appear less often in the subset than in documents outside the subset.
	/// </para>
	/// </summary>
	public ChiSquareHeuristicDescriptor IncludeNegatives(bool includeNegatives = true)
	{
		IncludeNegativesValue = includeNegatives;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("background_is_superset");
		writer.WriteBooleanValue(BackgroundIsSupersetValue);
		writer.WritePropertyName("include_negatives");
		writer.WriteBooleanValue(IncludeNegativesValue);
		writer.WriteEndObject();
	}
}