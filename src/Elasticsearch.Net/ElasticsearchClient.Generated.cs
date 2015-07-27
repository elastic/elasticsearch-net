using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;

///Generated File Please Do Not Edit Manually
	
namespace Elasticsearch.Net
{
	///<summary>
	///Raw operations with elasticsearch
	///</summary>
	public partial class ElasticsearchClient : IElasticsearchClient
	{
	
		///<summary>Represents a POST on /_bench/abort/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/search-benchmark.html</para>	
	    ///</summary>
		///<param name="name">A benchmark name</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> AbortBenchmark<T>(string name, Func<AbortBenchmarkRequestParameters, AbortBenchmarkRequestParameters> requestParameters = null) =>
			this.DoRequest<T,AbortBenchmarkRequestParameters>("POST", Url($"_bench/abort/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a POST on /_bench/abort/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/search-benchmark.html</para>	
	    ///</summary>
		///<param name="name">A benchmark name</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> AbortBenchmarkAsync<T>(string name, Func<AbortBenchmarkRequestParameters, AbortBenchmarkRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,AbortBenchmarkRequestParameters>("POST", Url($"_bench/abort/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a POST on /_bulk 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-bulk.html</para>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Bulk<T>(object body, Func<BulkRequestParameters, BulkRequestParameters> requestParameters = null) =>
			this.DoRequest<T,BulkRequestParameters>("POST", Url($"_bulk"), requestParameters, body);
		
		///<summary>Represents a POST on /_bulk 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-bulk.html</para>	
	    ///</summary>
		///<param name="body">The operation definition and data (action-data pairs), separated by newlines</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> BulkAsync<T>(object body, Func<BulkRequestParameters, BulkRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,BulkRequestParameters>("POST", Url($"_bulk"), requestParameters, body);
		
		///<summary>Represents a GET on /_cat/aliases 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-alias.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatAliases<T>(Func<CatAliasesRequestParameters, CatAliasesRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatAliasesRequestParameters>("GET", Url($"_cat/aliases"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/aliases 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-alias.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatAliasesAsync<T>(Func<CatAliasesRequestParameters, CatAliasesRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatAliasesRequestParameters>("GET", Url($"_cat/aliases"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/allocation 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-allocation.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatAllocation<T>(Func<CatAllocationRequestParameters, CatAllocationRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatAllocationRequestParameters>("GET", Url($"_cat/allocation"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/allocation 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-allocation.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatAllocationAsync<T>(Func<CatAllocationRequestParameters, CatAllocationRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatAllocationRequestParameters>("GET", Url($"_cat/allocation"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/count 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-count.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatCount<T>(Func<CatCountRequestParameters, CatCountRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatCountRequestParameters>("GET", Url($"_cat/count"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/count 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-count.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatCountAsync<T>(Func<CatCountRequestParameters, CatCountRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatCountRequestParameters>("GET", Url($"_cat/count"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/fielddata 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-fielddata.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatFielddata<T>(Func<CatFielddataRequestParameters, CatFielddataRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatFielddataRequestParameters>("GET", Url($"_cat/fielddata"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/fielddata 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-fielddata.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatFielddataAsync<T>(Func<CatFielddataRequestParameters, CatFielddataRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatFielddataRequestParameters>("GET", Url($"_cat/fielddata"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/health 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-health.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatHealth<T>(Func<CatHealthRequestParameters, CatHealthRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatHealthRequestParameters>("GET", Url($"_cat/health"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/health 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-health.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatHealthAsync<T>(Func<CatHealthRequestParameters, CatHealthRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatHealthRequestParameters>("GET", Url($"_cat/health"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatHelp<T>(Func<CatHelpRequestParameters, CatHelpRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatHelpRequestParameters>("GET", Url($"_cat"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatHelpAsync<T>(Func<CatHelpRequestParameters, CatHelpRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatHelpRequestParameters>("GET", Url($"_cat"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/indices 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-indices.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatIndices<T>(Func<CatIndicesRequestParameters, CatIndicesRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatIndicesRequestParameters>("GET", Url($"_cat/indices"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/indices 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-indices.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatIndicesAsync<T>(Func<CatIndicesRequestParameters, CatIndicesRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatIndicesRequestParameters>("GET", Url($"_cat/indices"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/master 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-master.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatMaster<T>(Func<CatMasterRequestParameters, CatMasterRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatMasterRequestParameters>("GET", Url($"_cat/master"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/master 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-master.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatMasterAsync<T>(Func<CatMasterRequestParameters, CatMasterRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatMasterRequestParameters>("GET", Url($"_cat/master"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/nodes 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-nodes.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatNodes<T>(Func<CatNodesRequestParameters, CatNodesRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatNodesRequestParameters>("GET", Url($"_cat/nodes"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/nodes 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-nodes.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatNodesAsync<T>(Func<CatNodesRequestParameters, CatNodesRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatNodesRequestParameters>("GET", Url($"_cat/nodes"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/pending_tasks 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-pending-tasks.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatPendingTasks<T>(Func<CatPendingTasksRequestParameters, CatPendingTasksRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatPendingTasksRequestParameters>("GET", Url($"_cat/pending_tasks"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/pending_tasks 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-pending-tasks.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatPendingTasksAsync<T>(Func<CatPendingTasksRequestParameters, CatPendingTasksRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatPendingTasksRequestParameters>("GET", Url($"_cat/pending_tasks"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/plugins 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-plugins.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatPlugins<T>(Func<CatPluginsRequestParameters, CatPluginsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatPluginsRequestParameters>("GET", Url($"_cat/plugins"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/plugins 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-plugins.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatPluginsAsync<T>(Func<CatPluginsRequestParameters, CatPluginsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatPluginsRequestParameters>("GET", Url($"_cat/plugins"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/recovery 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-recovery.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatRecovery<T>(Func<CatRecoveryRequestParameters, CatRecoveryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatRecoveryRequestParameters>("GET", Url($"_cat/recovery"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/recovery 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-recovery.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatRecoveryAsync<T>(Func<CatRecoveryRequestParameters, CatRecoveryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatRecoveryRequestParameters>("GET", Url($"_cat/recovery"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/segments 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-segments.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatSegments<T>(Func<CatSegmentsRequestParameters, CatSegmentsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatSegmentsRequestParameters>("GET", Url($"_cat/segments"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/segments 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-segments.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatSegmentsAsync<T>(Func<CatSegmentsRequestParameters, CatSegmentsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatSegmentsRequestParameters>("GET", Url($"_cat/segments"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/shards 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-shards.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatShards<T>(Func<CatShardsRequestParameters, CatShardsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatShardsRequestParameters>("GET", Url($"_cat/shards"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/shards 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cat-shards.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatShardsAsync<T>(Func<CatShardsRequestParameters, CatShardsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatShardsRequestParameters>("GET", Url($"_cat/shards"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/thread_pool 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-thread-pool.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CatThreadPool<T>(Func<CatThreadPoolRequestParameters, CatThreadPoolRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CatThreadPoolRequestParameters>("GET", Url($"_cat/thread_pool"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a GET on /_cat/thread_pool 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/cat-thread-pool.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CatThreadPoolAsync<T>(Func<CatThreadPoolRequestParameters, CatThreadPoolRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CatThreadPoolRequestParameters>("GET", Url($"_cat/thread_pool"), requestParameters, contentType: "text/plain");
		
		///<summary>Represents a DELETE on /_search/scroll/{scroll_id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-request-scroll.html</para>	
	    ///</summary>
		///<param name="scroll_id">A comma-separated list of scroll IDs to clear</param>
		///<param name="body">A comma-separated list of scroll IDs to clear if none was specified via the scroll_id parameter</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClearScroll<T>(string scroll_id, object body, Func<ClearScrollRequestParameters, ClearScrollRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClearScrollRequestParameters>("DELETE", Url($"_search/scroll/{scroll_id.NotNull("scroll_id")}"), requestParameters, body);
		
		///<summary>Represents a DELETE on /_search/scroll/{scroll_id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-request-scroll.html</para>	
	    ///</summary>
		///<param name="scroll_id">A comma-separated list of scroll IDs to clear</param>
		///<param name="body">A comma-separated list of scroll IDs to clear if none was specified via the scroll_id parameter</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClearScrollAsync<T>(string scroll_id, object body, Func<ClearScrollRequestParameters, ClearScrollRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClearScrollRequestParameters>("DELETE", Url($"_search/scroll/{scroll_id.NotNull("scroll_id")}"), requestParameters, body);
		
		///<summary>Represents a GET on /_cluster/settings 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-update-settings.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterGetSettings<T>(Func<ClusterGetSettingsRequestParameters, ClusterGetSettingsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterGetSettingsRequestParameters>("GET", Url($"_cluster/settings"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/settings 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-update-settings.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterGetSettingsAsync<T>(Func<ClusterGetSettingsRequestParameters, ClusterGetSettingsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterGetSettingsRequestParameters>("GET", Url($"_cluster/settings"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/health 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-health.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterHealth<T>(Func<ClusterHealthRequestParameters, ClusterHealthRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterHealthRequestParameters>("GET", Url($"_cluster/health"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/health 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-health.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterHealthAsync<T>(Func<ClusterHealthRequestParameters, ClusterHealthRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterHealthRequestParameters>("GET", Url($"_cluster/health"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/pending_tasks 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-pending.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterPendingTasks<T>(Func<ClusterPendingTasksRequestParameters, ClusterPendingTasksRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterPendingTasksRequestParameters>("GET", Url($"_cluster/pending_tasks"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/pending_tasks 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-pending.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterPendingTasksAsync<T>(Func<ClusterPendingTasksRequestParameters, ClusterPendingTasksRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterPendingTasksRequestParameters>("GET", Url($"_cluster/pending_tasks"), requestParameters);
		
		///<summary>Represents a PUT on /_cluster/settings 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-update-settings.html</para>	
	    ///</summary>
		///<param name="body">The settings to be updated. Can be either `transient` or `persistent` (survives cluster restart).</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterPutSettings<T>(object body, Func<ClusterSettingsRequestParameters, ClusterSettingsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterSettingsRequestParameters>("PUT", Url($"_cluster/settings"), requestParameters, body);
		
		///<summary>Represents a PUT on /_cluster/settings 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-update-settings.html</para>	
	    ///</summary>
		///<param name="body">The settings to be updated. Can be either `transient` or `persistent` (survives cluster restart).</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterPutSettingsAsync<T>(object body, Func<ClusterSettingsRequestParameters, ClusterSettingsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterSettingsRequestParameters>("PUT", Url($"_cluster/settings"), requestParameters, body);
		
		///<summary>Represents a POST on /_cluster/reroute 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-reroute.html</para>	
	    ///</summary>
		///<param name="body">The definition of `commands` to perform (`move`, `cancel`, `allocate`)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterReroute<T>(object body, Func<ClusterRerouteRequestParameters, ClusterRerouteRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterRerouteRequestParameters>("POST", Url($"_cluster/reroute"), requestParameters, body);
		
		///<summary>Represents a POST on /_cluster/reroute 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-reroute.html</para>	
	    ///</summary>
		///<param name="body">The definition of `commands` to perform (`move`, `cancel`, `allocate`)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterRerouteAsync<T>(object body, Func<ClusterRerouteRequestParameters, ClusterRerouteRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterRerouteRequestParameters>("POST", Url($"_cluster/reroute"), requestParameters, body);
		
		///<summary>Represents a GET on /_cluster/state 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-state.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterState<T>(Func<ClusterStateRequestParameters, ClusterStateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterStateRequestParameters>("GET", Url($"_cluster/state"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/state 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-state.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterStateAsync<T>(Func<ClusterStateRequestParameters, ClusterStateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterStateRequestParameters>("GET", Url($"_cluster/state"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/stats 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ClusterStats<T>(Func<ClusterStatsRequestParameters, ClusterStatsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClusterStatsRequestParameters>("GET", Url($"_cluster/stats"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/stats 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ClusterStatsAsync<T>(Func<ClusterStatsRequestParameters, ClusterStatsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClusterStatsRequestParameters>("GET", Url($"_cluster/stats"), requestParameters);
		
		///<summary>Represents a POST on /_count 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-count.html</para>	
	    ///</summary>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Count<T>(object body, Func<CountRequestParameters, CountRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CountRequestParameters>("POST", Url($"_count"), requestParameters, body);
		
		///<summary>Represents a POST on /_count 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-count.html</para>	
	    ///</summary>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CountAsync<T>(object body, Func<CountRequestParameters, CountRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CountRequestParameters>("POST", Url($"_count"), requestParameters, body);
		
		///<summary>Represents a GET on /{index}/{type}/_percolate/count 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-percolate.html</para>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> CountPercolateGet<T>(string index, string type, Func<PercolateCountRequestParameters, PercolateCountRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PercolateCountRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_percolate/count"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/_percolate/count 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-percolate.html</para>	
	    ///</summary>
		///<param name="index">The index of the document being count percolated.</param>
		///<param name="type">The type of the document being count percolated.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> CountPercolateGetAsync<T>(string index, string type, Func<PercolateCountRequestParameters, PercolateCountRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PercolateCountRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_percolate/count"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/{type}/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-delete.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Delete<T>(string index, string type, string id, Func<DeleteRequestParameters, DeleteRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteRequestParameters>("DELETE", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}"), requestParameters, allow404: true);
		
		///<summary>Represents a DELETE on /{index}/{type}/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-delete.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> DeleteAsync<T>(string index, string type, string id, Func<DeleteRequestParameters, DeleteRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteRequestParameters>("DELETE", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}"), requestParameters, allow404: true);
		
		///<summary>Represents a DELETE on /{index}/_query 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-delete-by-query.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="body">A query to restrict the operation specified with the Query DSL</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> DeleteByQuery<T>(string index, object body, Func<DeleteByQueryRequestParameters, DeleteByQueryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteByQueryRequestParameters>("DELETE", Url($"{index.NotNull("index")}/_query"), requestParameters, body);
		
		///<summary>Represents a DELETE on /{index}/_query 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-delete-by-query.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to restrict the operation; use `_all` to perform the operation on all indices</param>
		///<param name="body">A query to restrict the operation specified with the Query DSL</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> DeleteByQueryAsync<T>(string index, object body, Func<DeleteByQueryRequestParameters, DeleteByQueryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteByQueryRequestParameters>("DELETE", Url($"{index.NotNull("index")}/_query"), requestParameters, body);
		
		///<summary>Represents a DELETE on /_scripts/{lang}/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-scripting.html</para>	
	    ///</summary>
		///<param name="lang">Script language</param>
		///<param name="id">Script ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> DeleteScript<T>(string lang, string id, Func<DeleteScriptRequestParameters, DeleteScriptRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteScriptRequestParameters>("DELETE", Url($"_scripts/{lang.NotNull("lang")}/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_scripts/{lang}/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-scripting.html</para>	
	    ///</summary>
		///<param name="lang">Script language</param>
		///<param name="id">Script ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> DeleteScriptAsync<T>(string lang, string id, Func<DeleteScriptRequestParameters, DeleteScriptRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteScriptRequestParameters>("DELETE", Url($"_scripts/{lang.NotNull("lang")}/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_search/template/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-template.html</para>	
	    ///</summary>
		///<param name="id">Template ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> DeleteTemplate<T>(string id, Func<DeleteTemplateRequestParameters, DeleteTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteTemplateRequestParameters>("DELETE", Url($"_search/template/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_search/template/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-template.html</para>	
	    ///</summary>
		///<param name="id">Template ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> DeleteTemplateAsync<T>(string id, Func<DeleteTemplateRequestParameters, DeleteTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteTemplateRequestParameters>("DELETE", Url($"_search/template/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a HEAD on /{index}/{type}/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-get.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Exists<T>(string index, string type, string id, Func<DocumentExistsRequestParameters, DocumentExistsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DocumentExistsRequestParameters>("HEAD", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /{index}/{type}/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-get.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ExistsAsync<T>(string index, string type, string id, Func<DocumentExistsRequestParameters, DocumentExistsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DocumentExistsRequestParameters>("HEAD", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}"), requestParameters, allow404: true);
		
		///<summary>Represents a GET on /{index}/{type}/{id}/_explain 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-explain.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ExplainGet<T>(string index, string type, string id, Func<ExplainRequestParameters, ExplainRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ExplainRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_explain"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/{id}/_explain 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-explain.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ExplainGetAsync<T>(string index, string type, string id, Func<ExplainRequestParameters, ExplainRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ExplainRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_explain"), requestParameters);
		
		///<summary>Represents a GET on /_field_stats 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-field-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> FieldStatsGet<T>(Func<FieldStatsRequestParameters, FieldStatsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,FieldStatsRequestParameters>("GET", Url($"_field_stats"), requestParameters);
		
		///<summary>Represents a GET on /_field_stats 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-field-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> FieldStatsGetAsync<T>(Func<FieldStatsRequestParameters, FieldStatsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,FieldStatsRequestParameters>("GET", Url($"_field_stats"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-get.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Get<T>(string index, string type, string id, Func<GetRequestParameters, GetRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}"), requestParameters, allow404: true);
		
		///<summary>Represents a GET on /{index}/{type}/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-get.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> GetAsync<T>(string index, string type, string id, Func<GetRequestParameters, GetRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}"), requestParameters, allow404: true);
		
		///<summary>Represents a GET on /_scripts/{lang}/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-scripting.html</para>	
	    ///</summary>
		///<param name="lang">Script language</param>
		///<param name="id">Script ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> GetScript<T>(string lang, string id, Func<GetScriptRequestParameters, GetScriptRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetScriptRequestParameters>("GET", Url($"_scripts/{lang.NotNull("lang")}/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a GET on /_scripts/{lang}/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-scripting.html</para>	
	    ///</summary>
		///<param name="lang">Script language</param>
		///<param name="id">Script ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> GetScriptAsync<T>(string lang, string id, Func<GetScriptRequestParameters, GetScriptRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetScriptRequestParameters>("GET", Url($"_scripts/{lang.NotNull("lang")}/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/{id}/_source 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-get.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document; use `_all` to fetch the first document matching the ID across all types</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> GetSource<T>(string index, string type, string id, Func<SourceRequestParameters, SourceRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SourceRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_source"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/{id}/_source 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-get.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document; use `_all` to fetch the first document matching the ID across all types</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> GetSourceAsync<T>(string index, string type, string id, Func<SourceRequestParameters, SourceRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SourceRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_source"), requestParameters);
		
		///<summary>Represents a GET on /_search/template/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-template.html</para>	
	    ///</summary>
		///<param name="id">Template ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> GetTemplate<T>(string id, Func<GetTemplateRequestParameters, GetTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetTemplateRequestParameters>("GET", Url($"_search/template/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a GET on /_search/template/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-template.html</para>	
	    ///</summary>
		///<param name="id">Template ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> GetTemplateAsync<T>(string id, Func<GetTemplateRequestParameters, GetTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetTemplateRequestParameters>("GET", Url($"_search/template/{id.NotNull("id")}"), requestParameters);
		
		///<summary>Represents a POST on /{index}/{type} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-index_.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Index<T>(string index, string type, object body, Func<IndexRequestParameters, IndexRequestParameters> requestParameters = null) =>
			this.DoRequest<T,IndexRequestParameters>("POST", Url($"{index.NotNull("index")}/{type.NotNull("type")}"), requestParameters, body);
		
		///<summary>Represents a POST on /{index}/{type} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-index_.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="body">The document</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndexAsync<T>(string index, string type, object body, Func<IndexRequestParameters, IndexRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,IndexRequestParameters>("POST", Url($"{index.NotNull("index")}/{type.NotNull("type")}"), requestParameters, body);
		
		///<summary>Represents a GET on /_analyze 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-analyze.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesAnalyzeGetForAll<T>(Func<AnalyzeRequestParameters, AnalyzeRequestParameters> requestParameters = null) =>
			this.DoRequest<T,AnalyzeRequestParameters>("GET", Url($"_analyze"), requestParameters);
		
		///<summary>Represents a GET on /_analyze 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-analyze.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesAnalyzeGetForAllAsync<T>(Func<AnalyzeRequestParameters, AnalyzeRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,AnalyzeRequestParameters>("GET", Url($"_analyze"), requestParameters);
		
		///<summary>Represents a POST on /_cache/clear 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-clearcache.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesClearCacheForAll<T>(Func<ClearCacheRequestParameters, ClearCacheRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ClearCacheRequestParameters>("POST", Url($"_cache/clear"), requestParameters);
		
		///<summary>Represents a POST on /_cache/clear 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-clearcache.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesClearCacheForAllAsync<T>(Func<ClearCacheRequestParameters, ClearCacheRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ClearCacheRequestParameters>("POST", Url($"_cache/clear"), requestParameters);
		
		///<summary>Represents a POST on /{index}/_close 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-open-close.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesClose<T>(string index, Func<CloseIndexRequestParameters, CloseIndexRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CloseIndexRequestParameters>("POST", Url($"{index.NotNull("index")}/_close"), requestParameters);
		
		///<summary>Represents a POST on /{index}/_close 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-open-close.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesCloseAsync<T>(string index, Func<CloseIndexRequestParameters, CloseIndexRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CloseIndexRequestParameters>("POST", Url($"{index.NotNull("index")}/_close"), requestParameters);
		
		///<summary>Represents a PUT on /{index} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-create-index.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesCreate<T>(string index, object body, Func<CreateIndexRequestParameters, CreateIndexRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CreateIndexRequestParameters>("PUT", Url($"{index.NotNull("index")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /{index} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-create-index.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="body">The configuration for the index (`settings` and `mappings`)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesCreateAsync<T>(string index, object body, Func<CreateIndexRequestParameters, CreateIndexRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CreateIndexRequestParameters>("PUT", Url($"{index.NotNull("index")}"), requestParameters, body);
		
		///<summary>Represents a DELETE on /{index} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-delete-index.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to delete; use `_all` or `*` string to delete all indices</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesDelete<T>(string index, Func<DeleteIndexRequestParameters, DeleteIndexRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteIndexRequestParameters>("DELETE", Url($"{index.NotNull("index")}"), requestParameters);
		
		///<summary>Represents a DELETE on /{index} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-delete-index.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to delete; use `_all` or `*` string to delete all indices</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesDeleteAsync<T>(string index, Func<DeleteIndexRequestParameters, DeleteIndexRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteIndexRequestParameters>("DELETE", Url($"{index.NotNull("index")}"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/_alias/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="name">A comma-separated list of aliases to delete (supports wildcards); use `_all` to delete all aliases for the specified indices.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesDeleteAlias<T>(string index, string name, Func<DeleteAliasRequestParameters, DeleteAliasRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteAliasRequestParameters>("DELETE", Url($"{index.NotNull("index")}/_alias/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/_alias/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="name">A comma-separated list of aliases to delete (supports wildcards); use `_all` to delete all aliases for the specified indices.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesDeleteAliasAsync<T>(string index, string name, Func<DeleteAliasRequestParameters, DeleteAliasRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteAliasRequestParameters>("DELETE", Url($"{index.NotNull("index")}/_alias/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/{type}/_mapping 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-delete-mapping.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="type">A comma-separated list of document types to delete (supports wildcards); use `_all` to delete all document types in the specified indices.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesDeleteMapping<T>(string index, string type, Func<DeleteMappingRequestParameters, DeleteMappingRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteMappingRequestParameters>("DELETE", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_mapping"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/{type}/_mapping 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-delete-mapping.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names (supports wildcards); use `_all` for all indices</param>
		///<param name="type">A comma-separated list of document types to delete (supports wildcards); use `_all` to delete all document types in the specified indices.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesDeleteMappingAsync<T>(string index, string type, Func<DeleteMappingRequestParameters, DeleteMappingRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteMappingRequestParameters>("DELETE", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_mapping"), requestParameters);
		
		///<summary>Represents a DELETE on /_template/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesDeleteTemplateForAll<T>(string name, Func<DeleteTemplateRequestParameters, DeleteTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteTemplateRequestParameters>("DELETE", Url($"_template/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_template/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesDeleteTemplateForAllAsync<T>(string name, Func<DeleteTemplateRequestParameters, DeleteTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteTemplateRequestParameters>("DELETE", Url($"_template/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/_warmer/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-warmers.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to delete warmers from (supports wildcards); use `_all` to perform the operation on all indices.</param>
		///<param name="name">A comma-separated list of warmer names to delete (supports wildcards); use `_all` to delete all warmers in the specified indices. You must specify a name either in the uri or in the parameters.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesDeleteWarmer<T>(string index, string name, Func<DeleteWarmerRequestParameters, DeleteWarmerRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteWarmerRequestParameters>("DELETE", Url($"{index.NotNull("index")}/_warmer/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a DELETE on /{index}/_warmer/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-warmers.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names to delete warmers from (supports wildcards); use `_all` to perform the operation on all indices.</param>
		///<param name="name">A comma-separated list of warmer names to delete (supports wildcards); use `_all` to delete all warmers in the specified indices. You must specify a name either in the uri or in the parameters.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesDeleteWarmerAsync<T>(string index, string name, Func<DeleteWarmerRequestParameters, DeleteWarmerRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteWarmerRequestParameters>("DELETE", Url($"{index.NotNull("index")}/_warmer/{name.NotNull("name")}"), requestParameters);
		
		///<summary>Represents a HEAD on /{index} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-exists.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to check</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesExists<T>(string index, Func<IndexExistsRequestParameters, IndexExistsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,IndexExistsRequestParameters>("HEAD", Url($"{index.NotNull("index")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /{index} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-exists.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of indices to check</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesExistsAsync<T>(string index, Func<IndexExistsRequestParameters, IndexExistsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,IndexExistsRequestParameters>("HEAD", Url($"{index.NotNull("index")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /_alias/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesExistsAliasForAll<T>(string name, Func<AliasExistsRequestParameters, AliasExistsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,AliasExistsRequestParameters>("HEAD", Url($"_alias/{name.NotNull("name")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /_alias/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="name">A comma-separated list of alias names to return</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesExistsAliasForAllAsync<T>(string name, Func<AliasExistsRequestParameters, AliasExistsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,AliasExistsRequestParameters>("HEAD", Url($"_alias/{name.NotNull("name")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /_template/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesExistsTemplateForAll<T>(string name, Func<TemplateExistsRequestParameters, TemplateExistsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,TemplateExistsRequestParameters>("HEAD", Url($"_template/{name.NotNull("name")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /_template/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesExistsTemplateForAllAsync<T>(string name, Func<TemplateExistsRequestParameters, TemplateExistsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,TemplateExistsRequestParameters>("HEAD", Url($"_template/{name.NotNull("name")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /{index}/{type} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-types-exists.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to check the types across all indices</param>
		///<param name="type">A comma-separated list of document types to check</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesExistsType<T>(string index, string type, Func<TypeExistsRequestParameters, TypeExistsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,TypeExistsRequestParameters>("HEAD", Url($"{index.NotNull("index")}/{type.NotNull("type")}"), requestParameters, allow404: true);
		
		///<summary>Represents a HEAD on /{index}/{type} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-types-exists.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names; use `_all` to check the types across all indices</param>
		///<param name="type">A comma-separated list of document types to check</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesExistsTypeAsync<T>(string index, string type, Func<TypeExistsRequestParameters, TypeExistsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,TypeExistsRequestParameters>("HEAD", Url($"{index.NotNull("index")}/{type.NotNull("type")}"), requestParameters, allow404: true);
		
		///<summary>Represents a POST on /_flush 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-flush.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesFlushForAll<T>(Func<FlushRequestParameters, FlushRequestParameters> requestParameters = null) =>
			this.DoRequest<T,FlushRequestParameters>("POST", Url($"_flush"), requestParameters);
		
		///<summary>Represents a POST on /_flush 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-flush.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesFlushForAllAsync<T>(Func<FlushRequestParameters, FlushRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,FlushRequestParameters>("POST", Url($"_flush"), requestParameters);
		
		///<summary>Represents a POST on /_flush/synced 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-synced-flush.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesFlushSyncedForAll<T>(Func<SyncedFlushRequestParameters, SyncedFlushRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SyncedFlushRequestParameters>("POST", Url($"_flush/synced"), requestParameters);
		
		///<summary>Represents a POST on /_flush/synced 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-synced-flush.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesFlushSyncedForAllAsync<T>(Func<SyncedFlushRequestParameters, SyncedFlushRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SyncedFlushRequestParameters>("POST", Url($"_flush/synced"), requestParameters);
		
		///<summary>Represents a GET on /{index} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-index.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGet<T>(string index, Func<GetIndexRequestParameters, GetIndexRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetIndexRequestParameters>("GET", Url($"{index.NotNull("index")}"), requestParameters);
		
		///<summary>Represents a GET on /{index} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-index.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetAsync<T>(string index, Func<GetIndexRequestParameters, GetIndexRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetIndexRequestParameters>("GET", Url($"{index.NotNull("index")}"), requestParameters);
		
		///<summary>Represents a GET on /_alias 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetAliasForAll<T>(Func<GetAliasRequestParameters, GetAliasRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetAliasRequestParameters>("GET", Url($"_alias"), requestParameters);
		
		///<summary>Represents a GET on /_alias 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetAliasForAllAsync<T>(Func<GetAliasRequestParameters, GetAliasRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetAliasRequestParameters>("GET", Url($"_alias"), requestParameters);
		
		///<summary>Represents a GET on /_aliases 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetAliasesForAll<T>(Func<GetAliasesRequestParameters, GetAliasesRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetAliasesRequestParameters>("GET", Url($"_aliases"), requestParameters);
		
		///<summary>Represents a GET on /_aliases 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetAliasesForAllAsync<T>(Func<GetAliasesRequestParameters, GetAliasesRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetAliasesRequestParameters>("GET", Url($"_aliases"), requestParameters);
		
		///<summary>Represents a GET on /_mapping/field/{field} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-field-mapping.html</para>	
	    ///</summary>
		///<param name="field">A comma-separated list of fields</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetFieldMappingForAll<T>(string field, Func<GetFieldMappingRequestParameters, GetFieldMappingRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetFieldMappingRequestParameters>("GET", Url($"_mapping/field/{field.NotNull("field")}"), requestParameters);
		
		///<summary>Represents a GET on /_mapping/field/{field} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-field-mapping.html</para>	
	    ///</summary>
		///<param name="field">A comma-separated list of fields</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetFieldMappingForAllAsync<T>(string field, Func<GetFieldMappingRequestParameters, GetFieldMappingRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetFieldMappingRequestParameters>("GET", Url($"_mapping/field/{field.NotNull("field")}"), requestParameters);
		
		///<summary>Represents a GET on /_mapping 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-mapping.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetMappingForAll<T>(Func<GetMappingRequestParameters, GetMappingRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetMappingRequestParameters>("GET", Url($"_mapping"), requestParameters);
		
		///<summary>Represents a GET on /_mapping 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-mapping.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetMappingForAllAsync<T>(Func<GetMappingRequestParameters, GetMappingRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetMappingRequestParameters>("GET", Url($"_mapping"), requestParameters);
		
		///<summary>Represents a GET on /_settings 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-settings.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetSettingsForAll<T>(Func<GetIndexSettingsRequestParameters, GetIndexSettingsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetIndexSettingsRequestParameters>("GET", Url($"_settings"), requestParameters);
		
		///<summary>Represents a GET on /_settings 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-get-settings.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetSettingsForAllAsync<T>(Func<GetIndexSettingsRequestParameters, GetIndexSettingsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetIndexSettingsRequestParameters>("GET", Url($"_settings"), requestParameters);
		
		///<summary>Represents a GET on /_template 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetTemplateForAll<T>(Func<GetTemplateRequestParameters, GetTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetTemplateRequestParameters>("GET", Url($"_template"), requestParameters);
		
		///<summary>Represents a GET on /_template 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetTemplateForAllAsync<T>(Func<GetTemplateRequestParameters, GetTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetTemplateRequestParameters>("GET", Url($"_template"), requestParameters);
		
		///<summary>Represents a GET on /_upgrade 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-upgrade.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetUpgradeForAll<T>(Func<UpgradeStatusRequestParameters, UpgradeStatusRequestParameters> requestParameters = null) =>
			this.DoRequest<T,UpgradeStatusRequestParameters>("GET", Url($"_upgrade"), requestParameters);
		
		///<summary>Represents a GET on /_upgrade 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-upgrade.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetUpgradeForAllAsync<T>(Func<UpgradeStatusRequestParameters, UpgradeStatusRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,UpgradeStatusRequestParameters>("GET", Url($"_upgrade"), requestParameters);
		
		///<summary>Represents a GET on /_warmer 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-warmers.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesGetWarmerForAll<T>(Func<GetWarmerRequestParameters, GetWarmerRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetWarmerRequestParameters>("GET", Url($"_warmer"), requestParameters);
		
		///<summary>Represents a GET on /_warmer 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-warmers.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesGetWarmerForAllAsync<T>(Func<GetWarmerRequestParameters, GetWarmerRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetWarmerRequestParameters>("GET", Url($"_warmer"), requestParameters);
		
		///<summary>Represents a POST on /{index}/_open 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-open-close.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesOpen<T>(string index, Func<OpenIndexRequestParameters, OpenIndexRequestParameters> requestParameters = null) =>
			this.DoRequest<T,OpenIndexRequestParameters>("POST", Url($"{index.NotNull("index")}/_open"), requestParameters);
		
		///<summary>Represents a POST on /{index}/_open 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-open-close.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesOpenAsync<T>(string index, Func<OpenIndexRequestParameters, OpenIndexRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,OpenIndexRequestParameters>("POST", Url($"{index.NotNull("index")}/_open"), requestParameters);
		
		///<summary>Represents a POST on /_optimize 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-optimize.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesOptimizeForAll<T>(Func<OptimizeRequestParameters, OptimizeRequestParameters> requestParameters = null) =>
			this.DoRequest<T,OptimizeRequestParameters>("POST", Url($"_optimize"), requestParameters);
		
		///<summary>Represents a POST on /_optimize 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-optimize.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesOptimizeForAllAsync<T>(Func<OptimizeRequestParameters, OptimizeRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,OptimizeRequestParameters>("POST", Url($"_optimize"), requestParameters);
		
		///<summary>Represents a PUT on /{index}/_alias/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the alias should point to (supports wildcards); use `_all` to perform the operation on all indices.</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesPutAlias<T>(string index, string name, object body, Func<PutAliasRequestParameters, PutAliasRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PutAliasRequestParameters>("PUT", Url($"{index.NotNull("index")}/_alias/{name.NotNull("name")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /{index}/_alias/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the alias should point to (supports wildcards); use `_all` to perform the operation on all indices.</param>
		///<param name="name">The name of the alias to be created or updated</param>
		///<param name="body">The settings for the alias, such as `routing` or `filter`</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesPutAliasAsync<T>(string index, string name, object body, Func<PutAliasRequestParameters, PutAliasRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PutAliasRequestParameters>("PUT", Url($"{index.NotNull("index")}/_alias/{name.NotNull("name")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /{index}/{type}/_mapping 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-put-mapping.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the mapping should be added to (supports wildcards); use `_all` or omit to add the mapping on all indices.</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesPutMapping<T>(string index, string type, object body, Func<PutMappingRequestParameters, PutMappingRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PutMappingRequestParameters>("PUT", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_mapping"), requestParameters, body);
		
		///<summary>Represents a PUT on /{index}/{type}/_mapping 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-put-mapping.html</para>	
	    ///</summary>
		///<param name="index">A comma-separated list of index names the mapping should be added to (supports wildcards); use `_all` or omit to add the mapping on all indices.</param>
		///<param name="type">The name of the document type</param>
		///<param name="body">The mapping definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesPutMappingAsync<T>(string index, string type, object body, Func<PutMappingRequestParameters, PutMappingRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PutMappingRequestParameters>("PUT", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_mapping"), requestParameters, body);
		
		///<summary>Represents a PUT on /_settings 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-update-settings.html</para>	
	    ///</summary>
		///<param name="body">The index settings to be updated</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesPutSettingsForAll<T>(object body, Func<UpdateSettingsRequestParameters, UpdateSettingsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,UpdateSettingsRequestParameters>("PUT", Url($"_settings"), requestParameters, body);
		
		///<summary>Represents a PUT on /_settings 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-update-settings.html</para>	
	    ///</summary>
		///<param name="body">The index settings to be updated</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesPutSettingsForAllAsync<T>(object body, Func<UpdateSettingsRequestParameters, UpdateSettingsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,UpdateSettingsRequestParameters>("PUT", Url($"_settings"), requestParameters, body);
		
		///<summary>Represents a PUT on /_template/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesPutTemplateForAll<T>(string name, object body, Func<PutTemplateRequestParameters, PutTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PutTemplateRequestParameters>("PUT", Url($"_template/{name.NotNull("name")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_template/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-templates.html</para>	
	    ///</summary>
		///<param name="name">The name of the template</param>
		///<param name="body">The template definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesPutTemplateForAllAsync<T>(string name, object body, Func<PutTemplateRequestParameters, PutTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PutTemplateRequestParameters>("PUT", Url($"_template/{name.NotNull("name")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_warmer/{name} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-warmers.html</para>	
	    ///</summary>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesPutWarmerForAll<T>(string name, object body, Func<PutWarmerRequestParameters, PutWarmerRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PutWarmerRequestParameters>("PUT", Url($"_warmer/{name.NotNull("name")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_warmer/{name} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-warmers.html</para>	
	    ///</summary>
		///<param name="name">The name of the warmer</param>
		///<param name="body">The search request definition for the warmer (query, filters, facets, sorting, etc)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesPutWarmerForAllAsync<T>(string name, object body, Func<PutWarmerRequestParameters, PutWarmerRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PutWarmerRequestParameters>("PUT", Url($"_warmer/{name.NotNull("name")}"), requestParameters, body);
		
		///<summary>Represents a GET on /_recovery 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-recovery.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesRecoveryForAll<T>(Func<RecoveryStatusRequestParameters, RecoveryStatusRequestParameters> requestParameters = null) =>
			this.DoRequest<T,RecoveryStatusRequestParameters>("GET", Url($"_recovery"), requestParameters);
		
		///<summary>Represents a GET on /_recovery 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-recovery.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesRecoveryForAllAsync<T>(Func<RecoveryStatusRequestParameters, RecoveryStatusRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,RecoveryStatusRequestParameters>("GET", Url($"_recovery"), requestParameters);
		
		///<summary>Represents a POST on /_refresh 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-refresh.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesRefreshForAll<T>(Func<RefreshRequestParameters, RefreshRequestParameters> requestParameters = null) =>
			this.DoRequest<T,RefreshRequestParameters>("POST", Url($"_refresh"), requestParameters);
		
		///<summary>Represents a POST on /_refresh 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-refresh.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesRefreshForAllAsync<T>(Func<RefreshRequestParameters, RefreshRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,RefreshRequestParameters>("POST", Url($"_refresh"), requestParameters);
		
		///<summary>Represents a GET on /_segments 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-segments.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesSegmentsForAll<T>(Func<SegmentsRequestParameters, SegmentsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SegmentsRequestParameters>("GET", Url($"_segments"), requestParameters);
		
		///<summary>Represents a GET on /_segments 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-segments.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesSegmentsForAllAsync<T>(Func<SegmentsRequestParameters, SegmentsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SegmentsRequestParameters>("GET", Url($"_segments"), requestParameters);
		
		///<summary>Represents a GET on /_stats 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesStatsForAll<T>(Func<IndicesStatsRequestParameters, IndicesStatsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,IndicesStatsRequestParameters>("GET", Url($"_stats"), requestParameters);
		
		///<summary>Represents a GET on /_stats 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesStatsForAllAsync<T>(Func<IndicesStatsRequestParameters, IndicesStatsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,IndicesStatsRequestParameters>("GET", Url($"_stats"), requestParameters);
		
		///<summary>Represents a GET on /_status 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-status.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesStatusForAll<T>(Func<IndicesStatusRequestParameters, IndicesStatusRequestParameters> requestParameters = null) =>
			this.DoRequest<T,IndicesStatusRequestParameters>("GET", Url($"_status"), requestParameters);
		
		///<summary>Represents a GET on /_status 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-status.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesStatusForAllAsync<T>(Func<IndicesStatusRequestParameters, IndicesStatusRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,IndicesStatusRequestParameters>("GET", Url($"_status"), requestParameters);
		
		///<summary>Represents a POST on /_aliases 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="body">The definition of `actions` to perform</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesUpdateAliasesForAll<T>(object body, Func<AliasRequestParameters, AliasRequestParameters> requestParameters = null) =>
			this.DoRequest<T,AliasRequestParameters>("POST", Url($"_aliases"), requestParameters, body);
		
		///<summary>Represents a POST on /_aliases 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/indices-aliases.html</para>	
	    ///</summary>
		///<param name="body">The definition of `actions` to perform</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesUpdateAliasesForAllAsync<T>(object body, Func<AliasRequestParameters, AliasRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,AliasRequestParameters>("POST", Url($"_aliases"), requestParameters, body);
		
		///<summary>Represents a POST on /_upgrade 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-upgrade.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesUpgradeForAll<T>(Func<UpgradeRequestParameters, UpgradeRequestParameters> requestParameters = null) =>
			this.DoRequest<T,UpgradeRequestParameters>("POST", Url($"_upgrade"), requestParameters);
		
		///<summary>Represents a POST on /_upgrade 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/indices-upgrade.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesUpgradeForAllAsync<T>(Func<UpgradeRequestParameters, UpgradeRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,UpgradeRequestParameters>("POST", Url($"_upgrade"), requestParameters);
		
		///<summary>Represents a GET on /_validate/query 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-validate.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> IndicesValidateQueryGetForAll<T>(Func<ValidateQueryRequestParameters, ValidateQueryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ValidateQueryRequestParameters>("GET", Url($"_validate/query"), requestParameters);
		
		///<summary>Represents a GET on /_validate/query 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-validate.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> IndicesValidateQueryGetForAllAsync<T>(Func<ValidateQueryRequestParameters, ValidateQueryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ValidateQueryRequestParameters>("GET", Url($"_validate/query"), requestParameters);
		
		///<summary>Represents a GET on / 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Info<T>(Func<InfoRequestParameters, InfoRequestParameters> requestParameters = null) =>
			this.DoRequest<T,InfoRequestParameters>("GET", Url($""), requestParameters);
		
		///<summary>Represents a GET on / 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> InfoAsync<T>(Func<InfoRequestParameters, InfoRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,InfoRequestParameters>("GET", Url($""), requestParameters);
		
		///<summary>Represents a GET on /_bench 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/search-benchmark.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ListBenchmarks<T>(Func<ListBenchmarksRequestParameters, ListBenchmarksRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ListBenchmarksRequestParameters>("GET", Url($"_bench"), requestParameters);
		
		///<summary>Represents a GET on /_bench 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/search-benchmark.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ListBenchmarksAsync<T>(Func<ListBenchmarksRequestParameters, ListBenchmarksRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ListBenchmarksRequestParameters>("GET", Url($"_bench"), requestParameters);
		
		///<summary>Represents a GET on /_mget 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-multi-get.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> MgetGet<T>(Func<MultiGetRequestParameters, MultiGetRequestParameters> requestParameters = null) =>
			this.DoRequest<T,MultiGetRequestParameters>("GET", Url($"_mget"), requestParameters);
		
		///<summary>Represents a GET on /_mget 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-multi-get.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> MgetGetAsync<T>(Func<MultiGetRequestParameters, MultiGetRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,MultiGetRequestParameters>("GET", Url($"_mget"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/{id}/_mlt 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-more-like-this.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> MltGet<T>(string index, string type, string id, Func<MoreLikeThisRequestParameters, MoreLikeThisRequestParameters> requestParameters = null) =>
			this.DoRequest<T,MoreLikeThisRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_mlt"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/{id}/_mlt 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-more-like-this.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document (use `_all` to fetch the first document matching the ID across all types)</param>
		///<param name="id">The document ID</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> MltGetAsync<T>(string index, string type, string id, Func<MoreLikeThisRequestParameters, MoreLikeThisRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,MoreLikeThisRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_mlt"), requestParameters);
		
		///<summary>Represents a GET on /_mpercolate 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-percolate.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> MpercolateGet<T>(Func<MultiPercolateRequestParameters, MultiPercolateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,MultiPercolateRequestParameters>("GET", Url($"_mpercolate"), requestParameters);
		
		///<summary>Represents a GET on /_mpercolate 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-percolate.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> MpercolateGetAsync<T>(Func<MultiPercolateRequestParameters, MultiPercolateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,MultiPercolateRequestParameters>("GET", Url($"_mpercolate"), requestParameters);
		
		///<summary>Represents a GET on /_msearch 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-multi-search.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> MsearchGet<T>(Func<MultiSearchRequestParameters, MultiSearchRequestParameters> requestParameters = null) =>
			this.DoRequest<T,MultiSearchRequestParameters>("GET", Url($"_msearch"), requestParameters);
		
		///<summary>Represents a GET on /_msearch 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-multi-search.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> MsearchGetAsync<T>(Func<MultiSearchRequestParameters, MultiSearchRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,MultiSearchRequestParameters>("GET", Url($"_msearch"), requestParameters);
		
		///<summary>Represents a GET on /_mtermvectors 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-multi-termvectors.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> MtermvectorsGet<T>(Func<MultiTermVectorsRequestParameters, MultiTermVectorsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,MultiTermVectorsRequestParameters>("GET", Url($"_mtermvectors"), requestParameters);
		
		///<summary>Represents a GET on /_mtermvectors 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-multi-termvectors.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> MtermvectorsGetAsync<T>(Func<MultiTermVectorsRequestParameters, MultiTermVectorsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,MultiTermVectorsRequestParameters>("GET", Url($"_mtermvectors"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/nodes/hotthreads 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-hot-threads.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> NodesHotThreadsForAll<T>(Func<NodesHotThreadsRequestParameters, NodesHotThreadsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,NodesHotThreadsRequestParameters>("GET", Url($"_cluster/nodes/hotthreads"), requestParameters);
		
		///<summary>Represents a GET on /_cluster/nodes/hotthreads 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-hot-threads.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> NodesHotThreadsForAllAsync<T>(Func<NodesHotThreadsRequestParameters, NodesHotThreadsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,NodesHotThreadsRequestParameters>("GET", Url($"_cluster/nodes/hotthreads"), requestParameters);
		
		///<summary>Represents a GET on /_nodes 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-info.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> NodesInfoForAll<T>(Func<NodesInfoRequestParameters, NodesInfoRequestParameters> requestParameters = null) =>
			this.DoRequest<T,NodesInfoRequestParameters>("GET", Url($"_nodes"), requestParameters);
		
		///<summary>Represents a GET on /_nodes 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-info.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> NodesInfoForAllAsync<T>(Func<NodesInfoRequestParameters, NodesInfoRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,NodesInfoRequestParameters>("GET", Url($"_nodes"), requestParameters);
		
		///<summary>Represents a POST on /_shutdown 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-shutdown.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> NodesShutdownForAll<T>(Func<NodesShutdownRequestParameters, NodesShutdownRequestParameters> requestParameters = null) =>
			this.DoRequest<T,NodesShutdownRequestParameters>("POST", Url($"_shutdown"), requestParameters);
		
		///<summary>Represents a POST on /_shutdown 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-shutdown.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> NodesShutdownForAllAsync<T>(Func<NodesShutdownRequestParameters, NodesShutdownRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,NodesShutdownRequestParameters>("POST", Url($"_shutdown"), requestParameters);
		
		///<summary>Represents a GET on /_nodes/stats 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> NodesStatsForAll<T>(Func<NodesStatsRequestParameters, NodesStatsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,NodesStatsRequestParameters>("GET", Url($"_nodes/stats"), requestParameters);
		
		///<summary>Represents a GET on /_nodes/stats 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/cluster-nodes-stats.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> NodesStatsForAllAsync<T>(Func<NodesStatsRequestParameters, NodesStatsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,NodesStatsRequestParameters>("GET", Url($"_nodes/stats"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/_percolate 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-percolate.html</para>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> PercolateGet<T>(string index, string type, Func<PercolateRequestParameters, PercolateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PercolateRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_percolate"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/_percolate 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-percolate.html</para>	
	    ///</summary>
		///<param name="index">The index of the document being percolated.</param>
		///<param name="type">The type of the document being percolated.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> PercolateGetAsync<T>(string index, string type, Func<PercolateRequestParameters, PercolateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PercolateRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_percolate"), requestParameters);
		
		///<summary>Represents a HEAD on / 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Ping<T>(Func<PingRequestParameters, PingRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PingRequestParameters>("HEAD", Url($""), requestParameters);
		
		///<summary>Represents a HEAD on / 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> PingAsync<T>(Func<PingRequestParameters, PingRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PingRequestParameters>("HEAD", Url($""), requestParameters);
		
		///<summary>Represents a PUT on /_scripts/{lang}/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-scripting.html</para>	
	    ///</summary>
		///<param name="lang">Script language</param>
		///<param name="id">Script ID</param>
		///<param name="body">The document</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> PutScript<T>(string lang, string id, object body, Func<PutScriptRequestParameters, PutScriptRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PutScriptRequestParameters>("PUT", Url($"_scripts/{lang.NotNull("lang")}/{id.NotNull("id")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_scripts/{lang}/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-scripting.html</para>	
	    ///</summary>
		///<param name="lang">Script language</param>
		///<param name="id">Script ID</param>
		///<param name="body">The document</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> PutScriptAsync<T>(string lang, string id, object body, Func<PutScriptRequestParameters, PutScriptRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PutScriptRequestParameters>("PUT", Url($"_scripts/{lang.NotNull("lang")}/{id.NotNull("id")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_search/template/{id} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-template.html</para>	
	    ///</summary>
		///<param name="id">Template ID</param>
		///<param name="body">The document</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> PutTemplate<T>(string id, object body, Func<PutTemplateRequestParameters, PutTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,PutTemplateRequestParameters>("PUT", Url($"_search/template/{id.NotNull("id")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_search/template/{id} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-template.html</para>	
	    ///</summary>
		///<param name="id">Template ID</param>
		///<param name="body">The document</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> PutTemplateAsync<T>(string id, object body, Func<PutTemplateRequestParameters, PutTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,PutTemplateRequestParameters>("PUT", Url($"_search/template/{id.NotNull("id")}"), requestParameters, body);
		
		///<summary>Represents a GET on /_search/scroll 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-request-scroll.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> ScrollGet<T>(Func<ScrollRequestParameters, ScrollRequestParameters> requestParameters = null) =>
			this.DoRequest<T,ScrollRequestParameters>("GET", Url($"_search/scroll"), requestParameters);
		
		///<summary>Represents a GET on /_search/scroll 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-request-scroll.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> ScrollGetAsync<T>(Func<ScrollRequestParameters, ScrollRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,ScrollRequestParameters>("GET", Url($"_search/scroll"), requestParameters);
		
		///<summary>Represents a GET on /_search 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-search.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SearchGet<T>(Func<SearchRequestParameters, SearchRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SearchRequestParameters>("GET", Url($"_search"), requestParameters);
		
		///<summary>Represents a GET on /_search 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-search.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SearchGetAsync<T>(Func<SearchRequestParameters, SearchRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SearchRequestParameters>("GET", Url($"_search"), requestParameters);
		
		///<summary>Represents a POST on /_search/exists 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-exists.html</para>	
	    ///</summary>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SearchExists<T>(object body, Func<SearchExistsRequestParameters, SearchExistsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SearchExistsRequestParameters>("POST", Url($"_search/exists"), requestParameters, body, allow404: true);
		
		///<summary>Represents a POST on /_search/exists 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-exists.html</para>	
	    ///</summary>
		///<param name="body">A query to restrict the results specified with the Query DSL (optional)</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SearchExistsAsync<T>(object body, Func<SearchExistsRequestParameters, SearchExistsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SearchExistsRequestParameters>("POST", Url($"_search/exists"), requestParameters, body, allow404: true);
		
		///<summary>Represents a GET on /_search_shards 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-shards.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SearchShardsGet<T>(Func<SearchShardsRequestParameters, SearchShardsRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SearchShardsRequestParameters>("GET", Url($"_search_shards"), requestParameters);
		
		///<summary>Represents a GET on /_search_shards 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/search-shards.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SearchShardsGetAsync<T>(Func<SearchShardsRequestParameters, SearchShardsRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SearchShardsRequestParameters>("GET", Url($"_search_shards"), requestParameters);
		
		///<summary>Represents a GET on /_search/template 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/current/search-template.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SearchTemplateGet<T>(Func<SearchTemplateRequestParameters, SearchTemplateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SearchTemplateRequestParameters>("GET", Url($"_search/template"), requestParameters);
		
		///<summary>Represents a GET on /_search/template 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/current/search-template.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SearchTemplateGetAsync<T>(Func<SearchTemplateRequestParameters, SearchTemplateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SearchTemplateRequestParameters>("GET", Url($"_search/template"), requestParameters);
		
		///<summary>Represents a PUT on /_snapshot/{repository}/{snapshot} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">The snapshot definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotCreate<T>(string repository, string snapshot, object body, Func<SnapshotRequestParameters, SnapshotRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SnapshotRequestParameters>("PUT", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_snapshot/{repository}/{snapshot} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">The snapshot definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotCreateAsync<T>(string repository, string snapshot, object body, Func<SnapshotRequestParameters, SnapshotRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SnapshotRequestParameters>("PUT", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_snapshot/{repository} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="body">The repository definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotCreateRepository<T>(string repository, object body, Func<CreateRepositoryRequestParameters, CreateRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,CreateRepositoryRequestParameters>("PUT", Url($"_snapshot/{repository.NotNull("repository")}"), requestParameters, body);
		
		///<summary>Represents a PUT on /_snapshot/{repository} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="body">The repository definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotCreateRepositoryAsync<T>(string repository, object body, Func<CreateRepositoryRequestParameters, CreateRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,CreateRepositoryRequestParameters>("PUT", Url($"_snapshot/{repository.NotNull("repository")}"), requestParameters, body);
		
		///<summary>Represents a DELETE on /_snapshot/{repository}/{snapshot} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotDelete<T>(string repository, string snapshot, Func<DeleteSnapshotRequestParameters, DeleteSnapshotRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteSnapshotRequestParameters>("DELETE", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_snapshot/{repository}/{snapshot} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotDeleteAsync<T>(string repository, string snapshot, Func<DeleteSnapshotRequestParameters, DeleteSnapshotRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteSnapshotRequestParameters>("DELETE", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_snapshot/{repository} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A comma-separated list of repository names</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotDeleteRepository<T>(string repository, Func<DeleteRepositoryRequestParameters, DeleteRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,DeleteRepositoryRequestParameters>("DELETE", Url($"_snapshot/{repository.NotNull("repository")}"), requestParameters);
		
		///<summary>Represents a DELETE on /_snapshot/{repository} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A comma-separated list of repository names</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotDeleteRepositoryAsync<T>(string repository, Func<DeleteRepositoryRequestParameters, DeleteRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,DeleteRepositoryRequestParameters>("DELETE", Url($"_snapshot/{repository.NotNull("repository")}"), requestParameters);
		
		///<summary>Represents a GET on /_snapshot/{repository}/{snapshot} 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A comma-separated list of snapshot names</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotGet<T>(string repository, string snapshot, Func<GetSnapshotRequestParameters, GetSnapshotRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetSnapshotRequestParameters>("GET", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}"), requestParameters);
		
		///<summary>Represents a GET on /_snapshot/{repository}/{snapshot} 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A comma-separated list of snapshot names</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotGetAsync<T>(string repository, string snapshot, Func<GetSnapshotRequestParameters, GetSnapshotRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetSnapshotRequestParameters>("GET", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}"), requestParameters);
		
		///<summary>Represents a GET on /_snapshot 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotGetRepository<T>(Func<GetRepositoryRequestParameters, GetRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,GetRepositoryRequestParameters>("GET", Url($"_snapshot"), requestParameters);
		
		///<summary>Represents a GET on /_snapshot 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotGetRepositoryAsync<T>(Func<GetRepositoryRequestParameters, GetRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,GetRepositoryRequestParameters>("GET", Url($"_snapshot"), requestParameters);
		
		///<summary>Represents a POST on /_snapshot/{repository}/{snapshot}/_restore 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">Details of what to restore</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotRestore<T>(string repository, string snapshot, object body, Func<RestoreRequestParameters, RestoreRequestParameters> requestParameters = null) =>
			this.DoRequest<T,RestoreRequestParameters>("POST", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}/_restore"), requestParameters, body);
		
		///<summary>Represents a POST on /_snapshot/{repository}/{snapshot}/_restore 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="snapshot">A snapshot name</param>
		///<param name="body">Details of what to restore</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotRestoreAsync<T>(string repository, string snapshot, object body, Func<RestoreRequestParameters, RestoreRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,RestoreRequestParameters>("POST", Url($"_snapshot/{repository.NotNull("repository")}/{snapshot.NotNull("snapshot")}/_restore"), requestParameters, body);
		
		///<summary>Represents a GET on /_snapshot/_status 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotStatus<T>(Func<SnapshotStatusRequestParameters, SnapshotStatusRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SnapshotStatusRequestParameters>("GET", Url($"_snapshot/_status"), requestParameters);
		
		///<summary>Represents a GET on /_snapshot/_status 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotStatusAsync<T>(Func<SnapshotStatusRequestParameters, SnapshotStatusRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SnapshotStatusRequestParameters>("GET", Url($"_snapshot/_status"), requestParameters);
		
		///<summary>Represents a POST on /_snapshot/{repository}/_verify 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> SnapshotVerifyRepository<T>(string repository, Func<VerifyRepositoryRequestParameters, VerifyRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequest<T,VerifyRepositoryRequestParameters>("POST", Url($"_snapshot/{repository.NotNull("repository")}/_verify"), requestParameters);
		
		///<summary>Represents a POST on /_snapshot/{repository}/_verify 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html</para>	
	    ///</summary>
		///<param name="repository">A repository name</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SnapshotVerifyRepositoryAsync<T>(string repository, Func<VerifyRepositoryRequestParameters, VerifyRepositoryRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,VerifyRepositoryRequestParameters>("POST", Url($"_snapshot/{repository.NotNull("repository")}/_verify"), requestParameters);
		
		///<summary>Represents a POST on /_suggest 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-suggesters.html</para>	
	    ///</summary>
		///<param name="body">The request definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Suggest<T>(object body, Func<SuggestRequestParameters, SuggestRequestParameters> requestParameters = null) =>
			this.DoRequest<T,SuggestRequestParameters>("POST", Url($"_suggest"), requestParameters, body);
		
		///<summary>Represents a POST on /_suggest 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/search-suggesters.html</para>	
	    ///</summary>
		///<param name="body">The request definition</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> SuggestAsync<T>(object body, Func<SuggestRequestParameters, SuggestRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,SuggestRequestParameters>("POST", Url($"_suggest"), requestParameters, body);
		
		///<summary>Represents a GET on /{index}/{type}/_termvector 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-termvectors.html</para>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> TermvectorGet<T>(string index, string type, Func<TermvectorRequestParameters, TermvectorRequestParameters> requestParameters = null) =>
			this.DoRequest<T,TermvectorRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_termvector"), requestParameters);
		
		///<summary>Represents a GET on /{index}/{type}/_termvector 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-termvectors.html</para>	
	    ///</summary>
		///<param name="index">The index in which the document resides.</param>
		///<param name="type">The type of the document.</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> TermvectorGetAsync<T>(string index, string type, Func<TermvectorRequestParameters, TermvectorRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,TermvectorRequestParameters>("GET", Url($"{index.NotNull("index")}/{type.NotNull("type")}/_termvector"), requestParameters);
		
		///<summary>Represents a POST on /{index}/{type}/{id}/_update 
		///<para></para>Returns: ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-update.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The request definition using either `script` or partial `doc`</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public ElasticsearchResponse<T> Update<T>(string index, string type, string id, object body, Func<UpdateRequestParameters, UpdateRequestParameters> requestParameters = null) =>
			this.DoRequest<T,UpdateRequestParameters>("POST", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_update"), requestParameters, body);
		
		///<summary>Represents a POST on /{index}/{type}/{id}/_update 
		///<para></para>Returns: A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:
		///<para> - T, an object you own that the elasticsearch response will be deserialized to /para>
		///<para> - byte[], no deserialization, but the response stream will be closed</para>
		///<para> - Stream, no deserialization, response stream is your responsibility</para>
		///<para> - VoidResponse, no deserialization, response stream never read and closed</para>
		///<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth 
	    ///<para>See also: http://www.elastic.co/guide/en/elasticsearch/reference/1.6/docs-update.html</para>	
	    ///</summary>
		///<param name="index">The name of the index</param>
		///<param name="type">The type of the document</param>
		///<param name="id">Document ID</param>
		///<param name="body">The request definition using either `script` or partial `doc`</param>
		///<param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		public Task<ElasticsearchResponse<T>> UpdateAsync<T>(string index, string type, string id, object body, Func<UpdateRequestParameters, UpdateRequestParameters> requestParameters = null) =>
			this.DoRequestAsync<T,UpdateRequestParameters>("POST", Url($"{index.NotNull("index")}/{type.NotNull("type")}/{id.NotNull("id")}/_update"), requestParameters, body);
		
	
	  }
	  }
	
