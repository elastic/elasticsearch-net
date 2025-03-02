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

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(IndicesJsonConverter))]
public sealed class Indices :
	IUrlParameter,
	IEnumerable<IndexName>,
	IEquatable<Indices>
#if NET7_0_OR_GREATER
	, IParsable<Indices>
#endif
{
	private static readonly HashSet<IndexName> AllIndexList = new() { AllValue };

	internal const string AllValue = "_all";
	internal const string WildcardValue = "*";

	private readonly HashSet<IndexName>? _indices;
	private readonly bool _isAllIndices;

	internal Indices()
	{
		_indices = [];
	}

	internal Indices(IndexName indexName)
	{
		if (indexName.Equals(AllValue) || indexName.Equals(WildcardValue))
		{
			_isAllIndices = true;
			_indices = AllIndexList;
			return;
		}

		_indices = new HashSet<IndexName>
		{
			indexName
		};
	}

	internal Indices(IEnumerable<IndexName> indices)
	{
		var enumerated = indices.NotEmpty(nameof(indices));

		foreach (var index in enumerated)
		{
			if (index.Equals(AllValue) || index.Equals(WildcardValue))
			{
				_isAllIndices = true;
				_indices = AllIndexList;
				return;
			}
		}

		_indices = new HashSet<IndexName>(enumerated);
	}

	internal Indices(IEnumerable<string> indices)
	{
		var enumerated = indices.NotEmpty(nameof(indices));

		foreach (var index in enumerated)
		{
			if (index.Equals(AllValue) || index.Equals(WildcardValue))
			{
				_isAllIndices = true;
				_indices = AllIndexList;
				return;
			}
		}

		_indices = new HashSet<IndexName>(enumerated.Select(s => (IndexName)s));
	}

	public Indices And<T>()
	{
		if (_isAllIndices)
			return this;

		_indices.Add(typeof(T));

		return this;
	}

	internal HashSet<IndexName> IndexNames => _indices;

	public static Indices All { get; } = new Indices(AllValue);

	private string DebugDisplay => _isAllIndices ? "_all" : $"Count: {_indices.Count} [" + string.Join(",", _indices.Select((t, i) => $"({i + 1}: {t.DebugDisplay})")) + "]";

	public override string ToString() => DebugDisplay;

	string IUrlParameter.GetString(ITransportConfiguration? settings)
	{
		if (settings is not IElasticsearchClientSettings clientSettings)
			throw new Exception(
				"Tried to pass index names on querysting but it could not be resolved because no nest settings are available.");

		if (_isAllIndices)
			return "_all";

		var inferrer = clientSettings.Inferrer;

		if (_indices.Count == 1)
		{
			var value = inferrer.IndexName(_indices.First());
			return value;
		}

		var indices = _indices.Select(i => inferrer.IndexName(i));
		return string.Join(",", indices);
	}

	public static IndexName Index(string index) => index;

	public static IndexName Index(IndexName index) => index;

	public static IndexName Index<T>() => typeof(T);

	public static Indices Parse(string indicesString)
	{
		if (indicesString.IsNullOrEmptyCommaSeparatedList(out var indices))
			return null;

		return indices.Contains(AllValue) || indices.Contains(WildcardValue) ? All : new Indices(indices);
	}

	public static implicit operator Indices(string indicesString) => Parse(indicesString);

	public static implicit operator Indices(string[] indices) => indices.IsNullOrEmpty() ? null : new Indices(indices);

	public static implicit operator Indices(IndexName[] indices) => indices.IsNullOrEmpty() ? null : new Indices(indices);

	public static implicit operator Indices(IndexName index) => index == null ? null : new Indices(new[] { index });

	public static implicit operator Indices(Type type) => type == null ? null : new Indices(new IndexName[] { type });

	public static bool operator ==(Indices left, Indices right) => Equals(left, right);

	public static bool operator !=(Indices left, Indices right) => !Equals(left, right);

	public bool Equals(Indices other) => EqualsAllIndices(IndexNames, other.IndexNames);

	public override bool Equals(object obj)
	{
		if (obj is not Indices other)
			return false;

		return EqualsAllIndices(IndexNames, other.IndexNames);
	}

	private static bool EqualsAllIndices(HashSet<IndexName> thisIndices, HashSet<IndexName> otherIndices)
	{
		if (thisIndices == null && otherIndices == null)
			return true;

		if (thisIndices == null || otherIndices == null)
			return false;

		return thisIndices.Count == otherIndices.Count && !thisIndices.Except(otherIndices).Any();
	}

	public override int GetHashCode()
	{
		var hashCodes = new List<int>(IndexNames.Count);

		foreach (var item in IndexNames.OrderBy(s => s))
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
			return typeof(IndexName).GetHashCode() ^ hash;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<IndexName> GetEnumerator() => IndexNames.GetEnumerator();

	#region IParsable

	public static Indices Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Indices? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new Indices();
			return true;
		}

		var names = new List<IndexName>();
		foreach (var item in list)
		{
			if (!IndexName.TryParse(item, provider, out var name))
			{
				result = null;
				return false;
			}

			names.Add(name);
		}

		result = new Indices(names);
		return true;
	}

	#endregion IParsable
}

internal sealed class IndicesJsonConverter : JsonConverter<Indices>
{
	private IElasticsearchClientSettings _settings;

	public override Indices? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			Indices indices = reader.GetString();
			return indices;
		}
		else if (reader.TokenType == JsonTokenType.StartArray)
		{
			var indices = new List<string>();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				var index = reader.GetString();
				indices.Add(index);
			}
			return new Indices(indices);
		}

		reader.Read();
		return null;
	}

	public override void Write(Utf8JsonWriter writer, Indices value, JsonSerializerOptions options)
	{
		InitializeSettings(options);

		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(((IUrlParameter)value).GetString(_settings));
	}

	private void InitializeSettings(JsonSerializerOptions options)
	{
		if (_settings is null)
		{
			if (!options.TryGetClientSettings(out var settings))
				ThrowHelper.ThrowJsonExceptionForMissingSettings();

			_settings = settings;
		}
	}
}
