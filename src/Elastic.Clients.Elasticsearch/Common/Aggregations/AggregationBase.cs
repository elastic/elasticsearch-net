// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public abstract class AggregationBase
{
	protected AggregationBase(string name) => Name = name;

	/// <summary>
	/// Custom metadata associated with the aggregation.
	/// </summary>
	[JsonInclude]
	[JsonPropertyName("meta")]
	public Dictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// The name for this aggregation.
	/// </summary>
	[JsonIgnore]
	public string? Name { get; private set; }
		
	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator false(AggregationBase _) => false;

	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator true(AggregationBase _) => false;

	public static AggregationBase operator &(AggregationBase left, AggregationBase right) =>
		new AggregationCombinator(null, left, right);
}
