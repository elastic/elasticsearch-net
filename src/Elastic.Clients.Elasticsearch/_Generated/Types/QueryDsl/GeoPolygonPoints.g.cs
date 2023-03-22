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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class GeoPolygonPoints
{
	[JsonInclude, JsonPropertyName("points")]
	public ICollection<Elastic.Clients.Elasticsearch.GeoLocation> Points { get; set; }
}

public sealed partial class GeoPolygonPointsDescriptor : SerializableDescriptor<GeoPolygonPointsDescriptor>
{
	internal GeoPolygonPointsDescriptor(Action<GeoPolygonPointsDescriptor> configure) => configure.Invoke(this);

	public GeoPolygonPointsDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.GeoLocation> PointsValue { get; set; }

	public GeoPolygonPointsDescriptor Points(ICollection<Elastic.Clients.Elasticsearch.GeoLocation> points)
	{
		PointsValue = points;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("points");
		JsonSerializer.Serialize(writer, PointsValue, options);
		writer.WriteEndObject();
	}
}