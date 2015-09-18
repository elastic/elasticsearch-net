using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public interface IRequestPath
	{
		string Index { get; }
		string Type { get; }
		string Id { get; }
		string Name { get; }
		string Field { get; }
		string ScrollId { get; }
		string NodeId { get; }
		string Fields { get; }
		string Repository { get; }
		string Snapshot { get; }
		string Metric { get; }
		string IndexMetric { get; }
        void ResolveRouteValues(IConnectionSettingsValues settings);
	}
	public class RequestPath: IRequestPath
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

		private RequestPath Route(string name, IUrlParameter routeValue, bool required = true)
		{
			this._routeValues.Add(name, routeValue);
			return this;
		}
		private RequestPath Resolved(string name, string routeValue, bool required = true)
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

		public RequestPath Required(Indices indices) => Route("index", indices);
        public RequestPath Optional(Indices indices) => Route("index", indices, false);

		public RequestPath Required(Types types) => Route("type", types);
		public RequestPath Optional(Types types) => Route("type", types, false);

		[Obsolete("TODO: Rename to Required once NodeId type is implemented")]
		public RequestPath RequiredNodeId(string nodeId) => Resolved("node_id", nodeId);

		[Obsolete("TODO: Rename to Optional once NodeId type is implemented")]
		public RequestPath OptionalNodeId(string nodeId) => Resolved("node_id", nodeId, false);

		[Obsolete]
		public RequestPath RequiredId(string id) => Resolved("id", id);

		[Obsolete]
		public RequestPath OptionalId(string id) => Resolved("id", id, false);
	}
}