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
			return this.Dispatch<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				selector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatch<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.DispatchAsync<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatch<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest = null)
		{
			return this.DispatchAsync<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.RawDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
		}
	}
}