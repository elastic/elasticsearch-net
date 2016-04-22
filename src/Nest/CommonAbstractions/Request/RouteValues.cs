using System;
using System.Collections.Generic;
using Elasticsearch.Net;

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
		public string TaskId => GetResolved("task_id");
		public string Realms => GetResolved("realms");
		public string Username => GetResolved("username");

		private string GetResolved(string route)
		{
			string resolved;
			if (this._resolved.TryGetValue(route, out resolved)) return resolved;
			return null;
		}

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			if (routeValue == null && !required)
			{
				if (this._routeValues.ContainsKey(name))
					this._routeValues.Remove(name);
				return this;
			}
			else if (routeValue == null) return this;

			this._routeValues[name] = routeValue;
			return this;
		}
		public void Resolve(IConnectionSettingsValues settings)
		{
			foreach (var kv in _routeValues)
			{
				var key = kv.Value.GetString(settings);
				this._resolved[kv.Key] = key.IsNullOrEmpty() ? key : key;
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

		public void Remove(string route)
		{
			this._resolved.Remove(route);
			this._routeValues.Remove(route);
		}
	}
}
