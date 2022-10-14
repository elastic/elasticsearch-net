// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Requests;

public abstract class Request<TParameters> : IRequest<TParameters>
	where TParameters : class, IRequestParameters, new()
{
	private readonly TParameters _parameters;

	// ReSharper disable once VirtualMemberCallInConstructor
	internal Request()
	{
		_parameters = new TParameters();
		// ReSharper disable once VirtualMemberCallInConstructor
		RequestDefaults(_parameters);
	}

	protected Request(Func<RouteValues, RouteValues> pathSelector)
	{
		pathSelector(RequestState.RouteValues);
		_parameters = new TParameters();
		// ReSharper disable once VirtualMemberCallInConstructor
		RequestDefaults(_parameters);
	}

	protected virtual HttpMethod? DynamicHttpMethod { get; }

	protected abstract HttpMethod HttpMethod { get; }

	protected abstract bool SupportsBody { get; }

	internal virtual void BeforeRequest() { }

	//protected virtual bool CanBeEmpty => false;

	//protected virtual bool IsEmpty => false;

	[JsonIgnore] protected IRequest<TParameters> RequestState => this;

	protected virtual string? Accept { get; } = null;

	protected virtual string? ContentType { get; } = null;

	internal abstract ApiUrls ApiUrls { get; }

	[JsonIgnore] HttpMethod IRequest.HttpMethod => DynamicHttpMethod ?? HttpMethod;

	[JsonIgnore] bool IRequest.SupportsBody => SupportsBody;

	//[JsonIgnore] bool IRequest.CanBeEmpty => CanBeEmpty;

	//[JsonIgnore] bool IRequest.IsEmpty => IsEmpty;

	[JsonIgnore] string? IRequest.Accept => Accept;

	[JsonIgnore] string? IRequest.ContentType => ContentType;

	[JsonIgnore] TParameters IRequest<TParameters>.RequestParameters => _parameters;

	IRequestParameters IRequest.RequestParameters => _parameters;

	[JsonIgnore] RouteValues IRequest.RouteValues { get; } = new();

	string IRequest.GetUrl(IElasticsearchClientSettings settings) => ResolveUrl(RequestState.RouteValues, settings);

	protected virtual string ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings) =>
		ApiUrls.Resolve(routeValues, settings);

	/// <summary>
	///     Allows a request implementation to set certain request parameter defaults, use sparingly!
	/// </summary>
	protected virtual void RequestDefaults(TParameters parameters) { }

	protected TOut Q<TOut>(string name) => RequestState.RequestParameters.GetQueryStringValue<TOut>(name);

	protected void Q(string name, object value) => RequestState.RequestParameters.SetQueryString(name, value);

	protected void Q(string name, IStringable value) => RequestState.RequestParameters.SetQueryString(name, value.GetString());

	protected void SetAcceptHeader(string format)
	{
		RequestState.RequestParameters.RequestConfiguration ??= new RequestConfiguration();
		RequestState.RequestParameters.RequestConfiguration.Accept =
			RequestState.RequestParameters.AcceptHeaderFromFormat(format);
	}
}
