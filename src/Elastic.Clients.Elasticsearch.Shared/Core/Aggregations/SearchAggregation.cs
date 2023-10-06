// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;
#else
namespace Elastic.Clients.Elasticsearch.Aggregations;
#endif

// This is marked as internal for now, until we are ready to support plugin aggregations.
internal interface ISearchAggregation
{
	string? Name { get; }
}

/// <summary>
/// Base class for all aggregations.
/// </summary>
public abstract class SearchAggregation : ISearchAggregation
{
	internal SearchAggregation() { }

	public abstract string? Name { get; internal set; }

	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator false(SearchAggregation _) => false;

	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator true(SearchAggregation _) => false;

	public static SearchAggregation operator &(SearchAggregation left, SearchAggregation right) =>
		new AggregationCombinator(null, left, right);
}
