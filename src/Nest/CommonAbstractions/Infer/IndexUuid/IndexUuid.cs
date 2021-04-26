/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexUuid : IUrlParameter, IEquatable<IndexUuid>
	{
		public string Value { get; }

		public IndexUuid(string value) => Value = value ?? throw new ArgumentNullException(nameof(value));

		public string GetString(IConnectionConfigurationValues settings) => Value;

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
