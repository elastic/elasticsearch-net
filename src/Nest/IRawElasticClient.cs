using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IRawElasticClient
	{
		///<summary>HEAD /_alias/{name}</summary>
		ConnectionStatus AliasesExistHead(string name, object body, NameValueCollection queryString = null);

		///<summary>HEAD /_alias/{name}</summary>
		Task<ConnectionStatus> AliasesExistHeadAsync(string name, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/_alias/{name}</summary>
		ConnectionStatus AliasesExistHead(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/_alias/{name}</summary>
		Task<ConnectionStatus> AliasesExistHeadAsync(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>GET /_analyze</summary>
		ConnectionStatus AnalyzeGet(NameValueCollection queryString = null);

		///<summary>GET /_analyze</summary>
		Task<ConnectionStatus> AnalyzeGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_analyze</summary>
		ConnectionStatus AnalyzeGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_analyze</summary>
		Task<ConnectionStatus> AnalyzeGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_analyze</summary>
		ConnectionStatus AnalyzePost(object body, NameValueCollection queryString = null);

		///<summary>POST /_analyze</summary>
		Task<ConnectionStatus> AnalyzePostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_analyze</summary>
		ConnectionStatus AnalyzePost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_analyze</summary>
		Task<ConnectionStatus> AnalyzePostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /_bulk</summary>
		ConnectionStatus BulkPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_bulk</summary>
		Task<ConnectionStatus> BulkPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_bulk</summary>
		ConnectionStatus BulkPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_bulk</summary>
		Task<ConnectionStatus> BulkPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_bulk</summary>
		ConnectionStatus BulkPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_bulk</summary>
		Task<ConnectionStatus> BulkPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>PUT /_bulk</summary>
		ConnectionStatus BulkPut(object body, NameValueCollection queryString = null);

		///<summary>PUT /_bulk</summary>
		Task<ConnectionStatus> BulkPutAsync(object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_bulk</summary>
		ConnectionStatus BulkPut(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_bulk</summary>
		Task<ConnectionStatus> BulkPutAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/_bulk</summary>
		ConnectionStatus BulkPut(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/_bulk</summary>
		Task<ConnectionStatus> BulkPutAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>GET /_cache/clear</summary>
		ConnectionStatus ClearIndicesCacheGet(NameValueCollection queryString = null);

		///<summary>GET /_cache/clear</summary>
		Task<ConnectionStatus> ClearIndicesCacheGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_cache/clear</summary>
		ConnectionStatus ClearIndicesCacheGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_cache/clear</summary>
		Task<ConnectionStatus> ClearIndicesCacheGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_cache/clear</summary>
		ConnectionStatus ClearIndicesCachePost(object body, NameValueCollection queryString = null);

		///<summary>POST /_cache/clear</summary>
		Task<ConnectionStatus> ClearIndicesCachePostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_cache/clear</summary>
		ConnectionStatus ClearIndicesCachePost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_cache/clear</summary>
		Task<ConnectionStatus> ClearIndicesCachePostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /_search/scroll</summary>
		ConnectionStatus ClearScrollDelete(object body, NameValueCollection queryString = null);

		///<summary>DELETE /_search/scroll</summary>
		Task<ConnectionStatus> ClearScrollDeleteAsync(object body, NameValueCollection queryString = null);

		///<summary>DELETE /_search/scroll/{scroll_id}</summary>
		ConnectionStatus ClearScrollDelete(string scroll_id, object body, NameValueCollection queryString = null);

		///<summary>DELETE /_search/scroll/{scroll_id}</summary>
		Task<ConnectionStatus> ClearScrollDeleteAsync(string scroll_id, object body, NameValueCollection queryString = null);

		///<summary>POST /_close</summary>
		ConnectionStatus CloseIndexPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_close</summary>
		Task<ConnectionStatus> CloseIndexPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_close</summary>
		ConnectionStatus CloseIndexPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_close</summary>
		Task<ConnectionStatus> CloseIndexPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /_cluster/settings</summary>
		ConnectionStatus ClusterGetSettings(NameValueCollection queryString = null);

		///<summary>GET /_cluster/settings</summary>
		Task<ConnectionStatus> ClusterGetSettingsAsync(NameValueCollection queryString = null);

		///<summary>GET /_cluster/health</summary>
		ConnectionStatus ClusterHealthGet(NameValueCollection queryString = null);

		///<summary>GET /_cluster/health</summary>
		Task<ConnectionStatus> ClusterHealthGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_cluster/health/{index}</summary>
		ConnectionStatus ClusterHealthGet(string index, NameValueCollection queryString = null);

		///<summary>GET /_cluster/health/{index}</summary>
		Task<ConnectionStatus> ClusterHealthGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_cluster/reroute</summary>
		ConnectionStatus ClusterReroutePost(object body, NameValueCollection queryString = null);

		///<summary>POST /_cluster/reroute</summary>
		Task<ConnectionStatus> ClusterReroutePostAsync(object body, NameValueCollection queryString = null);

		///<summary>GET /_search_shards</summary>
		ConnectionStatus ClusterSearchShardsGet(NameValueCollection queryString = null);

		///<summary>GET /_search_shards</summary>
		Task<ConnectionStatus> ClusterSearchShardsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_search_shards</summary>
		ConnectionStatus ClusterSearchShardsGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_search_shards</summary>
		Task<ConnectionStatus> ClusterSearchShardsGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_search_shards</summary>
		ConnectionStatus ClusterSearchShardsGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_search_shards</summary>
		Task<ConnectionStatus> ClusterSearchShardsGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_search_shards</summary>
		ConnectionStatus ClusterSearchShardsPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_search_shards</summary>
		Task<ConnectionStatus> ClusterSearchShardsPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_search_shards</summary>
		ConnectionStatus ClusterSearchShardsPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_search_shards</summary>
		Task<ConnectionStatus> ClusterSearchShardsPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_search_shards</summary>
		ConnectionStatus ClusterSearchShardsPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_search_shards</summary>
		Task<ConnectionStatus> ClusterSearchShardsPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>GET /_cluster/state</summary>
		ConnectionStatus ClusterStateGet(NameValueCollection queryString = null);

		///<summary>GET /_cluster/state</summary>
		Task<ConnectionStatus> ClusterStateGetAsync(NameValueCollection queryString = null);

		///<summary>PUT /_cluster/settings</summary>
		ConnectionStatus ClusterUpdateSettingsPut(object body, NameValueCollection queryString = null);

		///<summary>PUT /_cluster/settings</summary>
		Task<ConnectionStatus> ClusterUpdateSettingsPutAsync(object body, NameValueCollection queryString = null);

		///<summary>GET /_count</summary>
		ConnectionStatus CountGet(NameValueCollection queryString = null);

		///<summary>GET /_count</summary>
		Task<ConnectionStatus> CountGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_count</summary>
		ConnectionStatus CountGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_count</summary>
		Task<ConnectionStatus> CountGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_count</summary>
		ConnectionStatus CountGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_count</summary>
		Task<ConnectionStatus> CountGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_count</summary>
		ConnectionStatus CountPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_count</summary>
		Task<ConnectionStatus> CountPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_count</summary>
		ConnectionStatus CountPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_count</summary>
		Task<ConnectionStatus> CountPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_count</summary>
		ConnectionStatus CountPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_count</summary>
		Task<ConnectionStatus> CountPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_create</summary>
		ConnectionStatus CreatePost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_create</summary>
		Task<ConnectionStatus> CreatePostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/{id}/_create</summary>
		ConnectionStatus CreatePut(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/{id}/_create</summary>
		Task<ConnectionStatus> CreatePutAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}</summary>
		ConnectionStatus CreateIndexPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}</summary>
		Task<ConnectionStatus> CreateIndexPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}</summary>
		ConnectionStatus CreateIndexPut(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}</summary>
		Task<ConnectionStatus> CreateIndexPutAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/{id}</summary>
		ConnectionStatus Delete(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/{id}</summary>
		Task<ConnectionStatus> DeleteAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_query</summary>
		ConnectionStatus DeleteByQuery(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_query</summary>
		Task<ConnectionStatus> DeleteByQueryAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/_query</summary>
		ConnectionStatus DeleteByQuery(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/_query</summary>
		Task<ConnectionStatus> DeleteByQueryAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>DELETE /</summary>
		ConnectionStatus DeleteIndex(object body, NameValueCollection queryString = null);

		///<summary>DELETE /</summary>
		Task<ConnectionStatus> DeleteIndexAsync(object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}</summary>
		ConnectionStatus DeleteIndex(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}</summary>
		Task<ConnectionStatus> DeleteIndexAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /_template/{name}</summary>
		ConnectionStatus DeleteIndexTemplate(string name, object body, NameValueCollection queryString = null);

		///<summary>DELETE /_template/{name}</summary>
		Task<ConnectionStatus> DeleteIndexTemplateAsync(string name, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/_mapping</summary>
		ConnectionStatus DeleteMapping(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/_mapping</summary>
		Task<ConnectionStatus> DeleteMappingAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_warmer</summary>
		ConnectionStatus DeleteWarmer(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_warmer</summary>
		Task<ConnectionStatus> DeleteWarmerAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_warmer/{name}</summary>
		ConnectionStatus DeleteWarmer(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_warmer/{name}</summary>
		Task<ConnectionStatus> DeleteWarmerAsync(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/_warmer/{name}</summary>
		ConnectionStatus DeleteWarmer(string index, string type, string name, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/{type}/_warmer/{name}</summary>
		Task<ConnectionStatus> DeleteWarmerAsync(string index, string type, string name, object body, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_explain</summary>
		ConnectionStatus ExplainGet(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_explain</summary>
		Task<ConnectionStatus> ExplainGetAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_explain</summary>
		ConnectionStatus ExplainPost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_explain</summary>
		Task<ConnectionStatus> ExplainPostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>GET /_flush</summary>
		ConnectionStatus FlushGet(NameValueCollection queryString = null);

		///<summary>GET /_flush</summary>
		Task<ConnectionStatus> FlushGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_flush</summary>
		ConnectionStatus FlushGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_flush</summary>
		Task<ConnectionStatus> FlushGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_flush</summary>
		ConnectionStatus FlushPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_flush</summary>
		Task<ConnectionStatus> FlushPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_flush</summary>
		ConnectionStatus FlushPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_flush</summary>
		Task<ConnectionStatus> FlushPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /_gateway/snapshot</summary>
		ConnectionStatus GatewaySnapshotPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_gateway/snapshot</summary>
		Task<ConnectionStatus> GatewaySnapshotPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_gateway/snapshot</summary>
		ConnectionStatus GatewaySnapshotPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_gateway/snapshot</summary>
		Task<ConnectionStatus> GatewaySnapshotPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}</summary>
		ConnectionStatus Get(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}</summary>
		Task<ConnectionStatus> GetAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /_alias/{name}</summary>
		ConnectionStatus GetAliases(string name, NameValueCollection queryString = null);

		///<summary>GET /_alias/{name}</summary>
		Task<ConnectionStatus> GetAliasesAsync(string name, NameValueCollection queryString = null);

		///<summary>GET /{index}/_alias/{name}</summary>
		ConnectionStatus GetAliases(string index, string name, NameValueCollection queryString = null);

		///<summary>GET /{index}/_alias/{name}</summary>
		Task<ConnectionStatus> GetAliasesAsync(string index, string name, NameValueCollection queryString = null);

		///<summary>GET /_template</summary>
		ConnectionStatus GetIndexTemplate(NameValueCollection queryString = null);

		///<summary>GET /_template</summary>
		Task<ConnectionStatus> GetIndexTemplateAsync(NameValueCollection queryString = null);

		///<summary>GET /_template/{name}</summary>
		ConnectionStatus GetIndexTemplate(string name, NameValueCollection queryString = null);

		///<summary>GET /_template/{name}</summary>
		Task<ConnectionStatus> GetIndexTemplateAsync(string name, NameValueCollection queryString = null);

		///<summary>GET /_aliases</summary>
		ConnectionStatus GetIndicesAliases(NameValueCollection queryString = null);

		///<summary>GET /_aliases</summary>
		Task<ConnectionStatus> GetIndicesAliasesAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_aliases</summary>
		ConnectionStatus GetIndicesAliases(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_aliases</summary>
		Task<ConnectionStatus> GetIndicesAliasesAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /_mapping</summary>
		ConnectionStatus GetMapping(NameValueCollection queryString = null);

		///<summary>GET /_mapping</summary>
		Task<ConnectionStatus> GetMappingAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_mapping</summary>
		ConnectionStatus GetMapping(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_mapping</summary>
		Task<ConnectionStatus> GetMappingAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_mapping</summary>
		ConnectionStatus GetMapping(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_mapping</summary>
		Task<ConnectionStatus> GetMappingAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /_settings</summary>
		ConnectionStatus GetSettings(NameValueCollection queryString = null);

		///<summary>GET /_settings</summary>
		Task<ConnectionStatus> GetSettingsAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_settings</summary>
		ConnectionStatus GetSettings(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_settings</summary>
		Task<ConnectionStatus> GetSettingsAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_source</summary>
		ConnectionStatus GetSource(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_source</summary>
		Task<ConnectionStatus> GetSourceAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/_warmer</summary>
		ConnectionStatus GetWarmer(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_warmer</summary>
		Task<ConnectionStatus> GetWarmerAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_warmer/{name}</summary>
		ConnectionStatus GetWarmer(string index, string name, NameValueCollection queryString = null);

		///<summary>GET /{index}/_warmer/{name}</summary>
		Task<ConnectionStatus> GetWarmerAsync(string index, string name, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_warmer/{name}</summary>
		ConnectionStatus GetWarmer(string index, string type, string name, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_warmer/{name}</summary>
		Task<ConnectionStatus> GetWarmerAsync(string index, string type, string name, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/{type}/{id}</summary>
		ConnectionStatus Head(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/{type}/{id}</summary>
		Task<ConnectionStatus> HeadAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>HEAD /_template/{name}</summary>
		ConnectionStatus HeadIndexTemplate(string name, object body, NameValueCollection queryString = null);

		///<summary>HEAD /_template/{name}</summary>
		Task<ConnectionStatus> HeadIndexTemplateAsync(string name, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/{type}/{id}/_source</summary>
		ConnectionStatus HeadSource(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/{type}/{id}/_source</summary>
		Task<ConnectionStatus> HeadSourceAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}</summary>
		ConnectionStatus IndexPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}</summary>
		Task<ConnectionStatus> IndexPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}</summary>
		ConnectionStatus IndexPost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}</summary>
		Task<ConnectionStatus> IndexPostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/{id}</summary>
		ConnectionStatus IndexPut(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/{id}</summary>
		Task<ConnectionStatus> IndexPutAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_alias/{name}</summary>
		ConnectionStatus IndexDeleteAliases(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>DELETE /{index}/_alias/{name}</summary>
		Task<ConnectionStatus> IndexDeleteAliasesAsync(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/completion</summary>
		ConnectionStatus IndexFilteredStatsCompletionGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/completion</summary>
		Task<ConnectionStatus> IndexFilteredStatsCompletionGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/completion/{fields}</summary>
		ConnectionStatus IndexFilteredStatsCompletionGet(string index, string fields, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/completion/{fields}</summary>
		Task<ConnectionStatus> IndexFilteredStatsCompletionGetAsync(string index, string fields, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/docs</summary>
		ConnectionStatus IndexFilteredStatsDocsGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/docs</summary>
		Task<ConnectionStatus> IndexFilteredStatsDocsGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/fielddata</summary>
		ConnectionStatus IndexFilteredStatsFielddataGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/fielddata</summary>
		Task<ConnectionStatus> IndexFilteredStatsFielddataGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/fielddata/{fields}</summary>
		ConnectionStatus IndexFilteredStatsFielddataGet(string index, string fields, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/fielddata/{fields}</summary>
		Task<ConnectionStatus> IndexFilteredStatsFielddataGetAsync(string index, string fields, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/filter_cache</summary>
		ConnectionStatus IndexFilteredStatsFilter_cacheGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/filter_cache</summary>
		Task<ConnectionStatus> IndexFilteredStatsFilter_cacheGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/flush</summary>
		ConnectionStatus IndexFilteredStatsFlushGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/flush</summary>
		Task<ConnectionStatus> IndexFilteredStatsFlushGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/get</summary>
		ConnectionStatus IndexFilteredStatsGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/get</summary>
		Task<ConnectionStatus> IndexFilteredStatsGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/id_cache</summary>
		ConnectionStatus IndexFilteredStatsId_cacheGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/id_cache</summary>
		Task<ConnectionStatus> IndexFilteredStatsId_cacheGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/indexing</summary>
		ConnectionStatus IndexFilteredStatsIndexingGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/indexing</summary>
		Task<ConnectionStatus> IndexFilteredStatsIndexingGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/indexing/{indexingTypes2}</summary>
		ConnectionStatus IndexFilteredStatsIndexingGet(string index, string indexingTypes2, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/indexing/{indexingTypes2}</summary>
		Task<ConnectionStatus> IndexFilteredStatsIndexingGetAsync(string index, string indexingTypes2, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/merge</summary>
		ConnectionStatus IndexFilteredStatsMergeGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/merge</summary>
		Task<ConnectionStatus> IndexFilteredStatsMergeGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/percolate</summary>
		ConnectionStatus IndexFilteredStatsPercolateGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/percolate</summary>
		Task<ConnectionStatus> IndexFilteredStatsPercolateGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/refresh</summary>
		ConnectionStatus IndexFilteredStatsRefreshGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/refresh</summary>
		Task<ConnectionStatus> IndexFilteredStatsRefreshGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/search</summary>
		ConnectionStatus IndexFilteredStatsSearchGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/search</summary>
		Task<ConnectionStatus> IndexFilteredStatsSearchGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/search/{searchGroupsStats2}</summary>
		ConnectionStatus IndexFilteredStatsSearchGet(string index, string searchGroupsStats2, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/search/{searchGroupsStats2}</summary>
		Task<ConnectionStatus> IndexFilteredStatsSearchGetAsync(string index, string searchGroupsStats2, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/store</summary>
		ConnectionStatus IndexFilteredStatsStoreGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/store</summary>
		Task<ConnectionStatus> IndexFilteredStatsStoreGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/warmer</summary>
		ConnectionStatus IndexFilteredStatsWarmerGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats/warmer</summary>
		Task<ConnectionStatus> IndexFilteredStatsWarmerGetAsync(string index, NameValueCollection queryString = null);

		///<summary>PUT /_alias</summary>
		ConnectionStatus IndexPutAlias(object body, NameValueCollection queryString = null);

		///<summary>PUT /_alias</summary>
		Task<ConnectionStatus> IndexPutAliasAsync(object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_alias</summary>
		ConnectionStatus IndexPutAlias(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_alias</summary>
		Task<ConnectionStatus> IndexPutAliasAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_alias/{name}</summary>
		ConnectionStatus IndexPutAlias(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_alias/{name}</summary>
		Task<ConnectionStatus> IndexPutAliasAsync(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /_alias/{name}</summary>
		ConnectionStatus IndexPutAliasByName(string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /_alias/{name}</summary>
		Task<ConnectionStatus> IndexPutAliasByNameAsync(string name, object body, NameValueCollection queryString = null);

		///<summary>GET /_cat/indices</summary>
		ConnectionStatus IndicesGet(NameValueCollection queryString = null);

		///<summary>GET /_cat/indices</summary>
		Task<ConnectionStatus> IndicesGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_cat/indices/{index}</summary>
		ConnectionStatus IndicesGet(string index, NameValueCollection queryString = null);

		///<summary>GET /_cat/indices/{index}</summary>
		Task<ConnectionStatus> IndicesGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_aliases</summary>
		ConnectionStatus IndicesAliasesPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_aliases</summary>
		Task<ConnectionStatus> IndicesAliasesPostAsync(object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}</summary>
		ConnectionStatus IndicesExistsHead(string index, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}</summary>
		Task<ConnectionStatus> IndicesExistsHeadAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /_segments</summary>
		ConnectionStatus IndicesSegmentsGet(NameValueCollection queryString = null);

		///<summary>GET /_segments</summary>
		Task<ConnectionStatus> IndicesSegmentsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_segments</summary>
		ConnectionStatus IndicesSegmentsGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_segments</summary>
		Task<ConnectionStatus> IndicesSegmentsGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /_stats/completion</summary>
		ConnectionStatus IndicesStatCompletionGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/completion</summary>
		Task<ConnectionStatus> IndicesStatCompletionGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/completion/{fields}</summary>
		ConnectionStatus IndicesStatCompletionGet(string fields, NameValueCollection queryString = null);

		///<summary>GET /_stats/completion/{fields}</summary>
		Task<ConnectionStatus> IndicesStatCompletionGetAsync(string fields, NameValueCollection queryString = null);

		///<summary>GET /_stats/docs</summary>
		ConnectionStatus IndicesStatDocsGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/docs</summary>
		Task<ConnectionStatus> IndicesStatDocsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/fielddata</summary>
		ConnectionStatus IndicesStatFielddataGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/fielddata</summary>
		Task<ConnectionStatus> IndicesStatFielddataGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/fielddata/{fields}</summary>
		ConnectionStatus IndicesStatFielddataGet(string fields, NameValueCollection queryString = null);

		///<summary>GET /_stats/fielddata/{fields}</summary>
		Task<ConnectionStatus> IndicesStatFielddataGetAsync(string fields, NameValueCollection queryString = null);

		///<summary>GET /_stats/filter_cache</summary>
		ConnectionStatus IndicesStatFilter_cacheGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/filter_cache</summary>
		Task<ConnectionStatus> IndicesStatFilter_cacheGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/flush</summary>
		ConnectionStatus IndicesStatFlushGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/flush</summary>
		Task<ConnectionStatus> IndicesStatFlushGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/get</summary>
		ConnectionStatus IndicesStatGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/get</summary>
		Task<ConnectionStatus> IndicesStatGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/id_cache</summary>
		ConnectionStatus IndicesStatId_cacheGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/id_cache</summary>
		Task<ConnectionStatus> IndicesStatId_cacheGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/indexing</summary>
		ConnectionStatus IndicesStatIndexingGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/indexing</summary>
		Task<ConnectionStatus> IndicesStatIndexingGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/indexing/{indexingTypes1}</summary>
		ConnectionStatus IndicesStatIndexingGet(string indexingTypes1, NameValueCollection queryString = null);

		///<summary>GET /_stats/indexing/{indexingTypes1}</summary>
		Task<ConnectionStatus> IndicesStatIndexingGetAsync(string indexingTypes1, NameValueCollection queryString = null);

		///<summary>GET /_stats/merge</summary>
		ConnectionStatus IndicesStatMergeGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/merge</summary>
		Task<ConnectionStatus> IndicesStatMergeGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/percolate</summary>
		ConnectionStatus IndicesStatPercolateGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/percolate</summary>
		Task<ConnectionStatus> IndicesStatPercolateGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/refresh</summary>
		ConnectionStatus IndicesStatRefreshGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/refresh</summary>
		Task<ConnectionStatus> IndicesStatRefreshGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/search</summary>
		ConnectionStatus IndicesStatSearchGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/search</summary>
		Task<ConnectionStatus> IndicesStatSearchGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/search/{searchGroupsStats1}</summary>
		ConnectionStatus IndicesStatSearchGet(string searchGroupsStats1, NameValueCollection queryString = null);

		///<summary>GET /_stats/search/{searchGroupsStats1}</summary>
		Task<ConnectionStatus> IndicesStatSearchGetAsync(string searchGroupsStats1, NameValueCollection queryString = null);

		///<summary>GET /_stats/store</summary>
		ConnectionStatus IndicesStatStoreGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/store</summary>
		Task<ConnectionStatus> IndicesStatStoreGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats/warmer</summary>
		ConnectionStatus IndicesStatWarmerGet(NameValueCollection queryString = null);

		///<summary>GET /_stats/warmer</summary>
		Task<ConnectionStatus> IndicesStatWarmerGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_stats</summary>
		ConnectionStatus IndicesStatsGet(NameValueCollection queryString = null);

		///<summary>GET /_stats</summary>
		Task<ConnectionStatus> IndicesStatsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats</summary>
		ConnectionStatus IndicesStatsGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_stats</summary>
		Task<ConnectionStatus> IndicesStatsGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /_status</summary>
		ConnectionStatus IndicesStatusGet(NameValueCollection queryString = null);

		///<summary>GET /_status</summary>
		Task<ConnectionStatus> IndicesStatusGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_status</summary>
		ConnectionStatus IndicesStatusGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_status</summary>
		Task<ConnectionStatus> IndicesStatusGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /</summary>
		ConnectionStatus MainGet(NameValueCollection queryString = null);

		///<summary>GET /</summary>
		Task<ConnectionStatus> MainGetAsync(NameValueCollection queryString = null);

		///<summary>HEAD /</summary>
		ConnectionStatus MainHead(object body, NameValueCollection queryString = null);

		///<summary>HEAD /</summary>
		Task<ConnectionStatus> MainHeadAsync(object body, NameValueCollection queryString = null);

		///<summary>GET /_cat/master</summary>
		ConnectionStatus MasterGet(NameValueCollection queryString = null);

		///<summary>GET /_cat/master</summary>
		Task<ConnectionStatus> MasterGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_mlt</summary>
		ConnectionStatus MoreLikeThisGet(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_mlt</summary>
		Task<ConnectionStatus> MoreLikeThisGetAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_mlt</summary>
		ConnectionStatus MoreLikeThisPost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_mlt</summary>
		Task<ConnectionStatus> MoreLikeThisPostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>GET /_mget</summary>
		ConnectionStatus MultiGet(NameValueCollection queryString = null);

		///<summary>GET /_mget</summary>
		Task<ConnectionStatus> MultiGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_mget</summary>
		ConnectionStatus MultiGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_mget</summary>
		Task<ConnectionStatus> MultiGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_mget</summary>
		ConnectionStatus MultiGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_mget</summary>
		Task<ConnectionStatus> MultiGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_mget</summary>
		ConnectionStatus MultiGetPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_mget</summary>
		Task<ConnectionStatus> MultiGetPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mget</summary>
		ConnectionStatus MultiGetPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mget</summary>
		Task<ConnectionStatus> MultiGetPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mget</summary>
		ConnectionStatus MultiGetPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mget</summary>
		Task<ConnectionStatus> MultiGetPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /_mpercolate</summary>
		ConnectionStatus MultiPercolatePost(object body, NameValueCollection queryString = null);

		///<summary>POST /_mpercolate</summary>
		Task<ConnectionStatus> MultiPercolatePostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mpercolate</summary>
		ConnectionStatus MultiPercolatePost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mpercolate</summary>
		Task<ConnectionStatus> MultiPercolatePostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mpercolate</summary>
		ConnectionStatus MultiPercolatePost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mpercolate</summary>
		Task<ConnectionStatus> MultiPercolatePostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>GET /_msearch</summary>
		ConnectionStatus MultiSearchGet(NameValueCollection queryString = null);

		///<summary>GET /_msearch</summary>
		Task<ConnectionStatus> MultiSearchGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_msearch</summary>
		ConnectionStatus MultiSearchGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_msearch</summary>
		Task<ConnectionStatus> MultiSearchGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_msearch</summary>
		ConnectionStatus MultiSearchGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_msearch</summary>
		Task<ConnectionStatus> MultiSearchGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_msearch</summary>
		ConnectionStatus MultiSearchPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_msearch</summary>
		Task<ConnectionStatus> MultiSearchPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_msearch</summary>
		ConnectionStatus MultiSearchPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_msearch</summary>
		Task<ConnectionStatus> MultiSearchPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_msearch</summary>
		ConnectionStatus MultiSearchPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_msearch</summary>
		Task<ConnectionStatus> MultiSearchPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>GET /_mtermvectors</summary>
		ConnectionStatus MultiTermVectorsGet(NameValueCollection queryString = null);

		///<summary>GET /_mtermvectors</summary>
		Task<ConnectionStatus> MultiTermVectorsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_mtermvectors</summary>
		ConnectionStatus MultiTermVectorsGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_mtermvectors</summary>
		Task<ConnectionStatus> MultiTermVectorsGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_mtermvectors</summary>
		ConnectionStatus MultiTermVectorsGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_mtermvectors</summary>
		Task<ConnectionStatus> MultiTermVectorsGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_mtermvectors</summary>
		ConnectionStatus MultiTermVectorsPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_mtermvectors</summary>
		Task<ConnectionStatus> MultiTermVectorsPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mtermvectors</summary>
		ConnectionStatus MultiTermVectorsPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mtermvectors</summary>
		Task<ConnectionStatus> MultiTermVectorsPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mtermvectors</summary>
		ConnectionStatus MultiTermVectorsPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mtermvectors</summary>
		Task<ConnectionStatus> MultiTermVectorsPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/http</summary>
		ConnectionStatus NodeInfoHttpGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/http</summary>
		Task<ConnectionStatus> NodeInfoHttpGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/jvm</summary>
		ConnectionStatus NodeInfoJvmGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/jvm</summary>
		Task<ConnectionStatus> NodeInfoJvmGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/network</summary>
		ConnectionStatus NodeInfoNetworkGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/network</summary>
		Task<ConnectionStatus> NodeInfoNetworkGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/os</summary>
		ConnectionStatus NodeInfoOsGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/os</summary>
		Task<ConnectionStatus> NodeInfoOsGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/plugin</summary>
		ConnectionStatus NodeInfoPluginGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/plugin</summary>
		Task<ConnectionStatus> NodeInfoPluginGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/process</summary>
		ConnectionStatus NodeInfoProcessGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/process</summary>
		Task<ConnectionStatus> NodeInfoProcessGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/settings</summary>
		ConnectionStatus NodeInfoSettingsGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/settings</summary>
		Task<ConnectionStatus> NodeInfoSettingsGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/thread_pool</summary>
		ConnectionStatus NodeInfoThread_poolGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/thread_pool</summary>
		Task<ConnectionStatus> NodeInfoThread_poolGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/transport</summary>
		ConnectionStatus NodeInfoTransportGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/transport</summary>
		Task<ConnectionStatus> NodeInfoTransportGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/fs</summary>
		ConnectionStatus NodeStatsFsGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/fs</summary>
		Task<ConnectionStatus> NodeStatsFsGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/http</summary>
		ConnectionStatus NodeStatsHttpGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/http</summary>
		Task<ConnectionStatus> NodeStatsHttpGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/indices</summary>
		ConnectionStatus NodeStatsIndicesGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/indices</summary>
		Task<ConnectionStatus> NodeStatsIndicesGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/indices/{flags}</summary>
		ConnectionStatus NodeStatsIndicesGet(string nodeId, string flags, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/indices/{flags}</summary>
		Task<ConnectionStatus> NodeStatsIndicesGetAsync(string nodeId, string flags, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/indices/{flags}/{fields}</summary>
		ConnectionStatus NodeStatsIndicesGet(string nodeId, string flags, string fields, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/indices/{flags}/{fields}</summary>
		Task<ConnectionStatus> NodeStatsIndicesGetAsync(string nodeId, string flags, string fields, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/jvm</summary>
		ConnectionStatus NodeStatsJvmGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/jvm</summary>
		Task<ConnectionStatus> NodeStatsJvmGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/network</summary>
		ConnectionStatus NodeStatsNetworkGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/network</summary>
		Task<ConnectionStatus> NodeStatsNetworkGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/os</summary>
		ConnectionStatus NodeStatsOsGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/os</summary>
		Task<ConnectionStatus> NodeStatsOsGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/process</summary>
		ConnectionStatus NodeStatsProcessGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/process</summary>
		Task<ConnectionStatus> NodeStatsProcessGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/thread_pool</summary>
		ConnectionStatus NodeStatsThread_poolGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/thread_pool</summary>
		Task<ConnectionStatus> NodeStatsThread_poolGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/transport</summary>
		ConnectionStatus NodeStatsTransportGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats/transport</summary>
		Task<ConnectionStatus> NodeStatsTransportGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_cat/nodes</summary>
		ConnectionStatus NodesGet(NameValueCollection queryString = null);

		///<summary>GET /_cat/nodes</summary>
		Task<ConnectionStatus> NodesGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/hot_threads</summary>
		ConnectionStatus NodesHotThreadsGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/hot_threads</summary>
		Task<ConnectionStatus> NodesHotThreadsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/hot_threads</summary>
		ConnectionStatus NodesHotThreadsGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/hot_threads</summary>
		Task<ConnectionStatus> NodesHotThreadsGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes</summary>
		ConnectionStatus NodesInfoGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes</summary>
		Task<ConnectionStatus> NodesInfoGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}</summary>
		ConnectionStatus NodesInfoGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}</summary>
		Task<ConnectionStatus> NodesInfoGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/http</summary>
		ConnectionStatus NodesInfoHttpGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/http</summary>
		Task<ConnectionStatus> NodesInfoHttpGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/jvm</summary>
		ConnectionStatus NodesInfoJvmGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/jvm</summary>
		Task<ConnectionStatus> NodesInfoJvmGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/network</summary>
		ConnectionStatus NodesInfoNetworkGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/network</summary>
		Task<ConnectionStatus> NodesInfoNetworkGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/os</summary>
		ConnectionStatus NodesInfoOsGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/os</summary>
		Task<ConnectionStatus> NodesInfoOsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/plugin</summary>
		ConnectionStatus NodesInfoPluginGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/plugin</summary>
		Task<ConnectionStatus> NodesInfoPluginGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/process</summary>
		ConnectionStatus NodesInfoProcessGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/process</summary>
		Task<ConnectionStatus> NodesInfoProcessGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/settings</summary>
		ConnectionStatus NodesInfoSettingsGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/settings</summary>
		Task<ConnectionStatus> NodesInfoSettingsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/thread_pool</summary>
		ConnectionStatus NodesInfoThread_poolGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/thread_pool</summary>
		Task<ConnectionStatus> NodesInfoThread_poolGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/transport</summary>
		ConnectionStatus NodesInfoTransportGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/transport</summary>
		Task<ConnectionStatus> NodesInfoTransportGetAsync(NameValueCollection queryString = null);

		///<summary>POST /_cluster/nodes/_restart</summary>
		ConnectionStatus NodesRestartPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_cluster/nodes/_restart</summary>
		Task<ConnectionStatus> NodesRestartPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /_cluster/nodes/{nodeId}/_restart</summary>
		ConnectionStatus NodesRestartPost(string nodeId, object body, NameValueCollection queryString = null);

		///<summary>POST /_cluster/nodes/{nodeId}/_restart</summary>
		Task<ConnectionStatus> NodesRestartPostAsync(string nodeId, object body, NameValueCollection queryString = null);

		///<summary>POST /_cluster/nodes/{nodeId}/_shutdown</summary>
		ConnectionStatus NodesShutdownPost(string nodeId, object body, NameValueCollection queryString = null);

		///<summary>POST /_cluster/nodes/{nodeId}/_shutdown</summary>
		Task<ConnectionStatus> NodesShutdownPostAsync(string nodeId, object body, NameValueCollection queryString = null);

		///<summary>POST /_shutdown</summary>
		ConnectionStatus NodesShutdownPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_shutdown</summary>
		Task<ConnectionStatus> NodesShutdownPostAsync(object body, NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats</summary>
		ConnectionStatus NodesStatsGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats</summary>
		Task<ConnectionStatus> NodesStatsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats</summary>
		ConnectionStatus NodesStatsGet(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/{nodeId}/stats</summary>
		Task<ConnectionStatus> NodesStatsGetAsync(string nodeId, NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/fs</summary>
		ConnectionStatus NodesStatsFsGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/fs</summary>
		Task<ConnectionStatus> NodesStatsFsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/http</summary>
		ConnectionStatus NodesStatsHttpGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/http</summary>
		Task<ConnectionStatus> NodesStatsHttpGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/indices</summary>
		ConnectionStatus NodesStatsIndicesGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/indices</summary>
		Task<ConnectionStatus> NodesStatsIndicesGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/indices/{flags}</summary>
		ConnectionStatus NodesStatsIndicesGet(string flags, NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/indices/{flags}</summary>
		Task<ConnectionStatus> NodesStatsIndicesGetAsync(string flags, NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/indices/{flags}/{fields}</summary>
		ConnectionStatus NodesStatsIndicesGet(string flags, string fields, NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/indices/{flags}/{fields}</summary>
		Task<ConnectionStatus> NodesStatsIndicesGetAsync(string flags, string fields, NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/jvm</summary>
		ConnectionStatus NodesStatsJvmGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/jvm</summary>
		Task<ConnectionStatus> NodesStatsJvmGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/network</summary>
		ConnectionStatus NodesStatsNetworkGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/network</summary>
		Task<ConnectionStatus> NodesStatsNetworkGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/os</summary>
		ConnectionStatus NodesStatsOsGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/os</summary>
		Task<ConnectionStatus> NodesStatsOsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/process</summary>
		ConnectionStatus NodesStatsProcessGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/process</summary>
		Task<ConnectionStatus> NodesStatsProcessGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/thread_pool</summary>
		ConnectionStatus NodesStatsThread_poolGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/thread_pool</summary>
		Task<ConnectionStatus> NodesStatsThread_poolGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/transport</summary>
		ConnectionStatus NodesStatsTransportGet(NameValueCollection queryString = null);

		///<summary>GET /_nodes/stats/transport</summary>
		Task<ConnectionStatus> NodesStatsTransportGetAsync(NameValueCollection queryString = null);

		///<summary>POST /_open</summary>
		ConnectionStatus OpenIndexPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_open</summary>
		Task<ConnectionStatus> OpenIndexPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_open</summary>
		ConnectionStatus OpenIndexPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_open</summary>
		Task<ConnectionStatus> OpenIndexPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /_optimize</summary>
		ConnectionStatus OptimizeGet(NameValueCollection queryString = null);

		///<summary>GET /_optimize</summary>
		Task<ConnectionStatus> OptimizeGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_optimize</summary>
		ConnectionStatus OptimizeGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_optimize</summary>
		Task<ConnectionStatus> OptimizeGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_optimize</summary>
		ConnectionStatus OptimizePost(object body, NameValueCollection queryString = null);

		///<summary>POST /_optimize</summary>
		Task<ConnectionStatus> OptimizePostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_optimize</summary>
		ConnectionStatus OptimizePost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_optimize</summary>
		Task<ConnectionStatus> OptimizePostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /_cluster/pending_tasks</summary>
		ConnectionStatus PendingClusterTasksGet(NameValueCollection queryString = null);

		///<summary>GET /_cluster/pending_tasks</summary>
		Task<ConnectionStatus> PendingClusterTasksGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_percolate</summary>
		ConnectionStatus PercolateGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_percolate</summary>
		Task<ConnectionStatus> PercolateGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_percolate</summary>
		ConnectionStatus PercolateGet(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_percolate</summary>
		Task<ConnectionStatus> PercolateGetAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_percolate</summary>
		ConnectionStatus PercolatePost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_percolate</summary>
		Task<ConnectionStatus> PercolatePostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_percolate</summary>
		ConnectionStatus PercolatePost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_percolate</summary>
		Task<ConnectionStatus> PercolatePostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_percolate/count</summary>
		ConnectionStatus PercolateCountGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_percolate/count</summary>
		Task<ConnectionStatus> PercolateCountGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_percolate/count</summary>
		ConnectionStatus PercolateCountGet(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_percolate/count</summary>
		Task<ConnectionStatus> PercolateCountGetAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_percolate/count</summary>
		ConnectionStatus PercolateCountPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_percolate/count</summary>
		Task<ConnectionStatus> PercolateCountPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_percolate/count</summary>
		ConnectionStatus PercolateCountPost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_percolate/count</summary>
		Task<ConnectionStatus> PercolateCountPostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /_template/{name}</summary>
		ConnectionStatus PutIndexTemplatePost(string name, object body, NameValueCollection queryString = null);

		///<summary>POST /_template/{name}</summary>
		Task<ConnectionStatus> PutIndexTemplatePostAsync(string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /_template/{name}</summary>
		ConnectionStatus PutIndexTemplate(string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /_template/{name}</summary>
		Task<ConnectionStatus> PutIndexTemplateAsync(string name, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mapping</summary>
		ConnectionStatus PutMappingPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_mapping</summary>
		Task<ConnectionStatus> PutMappingPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mapping</summary>
		ConnectionStatus PutMappingPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_mapping</summary>
		Task<ConnectionStatus> PutMappingPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_mapping</summary>
		ConnectionStatus PutMapping(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_mapping</summary>
		Task<ConnectionStatus> PutMappingAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/_mapping</summary>
		ConnectionStatus PutMapping(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/_mapping</summary>
		Task<ConnectionStatus> PutMappingAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_warmer/{name}</summary>
		ConnectionStatus PutWarmer(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_warmer/{name}</summary>
		Task<ConnectionStatus> PutWarmerAsync(string index, string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/_warmer/{name}</summary>
		ConnectionStatus PutWarmer(string index, string type, string name, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/{type}/_warmer/{name}</summary>
		Task<ConnectionStatus> PutWarmerAsync(string index, string type, string name, object body, NameValueCollection queryString = null);

		///<summary>GET /_refresh</summary>
		ConnectionStatus RefreshGet(NameValueCollection queryString = null);

		///<summary>GET /_refresh</summary>
		Task<ConnectionStatus> RefreshGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_refresh</summary>
		ConnectionStatus RefreshGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_refresh</summary>
		Task<ConnectionStatus> RefreshGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_refresh</summary>
		ConnectionStatus RefreshPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_refresh</summary>
		Task<ConnectionStatus> RefreshPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_refresh</summary>
		ConnectionStatus RefreshPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_refresh</summary>
		Task<ConnectionStatus> RefreshPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /_search</summary>
		ConnectionStatus SearchGet(NameValueCollection queryString = null);

		///<summary>GET /_search</summary>
		Task<ConnectionStatus> SearchGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_search</summary>
		ConnectionStatus SearchGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_search</summary>
		Task<ConnectionStatus> SearchGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_search</summary>
		ConnectionStatus SearchGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_search</summary>
		Task<ConnectionStatus> SearchGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_search</summary>
		ConnectionStatus SearchPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_search</summary>
		Task<ConnectionStatus> SearchPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_search</summary>
		ConnectionStatus SearchPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_search</summary>
		Task<ConnectionStatus> SearchPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_search</summary>
		ConnectionStatus SearchPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_search</summary>
		Task<ConnectionStatus> SearchPostAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>GET /_search/scroll</summary>
		ConnectionStatus SearchScrollGet(NameValueCollection queryString = null);

		///<summary>GET /_search/scroll</summary>
		Task<ConnectionStatus> SearchScrollGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_search/scroll/{scroll_id}</summary>
		ConnectionStatus SearchScrollGet(string scroll_id, NameValueCollection queryString = null);

		///<summary>GET /_search/scroll/{scroll_id}</summary>
		Task<ConnectionStatus> SearchScrollGetAsync(string scroll_id, NameValueCollection queryString = null);

		///<summary>POST /_search/scroll</summary>
		ConnectionStatus SearchScrollPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_search/scroll</summary>
		Task<ConnectionStatus> SearchScrollPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /_search/scroll/{scroll_id}</summary>
		ConnectionStatus SearchScrollPost(string scroll_id, object body, NameValueCollection queryString = null);

		///<summary>POST /_search/scroll/{scroll_id}</summary>
		Task<ConnectionStatus> SearchScrollPostAsync(string scroll_id, object body, NameValueCollection queryString = null);

		///<summary>GET /_cat/shards</summary>
		ConnectionStatus ShardsGet(NameValueCollection queryString = null);

		///<summary>GET /_cat/shards</summary>
		Task<ConnectionStatus> ShardsGetAsync(NameValueCollection queryString = null);

		///<summary>GET /_suggest</summary>
		ConnectionStatus SuggestGet(NameValueCollection queryString = null);

		///<summary>GET /_suggest</summary>
		Task<ConnectionStatus> SuggestGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_suggest</summary>
		ConnectionStatus SuggestGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_suggest</summary>
		Task<ConnectionStatus> SuggestGetAsync(string index, NameValueCollection queryString = null);

		///<summary>POST /_suggest</summary>
		ConnectionStatus SuggestPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_suggest</summary>
		Task<ConnectionStatus> SuggestPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_suggest</summary>
		ConnectionStatus SuggestPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_suggest</summary>
		Task<ConnectionStatus> SuggestPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_termvector</summary>
		ConnectionStatus TermVectorGet(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/{id}/_termvector</summary>
		Task<ConnectionStatus> TermVectorGetAsync(string index, string type, string id, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_termvector</summary>
		ConnectionStatus TermVectorPost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_termvector</summary>
		Task<ConnectionStatus> TermVectorPostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/{type}</summary>
		ConnectionStatus TypesExistsHead(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>HEAD /{index}/{type}</summary>
		Task<ConnectionStatus> TypesExistsHeadAsync(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_update</summary>
		ConnectionStatus UpdatePost(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/{id}/_update</summary>
		Task<ConnectionStatus> UpdatePostAsync(string index, string type, string id, object body, NameValueCollection queryString = null);

		///<summary>PUT /_settings</summary>
		ConnectionStatus UpdateSettingsPut(object body, NameValueCollection queryString = null);

		///<summary>PUT /_settings</summary>
		Task<ConnectionStatus> UpdateSettingsPutAsync(object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_settings</summary>
		ConnectionStatus UpdateSettingsPut(string index, object body, NameValueCollection queryString = null);

		///<summary>PUT /{index}/_settings</summary>
		Task<ConnectionStatus> UpdateSettingsPutAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>GET /_validate/query</summary>
		ConnectionStatus ValidateQueryGet(NameValueCollection queryString = null);

		///<summary>GET /_validate/query</summary>
		Task<ConnectionStatus> ValidateQueryGetAsync(NameValueCollection queryString = null);

		///<summary>GET /{index}/_validate/query</summary>
		ConnectionStatus ValidateQueryGet(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/_validate/query</summary>
		Task<ConnectionStatus> ValidateQueryGetAsync(string index, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_validate/query</summary>
		ConnectionStatus ValidateQueryGet(string index, string type, NameValueCollection queryString = null);

		///<summary>GET /{index}/{type}/_validate/query</summary>
		Task<ConnectionStatus> ValidateQueryGetAsync(string index, string type, NameValueCollection queryString = null);

		///<summary>POST /_validate/query</summary>
		ConnectionStatus ValidateQueryPost(object body, NameValueCollection queryString = null);

		///<summary>POST /_validate/query</summary>
		Task<ConnectionStatus> ValidateQueryPostAsync(object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_validate/query</summary>
		ConnectionStatus ValidateQueryPost(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/_validate/query</summary>
		Task<ConnectionStatus> ValidateQueryPostAsync(string index, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_validate/query</summary>
		ConnectionStatus ValidateQueryPost(string index, string type, object body, NameValueCollection queryString = null);

		///<summary>POST /{index}/{type}/_validate/query</summary>
		Task<ConnectionStatus> ValidateQueryPostAsync(string index, string type, object body, NameValueCollection queryString = null);



	}
}
