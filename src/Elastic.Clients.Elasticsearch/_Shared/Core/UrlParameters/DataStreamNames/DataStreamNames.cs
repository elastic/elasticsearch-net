// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(DataStreamNamesConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public sealed class DataStreamNames :
	IUrlParameter,
	IEnumerable<DataStreamName>,
	IEquatable<DataStreamNames>
#if NET7_0_OR_GREATER
	, IParsable<DataStreamNames>
#endif
{
	internal readonly IList<DataStreamName> Names;

	internal DataStreamNames() => Names = new List<DataStreamName>();

	internal DataStreamNames(IEnumerable<DataStreamName> dataStreamNames) => Names = dataStreamNames.ToList();

	internal DataStreamNames(IList<DataStreamName> dataStreamNames) => Names = dataStreamNames;

	private string DebugDisplay =>
		$"Count: {Names.Count} [" + string.Join(",", Names.Select((t, i) => $"({i + 1}: {t?.DebugDisplay ?? "NULL"})")) + "]";

	public override string ToString() => string.Join(",", Names.Where(f => f is not null).Select(f => f.Name));

	public static implicit operator DataStreamNames(string[] dataStreamNames) => dataStreamNames.IsNullOrEmpty() ? null : new DataStreamNames(dataStreamNames.Select(f => new DataStreamName(f)));

	public static implicit operator DataStreamNames(string dataStreamName) => dataStreamName.IsNullOrEmptyCommaSeparatedList(out var split)
		? null
		: new DataStreamNames(split.Select(f => new DataStreamName(f)));

	public static implicit operator DataStreamNames(DataStreamName dataStreamName) => dataStreamName == null ? null : new DataStreamNames(new[] { dataStreamName });

	public static implicit operator DataStreamNames(DataStreamName[] dataStreamNames) => dataStreamNames.IsNullOrEmpty() ? null : new DataStreamNames(dataStreamNames);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<DataStreamName> GetEnumerator() => Names.GetEnumerator();

	public static bool operator ==(DataStreamNames left, DataStreamNames right) => Equals(left, right);

	public static bool operator !=(DataStreamNames left, DataStreamNames right) => !Equals(left, right);

	public override bool Equals(object obj) =>
		obj switch
		{
			DataStreamNames f => Equals(f),
			string s => Equals(s),
			DataStreamName fn => Equals(fn),
			DataStreamName[] fns => Equals(fns),
			_ => false,
		};

	public bool Equals(DataStreamNames other) => EqualsAllNames(Names, other.Names);

	private static bool EqualsAllNames(IList<DataStreamName> thisTypes, IList<DataStreamName> otherTypes)
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
		var hashCodes = new List<int>(Names.Count);
		foreach (var item in Names)
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
			return typeof(DataStreamName).GetHashCode() ^ hash;
		}
	}

	string IUrlParameter.GetString(ITransportConfiguration? settings) => ToString();

	#region IParsable

	public static DataStreamNames Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out DataStreamNames? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new DataStreamNames();
			return true;
		}

		var names = new List<DataStreamName>();
		foreach (var item in list)
		{
			if (!DataStreamName.TryParse(item, provider, out var name))
			{
				result = null;
				return false;
			}

			names.Add(name);
		}

		result = new DataStreamNames(names);
		return true;
	}

	#endregion IParsable
}

internal sealed class DataStreamNamesConverter : JsonConverter<DataStreamNames>
{
	public override DataStreamNames? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException($"Expected {JsonTokenType.StartArray} token but read '{reader.TokenType}' token for DataStreamNames.");

		return JsonSerializer.Deserialize<string[]>(ref reader, options);
	}

	public override void Write(Utf8JsonWriter writer, DataStreamNames value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		JsonSerializer.Serialize(writer, value.Names, options);
	}
}
