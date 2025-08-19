// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Core;

namespace Elastic.Clients.Elasticsearch.Aggregations;

/// <summary>
/// <para>Buckets path can be expressed in different ways, and an aggregation may accept some or all of these<br/>forms depending on its type. Please refer to each aggregation's documentation to know what buckets<br/>path forms they accept.</para>
/// </summary>
[JsonConverter(typeof(Json.BucketsPathConverter))]
public sealed class BucketsPath : IComplexUnion<BucketsPath.Kind>
{
	public enum Kind
	{
		Single,
		Array,
		Dictionary
	}

	internal readonly Kind _kind;
	internal readonly object _value;

	Kind IComplexUnion<Kind>.ValueKind => _kind;

	object IComplexUnion<Kind>.Value => _value;

	private BucketsPath(Kind kind, object value)
	{
		_kind = kind;
		_value = value;
	}

	public static BucketsPath Single(string single) => new(Kind.Single, single);

	public bool IsSingle => _kind == Kind.Single;

	public bool TryGetSingle([NotNullWhen(true)] out string? single)
	{
		single = null;
		if (_kind == Kind.Single)
		{
			single = (string)_value;
			return true;
		}

		return false;
	}

	public static implicit operator BucketsPath(string single) => BucketsPath.Single(single);

	public static BucketsPath Array(string[] array) => new(Kind.Array, array);

	public bool IsArray => _kind == Kind.Array;

	public bool TryGetArray([NotNullWhen(true)] out string[]? array)
	{
		array = null;
		if (_kind == Kind.Array)
		{
			array = (string[])_value;
			return true;
		}

		return false;
	}

	public static implicit operator BucketsPath(string[] array) => BucketsPath.Array(array);

	public static BucketsPath Dictionary(Dictionary<string, string> dictionary) => new(Kind.Dictionary, dictionary);

	public bool IsDictionary => _kind == Kind.Dictionary;

	public bool TryGetDictionary([NotNullWhen(true)] out Dictionary<string, string>? dictionary)
	{
		dictionary = null;
		if (_kind == Kind.Dictionary)
		{
			dictionary = (Dictionary<string, string>)_value;
			return true;
		}

		return false;
	}

	public static implicit operator BucketsPath(Dictionary<string, string> dictionary) => BucketsPath.Dictionary(dictionary);
}
