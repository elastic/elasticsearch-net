using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	//[DebuggerDisplay("{" + nameof(DebugDisplay) + ",nq}")]
	//public class NodeIds : IEquatable<NodeIds>, IUrlParameter
	//{
	//	public NodeIds(IEnumerable<string> nodeIds)
	//	{
	//		Value = nodeIds?.ToList();
	//		if (!Value.HasAny())
	//			throw new ArgumentException($"can not create {nameof(NodeIds)} on an empty enumerable of ",
	//				nameof(nodeIds));
	//	}

	//	internal IList<string> Value { get; }

	//	private string DebugDisplay => ((IUrlParameter)this).GetString(null);

	//	public bool Equals(NodeIds other) => EqualsAllIds(Value, other.Value);

	//	string IUrlParameter.GetString(ITransportConfiguration settings) => string.Join(",", Value);

	//	public override string ToString() => DebugDisplay;

	//	public static NodeIds Parse(string nodeIds) =>
	//		nodeIds.IsNullOrEmptyCommaSeparatedList(out var nodes) ? null : new NodeIds(nodes);

	//	public static implicit operator NodeIds(string nodes) => Parse(nodes);

	//	public static implicit operator NodeIds(string[] nodes) => nodes.IsEmpty() ? null : new NodeIds(nodes);

	//	public static bool operator ==(NodeIds left, NodeIds right) => Equals(left, right);

	//	public static bool operator !=(NodeIds left, NodeIds right) => !Equals(left, right);

	//	private static bool EqualsAllIds(ICollection<string> thisIds, ICollection<string> otherIds)
	//	{
	//		if (thisIds == null && otherIds == null)
	//			return true;
	//		if (thisIds == null || otherIds == null)
	//			return false;
	//		if (thisIds.Count != otherIds.Count)
	//			return false;

	//		return thisIds.Count == otherIds.Count && !thisIds.Except(otherIds).Any();
	//	}

	//	public override bool Equals(object obj) => obj is string s ? Equals(Parse(s)) : obj is NodeIds i && Equals(i);

	//	public override int GetHashCode() => Value.GetHashCode();
	//}
}
