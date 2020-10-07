// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		string IUrlParameter.GetString(ITransportConfigurationValues settings) => string.Join(",", Value);

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
