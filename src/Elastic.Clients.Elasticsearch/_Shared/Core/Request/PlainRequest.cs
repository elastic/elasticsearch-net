// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Requests;

public abstract class PlainRequest<TParameters> : Request<TParameters>
	where TParameters : RequestParameters, new()
{
	private IRequestConfiguration? _requestConfiguration; // TODO: Remove this from the request classes and add to the endpoint methods instead

	// This internal ctor ensures that only types defined within the Elastic.Clients.Elasticsearch assembly can derive from this base class.
	// We don't expect consumers to derive from this public base class.
	internal PlainRequest() { }

	protected PlainRequest(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }

	///<summary>Include the stack trace of returned errors.</summary>
	[JsonIgnore]
	public bool? ErrorTrace
	{
		get => Q<bool?>("error_trace");
		set => Q("error_trace", value);
	}

	/// <summary>
	/// A list of filters used to reduce the response.
	/// <para>
	///     Use of response filtering can result in a response from Elasticsearch
	///     that cannot be correctly deserialized to the respective response type for the request.
	///     In such situations, use the low level client to issue the request and handle response deserialization.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string[] FilterPath
	{
		get => Q<string[]>("filter_path");
		set => Q("filter_path", value);
	}

	///<summary>Return human-readable values for statistics.</summary>
	[JsonIgnore]
	public bool? Human
	{
		get => Q<bool?>("human");
		set => Q("human", value);
	}

	///<summary>Pretty format the returned JSON response.</summary>
	[JsonIgnore]
	public bool? Pretty
	{
		get => Q<bool?>("pretty");
		set => Q("pretty", value);
	}

	/// <summary>
	///     The URL-encoded request definition. Useful for libraries that do not accept a request body for non-POST
	///     requests.
	/// </summary>
	[JsonIgnore]
	public string SourceQueryString
	{
		get => Q<string>("source");
		set => Q("source", value);
	}

	/// <summary>
	/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
	/// </summary>
	[JsonIgnore]
	public IRequestConfiguration? RequestConfiguration
	{
		get => _requestConfiguration;
		set => _requestConfiguration = value;
	}
}
