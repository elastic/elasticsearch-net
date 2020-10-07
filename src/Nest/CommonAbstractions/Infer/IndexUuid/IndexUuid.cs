// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;

namespace Nest
{
	public class IndexUuid : IUrlParameter, IEquatable<IndexUuid>
	{
		public string Value { get; }

		public IndexUuid(string value) => Value = value ?? throw new ArgumentNullException(nameof(value));

		public string GetString(ITransportConfigurationValues settings) => Value;

		public bool Equals(IndexUuid other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((IndexUuid)obj);
		}

		public override int GetHashCode() => (Value != null ? Value.GetHashCode() : 0);

		public static bool operator ==(IndexUuid left, IndexUuid right) => Equals(left, right);

		public static bool operator !=(IndexUuid left, IndexUuid right) => !Equals(left, right);

		public static implicit operator IndexUuid(string value) => string.IsNullOrEmpty(value) ? null : new IndexUuid(value);
	}
}
