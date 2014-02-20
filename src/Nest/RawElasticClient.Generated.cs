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
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus Bulk(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			//var url = "_bulk".Inject(new {  });
			var url = "_bulk".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkAsync(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			//var url = "_bulk".Inject(new {  });
			var url = "_bulk".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus Bulk(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_bulk".Inject(new { index = Stringify(index) });
			var url = "{0}/_bulk".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkAsync(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_bulk".Inject(new { index = Stringify(index) });
			var url = "{0}/_bulk".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus Bulk(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_bulk".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkAsync(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_bulk".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPut(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			//var url = "_bulk".Inject(new {  });
			var url = "_bulk".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPutAsync(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			//var url = "_bulk".Inject(new {  });
			var url = "_bulk".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPut(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_bulk".Inject(new { index = Stringify(index) });
			var url = "{0}/_bulk".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPutAsync(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_bulk".Inject(new { index = Stringify(index) });
			var url = "{0}/_bulk".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ConnectionStatus BulkPut(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_bulk".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="index">Default index for items which don&#39;t provide one</param>
		///<param name="type">Default document type for items which don&#39;t provide one</param>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public Task<ConnectionStatus> BulkPutAsync(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_bulk".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_bulk".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new BulkQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>GET /_cat/aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-aliases.html</pre>	
	    ///</summary>
		public ConnectionStatus CatAliases(Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
		{
			var url = "_cat/aliases";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-aliases.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatAliasesAsync(Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
		{
			var url = "_cat/aliases";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/aliases/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus CatAliases(string name, Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_cat/aliases/{name}".Inject(new { name = Stringify(name) });
			var url = "_cat/aliases/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/aliases/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> CatAliasesAsync(string name, Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_cat/aliases/{name}".Inject(new { name = Stringify(name) });
			var url = "_cat/aliases/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/allocation
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-allocation.html</pre>	
	    ///</summary>
		public ConnectionStatus CatAllocation(Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
		{
			var url = "_cat/allocation";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAllocationQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/allocation
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-allocation.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatAllocationAsync(Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
		{
			var url = "_cat/allocation";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAllocationQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/allocation/{node_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-allocation.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information</param>
		public ConnectionStatus CatAllocation(string node_id, Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cat/allocation/{node_id}".Inject(new { node_id = Stringify(node_id) });
			var url = "_cat/allocation/{0}".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAllocationQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/allocation/{node_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-allocation.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information</param>
		public Task<ConnectionStatus> CatAllocationAsync(string node_id, Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cat/allocation/{node_id}".Inject(new { node_id = Stringify(node_id) });
			var url = "_cat/allocation/{0}".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatAllocationQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-count.html</pre>	
	    ///</summary>
		public ConnectionStatus CatCount(Func<CatCountQueryString, CatCountQueryString> queryString = null)
		{
			var url = "_cat/count";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatCountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-count.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatCountAsync(Func<CatCountQueryString, CatCountQueryString> queryString = null)
		{
			var url = "_cat/count";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatCountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/count/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public ConnectionStatus CatCount(string index, Func<CatCountQueryString, CatCountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/count/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/count/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatCountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/count/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public Task<ConnectionStatus> CatCountAsync(string index, Func<CatCountQueryString, CatCountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/count/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/count/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatCountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/health
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-health.html</pre>	
	    ///</summary>
		public ConnectionStatus CatHealth(Func<CatHealthQueryString, CatHealthQueryString> queryString = null)
		{
			var url = "_cat/health";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatHealthQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/health
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-health.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatHealthAsync(Func<CatHealthQueryString, CatHealthQueryString> queryString = null)
		{
			var url = "_cat/health";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatHealthQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat.html</pre>	
	    ///</summary>
		public ConnectionStatus CatHelp(Func<CatHelpQueryString, CatHelpQueryString> queryString = null)
		{
			var url = "_cat";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatHelpQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatHelpAsync(Func<CatHelpQueryString, CatHelpQueryString> queryString = null)
		{
			var url = "_cat";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatHelpQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/indices
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-indices.html</pre>	
	    ///</summary>
		public ConnectionStatus CatIndices(Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
		{
			var url = "_cat/indices";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatIndicesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/indices
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-indices.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatIndicesAsync(Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
		{
			var url = "_cat/indices";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatIndicesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/indices/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-indices.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public ConnectionStatus CatIndices(string index, Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/indices/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/indices/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatIndicesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/indices/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-indices.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public Task<ConnectionStatus> CatIndicesAsync(string index, Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/indices/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/indices/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatIndicesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/master
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-master.html</pre>	
	    ///</summary>
		public ConnectionStatus CatMaster(Func<CatMasterQueryString, CatMasterQueryString> queryString = null)
		{
			var url = "_cat/master";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatMasterQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/master
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-master.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatMasterAsync(Func<CatMasterQueryString, CatMasterQueryString> queryString = null)
		{
			var url = "_cat/master";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatMasterQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/nodes
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-nodes.html</pre>	
	    ///</summary>
		public ConnectionStatus CatNodes(Func<CatNodesQueryString, CatNodesQueryString> queryString = null)
		{
			var url = "_cat/nodes";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatNodesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/nodes
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-nodes.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatNodesAsync(Func<CatNodesQueryString, CatNodesQueryString> queryString = null)
		{
			var url = "_cat/nodes";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatNodesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/pending_tasks
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-pending-tasks.html</pre>	
	    ///</summary>
		public ConnectionStatus CatPendingTasks(Func<CatPendingTasksQueryString, CatPendingTasksQueryString> queryString = null)
		{
			var url = "_cat/pending_tasks";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatPendingTasksQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/pending_tasks
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-pending-tasks.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatPendingTasksAsync(Func<CatPendingTasksQueryString, CatPendingTasksQueryString> queryString = null)
		{
			var url = "_cat/pending_tasks";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatPendingTasksQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/recovery
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-recovery.html</pre>	
	    ///</summary>
		public ConnectionStatus CatRecovery(Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
		{
			var url = "_cat/recovery";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatRecoveryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/recovery
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-recovery.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatRecoveryAsync(Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
		{
			var url = "_cat/recovery";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatRecoveryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/recovery/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-recovery.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public ConnectionStatus CatRecovery(string index, Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/recovery/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/recovery/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatRecoveryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/recovery/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-recovery.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public Task<ConnectionStatus> CatRecoveryAsync(string index, Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/recovery/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/recovery/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatRecoveryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/shards
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-shards.html</pre>	
	    ///</summary>
		public ConnectionStatus CatShards(Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
		{
			var url = "_cat/shards";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatShardsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/shards
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-shards.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatShardsAsync(Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
		{
			var url = "_cat/shards";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatShardsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/shards/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-shards.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public ConnectionStatus CatShards(string index, Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/shards/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/shards/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatShardsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/shards/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cat-shards.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to limit the returned information</param>
		public Task<ConnectionStatus> CatShardsAsync(string index, Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cat/shards/{index}".Inject(new { index = Stringify(index) });
			var url = "_cat/shards/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatShardsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/thread_pool
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/cat-thread-pool.html</pre>	
	    ///</summary>
		public ConnectionStatus CatThreadPool(Func<CatThreadPoolQueryString, CatThreadPoolQueryString> queryString = null)
		{
			var url = "_cat/thread_pool";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatThreadPoolQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cat/thread_pool
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/cat-thread-pool.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CatThreadPoolAsync(Func<CatThreadPoolQueryString, CatThreadPoolQueryString> queryString = null)
		{
			var url = "_cat/thread_pool";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CatThreadPoolQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="scroll_id">A comma-separated list of scroll IDs to clear</param>
		public ConnectionStatus ClearScroll(string scroll_id, Func<ClearScrollQueryString, ClearScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
			//var url = "_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			var url = "_search/scroll/{0}".F(Encoded(scroll_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="scroll_id">A comma-separated list of scroll IDs to clear</param>
		public Task<ConnectionStatus> ClearScrollAsync(string scroll_id, Func<ClearScrollQueryString, ClearScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
			//var url = "_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			var url = "_search/scroll/{0}".F(Encoded(scroll_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-update-settings.html</pre>	
	    ///</summary>
		public ConnectionStatus ClusterGetSettings(Func<ClusterGetSettingsQueryString, ClusterGetSettingsQueryString> queryString = null)
		{
			var url = "_cluster/settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterGetSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-update-settings.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterGetSettingsAsync(Func<ClusterGetSettingsQueryString, ClusterGetSettingsQueryString> queryString = null)
		{
			var url = "_cluster/settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterGetSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-health.html</pre>	
	    ///</summary>
		public ConnectionStatus ClusterHealth(Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			var url = "_cluster/health";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-health.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterHealthAsync(Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			var url = "_cluster/health";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-health.html</pre>	
	    ///</summary>
		///<param name="index">Limit the information returned to a specific index</param>
		public ConnectionStatus ClusterHealth(string index, Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cluster/health/{index}".Inject(new { index = Stringify(index) });
			var url = "_cluster/health/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/health/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-health.html</pre>	
	    ///</summary>
		///<param name="index">Limit the information returned to a specific index</param>
		public Task<ConnectionStatus> ClusterHealthAsync(string index, Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cluster/health/{index}".Inject(new { index = Stringify(index) });
			var url = "_cluster/health/{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterHealthQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/pending_tasks
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-pending.html</pre>	
	    ///</summary>
		public ConnectionStatus ClusterPendingTasks(Func<ClusterPendingTasksQueryString, ClusterPendingTasksQueryString> queryString = null)
		{
			var url = "_cluster/pending_tasks";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterPendingTasksQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/pending_tasks
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-pending.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterPendingTasksAsync(Func<ClusterPendingTasksQueryString, ClusterPendingTasksQueryString> queryString = null)
		{
			var url = "_cluster/pending_tasks";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterPendingTasksQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>PUT /_cluster/settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-update-settings.html</pre>	
	    ///</summary>
		///<param name="body">The settings to be updated. Can be either `transient` or `persistent` (survives cluster restart).</param>
		public ConnectionStatus ClusterPutSettings(object body, Func<ClusterPutSettingsQueryString, ClusterPutSettingsQueryString> queryString = null)
		{
			//var url = "_cluster/settings".Inject(new {  });
			var url = "_cluster/settings".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterPutSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_cluster/settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-update-settings.html</pre>	
	    ///</summary>
		///<param name="body">The settings to be updated. Can be either `transient` or `persistent` (survives cluster restart).</param>
		public Task<ConnectionStatus> ClusterPutSettingsAsync(object body, Func<ClusterPutSettingsQueryString, ClusterPutSettingsQueryString> queryString = null)
		{
			//var url = "_cluster/settings".Inject(new {  });
			var url = "_cluster/settings".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterPutSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_cluster/reroute
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-reroute.html</pre>	
	    ///</summary>
		///<param name="body">The definition of `commands` to perform (`move`, `cancel`, `allocate`)</param>
		public ConnectionStatus ClusterReroute(object body, Func<ClusterRerouteQueryString, ClusterRerouteQueryString> queryString = null)
		{
			//var url = "_cluster/reroute".Inject(new {  });
			var url = "_cluster/reroute".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterRerouteQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_cluster/reroute
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-reroute.html</pre>	
	    ///</summary>
		///<param name="body">The definition of `commands` to perform (`move`, `cancel`, `allocate`)</param>
		public Task<ConnectionStatus> ClusterRerouteAsync(object body, Func<ClusterRerouteQueryString, ClusterRerouteQueryString> queryString = null)
		{
			//var url = "_cluster/reroute".Inject(new {  });
			var url = "_cluster/reroute".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterRerouteQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_cluster/state
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-state.html</pre>	
	    ///</summary>
		public ConnectionStatus ClusterState(Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			var url = "_cluster/state";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/state
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-state.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterStateAsync(Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			var url = "_cluster/state";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/state/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-state.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		public ConnectionStatus ClusterState(string metric, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_cluster/state/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_cluster/state/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/state/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-state.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		public Task<ConnectionStatus> ClusterStateAsync(string metric, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_cluster/state/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_cluster/state/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/state/{metric}/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-state.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus ClusterState(string metric, string index, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cluster/state/{metric}/{index}".Inject(new { metric = Stringify(metric), index = Stringify(index) });
			var url = "_cluster/state/{0}/{1}".F(Encoded(metric), Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/state/{metric}/{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-state.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> ClusterStateAsync(string metric, string index, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index.ThrowIfNullOrEmpty("index");
			//var url = "_cluster/state/{metric}/{index}".Inject(new { metric = Stringify(metric), index = Stringify(index) });
			var url = "_cluster/state/{0}/{1}".F(Encoded(metric), Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-stats.html</pre>	
	    ///</summary>
		public ConnectionStatus ClusterStats(Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
		{
			var url = "_cluster/stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-stats.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ClusterStatsAsync(Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
		{
			var url = "_cluster/stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/stats/nodes/{node_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus ClusterStats(string node_id, Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cluster/stats/nodes/{node_id}".Inject(new { node_id = Stringify(node_id) });
			var url = "_cluster/stats/nodes/{0}".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/stats/nodes/{node_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> ClusterStatsAsync(string node_id, Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cluster/stats/nodes/{node_id}".Inject(new { node_id = Stringify(node_id) });
			var url = "_cluster/stats/nodes/{0}".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClusterStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		public ConnectionStatus Count(object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			//var url = "_count".Inject(new {  });
			var url = "_count".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		public Task<ConnectionStatus> CountAsync(object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			//var url = "_count".Inject(new {  });
			var url = "_count".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		public ConnectionStatus Count(string index, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_count".Inject(new { index = Stringify(index) });
			var url = "{0}/_count".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		public Task<ConnectionStatus> CountAsync(string index, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_count".Inject(new { index = Stringify(index) });
			var url = "{0}/_count".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		public ConnectionStatus Count(string index, string type, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		public Task<ConnectionStatus> CountAsync(string index, string type, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		public ConnectionStatus CountGet(Func<CountQueryString, CountQueryString> queryString = null)
		{
			var url = "_count";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> CountGetAsync(Func<CountQueryString, CountQueryString> queryString = null)
		{
			var url = "_count";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		public ConnectionStatus CountGet(string index, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_count".Inject(new { index = Stringify(index) });
			var url = "{0}/_count".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		public Task<ConnectionStatus> CountGetAsync(string index, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_count".Inject(new { index = Stringify(index) });
			var url = "{0}/_count".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		public ConnectionStatus CountGet(string index, string type, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-count.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the results</param>
		///<param name="type">A comma-separated list of types to restrict the results</param>
		public Task<ConnectionStatus> CountGetAsync(string index, string type, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		public ConnectionStatus CountPercolateGet(string index, string type, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate/count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		public Task<ConnectionStatus> CountPercolateGetAsync(string index, string type, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate/count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		public ConnectionStatus CountPercolateGet(string index, string type, string id, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate/count".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		public Task<ConnectionStatus> CountPercolateGetAsync(string index, string type, string id, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate/count".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="body">The count percolator request definition using the percolate DSL</param>
		public ConnectionStatus CountPercolate(string index, string type, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate/count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="body">The count percolator request definition using the percolate DSL</param>
		public Task<ConnectionStatus> CountPercolateAsync(string index, string type, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate/count".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		///<param name="body">The count percolator request definition using the percolate DSL</param>
		public ConnectionStatus CountPercolate(string index, string type, string id, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate/count".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_percolate/count
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		///<param name="body">The count percolator request definition using the percolate DSL</param>
		public Task<ConnectionStatus> CountPercolateAsync(string index, string type, string id, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate/count".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate/count".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CountPercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-delete.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus Delete(string index, string type, string id, Func<DeleteQueryString, DeleteQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-delete.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> DeleteAsync(string index, string type, string id, Func<DeleteQueryString, DeleteQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-delete-by-query.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="body">A query to restrict the operation specified with the Query DSL</param>
		public ConnectionStatus DeleteByQuery(string index, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_query".Inject(new { index = Stringify(index) });
			var url = "{0}/_query".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-delete-by-query.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="body">A query to restrict the operation specified with the Query DSL</param>
		public Task<ConnectionStatus> DeleteByQueryAsync(string index, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_query".Inject(new { index = Stringify(index) });
			var url = "{0}/_query".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-delete-by-query.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of types to restrict the operation</param>
		///<param name="body">A query to restrict the operation specified with the Query DSL</param>
		public ConnectionStatus DeleteByQuery(string index, string type, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_query".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_query".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-delete-by-query.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of types to restrict the operation</param>
		///<param name="body">A query to restrict the operation specified with the Query DSL</param>
		public Task<ConnectionStatus> DeleteByQueryAsync(string index, string type, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_query".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_query".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteByQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, body, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus Exists(string index, string type, string id, Func<ExistsQueryString, ExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExistsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> ExistsAsync(string index, string type, string id, Func<ExistsQueryString, ExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExistsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_explain
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-explain.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus ExplainGet(string index, string type, string id, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_explain".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_explain
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-explain.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> ExplainGetAsync(string index, string type, string id, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_explain".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_explain
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-explain.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="body">The query definition using the Query DSL</param>
		public ConnectionStatus Explain(string index, string type, string id, object body, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_explain".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_explain
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-explain.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="body">The query definition using the Query DSL</param>
		public Task<ConnectionStatus> ExplainAsync(string index, string type, string id, object body, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_explain".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_explain".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ExplainQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus Get(string index, string type, string id, Func<GetQueryString, GetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> GetAsync(string index, string type, string id, Func<GetQueryString, GetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_source
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document; use `_all` to fetch the first document matching the ID across all types</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus GetSource(string index, string type, string id, Func<GetSourceQueryString, GetSourceQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_source".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_source".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetSourceQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_source
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document; use `_all` to fetch the first document matching the ID across all types</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> GetSourceAsync(string index, string type, string id, Func<GetSourceQueryString, GetSourceQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_source".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_source".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetSourceQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public ConnectionStatus Index(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexAsync(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public ConnectionStatus Index(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexAsync(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public ConnectionStatus IndexPut(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexPutAsync(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public ConnectionStatus IndexPut(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/{id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-index_.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The document</param>
		public Task<ConnectionStatus> IndexPutAsync(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>GET /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesAnalyzeGetForAll(Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			var url = "_analyze";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesAnalyzeGetForAllAsync(Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			var url = "_analyze";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		public ConnectionStatus IndicesAnalyzeGet(string index, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_analyze".Inject(new { index = Stringify(index) });
			var url = "{0}/_analyze".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		public Task<ConnectionStatus> IndicesAnalyzeGetAsync(string index, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_analyze".Inject(new { index = Stringify(index) });
			var url = "{0}/_analyze".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		///<param name="body">The text on which the analysis should be performed</param>
		public ConnectionStatus IndicesAnalyzeForAll(object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			//var url = "_analyze".Inject(new {  });
			var url = "_analyze".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		///<param name="body">The text on which the analysis should be performed</param>
		public Task<ConnectionStatus> IndicesAnalyzeForAllAsync(object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			//var url = "_analyze".Inject(new {  });
			var url = "_analyze".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		///<param name="body">The text on which the analysis should be performed</param>
		public ConnectionStatus IndicesAnalyze(string index, object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_analyze".Inject(new { index = Stringify(index) });
			var url = "{0}/_analyze".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_analyze
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-analyze.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index to scope the operation</param>
		///<param name="body">The text on which the analysis should be performed</param>
		public Task<ConnectionStatus> IndicesAnalyzeAsync(string index, object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_analyze".Inject(new { index = Stringify(index) });
			var url = "{0}/_analyze".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AnalyzeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesClearCacheForAll(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			var url = "_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesClearCacheForAllAsync(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			var url = "_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public ConnectionStatus IndicesClearCache(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_cache/clear".Inject(new { index = Stringify(index) });
			var url = "{0}/_cache/clear".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public Task<ConnectionStatus> IndicesClearCacheAsync(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_cache/clear".Inject(new { index = Stringify(index) });
			var url = "{0}/_cache/clear".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesClearCacheGetForAll(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			var url = "_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesClearCacheGetForAllAsync(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			var url = "_cache/clear";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public ConnectionStatus IndicesClearCacheGet(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_cache/clear".Inject(new { index = Stringify(index) });
			var url = "{0}/_cache/clear".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_cache/clear
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-clearcache.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index name to limit the operation</param>
		public Task<ConnectionStatus> IndicesClearCacheGetAsync(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_cache/clear".Inject(new { index = Stringify(index) });
			var url = "{0}/_cache/clear".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ClearCacheQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_close
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-open-close.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public ConnectionStatus IndicesClose(string index, Func<CloseIndexQueryString, CloseIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_close".Inject(new { index = Stringify(index) });
			var url = "{0}/_close".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CloseIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_close
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-open-close.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public Task<ConnectionStatus> IndicesCloseAsync(string index, Func<CloseIndexQueryString, CloseIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_close".Inject(new { index = Stringify(index) });
			var url = "{0}/_close".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CloseIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>PUT /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-create-index.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public ConnectionStatus IndicesCreate(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-create-index.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public Task<ConnectionStatus> IndicesCreateAsync(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-create-index.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public ConnectionStatus IndicesCreatePost(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-create-index.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		public Task<ConnectionStatus> IndicesCreatePostAsync(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new CreateIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>DELETE /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-delete-index.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to delete; use `_all` or `*` string to delete all indices</param>
		public ConnectionStatus IndicesDelete(string index, Func<DeleteIndexQueryString, DeleteIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-delete-index.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to delete; use `_all` or `*` string to delete all indices</param>
		public Task<ConnectionStatus> IndicesDeleteAsync(string index, Func<DeleteIndexQueryString, DeleteIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="name">A comma-separated list of aliases to delete (supports wildcards); use `_all` to delete all aliases for the specified indices.</param>
		public ConnectionStatus IndicesDeleteAlias(string index, string name, Func<IndicesDeleteAliasQueryString, IndicesDeleteAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="name">A comma-separated list of aliases to delete (supports wildcards); use `_all` to delete all aliases for the specified indices.</param>
		public Task<ConnectionStatus> IndicesDeleteAliasAsync(string index, string name, Func<IndicesDeleteAliasQueryString, IndicesDeleteAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesDeleteAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-delete-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="type">A comma-separated list of document types to delete (supports wildcards); use `_all` to delete all document types in the specified indices.</param>
		public ConnectionStatus IndicesDeleteMapping(string index, string type, Func<DeleteMappingQueryString, DeleteMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mapping".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-delete-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="type">A comma-separated list of document types to delete (supports wildcards); use `_all` to delete all document types in the specified indices.</param>
		public Task<ConnectionStatus> IndicesDeleteMappingAsync(string index, string type, Func<DeleteMappingQueryString, DeleteMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mapping".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public ConnectionStatus IndicesDeleteTemplateForAll(string name, Func<DeleteTemplateQueryString, DeleteTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public Task<ConnectionStatus> IndicesDeleteTemplateForAllAsync(string name, Func<DeleteTemplateQueryString, DeleteTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to delete warmers from (supports wildcards); use `_all` to perform the operation on all indices.</param>
		///<param name="name">A comma-separated list of warmer names to delete (supports wildcards); use `_all` to delete all warmers in the specified indices. You must specify a name either in the uri or in the parameters.</param>
		public ConnectionStatus IndicesDeleteWarmer(string index, string name, Func<DeleteWarmerQueryString, DeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to delete warmers from (supports wildcards); use `_all` to perform the operation on all indices.</param>
		///<param name="name">A comma-separated list of warmer names to delete (supports wildcards); use `_all` to delete all warmers in the specified indices. You must specify a name either in the uri or in the parameters.</param>
		public Task<ConnectionStatus> IndicesDeleteWarmerAsync(string index, string name, Func<DeleteWarmerQueryString, DeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new DeleteWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-settings.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to check</param>
		public ConnectionStatus IndicesExists(string index, Func<IndexExistsQueryString, IndexExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexExistsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-settings.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to check</param>
		public Task<ConnectionStatus> IndicesExistsAsync(string index, Func<IndexExistsQueryString, IndexExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}".Inject(new { index = Stringify(index) });
			var url = "{0}".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndexExistsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesExistsAliasForAll(string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesExistsAliasForAllAsync(string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesExistsAlias(string index, string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesExistsAliasAsync(string index, string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/_alias
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public ConnectionStatus IndicesExistsAlias(string index, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_alias".Inject(new { index = Stringify(index) });
			var url = "{0}/_alias".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/_alias
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public Task<ConnectionStatus> IndicesExistsAliasAsync(string index, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_alias".Inject(new { index = Stringify(index) });
			var url = "{0}/_alias".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public ConnectionStatus IndicesExistsTemplateForAll(string name, Func<IndicesExistsTemplateQueryString, IndicesExistsTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public Task<ConnectionStatus> IndicesExistsTemplateForAllAsync(string name, Func<IndicesExistsTemplateQueryString, IndicesExistsTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-types-exists.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to check the types across all indices</param>
		///<param name="type">A comma-separated list of document types to check</param>
		public ConnectionStatus IndicesExistsType(string index, string type, Func<IndicesExistsTypeQueryString, IndicesExistsTypeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsTypeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /{index}/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-types-exists.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to check the types across all indices</param>
		///<param name="type">A comma-separated list of document types to check</param>
		public Task<ConnectionStatus> IndicesExistsTypeAsync(string index, string type, Func<IndicesExistsTypeQueryString, IndicesExistsTypeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesExistsTypeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesFlushForAll(Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			var url = "_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesFlushForAllAsync(Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			var url = "_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public ConnectionStatus IndicesFlush(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_flush".Inject(new { index = Stringify(index) });
			var url = "{0}/_flush".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public Task<ConnectionStatus> IndicesFlushAsync(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_flush".Inject(new { index = Stringify(index) });
			var url = "{0}/_flush".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesFlushGetForAll(Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			var url = "_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesFlushGetForAllAsync(Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			var url = "_flush";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public ConnectionStatus IndicesFlushGet(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_flush".Inject(new { index = Stringify(index) });
			var url = "{0}/_flush".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_flush
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-flush.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public Task<ConnectionStatus> IndicesFlushGetAsync(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_flush".Inject(new { index = Stringify(index) });
			var url = "{0}/_flush".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new FlushQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_alias
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetAliasForAll(Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			var url = "_alias";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_alias
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetAliasForAllAsync(Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			var url = "_alias";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesGetAliasForAll(string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesGetAliasForAllAsync(string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public ConnectionStatus IndicesGetAlias(string index, string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to return</param>
		public Task<ConnectionStatus> IndicesGetAliasAsync(string index, string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_alias
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public ConnectionStatus IndicesGetAlias(string index, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_alias".Inject(new { index = Stringify(index) });
			var url = "{0}/_alias".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_alias
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public Task<ConnectionStatus> IndicesGetAliasAsync(string index, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_alias".Inject(new { index = Stringify(index) });
			var url = "{0}/_alias".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetAliasesForAll(Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			var url = "_aliases";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetAliasesForAllAsync(Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			var url = "_aliases";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public ConnectionStatus IndicesGetAliases(string index, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_aliases".Inject(new { index = Stringify(index) });
			var url = "{0}/_aliases".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		public Task<ConnectionStatus> IndicesGetAliasesAsync(string index, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_aliases".Inject(new { index = Stringify(index) });
			var url = "{0}/_aliases".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_aliases/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to filter</param>
		public ConnectionStatus IndicesGetAliases(string index, string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_aliases/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_aliases/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_aliases/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to filter aliases</param>
		///<param name="name">A comma-separated list of alias names to filter</param>
		public Task<ConnectionStatus> IndicesGetAliasesAsync(string index, string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_aliases/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_aliases/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_aliases/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to filter</param>
		public ConnectionStatus IndicesGetAliasesForAll(string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_aliases/{name}".Inject(new { name = Stringify(name) });
			var url = "_aliases/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_aliases/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to filter</param>
		public Task<ConnectionStatus> IndicesGetAliasesForAllAsync(string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_aliases/{name}".Inject(new { name = Stringify(name) });
			var url = "_aliases/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetAliasesQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMappingForAll(string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			field.ThrowIfNullOrEmpty("field");
			//var url = "_mapping/field/{field}".Inject(new { field = Stringify(field) });
			var url = "_mapping/field/{0}".F(Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingForAllAsync(string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			field.ThrowIfNullOrEmpty("field");
			//var url = "_mapping/field/{field}".Inject(new { field = Stringify(field) });
			var url = "_mapping/field/{0}".F(Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMapping(string index, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			field.ThrowIfNullOrEmpty("field");
			//var url = "{index}/_mapping/field/{field}".Inject(new { index = Stringify(index), field = Stringify(field) });
			var url = "{0}/_mapping/field/{1}".F(Encoded(index), Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingAsync(string index, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			field.ThrowIfNullOrEmpty("field");
			//var url = "{index}/_mapping/field/{field}".Inject(new { index = Stringify(index), field = Stringify(field) });
			var url = "{0}/_mapping/field/{1}".F(Encoded(index), Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/{type}/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="type">A comma-separated list of document types</param>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMappingForAll(string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
			//var url = "_mapping/{type}/field/{field}".Inject(new { type = Stringify(type), field = Stringify(field) });
			var url = "_mapping/{0}/field/{1}".F(Encoded(type), Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/{type}/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="type">A comma-separated list of document types</param>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingForAllAsync(string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
			//var url = "_mapping/{type}/field/{field}".Inject(new { type = Stringify(type), field = Stringify(field) });
			var url = "_mapping/{0}/field/{1}".F(Encoded(type), Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/{type}/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		///<param name="field">A comma-separated list of fields</param>
		public ConnectionStatus IndicesGetFieldMapping(string index, string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
			//var url = "{index}/_mapping/{type}/field/{field}".Inject(new { index = Stringify(index), type = Stringify(type), field = Stringify(field) });
			var url = "{0}/_mapping/{1}/field/{2}".F(Encoded(index), Encoded(type), Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/{type}/field/{field}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-field-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		///<param name="field">A comma-separated list of fields</param>
		public Task<ConnectionStatus> IndicesGetFieldMappingAsync(string index, string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
			//var url = "{index}/_mapping/{type}/field/{field}".Inject(new { index = Stringify(index), type = Stringify(type), field = Stringify(field) });
			var url = "{0}/_mapping/{1}/field/{2}".F(Encoded(index), Encoded(type), Encoded(field));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesGetFieldMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetMappingForAll(Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			var url = "_mapping";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetMappingForAllAsync(Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			var url = "_mapping";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		public ConnectionStatus IndicesGetMapping(string index, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mapping".Inject(new { index = Stringify(index) });
			var url = "{0}/_mapping".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		public Task<ConnectionStatus> IndicesGetMappingAsync(string index, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mapping".Inject(new { index = Stringify(index) });
			var url = "{0}/_mapping".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="type">A comma-separated list of document types</param>
		public ConnectionStatus IndicesGetMappingForAll(string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			//var url = "_mapping/{type}".Inject(new { type = Stringify(type) });
			var url = "_mapping/{0}".F(Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="type">A comma-separated list of document types</param>
		public Task<ConnectionStatus> IndicesGetMappingForAllAsync(string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			//var url = "_mapping/{type}".Inject(new { type = Stringify(type) });
			var url = "_mapping/{0}".F(Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		public ConnectionStatus IndicesGetMapping(string index, string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/_mapping/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/_mapping/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="type">A comma-separated list of document types</param>
		public Task<ConnectionStatus> IndicesGetMappingAsync(string index, string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/_mapping/{type}".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/_mapping/{1}".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetSettingsForAll(Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			var url = "_settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetSettingsForAllAsync(Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			var url = "_settings";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesGetSettings(string index, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_settings".Inject(new { index = Stringify(index) });
			var url = "{0}/_settings".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesGetSettingsAsync(string index, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_settings".Inject(new { index = Stringify(index) });
			var url = "{0}/_settings".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_settings/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="name">The name of the settings that should be included</param>
		public ConnectionStatus IndicesGetSettings(string index, string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_settings/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_settings/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_settings/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="name">The name of the settings that should be included</param>
		public Task<ConnectionStatus> IndicesGetSettingsAsync(string index, string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_settings/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_settings/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_settings/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="name">The name of the settings that should be included</param>
		public ConnectionStatus IndicesGetSettingsForAll(string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_settings/{name}".Inject(new { name = Stringify(name) });
			var url = "_settings/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_settings/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-get-mapping.html</pre>	
	    ///</summary>
		///<param name="name">The name of the settings that should be included</param>
		public Task<ConnectionStatus> IndicesGetSettingsForAllAsync(string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_settings/{name}".Inject(new { name = Stringify(name) });
			var url = "_settings/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetIndexSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetTemplateForAll(Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
		{
			var url = "_template";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetTemplateForAllAsync(Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
		{
			var url = "_template";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public ConnectionStatus IndicesGetTemplateForAll(string name, Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		public Task<ConnectionStatus> IndicesGetTemplateForAllAsync(string name, Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_warmer
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesGetWarmerForAll(Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			var url = "_warmer";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_warmer
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesGetWarmerForAllAsync(Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			var url = "_warmer";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		public ConnectionStatus IndicesGetWarmer(string index, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_warmer".Inject(new { index = Stringify(index) });
			var url = "{0}/_warmer".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesGetWarmerAsync(string index, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_warmer".Inject(new { index = Stringify(index) });
			var url = "{0}/_warmer".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public ConnectionStatus IndicesGetWarmer(string index, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public Task<ConnectionStatus> IndicesGetWarmerAsync(string index, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public ConnectionStatus IndicesGetWarmerForAll(string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_warmer/{name}".Inject(new { name = Stringify(name) });
			var url = "_warmer/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public Task<ConnectionStatus> IndicesGetWarmerForAllAsync(string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_warmer/{name}".Inject(new { name = Stringify(name) });
			var url = "_warmer/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public ConnectionStatus IndicesGetWarmer(string index, string type, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			var url = "{0}/{1}/_warmer/{2}".F(Encoded(index), Encoded(type), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer (supports wildcards); leave empty to get all warmers</param>
		public Task<ConnectionStatus> IndicesGetWarmerAsync(string index, string type, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			var url = "{0}/{1}/_warmer/{2}".F(Encoded(index), Encoded(type), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new GetWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_open
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-open-close.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public ConnectionStatus IndicesOpen(string index, Func<OpenIndexQueryString, OpenIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_open".Inject(new { index = Stringify(index) });
			var url = "{0}/_open".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OpenIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_open
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-open-close.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public Task<ConnectionStatus> IndicesOpenAsync(string index, Func<OpenIndexQueryString, OpenIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_open".Inject(new { index = Stringify(index) });
			var url = "{0}/_open".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OpenIndexQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesOptimizeForAll(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			var url = "_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesOptimizeForAllAsync(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			var url = "_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesOptimize(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_optimize".Inject(new { index = Stringify(index) });
			var url = "{0}/_optimize".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesOptimizeAsync(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_optimize".Inject(new { index = Stringify(index) });
			var url = "{0}/_optimize".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesOptimizeGetForAll(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			var url = "_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesOptimizeGetForAllAsync(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			var url = "_optimize";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesOptimizeGet(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_optimize".Inject(new { index = Stringify(index) });
			var url = "{0}/_optimize".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_optimize
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-optimize.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesOptimizeGetAsync(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_optimize".Inject(new { index = Stringify(index) });
			var url = "{0}/_optimize".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new OptimizeQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>PUT /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the alias should point to (supports wildcards); use `_all` or omit to perform the operation on all indices.</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndicesPutAlias(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the alias should point to (supports wildcards); use `_all` or omit to perform the operation on all indices.</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndicesPutAliasAsync(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndicesPutAliasForAll(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndicesPutAliasForAllAsync(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the alias should point to (supports wildcards); use `_all` or omit to perform the operation on all indices.</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndicesPutAliasPost(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the alias should point to (supports wildcards); use `_all` or omit to perform the operation on all indices.</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndicesPutAliasPostAsync(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_alias/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_alias/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public ConnectionStatus IndicesPutAliasPostForAll(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_alias/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		public Task<ConnectionStatus> IndicesPutAliasPostForAllAsync(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_alias/{name}".Inject(new { name = Stringify(name) });
			var url = "_alias/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesPutAliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the mapping should be added to (supports wildcards); use `_all` or omit to add the mapping on all indices.</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public ConnectionStatus IndicesPutMapping(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mapping".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the mapping should be added to (supports wildcards); use `_all` or omit to add the mapping on all indices.</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public Task<ConnectionStatus> IndicesPutMappingAsync(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mapping".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public ConnectionStatus IndicesPutMappingForAll(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			//var url = "_mapping/{type}".Inject(new { type = Stringify(type) });
			var url = "_mapping/{0}".F(Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public Task<ConnectionStatus> IndicesPutMappingForAllAsync(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			//var url = "_mapping/{type}".Inject(new { type = Stringify(type) });
			var url = "_mapping/{0}".F(Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the mapping should be added to (supports wildcards); use `_all` or omit to add the mapping on all indices.</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public ConnectionStatus IndicesPutMappingPost(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mapping".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mapping
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the mapping should be added to (supports wildcards); use `_all` or omit to add the mapping on all indices.</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public Task<ConnectionStatus> IndicesPutMappingPostAsync(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mapping".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mapping".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public ConnectionStatus IndicesPutMappingPostForAll(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			//var url = "_mapping/{type}".Inject(new { type = Stringify(type) });
			var url = "_mapping/{0}".F(Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_mapping/{type}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-put-mapping.html</pre>	
	    ///</summary>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		public Task<ConnectionStatus> IndicesPutMappingPostForAllAsync(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			//var url = "_mapping/{type}".Inject(new { type = Stringify(type) });
			var url = "_mapping/{0}".F(Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutMappingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-update-settings.html</pre>	
	    ///</summary>
		///<param name="body">The index settings to be updated</param>
		public ConnectionStatus IndicesPutSettingsForAll(object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			//var url = "_settings".Inject(new {  });
			var url = "_settings".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-update-settings.html</pre>	
	    ///</summary>
		///<param name="body">The index settings to be updated</param>
		public Task<ConnectionStatus> IndicesPutSettingsForAllAsync(object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			//var url = "_settings".Inject(new {  });
			var url = "_settings".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-update-settings.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The index settings to be updated</param>
		public ConnectionStatus IndicesPutSettings(string index, object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_settings".Inject(new { index = Stringify(index) });
			var url = "{0}/_settings".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_settings
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-update-settings.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The index settings to be updated</param>
		public Task<ConnectionStatus> IndicesPutSettingsAsync(string index, object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_settings".Inject(new { index = Stringify(index) });
			var url = "{0}/_settings".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateSettingsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public ConnectionStatus IndicesPutTemplateForAll(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public Task<ConnectionStatus> IndicesPutTemplateForAllAsync(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public ConnectionStatus IndicesPutTemplatePostForAll(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_template/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-templates.html</pre>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		public Task<ConnectionStatus> IndicesPutTemplatePostForAllAsync(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_template/{name}".Inject(new { name = Stringify(name) });
			var url = "_template/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutTemplateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmerForAll(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_warmer/{name}".Inject(new { name = Stringify(name) });
			var url = "_warmer/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerForAllAsync(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_warmer/{name}".Inject(new { name = Stringify(name) });
			var url = "_warmer/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmer(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerAsync(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register the warmer for; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmer(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			var url = "{0}/{1}/_warmer/{2}".F(Encoded(index), Encoded(type), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register the warmer for; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerAsync(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			var url = "{0}/{1}/_warmer/{2}".F(Encoded(index), Encoded(type), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmerPostForAll(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_warmer/{name}".Inject(new { name = Stringify(name) });
			var url = "_warmer/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerPostForAllAsync(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
			//var url = "_warmer/{name}".Inject(new { name = Stringify(name) });
			var url = "_warmer/{0}".F(Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmerPost(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerPostAsync(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/_warmer/{name}".Inject(new { index = Stringify(index), name = Stringify(name) });
			var url = "{0}/_warmer/{1}".F(Encoded(index), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register the warmer for; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public ConnectionStatus IndicesPutWarmerPost(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			var url = "{0}/{1}/_warmer/{2}".F(Encoded(index), Encoded(type), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_warmer/{name}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-warmers.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to register the warmer for; use `_all` or omit to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to register the warmer for; leave empty to perform the operation on all types</param>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		public Task<ConnectionStatus> IndicesPutWarmerPostAsync(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
			//var url = "{index}/{type}/_warmer/{name}".Inject(new { index = Stringify(index), type = Stringify(type), name = Stringify(name) });
			var url = "{0}/{1}/_warmer/{2}".F(Encoded(index), Encoded(type), Encoded(name));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PutWarmerQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesRefreshForAll(Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			var url = "_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesRefreshForAllAsync(Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			var url = "_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesRefresh(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_refresh".Inject(new { index = Stringify(index) });
			var url = "{0}/_refresh".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesRefreshAsync(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_refresh".Inject(new { index = Stringify(index) });
			var url = "{0}/_refresh".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesRefreshGetForAll(Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			var url = "_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesRefreshGetForAllAsync(Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			var url = "_refresh";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesRefreshGet(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_refresh".Inject(new { index = Stringify(index) });
			var url = "{0}/_refresh".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_refresh
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-refresh.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesRefreshGetAsync(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_refresh".Inject(new { index = Stringify(index) });
			var url = "{0}/_refresh".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new RefreshQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_segments
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-segments.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesSegmentsForAll(Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
		{
			var url = "_segments";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SegmentsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_segments
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-segments.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesSegmentsForAllAsync(Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
		{
			var url = "_segments";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SegmentsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_segments
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-segments.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesSegments(string index, Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_segments".Inject(new { index = Stringify(index) });
			var url = "{0}/_segments".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SegmentsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_segments
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-segments.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesSegmentsAsync(string index, Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_segments".Inject(new { index = Stringify(index) });
			var url = "{0}/_segments".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SegmentsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-gateway-snapshot.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesSnapshotIndexForAll(Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
		{
			var url = "_gateway/snapshot";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-gateway-snapshot.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesSnapshotIndexForAllAsync(Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
		{
			var url = "_gateway/snapshot";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-gateway-snapshot.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public ConnectionStatus IndicesSnapshotIndex(string index, Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_gateway/snapshot".Inject(new { index = Stringify(index) });
			var url = "{0}/_gateway/snapshot".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/_gateway/snapshot
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-gateway-snapshot.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string for all indices</param>
		public Task<ConnectionStatus> IndicesSnapshotIndexAsync(string index, Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_gateway/snapshot".Inject(new { index = Stringify(index) });
			var url = "{0}/_gateway/snapshot".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesStatsForAll(Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			var url = "_stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesStatsForAllAsync(Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			var url = "_stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned the specific metrics.</param>
		public ConnectionStatus IndicesStatsForAll(string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_stats/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_stats/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned the specific metrics.</param>
		public Task<ConnectionStatus> IndicesStatsForAllAsync(string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_stats/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_stats/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesStats(string index, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_stats".Inject(new { index = Stringify(index) });
			var url = "{0}/_stats".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesStatsAsync(string index, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_stats".Inject(new { index = Stringify(index) });
			var url = "{0}/_stats".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="metric">Limit the information returned the specific metrics.</param>
		public ConnectionStatus IndicesStats(string index, string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "{index}/_stats/{metric}".Inject(new { index = Stringify(index), metric = Stringify(metric) });
			var url = "{0}/_stats/{1}".F(Encoded(index), Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-stats.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="metric">Limit the information returned the specific metrics.</param>
		public Task<ConnectionStatus> IndicesStatsAsync(string index, string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "{index}/_stats/{metric}".Inject(new { index = Stringify(index), metric = Stringify(metric) });
			var url = "{0}/_stats/{1}".F(Encoded(index), Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_status
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-status.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesStatusForAll(Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			var url = "_status";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_status
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-status.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesStatusForAllAsync(Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			var url = "_status";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_status
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-status.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesStatus(string index, Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_status".Inject(new { index = Stringify(index) });
			var url = "{0}/_status".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_status
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-status.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesStatusAsync(string index, Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_status".Inject(new { index = Stringify(index) });
			var url = "{0}/_status".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new IndicesStatusQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="body">The definition of `actions` to perform</param>
		public ConnectionStatus IndicesUpdateAliasesForAll(object body, Func<AliasQueryString, AliasQueryString> queryString = null)
		{
			//var url = "_aliases".Inject(new {  });
			var url = "_aliases".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_aliases
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/indices-aliases.html</pre>	
	    ///</summary>
		///<param name="body">The definition of `actions` to perform</param>
		public Task<ConnectionStatus> IndicesUpdateAliasesForAllAsync(object body, Func<AliasQueryString, AliasQueryString> queryString = null)
		{
			//var url = "_aliases".Inject(new {  });
			var url = "_aliases".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new AliasQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		public ConnectionStatus IndicesValidateQueryGetForAll(Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			var url = "_validate/query";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> IndicesValidateQueryGetForAllAsync(Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			var url = "_validate/query";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus IndicesValidateQueryGet(string index, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_validate/query".Inject(new { index = Stringify(index) });
			var url = "{0}/_validate/query".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> IndicesValidateQueryGetAsync(string index, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_validate/query".Inject(new { index = Stringify(index) });
			var url = "{0}/_validate/query".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		public ConnectionStatus IndicesValidateQueryGet(string index, string type, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_validate/query".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		public Task<ConnectionStatus> IndicesValidateQueryGetAsync(string index, string type, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_validate/query".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="body">The query definition specified with the Query DSL</param>
		public ConnectionStatus IndicesValidateQueryForAll(object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			//var url = "_validate/query".Inject(new {  });
			var url = "_validate/query".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="body">The query definition specified with the Query DSL</param>
		public Task<ConnectionStatus> IndicesValidateQueryForAllAsync(object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			//var url = "_validate/query".Inject(new {  });
			var url = "_validate/query".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The query definition specified with the Query DSL</param>
		public ConnectionStatus IndicesValidateQuery(string index, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_validate/query".Inject(new { index = Stringify(index) });
			var url = "{0}/_validate/query".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The query definition specified with the Query DSL</param>
		public Task<ConnectionStatus> IndicesValidateQueryAsync(string index, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_validate/query".Inject(new { index = Stringify(index) });
			var url = "{0}/_validate/query".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="body">The query definition specified with the Query DSL</param>
		public ConnectionStatus IndicesValidateQuery(string index, string type, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_validate/query".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_validate/query
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-validate.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to restrict the operation; leave empty to perform the operation on all types</param>
		///<param name="body">The query definition specified with the Query DSL</param>
		public Task<ConnectionStatus> IndicesValidateQueryAsync(string index, string type, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_validate/query".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_validate/query".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ValidateQueryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /
	    ///<pre>http://www.elasticsearch.org/guide/</pre>	
	    ///</summary>
		public ConnectionStatus Info(Func<InfoQueryString, InfoQueryString> queryString = null)
		{
			var url = "";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new InfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /
	    ///<pre>http://www.elasticsearch.org/guide/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> InfoAsync(Func<InfoQueryString, InfoQueryString> queryString = null)
		{
			var url = "";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new InfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		public ConnectionStatus MgetGet(Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			var url = "_mget";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> MgetGetAsync(Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			var url = "_mget";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public ConnectionStatus MgetGet(string index, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mget".Inject(new { index = Stringify(index) });
			var url = "{0}/_mget".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		public Task<ConnectionStatus> MgetGetAsync(string index, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mget".Inject(new { index = Stringify(index) });
			var url = "{0}/_mget".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		public ConnectionStatus MgetGet(string index, string type, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mget".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		public Task<ConnectionStatus> MgetGetAsync(string index, string type, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mget".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public ConnectionStatus Mget(object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			//var url = "_mget".Inject(new {  });
			var url = "_mget".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public Task<ConnectionStatus> MgetAsync(object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			//var url = "_mget".Inject(new {  });
			var url = "_mget".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public ConnectionStatus Mget(string index, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mget".Inject(new { index = Stringify(index) });
			var url = "{0}/_mget".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public Task<ConnectionStatus> MgetAsync(string index, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mget".Inject(new { index = Stringify(index) });
			var url = "{0}/_mget".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public ConnectionStatus Mget(string index, string type, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mget".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mget
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-get.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">Document identifiers; can be either `docs` (containing full document information) or `ids` (when index and type is provided in the URL.</param>
		public Task<ConnectionStatus> MgetAsync(string index, string type, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mget".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mget".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_mlt
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-more-like-this.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public ConnectionStatus MltGet(string index, string type, string id, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_mlt".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MoreLikeThisQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_mlt
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-more-like-this.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		public Task<ConnectionStatus> MltGetAsync(string index, string type, string id, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_mlt".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MoreLikeThisQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_mlt
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-more-like-this.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="body">A specific search request definition</param>
		public ConnectionStatus Mlt(string index, string type, string id, object body, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_mlt".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MoreLikeThisQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_mlt
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-more-like-this.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="body">A specific search request definition</param>
		public Task<ConnectionStatus> MltAsync(string index, string type, string id, object body, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_mlt".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_mlt".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MoreLikeThisQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		public ConnectionStatus MpercolateGet(Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			var url = "_mpercolate";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> MpercolateGetAsync(Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			var url = "_mpercolate";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		public ConnectionStatus MpercolateGet(string index, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mpercolate".Inject(new { index = Stringify(index) });
			var url = "{0}/_mpercolate".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		public Task<ConnectionStatus> MpercolateGetAsync(string index, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mpercolate".Inject(new { index = Stringify(index) });
			var url = "{0}/_mpercolate".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		///<param name="type">The type of the document being percolated to use as default.</param>
		public ConnectionStatus MpercolateGet(string index, string type, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mpercolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mpercolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		///<param name="type">The type of the document being percolated to use as default.</param>
		public Task<ConnectionStatus> MpercolateGetAsync(string index, string type, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mpercolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mpercolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="body">The percolate request definitions (header &amp; body pair), separated by newlines</param>
		public ConnectionStatus Mpercolate(object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			//var url = "_mpercolate".Inject(new {  });
			var url = "_mpercolate".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="body">The percolate request definitions (header &amp; body pair), separated by newlines</param>
		public Task<ConnectionStatus> MpercolateAsync(object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			//var url = "_mpercolate".Inject(new {  });
			var url = "_mpercolate".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		///<param name="body">The percolate request definitions (header &amp; body pair), separated by newlines</param>
		public ConnectionStatus Mpercolate(string index, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mpercolate".Inject(new { index = Stringify(index) });
			var url = "{0}/_mpercolate".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		///<param name="body">The percolate request definitions (header &amp; body pair), separated by newlines</param>
		public Task<ConnectionStatus> MpercolateAsync(string index, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mpercolate".Inject(new { index = Stringify(index) });
			var url = "{0}/_mpercolate".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		///<param name="type">The type of the document being percolated to use as default.</param>
		///<param name="body">The percolate request definitions (header &amp; body pair), separated by newlines</param>
		public ConnectionStatus Mpercolate(string index, string type, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mpercolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mpercolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mpercolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated to use as default</param>
		///<param name="type">The type of the document being percolated to use as default.</param>
		///<param name="body">The percolate request definitions (header &amp; body pair), separated by newlines</param>
		public Task<ConnectionStatus> MpercolateAsync(string index, string type, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mpercolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mpercolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MpercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		public ConnectionStatus MsearchGet(Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			var url = "_msearch";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> MsearchGetAsync(Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			var url = "_msearch";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		public ConnectionStatus MsearchGet(string index, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_msearch".Inject(new { index = Stringify(index) });
			var url = "{0}/_msearch".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		public Task<ConnectionStatus> MsearchGetAsync(string index, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_msearch".Inject(new { index = Stringify(index) });
			var url = "{0}/_msearch".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		public ConnectionStatus MsearchGet(string index, string type, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_msearch".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		public Task<ConnectionStatus> MsearchGetAsync(string index, string type, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_msearch".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public ConnectionStatus Msearch(object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			//var url = "_msearch".Inject(new {  });
			var url = "_msearch".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public Task<ConnectionStatus> MsearchAsync(object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			//var url = "_msearch".Inject(new {  });
			var url = "_msearch".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public ConnectionStatus Msearch(string index, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_msearch".Inject(new { index = Stringify(index) });
			var url = "{0}/_msearch".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public Task<ConnectionStatus> MsearchAsync(string index, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_msearch".Inject(new { index = Stringify(index) });
			var url = "{0}/_msearch".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public ConnectionStatus Msearch(string index, string type, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_msearch".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_msearch
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-multi-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to use as default</param>
		///<param name="type">A comma-separated list of document types to use as default</param>
		///<param name="body">The request definitions (metadata-search request definition pairs), separated by newlines</param>
		public Task<ConnectionStatus> MsearchAsync(string index, string type, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_msearch".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_msearch".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MultiSearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		public ConnectionStatus MtermvectorsGet(Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			var url = "_mtermvectors";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> MtermvectorsGetAsync(Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			var url = "_mtermvectors";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		public ConnectionStatus MtermvectorsGet(string index, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mtermvectors".Inject(new { index = Stringify(index) });
			var url = "{0}/_mtermvectors".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		public Task<ConnectionStatus> MtermvectorsGetAsync(string index, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mtermvectors".Inject(new { index = Stringify(index) });
			var url = "{0}/_mtermvectors".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		public ConnectionStatus MtermvectorsGet(string index, string type, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mtermvectors".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mtermvectors".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		public Task<ConnectionStatus> MtermvectorsGetAsync(string index, string type, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mtermvectors".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mtermvectors".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="body">Define ids, parameters or a list of parameters per document here. You must at least provide a list of document ids. See documentation.</param>
		public ConnectionStatus Mtermvectors(object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			//var url = "_mtermvectors".Inject(new {  });
			var url = "_mtermvectors".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="body">Define ids, parameters or a list of parameters per document here. You must at least provide a list of document ids. See documentation.</param>
		public Task<ConnectionStatus> MtermvectorsAsync(object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			//var url = "_mtermvectors".Inject(new {  });
			var url = "_mtermvectors".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="body">Define ids, parameters or a list of parameters per document here. You must at least provide a list of document ids. See documentation.</param>
		public ConnectionStatus Mtermvectors(string index, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mtermvectors".Inject(new { index = Stringify(index) });
			var url = "{0}/_mtermvectors".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="body">Define ids, parameters or a list of parameters per document here. You must at least provide a list of document ids. See documentation.</param>
		public Task<ConnectionStatus> MtermvectorsAsync(string index, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_mtermvectors".Inject(new { index = Stringify(index) });
			var url = "{0}/_mtermvectors".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="body">Define ids, parameters or a list of parameters per document here. You must at least provide a list of document ids. See documentation.</param>
		public ConnectionStatus Mtermvectors(string index, string type, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mtermvectors".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mtermvectors".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_mtermvectors
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-multi-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="body">Define ids, parameters or a list of parameters per document here. You must at least provide a list of document ids. See documentation.</param>
		public Task<ConnectionStatus> MtermvectorsAsync(string index, string type, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_mtermvectors".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_mtermvectors".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new MtermvectorsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-hot-threads.html</pre>	
	    ///</summary>
		public ConnectionStatus NodesHotThreadsForAll(Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
		{
			var url = "_cluster/nodes/hotthreads";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesHotThreadsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-hot-threads.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> NodesHotThreadsForAllAsync(Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
		{
			var url = "_cluster/nodes/hotthreads";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesHotThreadsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-hot-threads.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus NodesHotThreads(string node_id, Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cluster/nodes/{node_id}/hotthreads".Inject(new { node_id = Stringify(node_id) });
			var url = "_cluster/nodes/{0}/hotthreads".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesHotThreadsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_cluster/nodes/{node_id}/hotthreads
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-hot-threads.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> NodesHotThreadsAsync(string node_id, Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cluster/nodes/{node_id}/hotthreads".Inject(new { node_id = Stringify(node_id) });
			var url = "_cluster/nodes/{0}/hotthreads".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesHotThreadsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		public ConnectionStatus NodesInfoForAll(Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			var url = "_nodes";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> NodesInfoForAllAsync(Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			var url = "_nodes";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus NodesInfo(string node_id, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_nodes/{node_id}".Inject(new { node_id = Stringify(node_id) });
			var url = "_nodes/{0}".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> NodesInfoAsync(string node_id, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_nodes/{node_id}".Inject(new { node_id = Stringify(node_id) });
			var url = "_nodes/{0}".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		///<param name="metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		public ConnectionStatus NodesInfoForAll(string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_nodes/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		///<param name="metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		public Task<ConnectionStatus> NodesInfoForAllAsync(string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_nodes/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		public ConnectionStatus NodesInfo(string node_id, string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/{node_id}/{metric}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric) });
			var url = "_nodes/{0}/{1}".F(Encoded(node_id), Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-info.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		public Task<ConnectionStatus> NodesInfoAsync(string node_id, string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/{node_id}/{metric}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric) });
			var url = "_nodes/{0}/{1}".F(Encoded(node_id), Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesInfoQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_shutdown
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-shutdown.html</pre>	
	    ///</summary>
		public ConnectionStatus NodesShutdownForAll(Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
		{
			var url = "_shutdown";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesShutdownQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_shutdown
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-shutdown.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> NodesShutdownForAllAsync(Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
		{
			var url = "_shutdown";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesShutdownQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_cluster/nodes/{node_id}/_shutdown
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-shutdown.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to perform the operation on; use `_local` to perform the operation on the node you&#39;re connected to, leave empty to perform the operation on all nodes</param>
		public ConnectionStatus NodesShutdown(string node_id, Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cluster/nodes/{node_id}/_shutdown".Inject(new { node_id = Stringify(node_id) });
			var url = "_cluster/nodes/{0}/_shutdown".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesShutdownQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_cluster/nodes/{node_id}/_shutdown
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-shutdown.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to perform the operation on; use `_local` to perform the operation on the node you&#39;re connected to, leave empty to perform the operation on all nodes</param>
		public Task<ConnectionStatus> NodesShutdownAsync(string node_id, Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_cluster/nodes/{node_id}/_shutdown".Inject(new { node_id = Stringify(node_id) });
			var url = "_cluster/nodes/{0}/_shutdown".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesShutdownQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		public ConnectionStatus NodesStatsForAll(Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			var url = "_nodes/stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> NodesStatsForAllAsync(Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			var url = "_nodes/stats";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public ConnectionStatus NodesStats(string node_id, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_nodes/{node_id}/stats".Inject(new { node_id = Stringify(node_id) });
			var url = "_nodes/{0}/stats".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		public Task<ConnectionStatus> NodesStatsAsync(string node_id, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			//var url = "_nodes/{node_id}/stats".Inject(new { node_id = Stringify(node_id) });
			var url = "_nodes/{0}/stats".F(Encoded(node_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		public ConnectionStatus NodesStatsForAll(string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/stats/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_nodes/stats/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		public Task<ConnectionStatus> NodesStatsForAllAsync(string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/stats/{metric}".Inject(new { metric = Stringify(metric) });
			var url = "_nodes/stats/{0}".F(Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		public ConnectionStatus NodesStats(string node_id, string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/{node_id}/stats/{metric}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric) });
			var url = "_nodes/{0}/stats/{1}".F(Encoded(node_id), Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/{metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		public Task<ConnectionStatus> NodesStatsAsync(string node_id, string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			//var url = "_nodes/{node_id}/stats/{metric}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric) });
			var url = "_nodes/{0}/stats/{1}".F(Encoded(node_id), Encoded(metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/{metric}/{index_metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		///<param name="index_metric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#39;t used if `indices` (or `all`) metric isn&#39;t specified.</param>
		public ConnectionStatus NodesStatsForAll(string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
			//var url = "_nodes/stats/{metric}/{index_metric}".Inject(new { metric = Stringify(metric), index_metric = Stringify(index_metric) });
			var url = "_nodes/stats/{0}/{1}".F(Encoded(metric), Encoded(index_metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/stats/{metric}/{index_metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		///<param name="index_metric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#39;t used if `indices` (or `all`) metric isn&#39;t specified.</param>
		public Task<ConnectionStatus> NodesStatsForAllAsync(string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
			//var url = "_nodes/stats/{metric}/{index_metric}".Inject(new { metric = Stringify(metric), index_metric = Stringify(index_metric) });
			var url = "_nodes/stats/{0}/{1}".F(Encoded(metric), Encoded(index_metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/{metric}/{index_metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		///<param name="index_metric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#39;t used if `indices` (or `all`) metric isn&#39;t specified.</param>
		public ConnectionStatus NodesStats(string node_id, string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
			//var url = "_nodes/{node_id}/stats/{metric}/{index_metric}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric), index_metric = Stringify(index_metric) });
			var url = "_nodes/{0}/stats/{1}/{2}".F(Encoded(node_id), Encoded(metric), Encoded(index_metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_nodes/{node_id}/stats/{metric}/{index_metric}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/cluster-nodes-stats.html</pre>	
	    ///</summary>
		///<param name="node_id">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#39;re connecting to, leave empty to get information from all nodes</param>
		///<param name="metric">Limit the information returned to the specified metrics</param>
		///<param name="index_metric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#39;t used if `indices` (or `all`) metric isn&#39;t specified.</param>
		public Task<ConnectionStatus> NodesStatsAsync(string node_id, string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
			//var url = "_nodes/{node_id}/stats/{metric}/{index_metric}".Inject(new { node_id = Stringify(node_id), metric = Stringify(metric), index_metric = Stringify(index_metric) });
			var url = "_nodes/{0}/stats/{1}/{2}".F(Encoded(node_id), Encoded(metric), Encoded(index_metric));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new NodesStatsQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		public ConnectionStatus PercolateGet(string index, string type, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		public Task<ConnectionStatus> PercolateGetAsync(string index, string type, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		public ConnectionStatus PercolateGet(string index, string type, string id, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		public Task<ConnectionStatus> PercolateGetAsync(string index, string type, string id, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="body">The percolator request definition using the percolate DSL</param>
		public ConnectionStatus Percolate(string index, string type, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="body">The percolator request definition using the percolate DSL</param>
		public Task<ConnectionStatus> PercolateAsync(string index, string type, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_percolate".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		///<param name="body">The percolator request definition using the percolate DSL</param>
		public ConnectionStatus Percolate(string index, string type, string id, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_percolate
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-percolate.html</pre>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="id">Substitute the document in the request body with a document that is known by the specified id. On top of the id, the index and type parameter will be used to retrieve the document from within the cluster.</param>
		///<param name="body">The percolator request definition using the percolate DSL</param>
		public Task<ConnectionStatus> PercolateAsync(string index, string type, string id, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_percolate".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_percolate".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PercolateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>HEAD /
	    ///<pre>http://www.elasticsearch.org/guide/</pre>	
	    ///</summary>
		public ConnectionStatus Ping(Func<PingQueryString, PingQueryString> queryString = null)
		{
			var url = "";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>HEAD /
	    ///<pre>http://www.elasticsearch.org/guide/</pre>	
	    ///</summary>
		public Task<ConnectionStatus> PingAsync(Func<PingQueryString, PingQueryString> queryString = null)
		{
			var url = "";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new PingQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("HEAD", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		public ConnectionStatus ScrollGet(Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			var url = "_search/scroll";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> ScrollGetAsync(Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			var url = "_search/scroll";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		public ConnectionStatus ScrollGet(string scroll_id, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
			//var url = "_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			var url = "_search/scroll/{0}".F(Encoded(scroll_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		public Task<ConnectionStatus> ScrollGetAsync(string scroll_id, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
			//var url = "_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			var url = "_search/scroll/{0}".F(Encoded(scroll_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public ConnectionStatus Scroll(object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			//var url = "_search/scroll".Inject(new {  });
			var url = "_search/scroll".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search/scroll
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public Task<ConnectionStatus> ScrollAsync(object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			//var url = "_search/scroll".Inject(new {  });
			var url = "_search/scroll".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public ConnectionStatus Scroll(string scroll_id, object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
			//var url = "_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			var url = "_search/scroll/{0}".F(Encoded(scroll_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search/scroll/{scroll_id}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-scroll.html</pre>	
	    ///</summary>
		///<param name="scroll_id">The scroll ID</param>
		///<param name="body">The scroll ID if not passed by URL or query parameter.</param>
		public Task<ConnectionStatus> ScrollAsync(string scroll_id, object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
			//var url = "_search/scroll/{scroll_id}".Inject(new { scroll_id = Stringify(scroll_id) });
			var url = "_search/scroll/{0}".F(Encoded(scroll_id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new ScrollQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		public ConnectionStatus SearchGet(Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			var url = "_search";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> SearchGetAsync(Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			var url = "_search";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus SearchGet(string index, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_search".Inject(new { index = Stringify(index) });
			var url = "{0}/_search".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> SearchGetAsync(string index, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_search".Inject(new { index = Stringify(index) });
			var url = "{0}/_search".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		public ConnectionStatus SearchGet(string index, string type, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_search".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		public Task<ConnectionStatus> SearchGetAsync(string index, string type, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_search".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="body">The search definition using the Query DSL</param>
		public ConnectionStatus Search(object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			//var url = "_search".Inject(new {  });
			var url = "_search".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="body">The search definition using the Query DSL</param>
		public Task<ConnectionStatus> SearchAsync(object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			//var url = "_search".Inject(new {  });
			var url = "_search".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The search definition using the Query DSL</param>
		public ConnectionStatus Search(string index, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_search".Inject(new { index = Stringify(index) });
			var url = "{0}/_search".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The search definition using the Query DSL</param>
		public Task<ConnectionStatus> SearchAsync(string index, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_search".Inject(new { index = Stringify(index) });
			var url = "{0}/_search".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		///<param name="body">The search definition using the Query DSL</param>
		public ConnectionStatus Search(string index, string type, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_search".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/_search
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to search; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="type">A comma-separated list of document types to search; leave empty to perform the operation on all types</param>
		///<param name="body">The search definition using the Query DSL</param>
		public Task<ConnectionStatus> SearchAsync(string index, string type, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			//var url = "{index}/{type}/_search".Inject(new { index = Stringify(index), type = Stringify(type) });
			var url = "{0}/{1}/_search".F(Encoded(index), Encoded(type));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SearchQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">The snapshot definition</param>
		public ConnectionStatus SnapshotCreate(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">The snapshot definition</param>
		public Task<ConnectionStatus> SnapshotCreateAsync(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">The snapshot definition</param>
		public ConnectionStatus SnapshotCreatePost(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">The snapshot definition</param>
		public Task<ConnectionStatus> SnapshotCreatePostAsync(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>PUT /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="body">The repository definition</param>
		public ConnectionStatus SnapshotCreateRepository(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("PUT", url, body, queryString: nv);
		}
		
		///<summary>PUT /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="body">The repository definition</param>
		public Task<ConnectionStatus> SnapshotCreateRepositoryAsync(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("PUT", url, body, queryString: nv);
		}
		
		///<summary>POST /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="body">The repository definition</param>
		public ConnectionStatus SnapshotCreateRepositoryPost(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="body">The repository definition</param>
		public Task<ConnectionStatus> SnapshotCreateRepositoryPostAsync(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotCreateRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>DELETE /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		public ConnectionStatus SnapshotDelete(string repository, string snapshot, Func<SnapshotDeleteQueryString, SnapshotDeleteQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotDeleteQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		public Task<ConnectionStatus> SnapshotDeleteAsync(string repository, string snapshot, Func<SnapshotDeleteQueryString, SnapshotDeleteQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotDeleteQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A comma-separated list of repository names</param>
		public ConnectionStatus SnapshotDeleteRepository(string repository, Func<SnapshotDeleteRepositoryQueryString, SnapshotDeleteRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotDeleteRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>DELETE /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A comma-separated list of repository names</param>
		public Task<ConnectionStatus> SnapshotDeleteRepositoryAsync(string repository, Func<SnapshotDeleteRepositoryQueryString, SnapshotDeleteRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotDeleteRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("DELETE", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A comma-separated list of snapshot names</param>
		public ConnectionStatus SnapshotGet(string repository, string snapshot, Func<SnapshotGetQueryString, SnapshotGetQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_snapshot/{repository}/{snapshot}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A comma-separated list of snapshot names</param>
		public Task<ConnectionStatus> SnapshotGetAsync(string repository, string snapshot, Func<SnapshotGetQueryString, SnapshotGetQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotGetQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_snapshot
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		public ConnectionStatus SnapshotGetRepository(Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
		{
			var url = "_snapshot";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotGetRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_snapshot
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> SnapshotGetRepositoryAsync(Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
		{
			var url = "_snapshot";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotGetRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A comma-separated list of repository names</param>
		public ConnectionStatus SnapshotGetRepository(string repository, Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotGetRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_snapshot/{repository}
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A comma-separated list of repository names</param>
		public Task<ConnectionStatus> SnapshotGetRepositoryAsync(string repository, Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			//var url = "_snapshot/{repository}".Inject(new { repository = Stringify(repository) });
			var url = "_snapshot/{0}".F(Encoded(repository));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotGetRepositoryQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /_snapshot/{repository}/{snapshot}/_restore
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">Details of what to restore</param>
		public ConnectionStatus SnapshotRestore(string repository, string snapshot, object body, Func<SnapshotRestoreQueryString, SnapshotRestoreQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}/_restore".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}/_restore".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotRestoreQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_snapshot/{repository}/{snapshot}/_restore
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/modules-snapshots.html</pre>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">Details of what to restore</param>
		public Task<ConnectionStatus> SnapshotRestoreAsync(string repository, string snapshot, object body, Func<SnapshotRestoreQueryString, SnapshotRestoreQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			//var url = "_snapshot/{repository}/{snapshot}/_restore".Inject(new { repository = Stringify(repository), snapshot = Stringify(snapshot) });
			var url = "_snapshot/{0}/{1}/_restore".F(Encoded(repository), Encoded(snapshot));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SnapshotRestoreQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="body">The request definition</param>
		public ConnectionStatus Suggest(object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			//var url = "_suggest".Inject(new {  });
			var url = "_suggest".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="body">The request definition</param>
		public Task<ConnectionStatus> SuggestAsync(object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			//var url = "_suggest".Inject(new {  });
			var url = "_suggest".F();
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The request definition</param>
		public ConnectionStatus Suggest(string index, object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_suggest".Inject(new { index = Stringify(index) });
			var url = "{0}/_suggest".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		///<param name="body">The request definition</param>
		public Task<ConnectionStatus> SuggestAsync(string index, object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_suggest".Inject(new { index = Stringify(index) });
			var url = "{0}/_suggest".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>GET /_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		public ConnectionStatus SuggestGet(Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			var url = "_suggest";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		public Task<ConnectionStatus> SuggestGetAsync(Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			var url = "_suggest";
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public ConnectionStatus SuggestGet(string index, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_suggest".Inject(new { index = Stringify(index) });
			var url = "{0}/_suggest".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/_suggest
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-search.html</pre>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to restrict the operation; use `_all` or empty string to perform the operation on all indices</param>
		public Task<ConnectionStatus> SuggestGetAsync(string index, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			//var url = "{index}/_suggest".Inject(new { index = Stringify(index) });
			var url = "{0}/_suggest".F(Encoded(index));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new SuggestQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_termvector
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="id">The id of the document.</param>
		public ConnectionStatus TermvectorGet(string index, string type, string id, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_termvector".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_termvector".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new TermvectorQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("GET", url, data: null, queryString: nv);
		}
		
		///<summary>GET /{index}/{type}/{id}/_termvector
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="id">The id of the document.</param>
		public Task<ConnectionStatus> TermvectorGetAsync(string index, string type, string id, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_termvector".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_termvector".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new TermvectorQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("GET", url, data: null, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_termvector
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="id">The id of the document.</param>
		///<param name="body">Define parameters. See documentation.</param>
		public ConnectionStatus Termvector(string index, string type, string id, object body, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_termvector".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_termvector".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new TermvectorQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_termvector
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-termvectors.html</pre>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="id">The id of the document.</param>
		///<param name="body">Define parameters. See documentation.</param>
		public Task<ConnectionStatus> TermvectorAsync(string index, string type, string id, object body, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_termvector".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_termvector".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new TermvectorQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_update
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-update.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The request definition using either `script` or partial `doc`</param>
		public ConnectionStatus Update(string index, string type, string id, object body, Func<UpdateQueryString, UpdateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_update".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_update".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequest("POST", url, body, queryString: nv);
		}
		
		///<summary>POST /{index}/{type}/{id}/_update
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-update.html</pre>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The request definition using either `script` or partial `doc`</param>
		public Task<ConnectionStatus> UpdateAsync(string index, string type, string id, object body, Func<UpdateQueryString, UpdateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
			//var url = "{index}/{type}/{id}/_update".Inject(new { index = Stringify(index), type = Stringify(type), id = Stringify(id) });
			var url = "{0}/{1}/{2}/_update".F(Encoded(index), Encoded(type), Encoded(id));
			NameValueCollection nv = null;
			if (queryString != null)
			{
				var qs = queryString(new UpdateQueryString());
				if (qs != null) nv = this.ToNameValueCollection(qs);
			}

			return this.DoRequestAsync("POST", url, body, queryString: nv);
		}
	
	  }
	  }
	
