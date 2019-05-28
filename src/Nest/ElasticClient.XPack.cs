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

		///<inheritdoc cref = "IXPackInfoRequest"/>
		public XPackInfoResponse Info(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null) => Info(selector.InvokeOrDefault(new XPackInfoDescriptor()));
		///<inheritdoc cref = "IXPackInfoRequest"/>
		public Task<XPackInfoResponse> InfoAsync(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null, CancellationToken ct = default) => InfoAsync(selector.InvokeOrDefault(new XPackInfoDescriptor()), ct);
		///<inheritdoc cref = "IXPackInfoRequest"/>
		public XPackInfoResponse Info(IXPackInfoRequest request) => DoRequest<IXPackInfoRequest, XPackInfoResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IXPackInfoRequest"/>
		public Task<XPackInfoResponse> InfoAsync(IXPackInfoRequest request, CancellationToken ct = default) => DoRequestAsync<IXPackInfoRequest, XPackInfoResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IXPackUsageRequest"/>
		public XPackUsageResponse Usage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null) => Usage(selector.InvokeOrDefault(new XPackUsageDescriptor()));
		///<inheritdoc cref = "IXPackUsageRequest"/>
		public Task<XPackUsageResponse> UsageAsync(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null, CancellationToken ct = default) => UsageAsync(selector.InvokeOrDefault(new XPackUsageDescriptor()), ct);
		///<inheritdoc cref = "IXPackUsageRequest"/>
		public XPackUsageResponse Usage(IXPackUsageRequest request) => DoRequest<IXPackUsageRequest, XPackUsageResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IXPackUsageRequest"/>
		public Task<XPackUsageResponse> UsageAsync(IXPackUsageRequest request, CancellationToken ct = default) => DoRequestAsync<IXPackUsageRequest, XPackUsageResponse>(request, request.RequestParameters, ct);
	}
}