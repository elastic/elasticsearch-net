// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Requests;

public abstract class Request
{
	internal Request() { }

	internal virtual string? Accept { get; } = null;

	internal virtual string? ContentType { get; } = null;

	[JsonIgnore] internal abstract HttpMethod HttpMethod { get; }

	[JsonIgnore] internal abstract bool SupportsBody { get; }

	[JsonIgnore] protected RouteValues RouteValues { get; } = new();

	[JsonIgnore] protected virtual HttpMethod? DynamicHttpMethod => null;

	internal abstract ApiUrls ApiUrls { get; }

	protected virtual (string ResolvedUrl, string UrlTemplate) ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings) =>
		ApiUrls.Resolve(routeValues, settings);

	internal virtual void BeforeRequest() { }

	internal (string ResolvedUrl, string UrlTemplate) GetUrl(IElasticsearchClientSettings settings) => ResolveUrl(RouteValues, settings);
}

public abstract class Request<TParameters> : Request
	where TParameters : RequestParameters, new()
{
	private readonly TParameters _parameters;

	internal Request() => _parameters = new TParameters();

	protected Request(Func<RouteValues, RouteValues> pathSelector)
	{
		pathSelector(RouteValues);
		_parameters = new TParameters();
	}

	[JsonIgnore] internal TParameters RequestParameters => _parameters;
		
	protected TOut Q<TOut>(string name) => RequestParameters.GetQueryStringValue<TOut>(name);

	protected void Q(string name, object value) => RequestParameters.SetQueryString(name, value);

	protected void Q(string name, IStringable value) => RequestParameters.SetQueryString(name, value.GetString());

	protected void SetAcceptHeader(string format)
	{
		RequestParameters.RequestConfiguration ??= new RequestConfiguration();
		RequestParameters.RequestConfiguration.Accept =
			RequestParameters.AcceptHeaderFromFormat(format);
	}
}
