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
		if (Aggregations is not null || Suggest is not null)
		{
			TypedKeys = true;
		}
	}

	protected override (string ResolvedUrl, string UrlTemplate, Dictionary<string, string>? resolvedRouteValues) ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings)
	{
		if (Pit is not null && !string.IsNullOrEmpty(Pit.Id ?? string.Empty) && routeValues.ContainsKey("index"))
		{
			routeValues.Remove("index");
		}

		return base.ResolveUrl(routeValues, settings);
	}
}

[JsonConverter(typeof(SearchRequestOfTConverterFactory))]
public partial class SearchRequest<TInferDocument> : SearchRequest
{
	public SearchRequest(Indices? indices) : base(indices)
	{
	}

	public SearchRequest() : base(typeof(TInferDocument))
	{
	}
}

public sealed partial class SearchRequestDescriptor<TDocument>
{
	public SearchRequestDescriptor<TDocument> Index(Indices indices)
	{
		Self.RouteValues.Optional("index", indices);
		return Self;
	}

	public SearchRequestDescriptor<TDocument> Pit(string id, Action<Core.Search.PointInTimeReferenceDescriptor> configure)
	{
		PitValue = null;
		PitDescriptorAction = null;
		configure += a => a.Id(id);
		PitDescriptorAction = configure;
		return Self;
	}

	internal override void BeforeRequest()
	{
		if (AggregationsValue is not null || /*AggregationsDescriptor is not null || AggregationsDescriptorAction is not null ||*/
			SuggestValue is not null || SuggestDescriptor is not null || SuggestDescriptorAction is not null)
		{
			TypedKeys(true);
		}
	}

	protected override (string ResolvedUrl, string UrlTemplate, Dictionary<string, string>? resolvedRouteValues) ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings)
	{
		if ((Self.PitValue is not null || Self.PitDescriptor is not null || Self.PitDescriptorAction is not null) && routeValues.ContainsKey("index"))
		{
			routeValues.Remove("index");
		}

		return base.ResolveUrl(routeValues, settings);
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

		return (JsonConverter)Activator.CreateInstance(typeof(SearchRequestOfTConverter<>).MakeGenericType(args[0]));
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
