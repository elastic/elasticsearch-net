// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using static Elasticsearch.Net.HttpMethod;

// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable once CheckNamespace
// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable RedundantExtendsListEntry
namespace Elasticsearch.Net.Specification.DanglingIndicesApi
{
	///<summary>
	/// Dangling Indices APIs.
	/// <para>Not intended to be instantiated directly. Use the <see cref = "IElasticLowLevelClient.DanglingIndices"/> property
	/// on <see cref = "IElasticLowLevelClient"/>.
	///</para>
	///</summary>
	public class LowLevelDanglingIndicesNamespace : NamespacedClientProxy
	{
		internal LowLevelDanglingIndicesNamespace(ElasticLowLevelClient client): base(client)
		{
		}

		///<summary>DELETE on /_dangling/{index_uuid} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
		///<param name = "indexUuid">The UUID of the dangling index</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse DeleteDanglingIndex<TResponse>(string indexUuid, DeleteDanglingIndexRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(DELETE, Url($"_dangling/{indexUuid:indexUuid}"), null, RequestParams(requestParameters));
		///<summary>DELETE on /_dangling/{index_uuid} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
		///<param name = "indexUuid">The UUID of the dangling index</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("dangling_indices.delete_dangling_index", "index_uuid")]
		public Task<TResponse> DeleteDanglingIndexAsync<TResponse>(string indexUuid, DeleteDanglingIndexRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(DELETE, Url($"_dangling/{indexUuid:indexUuid}"), ctx, null, RequestParams(requestParameters));
		///<summary>POST on /_dangling/{index_uuid} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
		///<param name = "indexUuid">The UUID of the dangling index</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ImportDanglingIndex<TResponse>(string indexUuid, ImportDanglingIndexRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, Url($"_dangling/{indexUuid:indexUuid}"), null, RequestParams(requestParameters));
		///<summary>POST on /_dangling/{index_uuid} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
		///<param name = "indexUuid">The UUID of the dangling index</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("dangling_indices.import_dangling_index", "index_uuid")]
		public Task<TResponse> ImportDanglingIndexAsync<TResponse>(string indexUuid, ImportDanglingIndexRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, Url($"_dangling/{indexUuid:indexUuid}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_dangling <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse List<TResponse>(ListDanglingIndicesRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, "_dangling", null, RequestParams(requestParameters));
		///<summary>GET on /_dangling <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("dangling_indices.list_dangling_indices", "")]
		public Task<TResponse> ListAsync<TResponse>(ListDanglingIndicesRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, "_dangling", ctx, null, RequestParams(requestParameters));
	}
}