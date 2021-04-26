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
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(RelationNameFormatter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class RelationName : IEquatable<RelationName>, IUrlParameter
	{
		private RelationName(string type) => Name = type;

		private RelationName(Type type) => Type = type;

		public string Name { get; }
		public Type Type { get; }

		internal string DebugDisplay => Type == null ? Name : $"{nameof(RelationName)} for typeof: {Type?.Name}";

		private static int TypeHashCode { get; } = typeof(RelationName).GetHashCode();

		public bool Equals(RelationName other) => EqualsMarker(other);

		string IUrlParameter.GetString(ITransportConfiguration settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new ArgumentNullException(nameof(settings),
					$"Can not resolve {nameof(RelationName)} if no {nameof(IConnectionSettingsValues)} is provided");

			return nestSettings.Inferrer.RelationName(this);
		}

		public static RelationName From<T>() => typeof(T);

		public static RelationName Create(Type type) => GetRelationNameForType(type);

		public static RelationName Create<T>() where T : class => GetRelationNameForType(typeof(T));

		private static RelationName GetRelationNameForType(Type type) => new RelationName(type);

		public static implicit operator RelationName(string typeName) => typeName.IsNullOrEmpty() ? null : new RelationName(typeName);

		public static implicit operator RelationName(Type type) => type == null ? null : new RelationName(type);

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (Name?.GetHashCode() ?? Type?.GetHashCode() ?? 0);
				return result;
			}
		}

		public static bool operator ==(RelationName left, RelationName right) => Equals(left, right);

		public static bool operator !=(RelationName left, RelationName right) => !Equals(left, right);

		public override bool Equals(object obj) =>
			obj is string s ? EqualsString(s) : obj is RelationName r && EqualsMarker(r);

		public bool EqualsMarker(RelationName other)
		{
			if (!Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (Type != null && other?.Type != null)
				return Type == other.Type;

			return false;
		}

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == Name;

		public override string ToString() => DebugDisplay;

	}
}
