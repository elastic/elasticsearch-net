// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
public sealed class NodeIds :
	IEquatable<NodeIds>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<NodeIds>
#endif
{
	public NodeIds()
	{
		Values = [];
	}

	public NodeIds(IEnumerable<string> nodeIds)
	{
		Values = nodeIds?.ToList();
		if (!Values.HasAny())
			throw new ArgumentException($"Can not create {nameof(NodeIds)} on an empty enumerable of ", nameof(nodeIds));
	}

	internal IList<string> Values { get; }

	private string DebugDisplay => ((IUrlParameter)this).GetString(null);

	public override string ToString() => DebugDisplay;

	public bool Equals(NodeIds other) => EqualsAllIds(Values, other.Values);

	string IUrlParameter.GetString(ITransportConfiguration? settings) => string.Join(",", Values);

	public static NodeIds Parse(string nodeIds) => nodeIds.IsNullOrEmptyCommaSeparatedList(out var nodes) ? null : new NodeIds(nodes);

	public static implicit operator NodeIds(string nodes) => Parse(nodes);

	public static implicit operator NodeIds(string[] nodes) => nodes.IsNullOrEmpty() ? null : new NodeIds(nodes);

	public static bool operator ==(NodeIds left, NodeIds right) => Equals(left, right);

	public static bool operator !=(NodeIds left, NodeIds right) => !Equals(left, right);

	private static bool EqualsAllIds(ICollection<string> thisIds, ICollection<string> otherIds)
	{
		if (thisIds == null && otherIds == null)
			return true;
		if (thisIds == null || otherIds == null)
			return false;
		if (thisIds.Count != otherIds.Count)
			return false;

		return thisIds.Count == otherIds.Count && !thisIds.Except(otherIds).Any();
	}

	public override bool Equals(object obj) => obj is string s ? Equals(Parse(s)) : obj is NodeIds i && Equals(i);

	public override int GetHashCode() => Values.GetHashCode();

	#region IParsable

	public static NodeIds Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out NodeIds? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new NodeIds();
			return true;
		}

		result = new NodeIds(list);
		return true;
	}

	#endregion IParsable
}
