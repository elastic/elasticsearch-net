using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public class NodeIds : IUrlParameter
	{
		private readonly IEnumerable<string> _nodeIds;

		public NodeIds(IEnumerable<string> nodeIds) { this._nodeIds = nodeIds; }

		public string GetString(IConnectionConfigurationValues settings) => 
			string.Join(",", this._nodeIds);

		public static NodeIds Parse(string nodeIds)
		{
			if (nodeIds.IsNullOrEmpty()) throw new ArgumentException("can not create NodeIds on an empty enumerable of ", nameof(nodeIds));
			var nodes = nodeIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return new NodeIds(nodes);
		}

		public static implicit operator NodeIds(string nodes) => Parse(nodes);
	}
}
