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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class PutFilterRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Instantiates a filter.
/// A filter contains a list of strings. It can be used by one or more anomaly detection jobs.
/// Specifically, filters are referenced in the <c>custom_rules</c> property of detector configuration objects.
/// </para>
/// </summary>
public sealed partial class PutFilterRequest : PlainRequest<PutFilterRequestParameters>
{
	public PutFilterRequest(Elastic.Clients.Elasticsearch.Id filterId) : base(r => r.Required("filter_id", filterId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPutFilter;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.put_filter";

	/// <summary>
	/// <para>
	/// A description of the filter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The items of the filter. A wildcard <c>*</c> can be used at the beginning or the end of an item.
	/// Up to 10000 items are allowed in each filter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("items")]
	public ICollection<string>? Items { get; set; }
}

/// <summary>
/// <para>
/// Instantiates a filter.
/// A filter contains a list of strings. It can be used by one or more anomaly detection jobs.
/// Specifically, filters are referenced in the <c>custom_rules</c> property of detector configuration objects.
/// </para>
/// </summary>
public sealed partial class PutFilterRequestDescriptor : RequestDescriptor<PutFilterRequestDescriptor, PutFilterRequestParameters>
{
	internal PutFilterRequestDescriptor(Action<PutFilterRequestDescriptor> configure) => configure.Invoke(this);

	public PutFilterRequestDescriptor(Elastic.Clients.Elasticsearch.Id filterId) : base(r => r.Required("filter_id", filterId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningPutFilter;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.put_filter";

	public PutFilterRequestDescriptor FilterId(Elastic.Clients.Elasticsearch.Id filterId)
	{
		RouteValues.Required("filter_id", filterId);
		return Self;
	}

	private string? DescriptionValue { get; set; }
	private ICollection<string>? ItemsValue { get; set; }

	/// <summary>
	/// <para>
	/// A description of the filter.
	/// </para>
	/// </summary>
	public PutFilterRequestDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The items of the filter. A wildcard <c>*</c> can be used at the beginning or the end of an item.
	/// Up to 10000 items are allowed in each filter.
	/// </para>
	/// </summary>
	public PutFilterRequestDescriptor Items(ICollection<string>? items)
	{
		ItemsValue = items;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (ItemsValue is not null)
		{
			writer.WritePropertyName("items");
			JsonSerializer.Serialize(writer, ItemsValue, options);
		}

		writer.WriteEndObject();
	}
}