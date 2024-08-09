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

namespace Elastic.Clients.Elasticsearch.Core.Search;

public sealed partial class CompletionContext
{
	/// <summary>
	/// <para>
	/// The factor by which the score of the suggestion should be boosted.
	/// The score is computed by multiplying the boost with the suggestion weight.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public double? Boost { get; set; }

	/// <summary>
	/// <para>
	/// The value of the category to filter/boost on.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("context")]
	public Elastic.Clients.Elasticsearch.Core.Search.Context Context { get; set; }

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("neighbours")]
	public ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? Neighbours { get; set; }

	/// <summary>
	/// <para>
	/// The precision of the geohash to encode the query geo point.
	/// Can be specified as a distance value (<c>5m</c>, <c>10km</c>, etc.), or as a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to index time precision level.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("precision")]
	public Elastic.Clients.Elasticsearch.GeohashPrecision? Precision { get; set; }

	/// <summary>
	/// <para>
	/// Whether the category value should be treated as a prefix or not.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prefix")]
	public bool? Prefix { get; set; }
}

public sealed partial class CompletionContextDescriptor : SerializableDescriptor<CompletionContextDescriptor>
{
	internal CompletionContextDescriptor(Action<CompletionContextDescriptor> configure) => configure.Invoke(this);

	public CompletionContextDescriptor() : base()
	{
	}

	private double? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Search.Context ContextValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? NeighboursValue { get; set; }
	private Elastic.Clients.Elasticsearch.GeohashPrecision? PrecisionValue { get; set; }
	private bool? PrefixValue { get; set; }

	/// <summary>
	/// <para>
	/// The factor by which the score of the suggestion should be boosted.
	/// The score is computed by multiplying the boost with the suggestion weight.
	/// </para>
	/// </summary>
	public CompletionContextDescriptor Boost(double? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The value of the category to filter/boost on.
	/// </para>
	/// </summary>
	public CompletionContextDescriptor Context(Elastic.Clients.Elasticsearch.Core.Search.Context context)
	{
		ContextValue = context;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public CompletionContextDescriptor Neighbours(ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? neighbours)
	{
		NeighboursValue = neighbours;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The precision of the geohash to encode the query geo point.
	/// Can be specified as a distance value (<c>5m</c>, <c>10km</c>, etc.), or as a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to index time precision level.
	/// </para>
	/// </summary>
	public CompletionContextDescriptor Precision(Elastic.Clients.Elasticsearch.GeohashPrecision? precision)
	{
		PrecisionValue = precision;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Whether the category value should be treated as a prefix or not.
	/// </para>
	/// </summary>
	public CompletionContextDescriptor Prefix(bool? prefix = true)
	{
		PrefixValue = prefix;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("context");
		JsonSerializer.Serialize(writer, ContextValue, options);
		if (NeighboursValue is not null)
		{
			writer.WritePropertyName("neighbours");
			JsonSerializer.Serialize(writer, NeighboursValue, options);
		}

		if (PrecisionValue is not null)
		{
			writer.WritePropertyName("precision");
			JsonSerializer.Serialize(writer, PrecisionValue, options);
		}

		if (PrefixValue.HasValue)
		{
			writer.WritePropertyName("prefix");
			writer.WriteBooleanValue(PrefixValue.Value);
		}

		writer.WriteEndObject();
	}
}