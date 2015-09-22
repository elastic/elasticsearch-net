using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	//TODO INTERNAL ?
	public class RouteValues
	{
		private Dictionary<string, IUrlParameter> _routeValues = new Dictionary<string, IUrlParameter>();
		private Dictionary<string, string> _resolved = new Dictionary<string, string>();

		public string Index => _resolved["index"];
		public string Type => _resolved["type"];
		public string Id => _resolved["id"];
		public string Name => _resolved["name"];
		public string Field => _resolved["field"];
		public string ScrollId => _resolved["scroll_id"];
		public string NodeId => _resolved["node_id"];
		public string Fields => _resolved["fields"];
		public string Repository => _resolved["repository"];
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
			foreach (var kv in _routeValues)
			{
				this._resolved[kv.Key] = kv.Value.GetString(settings);
			}
		}

		internal RouteValues Required(string route, IUrlParameter value) => Route(route, value);
		internal RouteValues Optional(string route, IUrlParameter value) => Route(route, value, false);

		internal TActual Get<TActual>(string route) where TActual : class, IUrlParameter => this._routeValues[route] as TActual;

		public RouteValues Required(Indices indices) => Route("index", indices);
		public RouteValues Optional(Indices indices) => Route("index", indices, false);

		public RouteValues Required(Types types) => Route("type", types);
		public RouteValues Optional(Types types) => Route("type", types, false);

		public RouteValues Required(Ids ids) => Route("id", ids);
		public RouteValues Optional(Ids ids) => Route("id", ids, false);
		public void Remove(string route)
		{
			this._resolved.Remove(route);
			this._routeValues.Remove(route);
		}

		public RouteValues Required(string route, IEnumerable<Enum> enums) =>
			Resolved(route, string.Join(",", enums.Select(e => e.GetStringValue())));
		public RouteValues Optional(string route, IEnumerable<Enum> enums) =>
			Resolved(route, string.Join(",", enums.Select(e => e.GetStringValue())), false);

		public RouteValues Required(IEnumerable<ClusterStateMetric> enums) => Required("metric", enums.Cast<Enum>());
		public RouteValues Optional(IEnumerable<ClusterStateMetric> enums) => Optional("metric", enums.Cast<Enum>());


		[Obsolete("TODO: Rename to Required once NodeId type is implemented")]
		public RouteValues RequiredNodeId(string nodeId) => Resolved("node_id", nodeId);

		[Obsolete("TODO: Rename to Optional once NodeId type is implemented")]
		public RouteValues OptionalNodeId(string nodeId) => Resolved("node_id", nodeId, false);
	}
}