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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class CompletionContextConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.CompletionContext>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropContext = System.Text.Json.JsonEncodedText.Encode("context");
	private static readonly System.Text.Json.JsonEncodedText PropNeighbours = System.Text.Json.JsonEncodedText.Encode("neighbours");
	private static readonly System.Text.Json.JsonEncodedText PropPrecision = System.Text.Json.JsonEncodedText.Encode("precision");
	private static readonly System.Text.Json.JsonEncodedText PropPrefix = System.Text.Json.JsonEncodedText.Encode("prefix");

	public override Elastic.Clients.Elasticsearch.Core.Search.CompletionContext Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var readerSnapshot = reader;
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.Context> propContext = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>?> propNeighbours = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeohashPrecision?> propPrecision = default;
		LocalJsonValue<bool?> propPrefix = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propContext.TryReadProperty(ref reader, options, PropContext, null))
			{
				continue;
			}

			if (propNeighbours.TryReadProperty(ref reader, options, PropNeighbours, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.GeohashPrecision>(o, null)))
			{
				continue;
			}

			if (propPrecision.TryReadProperty(ref reader, options, PropPrecision, null))
			{
				continue;
			}

			if (propPrefix.TryReadProperty(ref reader, options, PropPrefix, null))
			{
				continue;
			}

			try
			{
				reader = readerSnapshot;
				var result = reader.ReadValue<Elastic.Clients.Elasticsearch.Core.Search.Context>(options, null);
				return new Elastic.Clients.Elasticsearch.Core.Search.CompletionContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
				{
					Context = result
				};
			}
			catch (System.Text.Json.JsonException)
			{
				throw;
			}
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Core.Search.CompletionContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Context = propContext.Value,
			Neighbours = propNeighbours.Value,
			Precision = propPrecision.Value,
			Prefix = propPrefix.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.CompletionContext value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropContext, value.Context, null, null);
		writer.WriteProperty(options, PropNeighbours, value.Neighbours, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.GeohashPrecision>(o, v, null));
		writer.WriteProperty(options, PropPrecision, value.Precision, null, null);
		writer.WriteProperty(options, PropPrefix, value.Prefix, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.CompletionContextConverter))]
public sealed partial class CompletionContext
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompletionContext(Elastic.Clients.Elasticsearch.Core.Search.Context context)
	{
		Context = context;
	}
#if NET7_0_OR_GREATER
	public CompletionContext()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CompletionContext()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CompletionContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The factor by which the score of the suggestion should be boosted.
	/// The score is computed by multiplying the boost with the suggestion weight.
	/// </para>
	/// </summary>
	public double? Boost { get; set; }

	/// <summary>
	/// <para>
	/// The value of the category to filter/boost on.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Core.Search.Context Context { get; set; }

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? Neighbours { get; set; }

	/// <summary>
	/// <para>
	/// The precision of the geohash to encode the query geo point.
	/// Can be specified as a distance value (<c>5m</c>, <c>10km</c>, etc.), or as a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.GeohashPrecision? Precision { get; set; }

	/// <summary>
	/// <para>
	/// Whether the category value should be treated as a prefix or not.
	/// </para>
	/// </summary>
	public bool? Prefix { get; set; }
}

public readonly partial struct CompletionContextDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.CompletionContext Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompletionContextDescriptor(Elastic.Clients.Elasticsearch.Core.Search.CompletionContext instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompletionContextDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.CompletionContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor(Elastic.Clients.Elasticsearch.Core.Search.CompletionContext instance) => new Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.CompletionContext(Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The factor by which the score of the suggestion should be boosted.
	/// The score is computed by multiplying the boost with the suggestion weight.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Boost(double? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value of the category to filter/boost on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Context(Elastic.Clients.Elasticsearch.Core.Search.Context value)
	{
		Instance.Context = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value of the category to filter/boost on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Context(System.Func<Elastic.Clients.Elasticsearch.Core.Search.ContextBuilder, Elastic.Clients.Elasticsearch.Core.Search.Context> action)
	{
		Instance.Context = Elastic.Clients.Elasticsearch.Core.Search.ContextBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Neighbours(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeohashPrecision>? value)
	{
		Instance.Neighbours = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Neighbours()
	{
		Instance.Neighbours = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfGeohashPrecision.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Neighbours(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfGeohashPrecision>? action)
	{
		Instance.Neighbours = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfGeohashPrecision.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Neighbours(params Elastic.Clients.Elasticsearch.GeohashPrecision[] values)
	{
		Instance.Neighbours = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of precision values at which neighboring geohashes should be taken into account.
	/// Precision value can be a distance value (<c>5m</c>, <c>10km</c>, etc.) or a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to generating neighbors for index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Neighbours(params System.Func<Elastic.Clients.Elasticsearch.GeohashPrecisionBuilder, Elastic.Clients.Elasticsearch.GeohashPrecision>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.GeohashPrecision>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.GeohashPrecisionBuilder.Build(action));
		}

		Instance.Neighbours = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The precision of the geohash to encode the query geo point.
	/// Can be specified as a distance value (<c>5m</c>, <c>10km</c>, etc.), or as a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Precision(Elastic.Clients.Elasticsearch.GeohashPrecision? value)
	{
		Instance.Precision = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The precision of the geohash to encode the query geo point.
	/// Can be specified as a distance value (<c>5m</c>, <c>10km</c>, etc.), or as a raw geohash precision (<c>1</c>..<c>12</c>).
	/// Defaults to index time precision level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Precision(System.Func<Elastic.Clients.Elasticsearch.GeohashPrecisionBuilder, Elastic.Clients.Elasticsearch.GeohashPrecision> action)
	{
		Instance.Precision = Elastic.Clients.Elasticsearch.GeohashPrecisionBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether the category value should be treated as a prefix or not.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor Prefix(bool? value = true)
	{
		Instance.Prefix = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.CompletionContext Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.CompletionContextDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.CompletionContext(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}