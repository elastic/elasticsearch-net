using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public class RouteValues
	{
		private readonly Dictionary<string, IUrlParameter> _routeValues = new Dictionary<string, IUrlParameter>();
		private readonly Dictionary<string, string> _resolved = new Dictionary<string, string>();

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
		public string Target => GetResolved("target");
		public string NewIndex => GetResolved("new_index");
		public string Alias => GetResolved("alias");
		public string WatchId => GetResolved("watch_id");
		public string ThreadPoolPatterns => GetResolved("thread_pool_patterns");
		public string ActionId => GetResolved("action_id");
		public string JobId => GetResolved("job_id");
		public string DatafeedId => GetResolved("datafeed_id");
		public string FilterId => GetResolved("filter_id");
		public string SnapshotId => GetResolved("snapshot_id");
		public string CategoryId => GetResolved("category_id");
		public string Timestamp => GetResolved("timestamp");
		public string Context => GetResolved("context");
		public WatcherStatsMetric? WatcherStatsMetric => GetResolved("watcher_stats_metric").ToEnum<WatcherStatsMetric>();

		private string GetResolved(string route) => this._resolved.TryGetValue(route, out var resolved) ? resolved : null;

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			if (routeValue == null && !required)
			{
				if (this._routeValues.ContainsKey(name))
					this._routeValues.Remove(name);
				return this;
			}
			if (routeValue == null) return this;

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
