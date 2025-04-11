// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(FieldValuesConverter))]
public readonly struct FieldValues :
	IEquatable<FieldValues>,
	IEnumerable<FieldValue>
{
	public static readonly FieldValues Empty = new();

	public IReadOnlyCollection<FieldValue> Values { get; }

	public FieldValues()
	{
		Values = [];
	}

	public FieldValues(IEnumerable<FieldValue> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values];
	}

	public FieldValues(IEnumerable<object?> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values.Select(FieldValue.FromValue)];
	}

	public FieldValues(IEnumerable<string> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values.Select(x => x)];
	}

	public FieldValues(IEnumerable<bool> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values.Select(x => x)];
	}

	public FieldValues(IEnumerable<int> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values.Select(x => x)];
	}

	public FieldValues(IEnumerable<long> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values.Select(x => x)];
	}

	public FieldValues(IEnumerable<double> values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		Values = [.. values.Select(x => x)];
	}

	public static implicit operator FieldValues(FieldValue[] values) => new(values);

	public static implicit operator FieldValues(object?[] values) => new(values);

	public static implicit operator FieldValues(string[] values) => new(values);

	public static implicit operator FieldValues(bool[] values) => new(values);

	public static implicit operator FieldValues(int[] values) => new(values);

	public static implicit operator FieldValues(long[] values) => new(values);

	public static implicit operator FieldValues(double[] values) => new(values);

	public static bool operator ==(FieldValues left, FieldValues right) => left.Equals(right);

	public static bool operator !=(FieldValues left, FieldValues right) => !(left == right);

	public override bool Equals(object? obj) => (obj is FieldValues other) && Equals(other);

	public override int GetHashCode() => Values.GetHashCode();

	public bool Equals(FieldValues other)
	{
		if (ReferenceEquals(Values, other.Values))
		{
			return true;
		}

		return Values.SequenceEqual(other.Values);
	}

	public IEnumerator<FieldValue> GetEnumerator() => Values.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
