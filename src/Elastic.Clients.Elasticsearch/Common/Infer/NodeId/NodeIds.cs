// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public partial class NodeIds
	{
		// This is temporary
		public NodeIds(IEnumerable<NodeId> nodeIds) => _nodeIdList.AddRange(nodeIds);

		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}


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
