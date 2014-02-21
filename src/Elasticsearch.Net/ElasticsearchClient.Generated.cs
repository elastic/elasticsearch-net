using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Generated File Please Do Not Edit Manually
	
namespace Elasticsearch.Net
{
	///<summary>
	///Raw operations with elasticsearch
	///</summary>
	public partial class ElasticsearchClient : IElasticsearchClient
	{
	
		
		///<summary>POST /_bulk
	    ///<pre>http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/docs-bulk.html</pre>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		public ElasticsearchResponse Bulk(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> BulkAsync(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Bulk(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> BulkAsync(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Bulk(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> BulkAsync(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse BulkPut(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> BulkPutAsync(object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
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
		public ElasticsearchResponse BulkPut(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> BulkPutAsync(string index, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse BulkPut(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> BulkPutAsync(string index, string type, object body, Func<BulkQueryString, BulkQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse CatAliases(Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatAliasesAsync(Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
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
		public ElasticsearchResponse CatAliases(string name, Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> CatAliasesAsync(string name, Func<CatAliasesQueryString, CatAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse CatAllocation(Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatAllocationAsync(Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
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
		public ElasticsearchResponse CatAllocation(string node_id, Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public Task<ElasticsearchResponse> CatAllocationAsync(string node_id, Func<CatAllocationQueryString, CatAllocationQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public ElasticsearchResponse CatCount(Func<CatCountQueryString, CatCountQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatCountAsync(Func<CatCountQueryString, CatCountQueryString> queryString = null)
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
		public ElasticsearchResponse CatCount(string index, Func<CatCountQueryString, CatCountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> CatCountAsync(string index, Func<CatCountQueryString, CatCountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse CatHealth(Func<CatHealthQueryString, CatHealthQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatHealthAsync(Func<CatHealthQueryString, CatHealthQueryString> queryString = null)
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
		public ElasticsearchResponse CatHelp(Func<CatHelpQueryString, CatHelpQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatHelpAsync(Func<CatHelpQueryString, CatHelpQueryString> queryString = null)
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
		public ElasticsearchResponse CatIndices(Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatIndicesAsync(Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
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
		public ElasticsearchResponse CatIndices(string index, Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> CatIndicesAsync(string index, Func<CatIndicesQueryString, CatIndicesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse CatMaster(Func<CatMasterQueryString, CatMasterQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatMasterAsync(Func<CatMasterQueryString, CatMasterQueryString> queryString = null)
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
		public ElasticsearchResponse CatNodes(Func<CatNodesQueryString, CatNodesQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatNodesAsync(Func<CatNodesQueryString, CatNodesQueryString> queryString = null)
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
		public ElasticsearchResponse CatPendingTasks(Func<CatPendingTasksQueryString, CatPendingTasksQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatPendingTasksAsync(Func<CatPendingTasksQueryString, CatPendingTasksQueryString> queryString = null)
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
		public ElasticsearchResponse CatRecovery(Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatRecoveryAsync(Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
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
		public ElasticsearchResponse CatRecovery(string index, Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> CatRecoveryAsync(string index, Func<CatRecoveryQueryString, CatRecoveryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse CatShards(Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatShardsAsync(Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
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
		public ElasticsearchResponse CatShards(string index, Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> CatShardsAsync(string index, Func<CatShardsQueryString, CatShardsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse CatThreadPool(Func<CatThreadPoolQueryString, CatThreadPoolQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CatThreadPoolAsync(Func<CatThreadPoolQueryString, CatThreadPoolQueryString> queryString = null)
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
		public ElasticsearchResponse ClearScroll(string scroll_id, Func<ClearScrollQueryString, ClearScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
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
		public Task<ElasticsearchResponse> ClearScrollAsync(string scroll_id, Func<ClearScrollQueryString, ClearScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
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
		public ElasticsearchResponse ClusterGetSettings(Func<ClusterGetSettingsQueryString, ClusterGetSettingsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsQueryString, ClusterGetSettingsQueryString> queryString = null)
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
		public ElasticsearchResponse ClusterHealth(Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> ClusterHealthAsync(Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
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
		public ElasticsearchResponse ClusterHealth(string index, Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> ClusterHealthAsync(string index, Func<ClusterHealthQueryString, ClusterHealthQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse ClusterPendingTasks(Func<ClusterPendingTasksQueryString, ClusterPendingTasksQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksQueryString, ClusterPendingTasksQueryString> queryString = null)
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
		public ElasticsearchResponse ClusterPutSettings(object body, Func<ClusterPutSettingsQueryString, ClusterPutSettingsQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> ClusterPutSettingsAsync(object body, Func<ClusterPutSettingsQueryString, ClusterPutSettingsQueryString> queryString = null)
		{
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
		public ElasticsearchResponse ClusterReroute(object body, Func<ClusterRerouteQueryString, ClusterRerouteQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> ClusterRerouteAsync(object body, Func<ClusterRerouteQueryString, ClusterRerouteQueryString> queryString = null)
		{
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
		public ElasticsearchResponse ClusterState(Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> ClusterStateAsync(Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
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
		public ElasticsearchResponse ClusterState(string metric, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> ClusterStateAsync(string metric, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse ClusterState(string metric, string index, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> ClusterStateAsync(string metric, string index, Func<ClusterStateQueryString, ClusterStateQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse ClusterStats(Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> ClusterStatsAsync(Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
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
		public ElasticsearchResponse ClusterStats(string node_id, Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public Task<ElasticsearchResponse> ClusterStatsAsync(string node_id, Func<ClusterStatsQueryString, ClusterStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public ElasticsearchResponse Count(object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> CountAsync(object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Count(string index, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> CountAsync(string index, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Count(string index, string type, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> CountAsync(string index, string type, object body, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse CountGet(Func<CountQueryString, CountQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> CountGetAsync(Func<CountQueryString, CountQueryString> queryString = null)
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
		public ElasticsearchResponse CountGet(string index, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> CountGetAsync(string index, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse CountGet(string index, string type, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> CountGetAsync(string index, string type, Func<CountQueryString, CountQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse CountPercolateGet(string index, string type, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> CountPercolateGetAsync(string index, string type, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse CountPercolateGet(string index, string type, string id, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> CountPercolateGetAsync(string index, string type, string id, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse CountPercolate(string index, string type, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> CountPercolateAsync(string index, string type, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse CountPercolate(string index, string type, string id, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> CountPercolateAsync(string index, string type, string id, object body, Func<CountPercolateQueryString, CountPercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Delete(string index, string type, string id, Func<DeleteQueryString, DeleteQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> DeleteAsync(string index, string type, string id, Func<DeleteQueryString, DeleteQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse DeleteByQuery(string index, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> DeleteByQueryAsync(string index, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse DeleteByQuery(string index, string type, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> DeleteByQueryAsync(string index, string type, object body, Func<DeleteByQueryQueryString, DeleteByQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Exists(string index, string type, string id, Func<ExistsQueryString, ExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> ExistsAsync(string index, string type, string id, Func<ExistsQueryString, ExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse ExplainGet(string index, string type, string id, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> ExplainGetAsync(string index, string type, string id, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Explain(string index, string type, string id, object body, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> ExplainAsync(string index, string type, string id, object body, Func<ExplainQueryString, ExplainQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Get(string index, string type, string id, Func<GetQueryString, GetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> GetAsync(string index, string type, string id, Func<GetQueryString, GetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse GetSource(string index, string type, string id, Func<GetSourceQueryString, GetSourceQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> GetSourceAsync(string index, string type, string id, Func<GetSourceQueryString, GetSourceQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Index(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndexAsync(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Index(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> IndexAsync(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse IndexPut(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndexPutAsync(string index, string type, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndexPut(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> IndexPutAsync(string index, string type, string id, object body, Func<IndexQueryString, IndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse IndicesAnalyzeGetForAll(Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesAnalyzeGetForAllAsync(Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesAnalyzeGet(string index, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesAnalyzeGetAsync(string index, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesAnalyzeForAll(object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> IndicesAnalyzeForAllAsync(object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
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
		public ElasticsearchResponse IndicesAnalyze(string index, object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesAnalyzeAsync(string index, object body, Func<AnalyzeQueryString, AnalyzeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesClearCacheForAll(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesClearCacheForAllAsync(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesClearCache(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesClearCacheAsync(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesClearCacheGetForAll(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesClearCacheGetForAllAsync(Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesClearCacheGet(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesClearCacheGetAsync(string index, Func<ClearCacheQueryString, ClearCacheQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesClose(string index, Func<CloseIndexQueryString, CloseIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesCloseAsync(string index, Func<CloseIndexQueryString, CloseIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesCreate(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesCreateAsync(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesCreatePost(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesCreatePostAsync(string index, object body, Func<CreateIndexQueryString, CreateIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesDelete(string index, Func<DeleteIndexQueryString, DeleteIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesDeleteAsync(string index, Func<DeleteIndexQueryString, DeleteIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesDeleteAlias(string index, string name, Func<IndicesDeleteAliasQueryString, IndicesDeleteAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesDeleteAliasAsync(string index, string name, Func<IndicesDeleteAliasQueryString, IndicesDeleteAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesDeleteMapping(string index, string type, Func<DeleteMappingQueryString, DeleteMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesDeleteMappingAsync(string index, string type, Func<DeleteMappingQueryString, DeleteMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesDeleteTemplateForAll(string name, Func<DeleteTemplateQueryString, DeleteTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesDeleteTemplateForAllAsync(string name, Func<DeleteTemplateQueryString, DeleteTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesDeleteWarmer(string index, string name, Func<DeleteWarmerQueryString, DeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesDeleteWarmerAsync(string index, string name, Func<DeleteWarmerQueryString, DeleteWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesExists(string index, Func<IndexExistsQueryString, IndexExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesExistsAsync(string index, Func<IndexExistsQueryString, IndexExistsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesExistsAliasForAll(string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesExistsAliasForAllAsync(string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesExistsAlias(string index, string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesExistsAliasAsync(string index, string name, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesExistsAlias(string index, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesExistsAliasAsync(string index, Func<IndicesExistsAliasQueryString, IndicesExistsAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesExistsTemplateForAll(string name, Func<IndicesExistsTemplateQueryString, IndicesExistsTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesExistsTemplateForAllAsync(string name, Func<IndicesExistsTemplateQueryString, IndicesExistsTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesExistsType(string index, string type, Func<IndicesExistsTypeQueryString, IndicesExistsTypeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesExistsTypeAsync(string index, string type, Func<IndicesExistsTypeQueryString, IndicesExistsTypeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesFlushForAll(Func<FlushQueryString, FlushQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesFlushForAllAsync(Func<FlushQueryString, FlushQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesFlush(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesFlushAsync(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesFlushGetForAll(Func<FlushQueryString, FlushQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesFlushGetForAllAsync(Func<FlushQueryString, FlushQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesFlushGet(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesFlushGetAsync(string index, Func<FlushQueryString, FlushQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesGetAliasForAll(Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesGetAliasForAllAsync(Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesGetAliasForAll(string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetAliasForAllAsync(string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetAlias(string index, string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetAliasAsync(string index, string name, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetAlias(string index, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesGetAliasAsync(string index, Func<GetAliasesQueryString, GetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesGetAliasesForAll(Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesGetAliasesForAllAsync(Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesGetAliases(string index, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesGetAliasesAsync(string index, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesGetAliases(string index, string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetAliasesAsync(string index, string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetAliasesForAll(string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetAliasesForAllAsync(string name, Func<IndicesGetAliasesQueryString, IndicesGetAliasesQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetFieldMappingForAll(string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			field.ThrowIfNullOrEmpty("field");
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
		public Task<ElasticsearchResponse> IndicesGetFieldMappingForAllAsync(string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			field.ThrowIfNullOrEmpty("field");
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
		public ElasticsearchResponse IndicesGetFieldMapping(string index, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			field.ThrowIfNullOrEmpty("field");
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
		public Task<ElasticsearchResponse> IndicesGetFieldMappingAsync(string index, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			field.ThrowIfNullOrEmpty("field");
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
		public ElasticsearchResponse IndicesGetFieldMappingForAll(string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
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
		public Task<ElasticsearchResponse> IndicesGetFieldMappingForAllAsync(string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
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
		public ElasticsearchResponse IndicesGetFieldMapping(string index, string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
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
		public Task<ElasticsearchResponse> IndicesGetFieldMappingAsync(string index, string type, string field, Func<IndicesGetFieldMappingQueryString, IndicesGetFieldMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			field.ThrowIfNullOrEmpty("field");
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
		public ElasticsearchResponse IndicesGetMappingForAll(Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesGetMappingForAllAsync(Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesGetMapping(string index, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesGetMappingAsync(string index, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesGetMappingForAll(string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesGetMappingForAllAsync(string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesGetMapping(string index, string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesGetMappingAsync(string index, string type, Func<GetMappingQueryString, GetMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesGetSettingsForAll(Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesGetSettingsForAllAsync(Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesGetSettings(string index, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesGetSettingsAsync(string index, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesGetSettings(string index, string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetSettingsAsync(string index, string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetSettingsForAll(string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetSettingsForAllAsync(string name, Func<GetIndexSettingsQueryString, GetIndexSettingsQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetTemplateForAll(Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesGetTemplateForAllAsync(Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesGetTemplateForAll(string name, Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetTemplateForAllAsync(string name, Func<GetTemplateQueryString, GetTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetWarmerForAll(Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesGetWarmerForAllAsync(Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesGetWarmer(string index, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesGetWarmerAsync(string index, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesGetWarmer(string index, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetWarmerAsync(string index, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetWarmerForAll(string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetWarmerForAllAsync(string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesGetWarmer(string index, string type, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesGetWarmerAsync(string index, string type, string name, Func<GetWarmerQueryString, GetWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesOpen(string index, Func<OpenIndexQueryString, OpenIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesOpenAsync(string index, Func<OpenIndexQueryString, OpenIndexQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesOptimizeForAll(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesOptimizeForAllAsync(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesOptimize(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesOptimizeAsync(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesOptimizeGetForAll(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesOptimizeGetForAllAsync(Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesOptimizeGet(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesOptimizeGetAsync(string index, Func<OptimizeQueryString, OptimizeQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesPutAlias(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutAliasAsync(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutAliasForAll(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutAliasForAllAsync(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutAliasPost(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutAliasPostAsync(string index, string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutAliasPostForAll(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutAliasPostForAllAsync(string name, object body, Func<IndicesPutAliasQueryString, IndicesPutAliasQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutMapping(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesPutMappingAsync(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesPutMappingForAll(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesPutMappingForAllAsync(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesPutMappingPost(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesPutMappingPostAsync(string index, string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesPutMappingPostForAll(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesPutMappingPostForAllAsync(string type, object body, Func<PutMappingQueryString, PutMappingQueryString> queryString = null)
		{
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesPutSettingsForAll(object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> IndicesPutSettingsForAllAsync(object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
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
		public ElasticsearchResponse IndicesPutSettings(string index, object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesPutSettingsAsync(string index, object body, Func<UpdateSettingsQueryString, UpdateSettingsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesPutTemplateForAll(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutTemplateForAllAsync(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutTemplatePostForAll(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutTemplatePostForAllAsync(string name, object body, Func<PutTemplateQueryString, PutTemplateQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutWarmerForAll(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutWarmerForAllAsync(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutWarmer(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutWarmerAsync(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutWarmer(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutWarmerAsync(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutWarmerPostForAll(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutWarmerPostForAllAsync(string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutWarmerPost(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutWarmerPostAsync(string index, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesPutWarmerPost(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
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
		public Task<ElasticsearchResponse> IndicesPutWarmerPostAsync(string index, string type, string name, object body, Func<PutWarmerQueryString, PutWarmerQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			name.ThrowIfNullOrEmpty("name");
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
		public ElasticsearchResponse IndicesRefreshForAll(Func<RefreshQueryString, RefreshQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesRefreshForAllAsync(Func<RefreshQueryString, RefreshQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesRefresh(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesRefreshAsync(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesRefreshGetForAll(Func<RefreshQueryString, RefreshQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesRefreshGetForAllAsync(Func<RefreshQueryString, RefreshQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesRefreshGet(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesRefreshGetAsync(string index, Func<RefreshQueryString, RefreshQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesSegmentsForAll(Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesSegmentsForAllAsync(Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesSegments(string index, Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesSegmentsAsync(string index, Func<SegmentsQueryString, SegmentsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesSnapshotIndexForAll(Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesSnapshotIndexForAllAsync(Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesSnapshotIndex(string index, Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesSnapshotIndexAsync(string index, Func<SnapshotQueryString, SnapshotQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesStatsForAll(Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesStatsForAllAsync(Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesStatsForAll(string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> IndicesStatsForAllAsync(string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse IndicesStats(string index, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesStatsAsync(string index, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesStats(string index, string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> IndicesStatsAsync(string index, string metric, Func<IndicesStatsQueryString, IndicesStatsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse IndicesStatusForAll(Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesStatusForAllAsync(Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesStatus(string index, Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesStatusAsync(string index, Func<IndicesStatusQueryString, IndicesStatusQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesUpdateAliasesForAll(object body, Func<AliasQueryString, AliasQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> IndicesUpdateAliasesForAllAsync(object body, Func<AliasQueryString, AliasQueryString> queryString = null)
		{
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
		public ElasticsearchResponse IndicesValidateQueryGetForAll(Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> IndicesValidateQueryGetForAllAsync(Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
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
		public ElasticsearchResponse IndicesValidateQueryGet(string index, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesValidateQueryGetAsync(string index, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesValidateQueryGet(string index, string type, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesValidateQueryGetAsync(string index, string type, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse IndicesValidateQueryForAll(object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> IndicesValidateQueryForAllAsync(object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
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
		public ElasticsearchResponse IndicesValidateQuery(string index, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> IndicesValidateQueryAsync(string index, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse IndicesValidateQuery(string index, string type, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> IndicesValidateQueryAsync(string index, string type, object body, Func<ValidateQueryQueryString, ValidateQueryQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Info(Func<InfoQueryString, InfoQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> InfoAsync(Func<InfoQueryString, InfoQueryString> queryString = null)
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
		public ElasticsearchResponse MgetGet(Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> MgetGetAsync(Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
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
		public ElasticsearchResponse MgetGet(string index, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MgetGetAsync(string index, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse MgetGet(string index, string type, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MgetGetAsync(string index, string type, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Mget(object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> MgetAsync(object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Mget(string index, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MgetAsync(string index, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Mget(string index, string type, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MgetAsync(string index, string type, object body, Func<MultiGetQueryString, MultiGetQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse MltGet(string index, string type, string id, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> MltGetAsync(string index, string type, string id, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Mlt(string index, string type, string id, object body, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> MltAsync(string index, string type, string id, object body, Func<MoreLikeThisQueryString, MoreLikeThisQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse MpercolateGet(Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> MpercolateGetAsync(Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
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
		public ElasticsearchResponse MpercolateGet(string index, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MpercolateGetAsync(string index, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse MpercolateGet(string index, string type, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MpercolateGetAsync(string index, string type, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Mpercolate(object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> MpercolateAsync(object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Mpercolate(string index, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MpercolateAsync(string index, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Mpercolate(string index, string type, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MpercolateAsync(string index, string type, object body, Func<MpercolateQueryString, MpercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse MsearchGet(Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> MsearchGetAsync(Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
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
		public ElasticsearchResponse MsearchGet(string index, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MsearchGetAsync(string index, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse MsearchGet(string index, string type, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MsearchGetAsync(string index, string type, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Msearch(object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> MsearchAsync(object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Msearch(string index, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MsearchAsync(string index, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Msearch(string index, string type, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MsearchAsync(string index, string type, object body, Func<MultiSearchQueryString, MultiSearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse MtermvectorsGet(Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> MtermvectorsGetAsync(Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
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
		public ElasticsearchResponse MtermvectorsGet(string index, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MtermvectorsGetAsync(string index, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse MtermvectorsGet(string index, string type, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MtermvectorsGetAsync(string index, string type, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Mtermvectors(object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> MtermvectorsAsync(object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Mtermvectors(string index, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> MtermvectorsAsync(string index, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Mtermvectors(string index, string type, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> MtermvectorsAsync(string index, string type, object body, Func<MtermvectorsQueryString, MtermvectorsQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse NodesHotThreadsForAll(Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> NodesHotThreadsForAllAsync(Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
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
		public ElasticsearchResponse NodesHotThreads(string node_id, Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public Task<ElasticsearchResponse> NodesHotThreadsAsync(string node_id, Func<NodesHotThreadsQueryString, NodesHotThreadsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public ElasticsearchResponse NodesInfoForAll(Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> NodesInfoForAllAsync(Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
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
		public ElasticsearchResponse NodesInfo(string node_id, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public Task<ElasticsearchResponse> NodesInfoAsync(string node_id, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public ElasticsearchResponse NodesInfoForAll(string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> NodesInfoForAllAsync(string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse NodesInfo(string node_id, string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> NodesInfoAsync(string node_id, string metric, Func<NodesInfoQueryString, NodesInfoQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse NodesShutdownForAll(Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> NodesShutdownForAllAsync(Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
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
		public ElasticsearchResponse NodesShutdown(string node_id, Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public Task<ElasticsearchResponse> NodesShutdownAsync(string node_id, Func<NodesShutdownQueryString, NodesShutdownQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public ElasticsearchResponse NodesStatsForAll(Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> NodesStatsForAllAsync(Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
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
		public ElasticsearchResponse NodesStats(string node_id, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public Task<ElasticsearchResponse> NodesStatsAsync(string node_id, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
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
		public ElasticsearchResponse NodesStatsForAll(string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> NodesStatsForAllAsync(string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse NodesStats(string node_id, string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
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
		public Task<ElasticsearchResponse> NodesStatsAsync(string node_id, string metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
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
		public ElasticsearchResponse NodesStatsForAll(string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
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
		public Task<ElasticsearchResponse> NodesStatsForAllAsync(string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
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
		public ElasticsearchResponse NodesStats(string node_id, string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
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
		public Task<ElasticsearchResponse> NodesStatsAsync(string node_id, string metric, string index_metric, Func<NodesStatsQueryString, NodesStatsQueryString> queryString = null)
		{
			node_id.ThrowIfNullOrEmpty("node_id");
			metric.ThrowIfNullOrEmpty("metric");
			index_metric.ThrowIfNullOrEmpty("index_metric");
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
		public ElasticsearchResponse PercolateGet(string index, string type, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> PercolateGetAsync(string index, string type, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse PercolateGet(string index, string type, string id, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> PercolateGetAsync(string index, string type, string id, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Percolate(string index, string type, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> PercolateAsync(string index, string type, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Percolate(string index, string type, string id, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> PercolateAsync(string index, string type, string id, object body, Func<PercolateQueryString, PercolateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Ping(Func<PingQueryString, PingQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> PingAsync(Func<PingQueryString, PingQueryString> queryString = null)
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
		public ElasticsearchResponse ScrollGet(Func<ScrollQueryString, ScrollQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> ScrollGetAsync(Func<ScrollQueryString, ScrollQueryString> queryString = null)
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
		public ElasticsearchResponse ScrollGet(string scroll_id, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
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
		public Task<ElasticsearchResponse> ScrollGetAsync(string scroll_id, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
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
		public ElasticsearchResponse Scroll(object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> ScrollAsync(object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Scroll(string scroll_id, object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
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
		public Task<ElasticsearchResponse> ScrollAsync(string scroll_id, object body, Func<ScrollQueryString, ScrollQueryString> queryString = null)
		{
			scroll_id.ThrowIfNullOrEmpty("scroll_id");
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
		public ElasticsearchResponse SearchGet(Func<SearchQueryString, SearchQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> SearchGetAsync(Func<SearchQueryString, SearchQueryString> queryString = null)
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
		public ElasticsearchResponse SearchGet(string index, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> SearchGetAsync(string index, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse SearchGet(string index, string type, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> SearchGetAsync(string index, string type, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse Search(object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> SearchAsync(object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Search(string index, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> SearchAsync(string index, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse Search(string index, string type, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public Task<ElasticsearchResponse> SearchAsync(string index, string type, object body, Func<SearchQueryString, SearchQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
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
		public ElasticsearchResponse SnapshotCreate(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public Task<ElasticsearchResponse> SnapshotCreateAsync(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public ElasticsearchResponse SnapshotCreatePost(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public Task<ElasticsearchResponse> SnapshotCreatePostAsync(string repository, string snapshot, object body, Func<SnapshotCreateQueryString, SnapshotCreateQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public ElasticsearchResponse SnapshotCreateRepository(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public Task<ElasticsearchResponse> SnapshotCreateRepositoryAsync(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public ElasticsearchResponse SnapshotCreateRepositoryPost(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public Task<ElasticsearchResponse> SnapshotCreateRepositoryPostAsync(string repository, object body, Func<SnapshotCreateRepositoryQueryString, SnapshotCreateRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public ElasticsearchResponse SnapshotDelete(string repository, string snapshot, Func<SnapshotDeleteQueryString, SnapshotDeleteQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public Task<ElasticsearchResponse> SnapshotDeleteAsync(string repository, string snapshot, Func<SnapshotDeleteQueryString, SnapshotDeleteQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public ElasticsearchResponse SnapshotDeleteRepository(string repository, Func<SnapshotDeleteRepositoryQueryString, SnapshotDeleteRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public Task<ElasticsearchResponse> SnapshotDeleteRepositoryAsync(string repository, Func<SnapshotDeleteRepositoryQueryString, SnapshotDeleteRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public ElasticsearchResponse SnapshotGet(string repository, string snapshot, Func<SnapshotGetQueryString, SnapshotGetQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public Task<ElasticsearchResponse> SnapshotGetAsync(string repository, string snapshot, Func<SnapshotGetQueryString, SnapshotGetQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public ElasticsearchResponse SnapshotGetRepository(Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> SnapshotGetRepositoryAsync(Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
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
		public ElasticsearchResponse SnapshotGetRepository(string repository, Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public Task<ElasticsearchResponse> SnapshotGetRepositoryAsync(string repository, Func<SnapshotGetRepositoryQueryString, SnapshotGetRepositoryQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
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
		public ElasticsearchResponse SnapshotRestore(string repository, string snapshot, object body, Func<SnapshotRestoreQueryString, SnapshotRestoreQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public Task<ElasticsearchResponse> SnapshotRestoreAsync(string repository, string snapshot, object body, Func<SnapshotRestoreQueryString, SnapshotRestoreQueryString> queryString = null)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
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
		public ElasticsearchResponse Suggest(object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
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
		public Task<ElasticsearchResponse> SuggestAsync(object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
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
		public ElasticsearchResponse Suggest(string index, object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> SuggestAsync(string index, object body, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse SuggestGet(Func<SuggestQueryString, SuggestQueryString> queryString = null)
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
		public Task<ElasticsearchResponse> SuggestGetAsync(Func<SuggestQueryString, SuggestQueryString> queryString = null)
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
		public ElasticsearchResponse SuggestGet(string index, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public Task<ElasticsearchResponse> SuggestGetAsync(string index, Func<SuggestQueryString, SuggestQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
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
		public ElasticsearchResponse TermvectorGet(string index, string type, string id, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> TermvectorGetAsync(string index, string type, string id, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Termvector(string index, string type, string id, object body, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> TermvectorAsync(string index, string type, string id, object body, Func<TermvectorQueryString, TermvectorQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public ElasticsearchResponse Update(string index, string type, string id, object body, Func<UpdateQueryString, UpdateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
		public Task<ElasticsearchResponse> UpdateAsync(string index, string type, string id, object body, Func<UpdateQueryString, UpdateQueryString> queryString = null)
		{
			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");
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
	
