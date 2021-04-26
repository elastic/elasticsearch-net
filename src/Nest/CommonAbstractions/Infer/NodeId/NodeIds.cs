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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Transport;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class NodeIds : IEquatable<NodeIds>, IUrlParameter
	{
		public NodeIds(IEnumerable<string> nodeIds)
		{
			Value = nodeIds?.ToList();
			if (!Value.HasAny())
				throw new ArgumentException($"can not create {nameof(NodeIds)} on an empty enumerable of ", nameof(nodeIds));
		}

		internal IList<string> Value { get; }

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public override string ToString() => DebugDisplay;

		public bool Equals(NodeIds other) => EqualsAllIds(Value, other.Value);

		string IUrlParameter.GetString(ITransportConfiguration settings) => string.Join(",", Value);

		public static NodeIds Parse(string nodeIds) => nodeIds.IsNullOrEmptyCommaSeparatedList(out var nodes) ? null : new NodeIds(nodes);

		public static implicit operator NodeIds(string nodes) => Parse(nodes);

		public static implicit operator NodeIds(string[] nodes) => nodes.IsEmpty() ? null : new NodeIds(nodes);

		public static bool operator ==(NodeIds left, NodeIds right) => Equals(left, right);

		public static bool operator !=(NodeIds left, NodeIds right) => !Equals(left, right);

		private static bool EqualsAllIds(ICollection<string> thisIds, ICollection<string> otherIds)
		{
			if (thisIds == null && otherIds == null) return true;
			if (thisIds == null || otherIds == null) return false;
			if (thisIds.Count != otherIds.Count) return false;

			return thisIds.Count == otherIds.Count && !thisIds.Except(otherIds).Any();
		}

		public override bool Equals(object obj) => obj is string s ? Equals(Parse(s)) : obj is NodeIds i && Equals(i);

		public override int GetHashCode() => Value.GetHashCode();
	}
}
