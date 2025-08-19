// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

public partial class SearchRequest
{
	internal override void BeforeRequest()
	{
		if (Aggregations is { Count: > 0 } || Suggest is { Suggesters: { Count: > 0 } })
		{
			TypedKeys ??= true;
		}
	}

	protected override (string ResolvedUrl, string UrlTemplate, Dictionary<string, string>? resolvedRouteValues) ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings)
	{
		if (!string.IsNullOrEmpty(Pit?.Id) && routeValues.ContainsKey("index"))
		{
			routeValues.Remove("index");
		}

		return base.ResolveUrl(routeValues, settings);
	}
}

[JsonConverter(typeof(Json.SearchRequestOfTConverterFactory))]
public partial class SearchRequest<TInferDocument> : SearchRequest
{
	static SearchRequest()
	{
		DynamicallyAccessed.PublicConstructors(typeof(Json.SearchRequestOfTConverter<TInferDocument>));
	}

	public SearchRequest(Indices? indices) : base(indices)
	{
	}

	public SearchRequest() : base(typeof(TInferDocument))
	{
	}
}

public readonly partial struct SearchRequestDescriptor<TDocument>
{
	[Obsolete("Use 'Indices()' instead.")]
	public SearchRequestDescriptor<TDocument> Index(Indices indices) => Indices(indices);

	public SearchRequestDescriptor<TDocument> Pit(string id, Action<Core.Search.PointInTimeReferenceDescriptor> configure)
	{
		configure += a => a.Id(id);
		return Pit(configure);
	}
}
