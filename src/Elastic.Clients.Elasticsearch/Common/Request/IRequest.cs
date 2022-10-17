// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Requests;

public interface IRequest
{
	[JsonIgnore] string? Accept { get; }

	[JsonIgnore] string? ContentType { get; }

	[JsonIgnore] HttpMethod HttpMethod { get; }

	[JsonIgnore] bool SupportsBody { get; }

	[JsonIgnore] RouteValues RouteValues { get; }

	[JsonIgnore] IRequestParameters RequestParameters { get; }

	//[JsonIgnore] bool CanBeEmpty { get; }

	//[JsonIgnore] bool IsEmpty { get; }

	string GetUrl(IElasticsearchClientSettings settings);
}

public interface IRequest<out TParameters> : IRequest
	where TParameters : class, IRequestParameters, new()
{
	/// <summary>
	/// Used to describe request parameters that are not part of the body. e.g. query string, connection configuration
	/// overrides, etc.
	/// </summary>
	[JsonIgnore]
	new TParameters RequestParameters { get; }
}
