// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using Elastic.Transport;

namespace Nest
{
	public class Timestamp : IUrlParameter, IEquatable<Timestamp>
	{
		internal readonly long Value;

		public Timestamp(long value) => Value = value;

		public bool Equals(Timestamp other) => Value == other.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(ITransportConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public static implicit operator Timestamp(DateTimeOffset categoryId) => new Timestamp(categoryId.ToUnixTimeMilliseconds());

		public static implicit operator Timestamp(long categoryId) => new Timestamp(categoryId);

		public static implicit operator long(Timestamp categoryId) => categoryId.Value;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case int l: return Value == l;
				case long l: return Value == l;
				case Timestamp i: return Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(Timestamp left, Timestamp right) => Equals(left, right);

		public static bool operator !=(Timestamp left, Timestamp right) => !Equals(left, right);
	}
}
