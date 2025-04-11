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

internal sealed partial class TrackHitsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.TrackHits>
{
	public override Elastic.Clients.Elasticsearch.Core.Search.TrackHits Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.True | Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.False, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.Number);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.Core.Search.TrackHits(reader.ReadValue<bool>(options, null)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.Core.Search.TrackHits(reader.ReadValue<int>(options, null)),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.Core.Search.TrackHits)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.TrackHits value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value.Tag)
		{
			case Elastic.Clients.Elasticsearch.UnionTag.T1:
				{
					writer.WriteValue(options, value.Value1, null);
					break;
				}

			case Elastic.Clients.Elasticsearch.UnionTag.T2:
				{
					writer.WriteValue(options, value.Value2, null);
					break;
				}

			default:
				throw new System.InvalidOperationException($"Unrecognized tag value: {value.Tag}");
		}
	}
}

/// <summary>
/// <para>
/// Number of hits matching the query to count accurately. If true, the exact
/// number of hits is returned at the cost of some performance. If false, the
/// response does not include the total number of hits matching the query.
/// Defaults to 10,000 hits.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.TrackHitsConverter))]
public sealed partial class TrackHits : Elastic.Clients.Elasticsearch.Union<bool, int>
{
	public TrackHits(bool value) : base(value)
	{
	}

	public TrackHits(int value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.TrackHits(bool value) => new Elastic.Clients.Elasticsearch.Core.Search.TrackHits(value);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.TrackHits(int value) => new Elastic.Clients.Elasticsearch.Core.Search.TrackHits(value);
}

public readonly partial struct TrackHitsFactory
{
	public Elastic.Clients.Elasticsearch.Core.Search.TrackHits Enabled(bool value = true)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.TrackHits(value);
	}

	public Elastic.Clients.Elasticsearch.Core.Search.TrackHits Count(int value)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.TrackHits(value);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.TrackHits Build(System.Func<Elastic.Clients.Elasticsearch.Core.Search.TrackHitsFactory, Elastic.Clients.Elasticsearch.Core.Search.TrackHits> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.TrackHitsFactory();
		return action.Invoke(builder);
	}
}