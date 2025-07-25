// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
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

[JsonConverter(typeof(SearchRequestOfTConverterFactory))]
public partial class SearchRequest<TInferDocument> : SearchRequest
{
	static SearchRequest()
	{
		DynamicallyAccessed.PublicConstructors(typeof(SearchRequestOfTConverter<TInferDocument>));
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

internal sealed class SearchRequestOfTConverterFactory :
	JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) =>
		typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(SearchRequest<>);

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();

#pragma warning disable IL3050
		return (JsonConverter)Activator.CreateInstance(typeof(SearchRequestOfTConverter<>).MakeGenericType(args[0]));
#pragma warning restore IL3050
	}
}

internal sealed class SearchRequestOfTConverter<T> :
	JsonConverter<SearchRequest<T>>
{
	public override SearchRequest<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		throw new NotSupportedException();

	public override void Write(Utf8JsonWriter writer, SearchRequest<T> value, JsonSerializerOptions options) =>
		writer.WriteValue(options, (SearchRequest)value);
}
