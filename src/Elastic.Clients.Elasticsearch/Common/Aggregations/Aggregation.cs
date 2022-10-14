// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Aggregations;

// This is marked as internal for now, until we are ready to support plugin aggregations.
internal interface IAggregation
{
	string? Name { get; }
}

/// <summary>
/// Base class for all aggregations.
/// </summary>
public abstract class Aggregation : IAggregation
{
	internal Aggregation() { }

	public abstract string? Name { get; internal set; }

	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator false(Aggregation _) => false;

	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator true(Aggregation _) => false;

	public static Aggregation operator &(Aggregation left, Aggregation right) =>
		new AggregationCombinator(null, left, right);
}
