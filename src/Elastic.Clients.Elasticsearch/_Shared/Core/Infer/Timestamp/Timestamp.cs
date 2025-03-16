// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public sealed class Timestamp : IUrlParameter, IEquatable<Timestamp>
{
	internal readonly long Value;

	public Timestamp(long value) => Value = value;

	public bool Equals(Timestamp other) => Value == other.Value;

	string IUrlParameter.GetString(ITransportConfiguration settings) => GetString();

	public override string ToString() => GetString();

	private string GetString() => Value.ToString(CultureInfo.InvariantCulture);

	public static implicit operator Timestamp(DateTimeOffset categoryId) => new(categoryId.ToUnixTimeMilliseconds());

	public static implicit operator Timestamp(long categoryId) => new(categoryId);

	public static implicit operator long(Timestamp categoryId) => categoryId.Value;

	public override bool Equals(object obj) => obj switch
	{
		int l => Value == l,
		long l => Value == l,
		Timestamp i => Value == i.Value,
		_ => false,
	};

	public override int GetHashCode() => Value.GetHashCode();

	public static bool operator ==(Timestamp left, Timestamp right) => Equals(left, right);

	public static bool operator !=(Timestamp left, Timestamp right) => !Equals(left, right);
}
