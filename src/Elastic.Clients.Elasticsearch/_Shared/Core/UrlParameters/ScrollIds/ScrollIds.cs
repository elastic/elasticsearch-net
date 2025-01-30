// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(ScrollIdsConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public sealed class ScrollIds : IUrlParameter, IEnumerable<ScrollId>, IEquatable<ScrollIds>
{
	internal readonly IList<ScrollId> Ids;

	internal ScrollIds() => Ids = new List<ScrollId>();

	internal ScrollIds(IEnumerable<ScrollId> dataStreamNames) => Ids = dataStreamNames.ToList();

	internal ScrollIds(IList<ScrollId> dataStreamNames) => Ids = dataStreamNames;

	private string DebugDisplay =>
		$"Count: {Ids.Count} [" + string.Join(",", Ids.Select((t, i) => $"({i + 1}: {t?.DebugDisplay ?? "NULL"})")) + "]";

	public override string ToString() => string.Join(",", Ids.Where(f => f is not null).Select(f => f.Id));

	public static implicit operator ScrollIds(string[] scrollIds) => scrollIds.IsNullOrEmpty() ? null : new ScrollIds(scrollIds.Select(f => new ScrollId(f)));

	public static implicit operator ScrollIds(string scrollIds) => scrollIds.IsNullOrEmptyCommaSeparatedList(out var split)
		? null
		: new ScrollIds(split.Select(f => new ScrollId(f)));

	public static implicit operator ScrollIds(ScrollId scrollId) => scrollId == null ? null : new ScrollIds(new[] { scrollId });

	public static implicit operator ScrollIds(ScrollId[] scrollIds) => scrollIds.IsNullOrEmpty() ? null : new ScrollIds(scrollIds);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<ScrollId> GetEnumerator() => Ids.GetEnumerator();

	public static bool operator ==(ScrollIds left, ScrollIds right) => Equals(left, right);

	public static bool operator !=(ScrollIds left, ScrollIds right) => !Equals(left, right);

	public override bool Equals(object obj) =>
		obj switch
		{
			ScrollIds f => Equals(f),
			string s => Equals(s),
			ScrollIds[] fns => Equals(fns),
			_ => false,
		};

	public bool Equals(ScrollIds other) => EqualsAllIds(Ids, other.Ids);

	private static bool EqualsAllIds(IList<ScrollId> thisTypes, IList<ScrollId> otherTypes)
	{
		if (thisTypes is null && otherTypes is null)
			return true;

		if (thisTypes is null || otherTypes is null)
			return false;

		if (thisTypes.Count != otherTypes.Count)
			return false;

		return !thisTypes.Except(otherTypes).Any();
	}

	public override int GetHashCode()
	{
		var hashCodes = new List<int>(Ids.Count);
		foreach (var item in Ids)
		{
			hashCodes.Add(item.GetHashCode());
		}

		hashCodes.Sort();

		unchecked
		{
			var hash = 17;
			foreach (var hashCode in hashCodes)
			{
				hash = hash * 23 + hashCode;
			}
			return typeof(ScrollId).GetHashCode() ^ hash;
		}
	}

	string IUrlParameter.GetString(ITransportConfiguration? settings) => ToString();
}

internal sealed class ScrollIdsConverter : JsonConverter<ScrollIds>
{
	public override ScrollIds? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException($"Expected {JsonTokenType.StartArray} token but read '{reader.TokenType}' token for ScrollIds.");

		return JsonSerializer.Deserialize<string[]>(ref reader, options);
	}

	public override void Write(Utf8JsonWriter writer, ScrollIds value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		JsonSerializer.Serialize(writer, value.Ids, options);
	}
}
