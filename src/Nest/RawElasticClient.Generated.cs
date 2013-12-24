using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Generated File Please Do Not Edit Manually
	
namespace Nest
{
	///<summary>
	///Raw operations with elasticsearch
	///</summary>
	public partial class RawElasticClient : IRawElasticClient
	{
	
		
		///<summary>POST /_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPost(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			var url = "/_bulk".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPostAsync(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			var url = "/_bulk".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPost(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_bulk".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPostAsync(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_bulk".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPost(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPostAsync(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPut(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			var url = "/_bulk".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPutAsync(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			var url = "/_bulk".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPut(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_bulk".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPutAsync(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_bulk".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPut(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_bulk
	    ///<pre>http://elasticsearch.org/guide/reference/api/bulk/</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPutAsync(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>DELETE /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="scroll_id">A comma-separated list of scroll IDs to clear</param>
		public ConnectionStatus ClearScrollDelete(string scroll_id, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			scroll_id.ThrowIfNull("scroll_id");
			var url = "/_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="scroll_id">A comma-separated list of scroll IDs to clear</param>
		public Task<ConnectionStatus> ClearScrollDeleteAsync(string scroll_id, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			scroll_id.ThrowIfNull("scroll_id");
			var url = "/_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/settings
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-update-settings/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterGetSettings(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_cluster/settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/settings
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-update-settings/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterGetSettingsAsync(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_cluster/settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-health/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterHealthGet(Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			var url = "/_cluster/health";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-health/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterHealthGetAsync(Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			var url = "/_cluster/health";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health/{index}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-health/</pre>	
	    ///</summary>
		///<param name="index">Limit the information returned to a specific index</param>
		public ConnectionStatus ClusterHealthGet(string index, Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/_cluster/health/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health/{index}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-health/</pre>	
	    ///</summary>
		///<param name="index">Limit the information returned to a specific index</param>
		public Task<ConnectionStatus> ClusterHealthGetAsync(string index, Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/_cluster/health/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-cluster-nodes-hot-threads/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterNodeHotThreadsGet(Func<ClusterNodeHotThreadsQueryString, ClusterNodeHotThreadsQueryString> queryString = null)
		{
			var url = "/_cluster/nodes/hotthreads";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeHotThreadsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-cluster-nodes-hot-threads/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterNodeHotThreadsGetAsync(Func<ClusterNodeHotThreadsQueryString, ClusterNodeHotThreadsQueryString> queryString = null)
		{
			var url = "/_cluster/nodes/hotthreads";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeHotThreadsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-cluster-nodes-hot-threads/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus ClusterNodeHotThreadsGet(string node_id, Func<ClusterNodeHotThreadsQueryString, ClusterNodeHotThreadsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}/hotthreads".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeHotThreadsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-cluster-nodes-hot-threads/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> ClusterNodeHotThreadsGetAsync(string node_id, Func<ClusterNodeHotThreadsQueryString, ClusterNodeHotThreadsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}/hotthreads".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeHotThreadsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-info/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterNodeInfoGet(Func<ClusterNodeInfoQueryString, ClusterNodeInfoQueryString> queryString = null)
		{
			var url = "/_cluster/nodes";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeInfoQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-info/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterNodeInfoGetAsync(Func<ClusterNodeInfoQueryString, ClusterNodeInfoQueryString> queryString = null)
		{
			var url = "/_cluster/nodes";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeInfoQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-info/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus ClusterNodeInfoGet(string node_id, Func<ClusterNodeInfoQueryString, ClusterNodeInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeInfoQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-info/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> ClusterNodeInfoGetAsync(string node_id, Func<ClusterNodeInfoQueryString, ClusterNodeInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeInfoQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_shutdown
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-shutdown/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterNodeShutdownPost(Func<ClusterNodeShutdownQueryString, ClusterNodeShutdownQueryString> queryString = null)
		{
			var url = "/_shutdown";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeShutdownQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_shutdown
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-shutdown/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterNodeShutdownPostAsync(Func<ClusterNodeShutdownQueryString, ClusterNodeShutdownQueryString> queryString = null)
		{
			var url = "/_shutdown";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeShutdownQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_cluster/nodes/{node_id}/_shutdown
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-shutdown/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to perform the operation on; use `_local` to perform the operation on the node you&#39;re connected to, leave empty to perform the operation on all nodes</param>
		public ConnectionStatus ClusterNodeShutdownPost(string node_id, Func<ClusterNodeShutdownQueryString, ClusterNodeShutdownQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}/_shutdown".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeShutdownQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_cluster/nodes/{node_id}/_shutdown
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-shutdown/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to perform the operation on; use `_local` to perform the operation on the node you&#39;re connected to, leave empty to perform the operation on all nodes</param>
		public Task<ConnectionStatus> ClusterNodeShutdownPostAsync(string node_id, Func<ClusterNodeShutdownQueryString, ClusterNodeShutdownQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}/_shutdown".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeShutdownQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterNodeStatsGet(Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			var url = "/_cluster/nodes/stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterNodeStatsGetAsync(Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			var url = "/_cluster/nodes/stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}/stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus ClusterNodeStatsGet(string node_id, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}/stats".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}/stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> ClusterNodeStatsGetAsync(string node_id, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			var url = "/_cluster/nodes/{node_id}/stats".Inject(new { node_id = Stringify(node_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="metric_family">Limit the information returned to a certain metric family</param>
		public ConnectionStatus ClusterNodeStatsGet(MetricFamilyOptions metric_family, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			metric_family.ThrowIfNull("metric_family");
			var url = "/_nodes/stats/{metric_family}".Inject(new { metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="metric_family">Limit the information returned to a certain metric family</param>
		public Task<ConnectionStatus> ClusterNodeStatsGetAsync(MetricFamilyOptions metric_family, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			metric_family.ThrowIfNull("metric_family");
			var url = "/_nodes/stats/{metric_family}".Inject(new { metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric_family">Limit the information returned to a certain metric family</param>
		public ConnectionStatus ClusterNodeStatsGet(string node_id, MetricFamilyOptions metric_family, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			metric_family.ThrowIfNull("metric_family");
			var url = "/_nodes/{node_id}/stats/{metric_family}".Inject(new { node_id = Stringify(node_id), metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric_family">Limit the information returned to a certain metric family</param>
		public Task<ConnectionStatus> ClusterNodeStatsGetAsync(string node_id, MetricFamilyOptions metric_family, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			metric_family.ThrowIfNull("metric_family");
			var url = "/_nodes/{node_id}/stats/{metric_family}".Inject(new { node_id = Stringify(node_id), metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/indices/{metric}/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned for `indices` family to a specific metric</param>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `indices` metric family (supports wildcards)</param>
		public ConnectionStatus ClusterNodeStatsGet(MetricOptions metric, string fields, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			metric.ThrowIfNull("metric");
			fields.ThrowIfNull("fields");
			var url = "/_nodes/stats/indices/{metric}/{fields}".Inject(new { metric = Stringify(metric), fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/indices/{metric}/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned for `indices` family to a specific metric</param>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `indices` metric family (supports wildcards)</param>
		public Task<ConnectionStatus> ClusterNodeStatsGetAsync(MetricOptions metric, string fields, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			metric.ThrowIfNull("metric");
			fields.ThrowIfNull("fields");
			var url = "/_nodes/stats/indices/{metric}/{fields}".Inject(new { metric = Stringify(metric), fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/indices/{metric}/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">Limit the information returned for `indices` family to a specific metric</param>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `indices` metric family (supports wildcards)</param>
		public ConnectionStatus ClusterNodeStatsGet(string node_id, MetricOptions metric, string fields, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			metric.ThrowIfNull("metric");
			fields.ThrowIfNull("fields");
			var url = "/_nodes/{node_id}/stats/indices/{metric}/{fields}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric), fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/indices/{metric}/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">Limit the information returned for `indices` family to a specific metric</param>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `indices` metric family (supports wildcards)</param>
		public Task<ConnectionStatus> ClusterNodeStatsGetAsync(string node_id, MetricOptions metric, string fields, Func<ClusterNodeStatsQueryString, ClusterNodeStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNull("node_id");
			metric.ThrowIfNull("metric");
			fields.ThrowIfNull("fields");
			var url = "/_nodes/{node_id}/stats/indices/{metric}/{fields}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric), fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterNodeStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>PUT /_cluster/settings
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-update-settings/</pre>	
	    ///</summary>
		///<param name="body">The settings to be updated. Can be either `transient` or `persistent` (survives cluster restart).</param>
		public ConnectionStatus ClusterPutSettings(object body, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_cluster/settings".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_cluster/settings
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-update-settings/</pre>	
	    ///</summary>
		///<param name="body">The settings to be updated. Can be either `transient` or `persistent` (survives cluster restart).</param>
		public Task<ConnectionStatus> ClusterPutSettingsAsync(object body, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_cluster/settings".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_cluster/reroute
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-reroute/</pre>	
	    ///</summary>
		///<param name="body">The definition of `commands` to perform (`move`, `cancel`, `allocate`)</param>
		public ConnectionStatus ClusterReroutePost(object body, Func<ClusterRerouteQueryString, ClusterRerouteQueryString> queryString = null)
		{
			var url = "/_cluster/reroute".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterRerouteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_cluster/reroute
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-reroute/</pre>	
	    ///</summary>
		///<param name="body">The definition of `commands` to perform (`move`, `cancel`, `allocate`)</param>
		public Task<ConnectionStatus> ClusterReroutePostAsync(object body, Func<ClusterRerouteQueryString, ClusterRerouteQueryString> queryString = null)
		{
			var url = "/_cluster/reroute".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterRerouteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_cluster/state
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-state/</pre>	
	    ///</summary>
		public ConnectionStatus ClusterStateGet(Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			var url = "/_cluster/state";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/state
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-state/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterStateGetAsync(Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			var url = "/_cluster/state";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="body">A query to restrict the results (optional)</param>
		public ConnectionStatus CountPost(object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			var url = "/_count".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="body">A query to restrict the results (optional)</param>
		public Task<ConnectionStatus> CountPostAsync(object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			var url = "/_count".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="body">A query to restrict the results (optional)</param>
		public ConnectionStatus CountPost(string index, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_count".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="body">A query to restrict the results (optional)</param>
		public Task<ConnectionStatus> CountPostAsync(string index, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_count".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		///<param name="body">A query to restrict the results (optional)</param>
		public ConnectionStatus CountPost(string index, string type, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		///<param name="body">A query to restrict the results (optional)</param>
		public Task<ConnectionStatus> CountPostAsync(string index, string type, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		public ConnectionStatus CountGet(Func<CountQueryString, CountQueryString> queryString = null)
		{
			var url = "/_count";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CountGetAsync(Func<CountQueryString, CountQueryString> queryString = null)
		{
			var url = "/_count";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		public ConnectionStatus CountGet(string index, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_count".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		public Task<ConnectionStatus> CountGetAsync(string index, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_count".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		public ConnectionStatus CountGet(string index, string type, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_count
	    ///<pre>http://elasticsearch.org/guide/reference/api/count/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		public Task<ConnectionStatus> CountGetAsync(string index, string type, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public ConnectionStatus CreatePost(string index, string type, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> CreatePostAsync(string index, string type, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_create
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public ConnectionStatus CreatePost(string index, string type, string id, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_create".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_create
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> CreatePostAsync(string index, string type, string id, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_create".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public ConnectionStatus CreatePut(string index, string type, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> CreatePutAsync(string index, string type, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/{id}/_create
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public ConnectionStatus CreatePut(string index, string type, string id, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_create".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/{id}/_create
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> CreatePutAsync(string index, string type, string id, object body, Func<CreateQueryString, CreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_create".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/delete/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus Delete(string index, string type, string id, Func<DeleteQueryString, DeleteQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/delete/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> DeleteAsync(string index, string type, string id, Func<DeleteQueryString, DeleteQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/delete-by-query/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="body">A query to restrict the operation</param>
		public ConnectionStatus DeleteByQuery(string index, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_query".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/delete-by-query/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="body">A query to restrict the operation</param>
		public Task<ConnectionStatus> DeleteByQueryAsync(string index, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_query".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/delete-by-query/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of types to restrict the operation</param>
		///<param name="body">A query to restrict the operation</param>
		public ConnectionStatus DeleteByQuery(string index, string type, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_query".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/delete-by-query/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of types to restrict the operation</param>
		///<param name="body">A query to restrict the operation</param>
		public Task<ConnectionStatus> DeleteByQueryAsync(string index, string type, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_query".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, body, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus ExistsHead(string index, string type, string id, Func<ExistsQueryString, ExistsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExistsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> ExistsHeadAsync(string index, string type, string id, Func<ExistsQueryString, ExistsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExistsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_explain
	    ///<pre>http://elasticsearch.org/guide/reference/api/explain/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus ExplainGet(string index, string type, string id, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_explain
	    ///<pre>http://elasticsearch.org/guide/reference/api/explain/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> ExplainGetAsync(string index, string type, string id, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_explain
	    ///<pre>http://elasticsearch.org/guide/reference/api/explain/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="body">The query definition using the Query DSL</param>
		public ConnectionStatus ExplainPost(string index, string type, string id, object body, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_explain
	    ///<pre>http://elasticsearch.org/guide/reference/api/explain/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="body">The query definition using the Query DSL</param>
		public Task<ConnectionStatus> ExplainPostAsync(string index, string type, string id, object body, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus Get(string index, string type, string id, Func<GetQueryString, GetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> GetAsync(string index, string type, string id, Func<GetQueryString, GetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_source
	    ///<pre>http://elasticsearch.org/guide/reference/api/get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document; use `_all` to fetch the first document matching the ID across all types</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus GetSource(string index, string type, string id, Func<GetSourceQueryString, GetSourceQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_source".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetSourceQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_source
	    ///<pre>http://elasticsearch.org/guide/reference/api/get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document; use `_all` to fetch the first document matching the ID across all types</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> GetSourceAsync(string index, string type, string id, Func<GetSourceQueryString, GetSourceQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_source".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetSourceQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public ConnectionStatus IndexPost(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexPostAsync(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public ConnectionStatus IndexPost(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexPostAsync(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public ConnectionStatus IndexPut(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexPutAsync(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public ConnectionStatus IndexPut(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/{id}
	    ///<pre>http://elasticsearch.org/guide/reference/api/index_/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexPutAsync(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>GET /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesAnalyzeGet(Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			var url = "/_analyze";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesAnalyzeGetAsync(Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			var url = "/_analyze";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		public ConnectionStatus IndicesAnalyzeGet(string index, Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_analyze".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		public Task<ConnectionStatus> IndicesAnalyzeGetAsync(string index, Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_analyze".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		///<param name="body">The text on which the analysis should be performed</param>
		public ConnectionStatus IndicesAnalyzePost(object body, Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			var url = "/_analyze".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		///<param name="body">The text on which the analysis should be performed</param>
		public Task<ConnectionStatus> IndicesAnalyzePostAsync(object body, Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			var url = "/_analyze".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		///<param name="body">The text on which the analysis should be performed</param>
		public ConnectionStatus IndicesAnalyzePost(string index, object body, Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_analyze".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-analyze/</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		///<param name="body">The text on which the analysis should be performed</param>
		public Task<ConnectionStatus> IndicesAnalyzePostAsync(string index, object body, Func<IndicesAnalyzeQueryString, IndicesAnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_analyze".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesAnalyzeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesClearCachePost(Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			var url = "/_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesClearCachePostAsync(Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			var url = "/_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public ConnectionStatus IndicesClearCachePost(string index, Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_cache/clear".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public Task<ConnectionStatus> IndicesClearCachePostAsync(string index, Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_cache/clear".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesClearCacheGet(Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			var url = "/_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesClearCacheGetAsync(Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			var url = "/_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public ConnectionStatus IndicesClearCacheGet(string index, Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_cache/clear".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-clearcache/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public Task<ConnectionStatus> IndicesClearCacheGetAsync(string index, Func<IndicesClearCacheQueryString, IndicesClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_cache/clear".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesClearCacheQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_close
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-open-close/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public ConnectionStatus IndicesClosePost(string index, Func<IndicesCloseQueryString, IndicesCloseQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_close".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesCloseQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_close
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-open-close/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public Task<ConnectionStatus> IndicesClosePostAsync(string index, Func<IndicesCloseQueryString, IndicesCloseQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_close".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesCloseQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>PUT /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-create-index/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public ConnectionStatus IndicesCreatePut(string index, object body, Func<IndicesCreateQueryString, IndicesCreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesCreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-create-index/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public Task<ConnectionStatus> IndicesCreatePutAsync(string index, object body, Func<IndicesCreateQueryString, IndicesCreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesCreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-create-index/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public ConnectionStatus IndicesCreatePost(string index, object body, Func<IndicesCreateQueryString, IndicesCreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesCreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-create-index/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public Task<ConnectionStatus> IndicesCreatePostAsync(string index, object body, Func<IndicesCreateQueryString, IndicesCreateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesCreateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>DELETE /
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-delete-index/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesDelete(Func<IndicesDeleteQueryString, IndicesDeleteQueryString> queryString = null)
		{
			var url = "/";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-delete-index/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesDeleteAsync(Func<IndicesDeleteQueryString, IndicesDeleteQueryString> queryString = null)
		{
			var url = "/";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-delete-index/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to delete; use `_all` or empty string to delete all indices</param>
		public ConnectionStatus IndicesDelete(string index, Func<IndicesDeleteQueryString, IndicesDeleteQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-delete-index/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to delete; use `_all` or empty string to delete all indices</param>
		public Task<ConnectionStatus> IndicesDeleteAsync(string index, Func<IndicesDeleteQueryString, IndicesDeleteQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with an alias</param>
		///<param name="name">The name of the alias to be deleted</param>
		public ConnectionStatus IndicesDeleteAlias(string index, string name, Func<IndicesDeleteAliasQueryString, IndicesDeleteAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with an alias</param>
		///<param name="name">The name of the alias to be deleted</param>
		public Task<ConnectionStatus> IndicesDeleteAliasAsync(string index, string name, Func<IndicesDeleteAliasQueryString, IndicesDeleteAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-delete-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` for all indices</param>
		///<param name="type">The name of the document type to delete</param>
		public ConnectionStatus IndicesDeleteMapping(string index, string type, Func<IndicesDeleteMappingQueryString, IndicesDeleteMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-delete-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` for all indices</param>
		///<param name="type">The name of the document type to delete</param>
		public Task<ConnectionStatus> IndicesDeleteMappingAsync(string index, string type, Func<IndicesDeleteMappingQueryString, IndicesDeleteMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public ConnectionStatus IndicesDeleteTemplate(string name, Func<IndicesDeleteTemplateQueryString, IndicesDeleteTemplateQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteTemplateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public Task<ConnectionStatus> IndicesDeleteTemplateAsync(string name, Func<IndicesDeleteTemplateQueryString, IndicesDeleteTemplateQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteTemplateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_warmer
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register warmer for; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesDeleteWarmer(string index, Func<IndicesDeleteWarmerQueryString, IndicesDeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_warmer".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_warmer
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register warmer for; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesDeleteWarmerAsync(string index, Func<IndicesDeleteWarmerQueryString, IndicesDeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_warmer".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to delete all warmers</param>
		public ConnectionStatus IndicesDeleteWarmer(string index, string name, Func<IndicesDeleteWarmerQueryString, IndicesDeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to delete all warmers</param>
		public Task<ConnectionStatus> IndicesDeleteWarmerAsync(string index, string name, Func<IndicesDeleteWarmerQueryString, IndicesDeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register warmer for; use `_all` or empty string to perform the operation on all types</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to delete all warmers</param>
		public ConnectionStatus IndicesDeleteWarmer(string index, string type, string name, Func<IndicesDeleteWarmerQueryString, IndicesDeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			name.ThrowIfNull("name");
			var url = "/{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register warmer for; use `_all` or empty string to perform the operation on all types</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to delete all warmers</param>
		public Task<ConnectionStatus> IndicesDeleteWarmerAsync(string index, string type, string name, Func<IndicesDeleteWarmerQueryString, IndicesDeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			name.ThrowIfNull("name");
			var url = "/{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-indices-exists/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to check</param>
		public ConnectionStatus IndicesExistsHead(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-indices-exists/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to check</param>
		public Task<ConnectionStatus> IndicesExistsHeadAsync(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesExistsAliasHead(string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_alias/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesExistsAliasHeadAsync(string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_alias/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesExistsAliasHead(string index, string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesExistsAliasHeadAsync(string index, string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-types-exists/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to check the types across all indices</param>
		///<param name="type">A comma-separated list of document types to check</param>
		public ConnectionStatus IndicesExistsTypeHead(string index, string type, Func<IndicesExistsTypeQueryString, IndicesExistsTypeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsTypeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-types-exists/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to check the types across all indices</param>
		///<param name="type">A comma-separated list of document types to check</param>
		public Task<ConnectionStatus> IndicesExistsTypeHeadAsync(string index, string type, Func<IndicesExistsTypeQueryString, IndicesExistsTypeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsTypeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesFlushPost(Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			var url = "/_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesFlushPostAsync(Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			var url = "/_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public ConnectionStatus IndicesFlushPost(string index, Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_flush".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public Task<ConnectionStatus> IndicesFlushPostAsync(string index, Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_flush".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesFlushGet(Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			var url = "/_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesFlushGetAsync(Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			var url = "/_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public ConnectionStatus IndicesFlushGet(string index, Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_flush".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-flush/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public Task<ConnectionStatus> IndicesFlushGetAsync(string index, Func<IndicesFlushQueryString, IndicesFlushQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_flush".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesFlushQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesGetAlias(string name, Func<IndicesGetAliasQueryString, IndicesGetAliasQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_alias/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesGetAliasAsync(string name, Func<IndicesGetAliasQueryString, IndicesGetAliasQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_alias/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesGetAlias(string index, string name, Func<IndicesGetAliasQueryString, IndicesGetAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesGetAliasAsync(string index, string name, Func<IndicesGetAliasQueryString, IndicesGetAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetAliases(Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			var url = "/_aliases";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetAliasesAsync(Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			var url = "/_aliases";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_aliases
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public ConnectionStatus IndicesGetAliases(string index, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_aliases".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_aliases
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public Task<ConnectionStatus> IndicesGetAliasesAsync(string index, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_aliases".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMapping(string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			field.ThrowIfNull("field");
			var url = "/_mapping/field/{field}".Inject(new { field = Stringify(field) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingAsync(string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			field.ThrowIfNull("field");
			var url = "/_mapping/field/{field}".Inject(new { field = Stringify(field) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMapping(string index, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			field.ThrowIfNull("field");
			var url = "/{index}/_mapping/field/{field}".Inject(new { index = Stringify(index), field = Stringify(field) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingAsync(string index, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			field.ThrowIfNull("field");
			var url = "/{index}/_mapping/field/{field}".Inject(new { index = Stringify(index), field = Stringify(field) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMapping(string index, string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			field.ThrowIfNull("field");
			var url = "/{index}/{type}/_mapping/field/{field}".Inject(new { index = Stringify(index), type = Stringify(type), field = Stringify(field) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingAsync(string index, string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			field.ThrowIfNull("field");
			var url = "/{index}/{type}/_mapping/field/{field}".Inject(new { index = Stringify(index), type = Stringify(type), field = Stringify(field) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-mapping/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetMapping(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_mapping";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-mapping/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetMappingAsync(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_mapping";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		public ConnectionStatus IndicesGetMapping(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_mapping".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		public Task<ConnectionStatus> IndicesGetMappingAsync(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_mapping".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		public ConnectionStatus IndicesGetMapping(string index, string type, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		public Task<ConnectionStatus> IndicesGetMappingAsync(string index, string type, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-settings/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetSettings(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-settings/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetSettingsAsync(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-settings/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesGetSettings(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_settings".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-get-settings/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesGetSettingsAsync(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_settings".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetTemplate(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_template";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetTemplateAsync(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/_template";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public ConnectionStatus IndicesGetTemplate(string name, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public Task<ConnectionStatus> IndicesGetTemplateAsync(string name, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		public ConnectionStatus IndicesGetWarmer(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_warmer".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesGetWarmerAsync(string index, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_warmer".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public ConnectionStatus IndicesGetWarmer(string index, string name, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public Task<ConnectionStatus> IndicesGetWarmerAsync(string index, string name, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public ConnectionStatus IndicesGetWarmer(string index, string type, string name, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			name.ThrowIfNull("name");
			var url = "/{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public Task<ConnectionStatus> IndicesGetWarmerAsync(string index, string type, string name, Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			name.ThrowIfNull("name");
			var url = "/{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_open
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-open-close/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public ConnectionStatus IndicesOpenPost(string index, Func<IndicesOpenQueryString, IndicesOpenQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_open".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOpenQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_open
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-open-close/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public Task<ConnectionStatus> IndicesOpenPostAsync(string index, Func<IndicesOpenQueryString, IndicesOpenQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_open".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOpenQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesOptimizePost(Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			var url = "/_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesOptimizePostAsync(Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			var url = "/_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesOptimizePost(string index, Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_optimize".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesOptimizePostAsync(string index, Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_optimize".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesOptimizeGet(Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			var url = "/_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesOptimizeGetAsync(Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			var url = "/_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesOptimizeGet(string index, Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_optimize".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-optimize/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesOptimizeGetAsync(string index, Func<IndicesOptimizeQueryString, IndicesOptimizeQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_optimize".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesOptimizeQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>PUT /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with an alias</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndexPutAlias(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with an alias</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndexPutAliasAsync(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndicesPutAlias(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_alias/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndicesPutAliasAsync(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_alias/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_alias
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with an alias</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndexPutAlias(string index, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_alias".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_alias
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with an alias</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndexPutAliasAsync(string index, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_alias".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_alias
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndicesPutAlias(object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			var url = "/_alias".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_alias
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndicesPutAliasAsync(object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			var url = "/_alias".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-put-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to perform the operation on all indices</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public ConnectionStatus IndicesPutMapping(string index, string type, object body, Func<IndicesPutMappingQueryString, IndicesPutMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-put-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to perform the operation on all indices</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public Task<ConnectionStatus> IndicesPutMappingAsync(string index, string type, object body, Func<IndicesPutMappingQueryString, IndicesPutMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-put-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to perform the operation on all indices</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public ConnectionStatus IndicesPutMappingPost(string index, string type, object body, Func<IndicesPutMappingQueryString, IndicesPutMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-put-mapping/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to perform the operation on all indices</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public Task<ConnectionStatus> IndicesPutMappingPostAsync(string index, string type, object body, Func<IndicesPutMappingQueryString, IndicesPutMappingQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutMappingQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-update-settings/</pre>	
	    ///</summary>
		///<param name="body">The index settings to be updated</param>
		public ConnectionStatus IndicesPutSettings(object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			var url = "/_settings".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-update-settings/</pre>	
	    ///</summary>
		///<param name="body">The index settings to be updated</param>
		public Task<ConnectionStatus> IndicesPutSettingsAsync(object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			var url = "/_settings".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-update-settings/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The index settings to be updated</param>
		public ConnectionStatus IndicesPutSettings(string index, object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_settings".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-update-settings/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The index settings to be updated</param>
		public Task<ConnectionStatus> IndicesPutSettingsAsync(string index, object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_settings".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public ConnectionStatus IndicesPutTemplate(string name, object body, Func<IndicesPutTemplateQueryString, IndicesPutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutTemplateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public Task<ConnectionStatus> IndicesPutTemplateAsync(string name, object body, Func<IndicesPutTemplateQueryString, IndicesPutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutTemplateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public ConnectionStatus IndicesPutTemplatePost(string name, object body, Func<IndicesPutTemplateQueryString, IndicesPutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutTemplateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-templates/</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public Task<ConnectionStatus> IndicesPutTemplatePostAsync(string name, object body, Func<IndicesPutTemplateQueryString, IndicesPutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNull("name");
			var url = "/_template/{name}".Inject(new { name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutTemplateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmer(string index, string name, object body, Func<IndicesPutWarmerQueryString, IndicesPutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerAsync(string index, string name, object body, Func<IndicesPutWarmerQueryString, IndicesPutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			name.ThrowIfNull("name");
			var url = "/{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register the warmer for; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmer(string index, string type, string name, object body, Func<IndicesPutWarmerQueryString, IndicesPutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			name.ThrowIfNull("name");
			var url = "/{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-warmers/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register the warmer for; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerAsync(string index, string type, string name, object body, Func<IndicesPutWarmerQueryString, IndicesPutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			name.ThrowIfNull("name");
			var url = "/{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutWarmerQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesRefreshPost(Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			var url = "/_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesRefreshPostAsync(Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			var url = "/_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesRefreshPost(string index, Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_refresh".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesRefreshPostAsync(string index, Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_refresh".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesRefreshGet(Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			var url = "/_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesRefreshGetAsync(Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			var url = "/_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesRefreshGet(string index, Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_refresh".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-refresh/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesRefreshGetAsync(string index, Func<IndicesRefreshQueryString, IndicesRefreshQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_refresh".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesRefreshQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_segments
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-segments/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesSegmentsGet(Func<IndicesSegmentsQueryString, IndicesSegmentsQueryString> queryString = null)
		{
			var url = "/_segments";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSegmentsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_segments
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-segments/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesSegmentsGetAsync(Func<IndicesSegmentsQueryString, IndicesSegmentsQueryString> queryString = null)
		{
			var url = "/_segments";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSegmentsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_segments
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-segments/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesSegmentsGet(string index, Func<IndicesSegmentsQueryString, IndicesSegmentsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_segments".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSegmentsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_segments
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-segments/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesSegmentsGetAsync(string index, Func<IndicesSegmentsQueryString, IndicesSegmentsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_segments".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSegmentsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-gateway-snapshot/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesSnapshotIndexPost(Func<IndicesSnapshotIndexQueryString, IndicesSnapshotIndexQueryString> queryString = null)
		{
			var url = "/_gateway/snapshot";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSnapshotIndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-gateway-snapshot/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesSnapshotIndexPostAsync(Func<IndicesSnapshotIndexQueryString, IndicesSnapshotIndexQueryString> queryString = null)
		{
			var url = "/_gateway/snapshot";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSnapshotIndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-gateway-snapshot/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public ConnectionStatus IndicesSnapshotIndexPost(string index, Func<IndicesSnapshotIndexQueryString, IndicesSnapshotIndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_gateway/snapshot".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSnapshotIndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-gateway-snapshot/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public Task<ConnectionStatus> IndicesSnapshotIndexPostAsync(string index, Func<IndicesSnapshotIndexQueryString, IndicesSnapshotIndexQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_gateway/snapshot".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesSnapshotIndexQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesStatsGet(Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			var url = "/_stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesStatsGetAsync(Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			var url = "/_stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndexStatsGet(string index, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_stats".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndexStatsGetAsync(string index, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_stats".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET _stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="metric_family">Limit the information returned to a specific metric</param>
		public ConnectionStatus IndicesStatsGet(MetricFamilyOptions metric_family, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			metric_family.ThrowIfNull("metric_family");
			var url = "_stats/{metric_family}".Inject(new { metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET _stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="metric_family">Limit the information returned to a specific metric</param>
		public Task<ConnectionStatus> IndicesStatsGetAsync(MetricFamilyOptions metric_family, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			metric_family.ThrowIfNull("metric_family");
			var url = "_stats/{metric_family}".Inject(new { metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="metric_family">Limit the information returned to a specific metric</param>
		public ConnectionStatus IndexStatsGet(string index, MetricFamilyOptions metric_family, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			metric_family.ThrowIfNull("metric_family");
			var url = "/{index}/_stats/{metric_family}".Inject(new { index = Stringify(index), metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/{metric_family}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="metric_family">Limit the information returned to a specific metric</param>
		public Task<ConnectionStatus> IndexStatsGetAsync(string index, MetricFamilyOptions metric_family, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			metric_family.ThrowIfNull("metric_family");
			var url = "/{index}/_stats/{metric_family}".Inject(new { index = Stringify(index), metric_family = Stringify(metric_family) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/indexing/{indexing_types}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="indexing_types">A comma-separated list of document types to include in the `indexing` statistics</param>
		public ConnectionStatus IndicesIndexingStatsGet(string indexing_types, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			indexing_types.ThrowIfNull("indexing_types");
			var url = "/_stats/indexing/{indexing_types}".Inject(new { indexing_types = Stringify(indexing_types) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/indexing/{indexing_types}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="indexing_types">A comma-separated list of document types to include in the `indexing` statistics</param>
		public Task<ConnectionStatus> IndicesIndexingStatsGetAsync(string indexing_types, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			indexing_types.ThrowIfNull("indexing_types");
			var url = "/_stats/indexing/{indexing_types}".Inject(new { indexing_types = Stringify(indexing_types) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/search/{search_groups}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="search_groups">A comma-separated list of search groups to include in the `search` statistics</param>
		public ConnectionStatus IndicesSearchStatsGet(string search_groups, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			search_groups.ThrowIfNull("search_groups");
			var url = "/_stats/search/{search_groups}".Inject(new { search_groups = Stringify(search_groups) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/search/{search_groups}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="search_groups">A comma-separated list of search groups to include in the `search` statistics</param>
		public Task<ConnectionStatus> IndicesSearchStatsGetAsync(string search_groups, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			search_groups.ThrowIfNull("search_groups");
			var url = "/_stats/search/{search_groups}".Inject(new { search_groups = Stringify(search_groups) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/search/{search_groups}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="search_groups">A comma-separated list of search groups to include in the `search` statistics</param>
		public ConnectionStatus IndexSearchStatsGet(string index, string search_groups, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			search_groups.ThrowIfNull("search_groups");
			var url = "/{index}/_stats/search/{search_groups}".Inject(new { index = Stringify(index), search_groups = Stringify(search_groups) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/search/{search_groups}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="search_groups">A comma-separated list of search groups to include in the `search` statistics</param>
		public Task<ConnectionStatus> IndexSearchStatsGetAsync(string index, string search_groups, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			search_groups.ThrowIfNull("search_groups");
			var url = "/{index}/_stats/search/{search_groups}".Inject(new { index = Stringify(index), search_groups = Stringify(search_groups) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/fielddata/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `search` statistics</param>
		public ConnectionStatus IndicesFieldDataStatsGet(string fields, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			fields.ThrowIfNull("fields");
			var url = "/_stats/fielddata/{fields}".Inject(new { fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/fielddata/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `search` statistics</param>
		public Task<ConnectionStatus> IndicesFieldDataStatsGetAsync(string fields, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			fields.ThrowIfNull("fields");
			var url = "/_stats/fielddata/{fields}".Inject(new { fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/fielddata/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `search` statistics</param>
		public ConnectionStatus IndexFieldDataStatsGet(string index, string fields, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			fields.ThrowIfNull("fields");
			var url = "/{index}/_stats/fielddata/{fields}".Inject(new { index = Stringify(index), fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/fielddata/{fields}
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-stats/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="fields">A comma-separated list of fields to return detailed information for, when returning the `search` statistics</param>
		public Task<ConnectionStatus> IndexFieldDataStatsGetAsync(string index, string fields, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			fields.ThrowIfNull("fields");
			var url = "/{index}/_stats/fielddata/{fields}".Inject(new { index = Stringify(index), fields = Stringify(fields) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_status
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-status/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesStatusGet(Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			var url = "/_status";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_status
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-status/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesStatusGetAsync(Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			var url = "/_status";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_status
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-status/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesStatusGet(string index, Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_status".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_status
	    ///<pre>http://elasticsearch.org/guide/reference/api/admin-indices-status/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesStatusGetAsync(string index, Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_status".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="body">The definition of `actions` to perform</param>
		public ConnectionStatus IndicesUpdateAliasesPost(object body, Func<IndicesUpdateAliasesQueryString, IndicesUpdateAliasesQueryString> queryString = null)
		{
			var url = "/_aliases".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesUpdateAliasesQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/admin-indices-aliases/</pre>	
	    ///</summary>
		///<param name="body">The definition of `actions` to perform</param>
		public Task<ConnectionStatus> IndicesUpdateAliasesPostAsync(object body, Func<IndicesUpdateAliasesQueryString, IndicesUpdateAliasesQueryString> queryString = null)
		{
			var url = "/_aliases".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesUpdateAliasesQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		public ConnectionStatus IndicesValidateQueryGet(Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			var url = "/_validate/query";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesValidateQueryGetAsync(Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			var url = "/_validate/query";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesValidateQueryGet(string index, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_validate/query".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesValidateQueryGetAsync(string index, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_validate/query".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		public ConnectionStatus IndicesValidateQueryGet(string index, string type, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		public Task<ConnectionStatus> IndicesValidateQueryGetAsync(string index, string type, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="body">The query definition</param>
		public ConnectionStatus IndicesValidateQueryPost(object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			var url = "/_validate/query".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="body">The query definition</param>
		public Task<ConnectionStatus> IndicesValidateQueryPostAsync(object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			var url = "/_validate/query".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The query definition</param>
		public ConnectionStatus IndicesValidateQueryPost(string index, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_validate/query".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The query definition</param>
		public Task<ConnectionStatus> IndicesValidateQueryPostAsync(string index, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_validate/query".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="body">The query definition</param>
		public ConnectionStatus IndicesValidateQueryPost(string index, string type, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/validate/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="body">The query definition</param>
		public Task<ConnectionStatus> IndicesValidateQueryPostAsync(string index, string type, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /
	    ///<pre>http://elasticsearch.org/guide/</pre>	
	    ///</summary>
		public ConnectionStatus InfoGet(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /
	    ///<pre>http://elasticsearch.org/guide/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> InfoGetAsync(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /
	    ///<pre>http://elasticsearch.org/guide/</pre>	
	    ///</summary>
		public ConnectionStatus InfoHead(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /
	    ///<pre>http://elasticsearch.org/guide/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> InfoHeadAsync(Func<FluentQueryString, FluentQueryString> queryString = null)
		{
			var url = "/";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FluentQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		public ConnectionStatus MgetGet(Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			var url = "/_mget";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> MgetGetAsync(Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			var url = "/_mget";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public ConnectionStatus MgetGet(string index, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_mget".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public Task<ConnectionStatus> MgetGetAsync(string index, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_mget".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		public ConnectionStatus MgetGet(string index, string type, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		public Task<ConnectionStatus> MgetGetAsync(string index, string type, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public ConnectionStatus MgetPost(object body, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			var url = "/_mget".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public Task<ConnectionStatus> MgetPostAsync(object body, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			var url = "/_mget".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public ConnectionStatus MgetPost(string index, object body, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_mget".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public Task<ConnectionStatus> MgetPostAsync(string index, object body, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_mget".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public ConnectionStatus MgetPost(string index, string type, object body, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mget
	    ///<pre>http://elasticsearch.org/guide/reference/api/multi-get/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public Task<ConnectionStatus> MgetPostAsync(string index, string type, object body, Func<MgetQueryString, MgetQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MgetQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_mlt
	    ///<pre>http://elasticsearch.org/guide/reference/api/more-like-this/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus MltGet(string index, string type, string id, Func<MltQueryString, MltQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MltQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_mlt
	    ///<pre>http://elasticsearch.org/guide/reference/api/more-like-this/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> MltGetAsync(string index, string type, string id, Func<MltQueryString, MltQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MltQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_mlt
	    ///<pre>http://elasticsearch.org/guide/reference/api/more-like-this/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="body">A specific search request definition</param>
		public ConnectionStatus MltPost(string index, string type, string id, object body, Func<MltQueryString, MltQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MltQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_mlt
	    ///<pre>http://elasticsearch.org/guide/reference/api/more-like-this/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="body">A specific search request definition</param>
		public Task<ConnectionStatus> MltPostAsync(string index, string type, string id, object body, Func<MltQueryString, MltQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MltQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		public ConnectionStatus MsearchGet(Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			var url = "/_msearch";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> MsearchGetAsync(Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			var url = "/_msearch";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		public ConnectionStatus MsearchGet(string index, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_msearch".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		public Task<ConnectionStatus> MsearchGetAsync(string index, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_msearch".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		public ConnectionStatus MsearchGet(string index, string type, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		public Task<ConnectionStatus> MsearchGetAsync(string index, string type, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public ConnectionStatus MsearchPost(object body, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			var url = "/_msearch".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public Task<ConnectionStatus> MsearchPostAsync(object body, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			var url = "/_msearch".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public ConnectionStatus MsearchPost(string index, object body, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_msearch".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public Task<ConnectionStatus> MsearchPostAsync(string index, object body, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_msearch".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public ConnectionStatus MsearchPost(string index, string type, object body, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/multi-search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public Task<ConnectionStatus> MsearchPostAsync(string index, string type, object body, Func<MsearchQueryString, MsearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MsearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_percolate
	    ///<pre>http://elasticsearch.org/guide/reference/api/percolate/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with a registered percolator query</param>
		///<param name="type">The document type</param>
		public ConnectionStatus PercolateGet(string index, string type, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_percolate
	    ///<pre>http://elasticsearch.org/guide/reference/api/percolate/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with a registered percolator query</param>
		///<param name="type">The document type</param>
		public Task<ConnectionStatus> PercolateGetAsync(string index, string type, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_percolate
	    ///<pre>http://elasticsearch.org/guide/reference/api/percolate/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with a registered percolator query</param>
		///<param name="type">The document type</param>
		///<param name="body">The document (`doc`) to percolate against registered queries; optionally also a `query` to limit the percolation to specific registered queries</param>
		public ConnectionStatus PercolatePost(string index, string type, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_percolate
	    ///<pre>http://elasticsearch.org/guide/reference/api/percolate/</pre>	
	    ///</summary>
		///<param name="index">The name of the index with a registered percolator query</param>
		///<param name="type">The document type</param>
		///<param name="body">The document (`doc`) to percolate against registered queries; optionally also a `query` to limit the percolation to specific registered queries</param>
		public Task<ConnectionStatus> PercolatePostAsync(string index, string type, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		public ConnectionStatus ScrollGet(Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			var url = "/_search/scroll";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ScrollGetAsync(Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			var url = "/_search/scroll";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		public ConnectionStatus ScrollGet(string scroll_id, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNull("scroll_id");
			var url = "/_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		public Task<ConnectionStatus> ScrollGetAsync(string scroll_id, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNull("scroll_id");
			var url = "/_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public ConnectionStatus ScrollPost(object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			var url = "/_search/scroll".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public Task<ConnectionStatus> ScrollPostAsync(object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			var url = "/_search/scroll".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public ConnectionStatus ScrollPost(string scroll_id, object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNull("scroll_id");
			var url = "/_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/scroll/</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public Task<ConnectionStatus> ScrollPostAsync(string scroll_id, object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNull("scroll_id");
			var url = "/_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		public ConnectionStatus SearchGet(Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			var url = "/_search";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> SearchGetAsync(Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			var url = "/_search";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus SearchGet(string index, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_search".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> SearchGetAsync(string index, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_search".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		public ConnectionStatus SearchGet(string index, string type, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		public Task<ConnectionStatus> SearchGetAsync(string index, string type, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="body">The search definition using the Query DSL</param>
		public ConnectionStatus SearchPost(object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			var url = "/_search".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="body">The search definition using the Query DSL</param>
		public Task<ConnectionStatus> SearchPostAsync(object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			var url = "/_search".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The search definition using the Query DSL</param>
		public ConnectionStatus SearchPost(string index, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_search".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The search definition using the Query DSL</param>
		public Task<ConnectionStatus> SearchPostAsync(string index, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_search".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		///<param name="body">The search definition using the Query DSL</param>
		public ConnectionStatus SearchPost(string index, string type, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/reference/api/search/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		///<param name="body">The search definition using the Query DSL</param>
		public Task<ConnectionStatus> SearchPostAsync(string index, string type, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			var url = "/{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		///<param name="body">The request definition</param>
		public ConnectionStatus SuggestPost(object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			var url = "/_suggest".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		///<param name="body">The request definition</param>
		public Task<ConnectionStatus> SuggestPostAsync(object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			var url = "/_suggest".Inject(new {  });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The request definition</param>
		public ConnectionStatus SuggestPost(string index, object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_suggest".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The request definition</param>
		public Task<ConnectionStatus> SuggestPostAsync(string index, object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_suggest".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		public ConnectionStatus SuggestGet(Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			var url = "/_suggest";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> SuggestGetAsync(Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			var url = "/_suggest";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus SuggestGet(string index, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_suggest".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_suggest
	    ///<pre>http://elasticsearch.org/guide/reference/api/search/suggest/</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> SuggestGetAsync(string index, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			var url = "/{index}/_suggest".Inject(new { index = Stringify(index) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_update
	    ///<pre>http://elasticsearch.org/guide/reference/api/update/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The request definition using either `script` or partial `doc`</param>
		public ConnectionStatus UpdatePost(string index, string type, string id, object body, Func<UpdateQueryString, UpdateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_update".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_update
	    ///<pre>http://elasticsearch.org/guide/reference/api/update/</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The request definition using either `script` or partial `doc`</param>
		public Task<ConnectionStatus> UpdatePostAsync(string index, string type, string id, object body, Func<UpdateQueryString, UpdateQueryString> queryString = null)
		{
			index.ThrowIfNull("index");
			type.ThrowIfNull("type");
			id.ThrowIfNull("id");
			var url = "/{index}/{type}/{id}/_update".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateQueryString());
				if (qs != null) nv = qs.NameValueCollection;
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
	
	  }
	  }
	
