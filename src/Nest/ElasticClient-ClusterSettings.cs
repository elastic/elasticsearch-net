using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterSettings(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				selector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatcher.Dispatch<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatcher.DispatchAsync<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatcher.Dispatch<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest = null)
		{
			return this.Dispatcher.DispatchAsync<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
		}
	}
}