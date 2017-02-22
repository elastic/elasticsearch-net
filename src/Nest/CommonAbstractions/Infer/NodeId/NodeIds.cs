using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class NodeIds : IUrlParameter
	{
		private readonly IEnumerable<string> _nodeIds;

		public NodeIds(IEnumerable<string> nodeIds) { this._nodeIds = nodeIds; }

		private string DebugDisplay => GetString(null);

		//TODO to explicit private implementation
		public string GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", this._nodeIds);

		public static NodeIds Parse(string nodeIds)
		{
			//TODO trim()?
			if (nodeIds.IsNullOrEmpty()) throw new ArgumentException("can not create NodeIds on an empty enumerable of ", nameof(nodeIds));
			var nodes = nodeIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return new NodeIds(nodes);
		}

		public static implicit operator NodeIds(string nodes) => Parse(nodes);
	}
}
