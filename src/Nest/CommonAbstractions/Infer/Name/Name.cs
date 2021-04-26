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
using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Name : IEquatable<Name>, IUrlParameter
	{
		public Name(string name) => Value = name?.Trim();

		internal string Value { get; }

		private string DebugDisplay => Value;

		public override string ToString() => DebugDisplay;

		private static int TypeHashCode { get; } = typeof(Name).GetHashCode();

		public bool Equals(Name other) => EqualsString(other?.Value);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => Value;

		public static implicit operator Name(string name) => name.IsNullOrEmpty() ? null : new Name(name);

		public static bool operator ==(Name left, Name right) => Equals(left, right);

		public static bool operator !=(Name left, Name right) => !Equals(left, right);

		public override bool Equals(object obj) =>
			obj is string s ? EqualsString(s) : obj is Name i && EqualsString(i.Value);

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Trim() == Value;

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (Value?.GetHashCode() ?? 0);
				return result;
			}
		}
	}
}
