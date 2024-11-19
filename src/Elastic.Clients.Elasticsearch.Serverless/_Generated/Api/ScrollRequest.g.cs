// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless;

public sealed partial class ScrollRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If true, the API response’s hit.total property is returned as an integer. If false, the API response’s hit.total property is returned as an object.
	/// </para>
	/// </summary>
	public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }
}

/// <summary>
/// <para>
/// Run a scrolling search.
/// </para>
/// <para>
/// IMPORTANT: The scroll API is no longer recommend for deep pagination. If you need to preserve the index state while paging through more than 10,000 hits, use the <c>search_after</c> parameter with a point in time (PIT).
/// </para>
/// <para>
/// The scroll API gets large sets of results from a single scrolling search request.
/// To get the necessary scroll ID, submit a search API request that includes an argument for the <c>scroll</c> query parameter.
/// The <c>scroll</c> parameter indicates how long Elasticsearch should retain the search context for the request.
/// The search response returns a scroll ID in the <c>_scroll_id</c> response body parameter.
/// You can then use the scroll ID with the scroll API to retrieve the next batch of results for the request.
/// If the Elasticsearch security features are enabled, the access to the results of a specific scroll ID is restricted to the user or API key that submitted the search.
/// </para>
/// <para>
/// You can also use the scroll API to specify a new scroll parameter that extends or shortens the retention period for the search context.
/// </para>
/// <para>
/// IMPORTANT: Results from a scrolling search reflect the state of the index at the time of the initial search request. Subsequent indexing or document changes only affect later search and scroll requests.
/// </para>
/// </summary>
public sealed partial class ScrollRequest : PlainRequest<ScrollRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceScroll;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "scroll";

	/// <summary>
	/// <para>
	/// If true, the API response’s hit.total property is returned as an integer. If false, the API response’s hit.total property is returned as an object.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

	/// <summary>
	/// <para>
	/// Period to retain the search context for scrolling.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("scroll")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Scroll { get; set; }

	/// <summary>
	/// <para>
	/// Scroll ID of the search.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("scroll_id")]
	public Elastic.Clients.Elasticsearch.Serverless.ScrollId ScrollId { get; set; }
}

/// <summary>
/// <para>
/// Run a scrolling search.
/// </para>
/// <para>
/// IMPORTANT: The scroll API is no longer recommend for deep pagination. If you need to preserve the index state while paging through more than 10,000 hits, use the <c>search_after</c> parameter with a point in time (PIT).
/// </para>
/// <para>
/// The scroll API gets large sets of results from a single scrolling search request.
/// To get the necessary scroll ID, submit a search API request that includes an argument for the <c>scroll</c> query parameter.
/// The <c>scroll</c> parameter indicates how long Elasticsearch should retain the search context for the request.
/// The search response returns a scroll ID in the <c>_scroll_id</c> response body parameter.
/// You can then use the scroll ID with the scroll API to retrieve the next batch of results for the request.
/// If the Elasticsearch security features are enabled, the access to the results of a specific scroll ID is restricted to the user or API key that submitted the search.
/// </para>
/// <para>
/// You can also use the scroll API to specify a new scroll parameter that extends or shortens the retention period for the search context.
/// </para>
/// <para>
/// IMPORTANT: Results from a scrolling search reflect the state of the index at the time of the initial search request. Subsequent indexing or document changes only affect later search and scroll requests.
/// </para>
/// </summary>
public sealed partial class ScrollRequestDescriptor : RequestDescriptor<ScrollRequestDescriptor, ScrollRequestParameters>
{
	internal ScrollRequestDescriptor(Action<ScrollRequestDescriptor> configure) => configure.Invoke(this);

	public ScrollRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceScroll;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "scroll";

	public ScrollRequestDescriptor RestTotalHitsAsInt(bool? restTotalHitsAsInt = true) => Qs("rest_total_hits_as_int", restTotalHitsAsInt);

	private Elastic.Clients.Elasticsearch.Serverless.Duration? ScrollValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScrollId ScrollIdValue { get; set; }

	/// <summary>
	/// <para>
	/// Period to retain the search context for scrolling.
	/// </para>
	/// </summary>
	public ScrollRequestDescriptor Scroll(Elastic.Clients.Elasticsearch.Serverless.Duration? scroll)
	{
		ScrollValue = scroll;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Scroll ID of the search.
	/// </para>
	/// </summary>
	public ScrollRequestDescriptor ScrollId(Elastic.Clients.Elasticsearch.Serverless.ScrollId scrollId)
	{
		ScrollIdValue = scrollId;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ScrollValue is not null)
		{
			writer.WritePropertyName("scroll");
			JsonSerializer.Serialize(writer, ScrollValue, options);
		}

		writer.WritePropertyName("scroll_id");
		JsonSerializer.Serialize(writer, ScrollIdValue, options);
		writer.WriteEndObject();
	}
}