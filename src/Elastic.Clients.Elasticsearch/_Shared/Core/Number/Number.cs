// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(Json.NumberConverter))]
public readonly struct Number
{
	private readonly byte _tag;
	private readonly long _data;

	public Number(long value)
	{
		_tag = 1;
		_data = value;
	}

	public Number(double value)
	{
		_tag = 2;
		_data = Unsafe.As<double, long>(ref value);
	}

	public static implicit operator Number(long value) => new(value);

	public static implicit operator Number(double value) => new(value);

	public bool TryGetLong(out long value)
	{
		if (_tag is not 1)
		{
			value = 0L;
			return false;
		}

		value = _data;
		return true;
	}

	public bool TryGetDouble(out double value)
	{
		if (_tag is not 2)
		{
			value = 0.0d;
			return false;
		}

		value = Unsafe.As<long, double>(ref Unsafe.AsRef(in _data));
		return true;
	}

	internal string DebugDisplay => _tag switch
	{
		1 => TryGetLong(out var l) ? l.ToString(CultureInfo.InvariantCulture) : string.Empty,
		2 => TryGetDouble(out var d) ? d.ToString(CultureInfo.InvariantCulture) : string.Empty,
		_ => "<empty>"
	};
}
