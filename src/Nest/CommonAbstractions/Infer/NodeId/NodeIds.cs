using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class NodeIds : IEquatable<NodeIds>, IUrlParameter
	{
		private readonly IList<string> _nodeIds;
		internal IList<string> Value => _nodeIds;

		public NodeIds(IEnumerable<string> nodeIds)
		{
			this._nodeIds = nodeIds?.ToList();
			if (!this._nodeIds.HasAny())
				throw new ArgumentException($"can not create {nameof(NodeIds )} on an empty enumerable of ", nameof(nodeIds));
		}

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => string.Join(",", this._nodeIds);

		public static NodeIds Parse(string nodeIds) => nodeIds.IsNullOrEmptyCommaSeparatedList(out var nodes) ? null : new NodeIds(nodes);

		public static implicit operator NodeIds(string nodes) => Parse(nodes);
		public static implicit operator NodeIds(string[] nodes) => nodes.IsEmpty() ? null : new NodeIds(nodes);

		public static bool operator ==(NodeIds left, NodeIds right) => Equals(left, right);

		public static bool operator !=(NodeIds left, NodeIds right) => !Equals(left, right);

		public bool Equals(NodeIds other) => EqualsAllIds(this.Value, other.Value);

		private static bool EqualsAllIds(ICollection<string> thisIds, ICollection<string> otherIds)
		{
			if (thisIds == null && otherIds == null) return true;
			if (thisIds == null || otherIds == null) return false;
			if (thisIds.Count != otherIds.Count) return false;
			return thisIds.Count == otherIds.Count && !thisIds.Except(otherIds).Any();
		}

		public override bool Equals(object obj) => obj is string s ? this.Equals(Parse(s)) : (obj is NodeIds i) && this.Equals(i);

		public override int GetHashCode() => this._nodeIds.GetHashCode();
	}
}
