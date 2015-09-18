using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public class RouteValues
	{
		private Dictionary<string, IUrlParameter> _routeValues = new Dictionary<string, IUrlParameter>();
		private Dictionary<string, string> _resolved = new Dictionary<string, string>();

		public string Index => _resolved["index"];
		public string Type => _resolved["type"];
		public string Id => _resolved["id"];
		public string Name => _resolved["name"];
		public string Field=> _resolved["field"];
		public string ScrollId => _resolved["scroll_id"];
		public string NodeId => _resolved["node_id"];
		public string Fields => _resolved["fields"];
		public string Repository => _resolved["repostitory"];
		public string Snapshot => _resolved["snapshot"];
		public string Feature => _resolved["feature"];
		public string Metric => _resolved["metric"];
		public string IndexMetric => _resolved["index_metric"];
		public string Lang => _resolved["lang"];

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			this._routeValues.Add(name, routeValue);
			return this;
		}
		private RouteValues Resolved(string name, string routeValue, bool required = true)
		{
			this._resolved.Add(name, routeValue);
			return this;
		}
		
		public void Resolve(IConnectionSettingsValues settings)
		{
			foreach(var kv in _routeValues)
			{
				this._resolved[kv.Key] = kv.Value.GetString(settings);
			}
		}

		public RouteValues Required(Indices indices) => Route("index", indices);
        public RouteValues Optional(Indices indices) => Route("index", indices, false);

		public RouteValues Required(Types types) => Route("type", types);
		public RouteValues Optional(Types types) => Route("type", types, false);

		[Obsolete("TODO: Rename to Required once NodeId type is implemented")]
		public RouteValues RequiredNodeId(string nodeId) => Resolved("node_id", nodeId);

		[Obsolete("TODO: Rename to Optional once NodeId type is implemented")]
		public RouteValues OptionalNodeId(string nodeId) => Resolved("node_id", nodeId, false);

		[Obsolete]
		public RouteValues RequiredId(string id) => Resolved("id", id);

		[Obsolete]
		public RouteValues OptionalId(string id) => Resolved("id", id, false);
	}
}