// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using Elastic.Transport;
using Elastic.Transport.Diagnostics;

namespace Elastic.Clients.Elasticsearch.Requests;

/// <summary>
/// Base type for requests sent by the client.
/// </summary>
public abstract class Request
{
	// This internal ctor ensures that only types defined within the Elastic.Clients.Elasticsearch assembly can derive from this base class.
	// We don't expect consumers to derive from this public base class.
	internal Request() { }

	internal virtual string? Accept { get; } = null;

	internal virtual string? ContentType { get; } = null;

	/// <summary>
	/// The default HTTP method for the request which is based on the Elasticsearch Specification endpoint definition. 
	/// </summary>
	[JsonIgnore]
	protected abstract HttpMethod StaticHttpMethod { get; }

	[JsonIgnore]
	internal abstract bool SupportsBody { get; }

	[JsonIgnore]
	protected RouteValues RouteValues { get; } = new();

	/// <summary>
	/// Allows for per request replacement of the specified HTTP method, including scenarios such as indexing which
	/// require access to the document to determine the correct URL path and method combination to choose.
	/// </summary>
	[JsonIgnore]
	protected virtual HttpMethod? DynamicHttpMethod => null;

	/// <summary>
	/// The final HTTP method used to send the request to the Elasticsearch server.
	/// </summary>
	[JsonIgnore]
	internal HttpMethod HttpMethod => DynamicHttpMethod ?? StaticHttpMethod;

	internal abstract ApiUrls ApiUrls { get; }

	protected virtual (string ResolvedUrl, string UrlTemplate) ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings) =>
		ApiUrls.Resolve(routeValues, settings);

	internal virtual void BeforeRequest() { }

	internal (string ResolvedUrl, string UrlTemplate) GetUrl(IElasticsearchClientSettings settings) => ResolveUrl(RouteValues, settings);

	[JsonIgnore]
	internal virtual string? OperationName { get; }
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
