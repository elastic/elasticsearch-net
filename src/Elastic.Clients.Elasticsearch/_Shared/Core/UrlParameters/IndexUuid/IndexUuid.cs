// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public sealed class IndexUuid : IUrlParameter, IEquatable<IndexUuid>
{
	public IndexUuid(string value) => Value = value ?? throw new ArgumentNullException(nameof(value));

	public string Value { get; }

	public bool Equals(IndexUuid other)
	{
		if (other is not null)
		{
			if (ReferenceEquals(this, other))
				return true;

			return Value == other.Value;
		}

		return false;
	}

	string IUrlParameter.GetString(ITransportConfiguration settings) => Value;

	public override string ToString() => Value;

	public override bool Equals(object obj)
	{
		if (obj is null)
			return false;

		if (ReferenceEquals(this, obj))
			return true;

		if (obj.GetType() != GetType())
			return false;

		return Equals((IndexUuid)obj);
	}

	public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

	public static bool operator ==(IndexUuid left, IndexUuid right) => Equals(left, right);

	public static bool operator !=(IndexUuid left, IndexUuid right) => !Equals(left, right);

	public static implicit operator IndexUuid(string value) =>
		string.IsNullOrEmpty(value) ? null : new IndexUuid(value);
}
