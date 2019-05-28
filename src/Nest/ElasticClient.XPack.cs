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
using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Specification.XPackApi;

// ReSharper disable once CheckNamespace
namespace Nest.Specification.XPackApi
{
	///<summary>
	/// Logically groups all XPack API's together so that they may be discovered more naturally.
	/// <para>Not intended to be instantiated directly please defer to the <see cref = "IElasticClient.XPack"/> property
	/// on <see cref = "IElasticClient"/>.
	///</para>
	///</summary>
	public class XPackNamespace : NamespacedClientProxy
	{
		internal XPackNamespace(ElasticClient client): base(client)
		{
		}

		/// <summary>
		/// <c>GET</c> request to the <c>xpack.info</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html</a>
		/// </summary>
		public XPackInfoResponse Info(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null) => Info(selector.InvokeOrDefault(new XPackInfoDescriptor()));
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.info</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html</a>
		/// </summary>
		public Task<XPackInfoResponse> InfoAsync(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null, CancellationToken ct = default) => InfoAsync(selector.InvokeOrDefault(new XPackInfoDescriptor()), ct);
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.info</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html</a>
		/// </summary>
		public XPackInfoResponse Info(IXPackInfoRequest request) => DoRequest<IXPackInfoRequest, XPackInfoResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.info</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/info-api.html</a>
		/// </summary>
		public Task<XPackInfoResponse> InfoAsync(IXPackInfoRequest request, CancellationToken ct = default) => DoRequestAsync<IXPackInfoRequest, XPackInfoResponse>(request, request.RequestParameters, ct);
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.usage</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "Retrieve information about xpack features usage">Retrieve information about xpack features usage</a>
		/// </summary>
		public XPackUsageResponse Usage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null) => Usage(selector.InvokeOrDefault(new XPackUsageDescriptor()));
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.usage</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "Retrieve information about xpack features usage">Retrieve information about xpack features usage</a>
		/// </summary>
		public Task<XPackUsageResponse> UsageAsync(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null, CancellationToken ct = default) => UsageAsync(selector.InvokeOrDefault(new XPackUsageDescriptor()), ct);
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.usage</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "Retrieve information about xpack features usage">Retrieve information about xpack features usage</a>
		/// </summary>
		public XPackUsageResponse Usage(IXPackUsageRequest request) => DoRequest<IXPackUsageRequest, XPackUsageResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>GET</c> request to the <c>xpack.usage</c> API, read more about this API online:
		/// <para> </para>
		/// <a href = "Retrieve information about xpack features usage">Retrieve information about xpack features usage</a>
		/// </summary>
		public Task<XPackUsageResponse> UsageAsync(IXPackUsageRequest request, CancellationToken ct = default) => DoRequestAsync<IXPackUsageRequest, XPackUsageResponse>(request, request.RequestParameters, ct);
	}
}