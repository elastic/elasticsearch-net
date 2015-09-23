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

		public string Index => GetResolved("index");
		public string Type => GetResolved("type");
		public string Id => GetResolved("id");
		public string Name => GetResolved("name");
		public string Field => GetResolved("field");
		public string ScrollId => GetResolved("scroll_id");
		public string NodeId => GetResolved("node_id");
		public string Fields => GetResolved("fields");
		public string Repository => GetResolved("repository");
		public string Snapshot => GetResolved("snapshot");
		public string Feature => GetResolved("feature");
		public string Metric => GetResolved("metric");
		public string IndexMetric => GetResolved("index_metric");
		public string Lang => GetResolved("lang");

		private string GetResolved(string route)
		{
			string resolved;
			if (this._resolved.TryGetValue(route, out resolved)) return resolved;
			return null;
		}

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

		internal TActual Get<TActual>(string route) where TActual : class, IUrlParameter
		{
			IUrlParameter actual;
			if (this._routeValues.TryGetValue(route, out actual) && actual != null)
				return (TActual)actual;
			return null;
		}

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