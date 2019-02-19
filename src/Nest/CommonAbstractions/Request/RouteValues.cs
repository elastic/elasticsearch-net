using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public class RouteValues
	{
		private readonly Dictionary<string, string> _resolved = new Dictionary<string, string>();
		private readonly Dictionary<string, IUrlParameter> _routeValues = new Dictionary<string, IUrlParameter>();
		public string ActionId => GetResolved("action_id");
		public string Alias => GetResolved("alias");
		public string CategoryId => GetResolved("category_id");
		public string Context => GetResolved("context");
		public string DatafeedId => GetResolved("datafeed_id");
		public string Feature => GetResolved("feature");
		public string Field => GetResolved("field");
		public string Fields => GetResolved("fields");
		public string FilterId => GetResolved("filter_id");
		public string Id => GetResolved("id");

		public string Index => GetResolved("index");
		public string IndexMetric => GetResolved("index_metric");
		public string JobId => GetResolved("job_id");
		public string Lang => GetResolved("lang");
		public string Metric => GetResolved("metric");
		public string Name => GetResolved("name");
		public string NewIndex => GetResolved("new_index");
		public string NodeId => GetResolved("node_id");
		public string Realms => GetResolved("realms");
		public string Repository => GetResolved("repository");
		public string ScrollId => GetResolved("scroll_id");
		public string Snapshot => GetResolved("snapshot");
		public string SnapshotId => GetResolved("snapshot_id");
		public string Target => GetResolved("target");
		public string TaskId => GetResolved("task_id");
		public string ThreadPoolPatterns => GetResolved("thread_pool_patterns");
		public string Timestamp => GetResolved("timestamp");
		public string Type => GetResolved("type");
		public string Username => GetResolved("username");
		public WatcherStatsMetric? WatcherStatsMetric => GetResolved("watcher_stats_metric").ToEnum<WatcherStatsMetric>();
		public string WatchId => GetResolved("watch_id");

		private string GetResolved(string route) => _resolved.TryGetValue(route, out var resolved) ? resolved : null;

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			if (routeValue == null && !required)
			{
				if (_routeValues.ContainsKey(name))
					_routeValues.Remove(name);
				return this;
			}
			if (routeValue == null) return this;

			_routeValues[name] = routeValue;
			return this;
		}

		public void Resolve(IConnectionSettingsValues settings)
		{
			foreach (var kv in _routeValues)
			{
				var key = kv.Value.GetString(settings);
				_resolved[kv.Key] = key.IsNullOrEmpty() ? key : key;
			}
		}

		internal RouteValues Required(string route, IUrlParameter value) => Route(route, value);

		internal RouteValues Optional(string route, IUrlParameter value) => Route(route, value, false);

		internal RouteValues Optional(string route, Metrics value) => Route(route, value, false);

		internal RouteValues Optional(string route, IndexMetrics value) => Route(route, value, false);

		internal TActual Get<TActual>(string route) where TActual : class, IUrlParameter
		{
			IUrlParameter actual;
			if (_routeValues.TryGetValue(route, out actual) && actual != null)
				return (TActual)actual;

			return null;
		}

		public void Remove(string route)
		{
			_resolved.Remove(route);
			_routeValues.Remove(route);
		}
	}
}
